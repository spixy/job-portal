using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.JobApplication;
using BusinessLayer.Facades.JobOffer;
using BusinessLayer.Facades.RegisteredUsers;
using PresentationLayer.Helpers;
using PresentationLayer.Models.JobApplication;

namespace PresentationLayer.Controllers
{
    /// <summary>
    /// Vytvara a vybavuje job applications
    /// </summary>
    public class JobApplicationController : Controller
    {
        private IJobApplicationFacade JobApplicationFacade => MvcApplication.Container.Resolve<JobApplicationFacade>();
        private IRegisteredUserFacade RegisteredUserFacade => MvcApplication.Container.Resolve<RegisteredUserFacade>();
		private IJobOfferFacade JobOfferFacade => MvcApplication.Container.Resolve<JobOfferFacade>();
	    public EducationSelectListHelper EducationSelectListHelper { get; set; }

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
				Answers = CreateEmptyAnswers(jobOffer),
				CandidateDto = await RegisteredUserFacade.GetByUsername(User.Identity.Name)
			};

	        JobApplicationCreateViewModel model = new JobApplicationCreateViewModel
			{
		        JobApplicationCreateDto = applicationCreateDto,
				Educations = this.EducationSelectListHelper.Get()
			};

			return View(model);
        }

		// POST: JobApplication/Create
		[HttpPost]
        public ActionResult Create(JobApplicationCreateViewModel jobApplicationDto)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				this.JobApplicationFacade.Create(jobApplicationDto.JobApplicationCreateDto);
				return RedirectToAction("Details", "Jobs", new { id = jobApplicationDto.JobApplicationCreateDto.JobOfferId });
			}
			catch
			{
				return View();
			}
		}

		// Helpers //

	    private static List<AnswerDto> CreateEmptyAnswers(JobOfferDto jobOffer)
		{
			int questionsCount = jobOffer.Questions.Count;
			var list = new List<AnswerDto>(questionsCount);
		    for (int i = 0; i < questionsCount; i++)
		    {
			    QuestionDto question = jobOffer.Questions[i];
			    list.Add(new AnswerDto
			    {
				    QuestionId = question.Id,
				    Question = question
			    });
		    }
		    return list;
	    }
	}
}
