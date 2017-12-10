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

        // POST: Job/Filter/Dto
        public async Task<ActionResult> Filter([FromBody]JobOfferFilterDto filter)
		{
			var jobs = await this.JobOfferFacade.Get(filter);
			return View("Index", jobs);
		}

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
                Offices = await OfficeSelectListHelper.Get()
            };

            return View(model);
        }

        // POST: Jobs/Create
        [HttpPost]
        public async Task<ActionResult> Create(JobOfferCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
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

	    // GET: RegisteredUser
	    public async Task<ActionResult> Recommended(int page = 1)
	    {
		    var user = await RegisteredUserFacade.GetByUsername(User.Identity.Name);

		    JobOfferFilterDto filter = new JobOfferFilterDto
		    {
			    PageSize = JobPortalSettings.DefaultPageSize,
			    RequestedPageNumber = page,
			    Education = user.Education,
			    Skills = user.Skills
		    };

		    var jobs = await this.JobOfferFacade.Get(filter);
		    return View("Index", jobs);
	    }
	}
}
