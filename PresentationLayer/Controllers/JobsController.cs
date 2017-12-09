using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Employers;
using BusinessLayer.Facades.JobOffer;
using PresentationLayer.Helpers;
using PresentationLayer.Models.JobOffer;

namespace PresentationLayer.Controllers
{
    public class JobsController : Controller
    {
	    private IJobOfferFacade JobOfferFacade => MvcApplication.Container.Resolve<JobOfferFacade>();
        public EmployerFacade EmployerFacade { get; set; }
        public OfficeSelectListHelper OfficeSelectListHelper { get; set; }

        // GET: Job
        public async Task<ActionResult> Index(int page = 1)
        {
            // TODO
            JobOfferFilterDto filter = new JobOfferFilterDto
            {
                PageSize = JobPortalSettings.DefaultPageSize,
                RequestedPageNumber = page
            };

            return await Filter(filter);
        }

        // POST: Job/Filter/Dto
        public async Task<ActionResult> Filter(JobOfferFilterDto filter)
        {
            try
            {
                IEnumerable<JobOfferDto> jobs = await this.JobOfferFacade.Get(filter);
                return View("Index", jobs);
            }
            catch
            {
                return View("Index", new JobOfferDto[0]);
            }
        }

	    // GET: Job/Details/5
	    public async Task<ActionResult> Details(int id)
	    {
		    JobOfferDto job = await this.JobOfferFacade.Get(id);
		    return View(job);
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

            var jobDto = model.JobOfferCreateDto;
            var currentEmployer = await EmployerFacade.GetByUsername(User.Identity.Name);
            jobDto.EmployerId = currentEmployer.Id;

            var created = JobOfferFacade.Create(jobDto);

            return RedirectToAction("Index", "Employer");
        }
    }
}
