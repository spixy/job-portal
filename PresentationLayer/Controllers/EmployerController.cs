using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Employers;
using BusinessLayer.Facades.JobApplication;
using BusinessLayer.Facades.JobOffer;
using System.Net;
using System.Web.Http;
using HttpGet = System.Web.Mvc.HttpGetAttribute;
using HttpPost = System.Web.Mvc.HttpPostAttribute;
using Authorize = System.Web.Mvc.AuthorizeAttribute;

namespace PresentationLayer.Controllers
{
	/// <summary>
	/// Administracia employerov
	/// </summary>
	[Authorize(Roles = "Employer" /*Role.Employer.GetString()*/)]
	public class EmployerController : Controller
	{
		private IEmployerFacade EmployerFacade => MvcApplication.Container.Resolve<EmployerFacade>();
		private IJobOfferFacade JobOfferFacade => MvcApplication.Container.Resolve<JobOfferFacade>();
		private IJobApplicationFacade JobApplicationFacade => MvcApplication.Container.Resolve<JobApplicationFacade>();

		private async Task<JobOfferDto> GetMyJobOffer(int id)
		{
			JobOfferDto job = await this.JobOfferFacade.Get(id);
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
			JobApplicationDto job = await this.JobApplicationFacade.Get(id);
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

		// GET: Employer
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

		// GET: Employer/JobApplication/5
		public async Task<ActionResult> JobApplication(int id)
		{
			JobApplicationDto job = await this.GetMyJobApplication(id);
			if (job != null)
			{
				return View(job);
			}
		    throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		// POST: Employer/JobApplication/5
		[HttpPost]
	    public async Task<ActionResult> JobApplication(int id, JobApplicationDto applicationDto)
	    {
			JobApplicationDto job = await this.JobApplicationFacade.Get(id);
		    if (job?.JobOffer?.Employer != null) // lol
		    {
			    EmployerDto employer = await EmployerFacade.GetByUsername(User.Identity.Name);
			    if (job.JobOffer.Employer.Id == employer.Id)
			    {
				    await this.JobApplicationFacade.Update(applicationDto);
				    return RedirectToAction("JobApplication", new { id });
			    }
			}

		    throw new HttpResponseException(HttpStatusCode.BadRequest);
		}

		// GET: Employer/CreateJob
		public ActionResult CreateJob()
	    {
		    return View();
	    }

		// POST: Employer/CreateJob
		[HttpPost]
	    public async Task<ActionResult> CreateJob(JobOfferDto jobDto)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			try
			{
				EmployerDto employer = await EmployerFacade.GetByUsername(User.Identity.Name);
				jobDto.Employer.Id = employer.Id;

				int id = this.JobOfferFacade.Create(jobDto);
			    if (id != 0)
				{
					return RedirectToAction("JobOffer", new { id });
				}
			}
		    catch
		    {
			    // ignored
		    }
		    return RedirectToAction("Index", "Employer");
		}

        // GET: Employer/EditJob/5
        public async Task<ActionResult> EditJob(int id)
		{
			JobOfferDto job = await this.GetMyJobOffer(id);
			if (job != null)
			{
				return View(job);
			}
			throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		// POST: Employer/EditJob/5
		[HttpPost]
        public async Task<ActionResult> EditJob(int id, JobOfferDto jobDto)
        {
	        if (!ModelState.IsValid)
	        {
		        return View();
	        }

	        JobOfferDto job = await this.GetMyJobOffer(id);
	        if (job != null)
			{
				await this.JobOfferFacade.Update(id, jobDto);
			}
	        return RedirectToAction("JobOffer", new { id });
		}

		// GET: Employer
		public async Task<ActionResult> AcceptApplication(int id)
		{
			var job = await this.GetMyJobApplication(id);
			if (job != null)
			{
				await this.JobApplicationFacade.AcceptApplication(id);
			}
			return RedirectToAction("JobOffer", new { id });
		}

		// GET: Employer
		public async Task<ActionResult> DeclineApplication(int id)
		{
			var job = await this.GetMyJobApplication(id);
			if (job != null)
			{
				await this.JobApplicationFacade.DeclineApplication(id);
			}
			return RedirectToAction("JobOffer", new { id });
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
			await this.JobOfferFacade.Delete(id);
			return RedirectToAction("Index");
		}
	}
}
