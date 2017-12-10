using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Employers;
using BusinessLayer.Facades.JobOffer;

namespace PresentationLayer.Controllers
{
    public class EmployerController : Controller
	{
		public EmployerFacade EmployerFacade { get; set; }
		public JobOfferFacade JobOfferFacade { get; set; }

		// GET: Employer
		public async Task<ActionResult> Index(int page = 1)
		{
			EmployerFilterDto filter = new EmployerFilterDto
			{
				PageSize = JobPortalSettings.DefaultPageSize,
				RequestedPageNumber = page
			};

			var employers = await this.EmployerFacade.Get(filter);
			return View(employers);
        }

		// GET: Employer/Details/5
		public async Task<ActionResult> Details(int id, int page = 1)
		{
			EmployerDto employer = await EmployerFacade.Get(id);

			JobOfferFilterDto filter = new JobOfferFilterDto
			{
				PageSize = JobPortalSettings.DefaultPageSize,
				RequestedPageNumber = page,
				EmployerId = employer.Id
			};

			var jobs = await this.JobOfferFacade.Get(filter);
			ViewBag.Headline = string.Format("{0} - {1}", employer.Name, "Job offers");
			return View(jobs);
		}
    }
}
