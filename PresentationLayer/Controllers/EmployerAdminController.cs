using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Employers;
using BusinessLayer.Facades.JobApplication;
using BusinessLayer.Facades.JobOffer;
using System.Net;
using System.Web.Http;
using HttpPost = System.Web.Mvc.HttpPostAttribute;
using Authorize = System.Web.Mvc.AuthorizeAttribute;

namespace PresentationLayer.Controllers
{
	/// <summary>
	/// Administracia employerov
	/// </summary>
	[Authorize(Roles = "Employer" /*Role.Employer.GetString()*/)]
	public class EmployerAdminController : Controller
	{
		public EmployerFacade EmployerFacade { get; set; }
		public JobOfferFacade JobOfferFacade { get; set; }
		public JobApplicationFacade JobApplicationFacade { get; set; }

		// GET: Employer
		// Get method for current employer
		public async Task<ActionResult> Index(int page = 1)
		{
			EmployerDto employer = await EmployerFacade.GetByUsername(User.Identity.Name);

			JobOfferFilterDto filter = new JobOfferFilterDto
			{
				PageSize = JobPortalSettings.DefaultPageSize,
				RequestedPageNumber = page,
				EmployerId = employer.Id
			};

			var jobs = await this.JobOfferFacade.Get(filter);
			return View(jobs);
		}

        // GET: Employer/JobOffer/5
        public async Task<ActionResult> JobOffer(int id)
		{
			JobOfferDto job = await this.GetMyJobOffer(id);
			if (job != null)
			{
				return View(job);
			}
			throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		// GET: Employer/CreateJob
		public ActionResult CreateJob()
	    {
		    return View();
	    }

		// POST: Employer/CreateJob
		[HttpPost]
	    public async Task<ActionResult> CreateJob(JobOfferCreateDto jobDto)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				EmployerDto employer = await EmployerFacade.GetByUsername(User.Identity.Name);
				jobDto.EmployerId = employer.Id;
				this.JobOfferFacade.Create(jobDto);
				return RedirectToAction("Index");
			}
		    catch
			{
				return View();
			}
		}

        // GET: Employer/EditJob/5
        public async Task<ActionResult> EditJob(int id)
		{
			try
			{
				JobOfferUpdateDto job = await this.JobOfferFacade.Get<JobOfferUpdateDto>(id, false);
				return View(job);
			}
			catch
			{
				return RedirectToAction("Index");
			}
		}

		// POST: Employer/EditJob/5
		[HttpPost]
        public async Task<ActionResult> EditJob(int id, JobOfferUpdateDto jobDto)
        {
	        if (!ModelState.IsValid)
	        {
		        return View();
	        }

	        try
	        {
		        JobOfferDto job = await this.GetMyJobOffer(id);
		        if (job != null)
		        {
			        await this.JobOfferFacade.Update(id, jobDto);
			        return RedirectToAction("JobOffer", new {id});
		        }
	        }
	        catch
			{
				// ignored
			}
			return View();
		}

		// GET: Employer
		public async Task<ActionResult> AcceptApplication(int id)
		{
			try
			{
				var job = await this.GetMyJobApplication(id);
				if (job != null)
				{
					await this.JobApplicationFacade.AcceptApplication(id);
					return RedirectToAction("JobOffer", new { job.JobOffer.Id });
				}
			}
			catch
			{
				// ignored
			}
			return Redirect(HttpContext.Request.UrlReferrer?.AbsoluteUri ?? "Index");
		}

		// GET: Employer
		public async Task<ActionResult> DeclineApplication(int id)
		{
			try
			{
				var job = await this.GetMyJobApplication(id);
				if (job != null)
				{
					await this.JobApplicationFacade.DeclineApplication(id);
					return RedirectToAction("JobOffer", new { job.JobOffer.Id });
				}
			}
			catch
			{
				// ignored
			}
			return Redirect(HttpContext.Request.UrlReferrer?.AbsoluteUri ?? "Index");
		}

		// GET: Employer/DeleteJob/5
		public async Task<ActionResult> DeleteJob(int id)
		{
			JobOfferDto job = await this.GetMyJobOffer(id);
			if (job != null)
			{
				return View(job);
			}
			throw new HttpResponseException(HttpStatusCode.NotFound);
        }

		// POST: Employer/DeleteJob/5
		[HttpPost]
		public async Task<ActionResult> DeleteJob(int id, JobOfferDto jobApplicationDto)
		{
			try
			{
				await this.JobOfferFacade.Delete(id);
				return RedirectToAction("Index");
			}
			catch
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}
		}

		// HELPERS //

		private async Task<JobOfferDto> GetMyJobOffer(int id)
		{
			JobOfferDto job = await this.JobOfferFacade.Get(id, false);
			if (job != null)
			{
				EmployerDto employer = await EmployerFacade.GetByUsername(User.Identity.Name);
				if (employer != null && job.Employer.Id == employer.Id)
				{
					return job;
				}
			}
			return null;
		}

		private async Task<JobApplicationDto> GetMyJobApplication(int id)
		{
			JobApplicationDto job = await this.JobApplicationFacade.Get(id, false);
			if (job != null)
			{
				EmployerDto employer = await EmployerFacade.GetByUsername(User.Identity.Name);
				if (employer != null && job.JobOffer != null && job.JobOffer.Employer.Id == employer.Id)
				{
					return job;
				}
			}
			return null;
		}
	}
}
