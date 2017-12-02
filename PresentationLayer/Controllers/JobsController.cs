using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.JobOffer;

namespace PresentationLayer.Controllers
{
    public class JobsController : Controller
    {
	    private IJobOfferFacade JobOfferFacade => MvcApplication.Container.Resolve<JobOfferFacade>();

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
	}
}
