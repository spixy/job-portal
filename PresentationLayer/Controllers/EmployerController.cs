using System;
using System.Diagnostics;
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
using AllowAnonymous = System.Web.Mvc.AllowAnonymousAttribute;

namespace PresentationLayer.Controllers
{
	/// <summary>
	/// Administracia employerov
	/// </summary>
	[System.Web.Mvc.Authorize(Roles = "Employer")]
	public class EmployerController : Controller
	{
		public IEmployerFacade EmployerFacade { get; set; }
        public IJobOfferFacade JobOfferFacade { get; set; }
        public IJobApplicationFacade JobApplicationFacade { get; set; }

		// GET: Employer
		public ActionResult Index()
        {
			return View();
        }

		// GET: Employer/MyJobs
		public async Task<ActionResult> MyJobs(int page = 1)
	    {
		    JobOfferFilterDto filter = new JobOfferFilterDto
		    {
			    PageSize = JobPortalSettings.DefaultPageSize,
			    RequestedPageNumber = page,
				EmployerId = 99, // TODO: ziskat zo session
				OfficeId = 99 // TODO: ziskat zo session
			};

		    var jobs = await this.JobOfferFacade.Get(filter);
		    return View(jobs);
	    }

		// GET: Employer/JobOffer/5
		public async Task<ActionResult> JobOffer(int id)
		{
			JobOfferDto job = await this.JobOfferFacade.Get(id);
			return View(job);
        }

		// GET: Employer/JobApplication/5
		public async Task<ActionResult> JobApplication(int id)
	    {
		    JobApplicationDto job = await this.JobApplicationFacade.Get(id);

		    if (job.JobOffer.EmployerId == 99) // TODO: ziskat zo session
			{
				return View(job);
			}

		    throw new HttpResponseException(HttpStatusCode.BadRequest);
		}

		// POST: Employer/JobApplication/5
		[HttpPost]
	    public async Task<ActionResult> JobApplication(int id, JobApplicationDto applicationDto)
	    {
		    try
		    {
			    JobApplicationDto application = await this.JobApplicationFacade.Get(id);

				if (application.JobOffer.EmployerId == 99) // TODO: ziskat zo session
				{
					await this.JobApplicationFacade.Update(applicationDto);
				}
				else
				{
					throw new HttpResponseException(HttpStatusCode.BadRequest);
				}
			}
		    catch (HttpResponseException)
		    {
			    throw;
		    }
			catch (Exception ex)
		    {
			    Debug.WriteLine(ex);
		    }

		    return RedirectToAction("Details", new { id });
	    }

		// GET: Employer/CreateJob
		public ActionResult CreateJob()
	    {
		    return View();
	    }

		// POST: Employer/CreateJob
		[HttpPost]
	    public ActionResult CreateJob(JobOfferDto jobDto)
	    {
		    try
		    {
				int id = this.JobOfferFacade.Create(jobDto);

			    if (id == 0)
			    {
				    throw new HttpResponseException(HttpStatusCode.BadRequest);
			    }

				return RedirectToAction("Details", new { id });
			}
		    catch (HttpResponseException)
		    {
			    throw;
		    }
			catch
		    {
			    return RedirectToAction("Index");
		    }		
	    }

        // GET: Employer/EditJob/5
        public ActionResult EditJob(int id)
        {
            return View();
        }

		// POST: Employer/EditJob/5
		[HttpPost]
        public async Task<ActionResult> EditJob(int id, JobOfferDto jobDto)
        {
            try
            {
				await this.JobOfferFacade.Update(id, jobDto);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}

			return RedirectToAction("Details", new { id });
		}

        // POST: Employer/DeleteJob/5
        [HttpPost]
        public async Task<ActionResult> DeleteJob(int id, EmployerDto employerDto)
		{
			await this.JobOfferFacade.Delete(id);
			return RedirectToAction("Index");
        }
	}
}
