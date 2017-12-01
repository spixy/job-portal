#define DI_FIX

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.JobOffer;

namespace PresentationLayer.Controllers
{
    public class JobController : Controller
    {
#if DI_FIX
        // TODO: docasne riesenie
        public IJobOfferFacade JobOfferFacade => MvcApplication.Container.Resolve<JobOfferFacade>();
#else
        public IJobOfferFacade JobOfferFacade { get; set; }
#endif

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

        // GET: Job/Details/5
        public async Task<ActionResult> Details(int id)
        {
            JobOfferDto job = await this.JobOfferFacade.Get(id);
            return View(job);
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

        // GET: Job/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Job/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(JobOfferDto jobOfferDto)
        {
            try
            {
                int id = this.JobOfferFacade.Create(jobOfferDto);

                if (id == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("Details", new { id });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Job/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            JobOfferDto job = await this.JobOfferFacade.Get(id);
            return View(job);
        }

        // POST: Job/Edit/5
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Edit(int id, JobOfferDto jobOfferDto)
        {
            try
            {
                // TODO: Add update logic here
                await this.JobOfferFacade.Update(id, jobOfferDto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return RedirectToAction("Details", new { id });
        }

        // GET: Job/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
           await this.JobOfferFacade.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
