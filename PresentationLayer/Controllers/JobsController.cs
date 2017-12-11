using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Employers;
using BusinessLayer.Facades.JobOffer;
using BusinessLayer.Facades.RegisteredUsers;
using PresentationLayer.Helpers;
using PresentationLayer.Models.JobOffer;
using HttpPost = System.Web.Mvc.HttpPostAttribute;

namespace PresentationLayer.Controllers
{
    public class JobsController : Controller
    {
	    public JobOfferFacade JobOfferFacade { get; set; }
		public EmployerFacade EmployerFacade { get; set; }
		public RegisteredUserFacade RegisteredUserFacade { get; set; }
		public OfficeSelectListHelper OfficeSelectListHelper { get; set; }

		// GET: Job
		public async Task<ActionResult> Index(int page = 1)
        {
            JobOfferFilterDto filter = new JobOfferFilterDto
            {
                PageSize = JobPortalSettings.DefaultPageSize,
                RequestedPageNumber = page
            };

	        var jobs = await this.JobOfferFacade.Get(filter);
	        return View("Index", jobs);
		}

        // GET: Jobs/Filter/{skill-name}
        public async Task<ActionResult> Filter(string skill)
        {
            var jobs = await JobOfferFacade.GetBySkill(skill);
            return View("Index", jobs);
        }

        // POST: Job/Filter/Dto
        //public async Task<ActionResult> Filter([FromBody]JobOfferFilterDto filter)
		//{
		//	var jobs = await this.JobOfferFacade.Get(filter);
		//	return View("Index", jobs);
		//}

	    // GET: Job/Details/5
	    public async Task<ActionResult> Details(int id)
	    {
		    JobOfferDto job = await this.JobOfferFacade.Get(id);
		    if (job != null)
		    {
			    return View(job);
		    }
		    throw new HttpResponseException(HttpStatusCode.NotFound);
		}

        // GET: Jobs/Create
        public async Task<ActionResult> Create()
        {
            JobOfferCreateViewModel model = new JobOfferCreateViewModel
            {
                Offices = await OfficeSelectListHelper.Get(),
                NumberOfQuestions = 1,
                NumberOfSkills = 1
            };

            return View(model);
        }

        // POST: Jobs/Create
        [HttpPost]
        public async Task<ActionResult> Create(JobOfferCreateViewModel model)
        {
            if (!CorrectNumberOfQuestions(model) || NumberOfSkillsChanged(model) || !ModelState.IsValid)
            {
                if (NumberOfSkillsChanged(model))
                {
                    ModelState.Remove("NumberOfSkills");
                    ChangeNumberOfSkills(model);
                }

                model.Offices = await OfficeSelectListHelper.Get();
                return View(model);
            }

	        try
	        {
		        var jobDto = model.JobOfferCreateDto;
		        var currentEmployer = await EmployerFacade.GetByUsername(User.Identity.Name);
		        jobDto.EmployerId = currentEmployer.Id;

		        JobOfferFacade.Create(jobDto);

		        return RedirectToAction("Index", "EmployerAdmin");
			}
	        catch
			{
				model.Offices = await OfficeSelectListHelper.Get();
				return View(model);
			}
        }

        #region Helpers (VERY UGLY, should use javascript instead)

        private bool CorrectNumberOfQuestions(JobOfferCreateViewModel model)
        {
            if (Request.Form["ChangeQuestions"] != null)
            {
                if (model.JobOfferCreateDto.Questions == null)
                {
                    model.JobOfferCreateDto.Questions = new List<QuestionDto>();
                }
                while (model.JobOfferCreateDto.Questions.Count < model.NumberOfQuestions)
                {
                    model.JobOfferCreateDto.Questions.Add(new QuestionDto());
                }
                return false;
            }
            return true;
        }

        private bool NumberOfSkillsChanged(JobOfferCreateViewModel model)
        {
            return NumberOfSkillsIncreased(model) || NumberOfSkillsDecreased(model);
        }

        private bool NumberOfSkillsIncreased(JobOfferCreateViewModel model)
        {
            if (Request.Form["AddSkill"] != null)
            {
                return true;
            }
            return false;
        }

        private bool NumberOfSkillsDecreased(JobOfferCreateViewModel model)
        {
            if (Request.Form["RemoveSkill"] != null)
            {
                return true;
            }
            return false;
        }

        private void ChangeNumberOfSkills(JobOfferCreateViewModel model)
        {
            if (model.JobOfferCreateDto.Skills == null)
            {
                model.JobOfferCreateDto.Skills = new List<SkillDto>();
            }

            if (NumberOfSkillsIncreased(model))
            {
                model.JobOfferCreateDto.Skills.Add(new SkillDto());
                model.NumberOfSkills++;
            }

            if (NumberOfSkillsDecreased(model) && model.NumberOfSkills > 1)
            {
                model.JobOfferCreateDto.Skills.RemoveAt(model.NumberOfSkills - 1);
                model.NumberOfSkills--;
            }
        }

        #endregion
    }
}
