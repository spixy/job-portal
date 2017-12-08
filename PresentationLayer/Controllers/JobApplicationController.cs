using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.JobApplication;
using BusinessLayer.Facades.JobOffer;
using Castle.MicroKernel.Registration;

namespace PresentationLayer.Controllers
{
    /// <summary>
    /// Vytvara a vybavuje job applications
    /// </summary>
    public class JobApplicationController : Controller
    {
        private IJobApplicationFacade JobApplicationFacade => MvcApplication.Container.Resolve<JobApplicationFacade>();
        private IJobOfferFacade JobOfferFacade => MvcApplication.Container.Resolve<JobOfferFacade>();

		// GET: JobApplication
		public ActionResult Index()
        {
            return View();
        }

		// GET: JobApplication/Details/5
		public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobApplication/Create/5
        public async Task<ActionResult> Create(int id)
        {
	        JobOfferDto jobOffer = await JobOfferFacade.Get(id, false);

			JobApplicationCreateDto applicationCreateDto = new JobApplicationCreateDto
	        {
		        JobOfferId = jobOffer.Id,
		        JobOfferName = jobOffer.Name,
				Answers = CreateEmptyAnswers(jobOffer)
			};

			return View(applicationCreateDto);
        }

	    private List<AnswerDto> CreateEmptyAnswers(JobOfferDto JobOffer)
	    {
		    var list = new List<AnswerDto>(JobOffer.Questions.Count);
		    foreach (QuestionDto question in JobOffer.Questions)
		    {
			    list.Add(new AnswerDto{ Question = question });
		    }
		    return list;
	    }

		// POST: JobApplication/Create
		[System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Create(JobApplicationCreateDto jobApplicationDto)
        {
			int id = await this.JobApplicationFacade.Create(jobApplicationDto);
	        if (id != 0)
	        {
		        return RedirectToAction("Details", new { id });
			}

			return RedirectToAction("Index", "Employer");
		}

        // GET: JobApplication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobApplication/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(int id, JobApplicationDto jobApplicationDto)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobApplication/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobApplication/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(int id, JobApplicationDto jobApplicationDto)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
