using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades;
using PresentationLayer.App_Start;

namespace PresentationLayer.Controllers
{
    public class JobController : Controller
    {
        public IJobOfferFacade JobOfferFacade { get; set; }

        // TODO: docasne riesenie
        private IJobOfferFacade jobOfferFacade => JobOfferFacade ?? MvcApplication.container.Resolve<JobOfferFacade>();

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
            JobOfferDto job = await this.jobOfferFacade.Get(id);

            if (id == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return View(job);
        }

        // POST: Job/Filter/Dto
        public async Task<ActionResult> Filter(JobOfferFilterDto filter)
        {
            try
            {
                IEnumerable<JobOfferDto> jobs = await this.jobOfferFacade.Get(filter);
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
                int id = this.jobOfferFacade.Create(jobOfferDto);

                if (id == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("Details", new { id });
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Job/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            JobOfferDto job = await this.jobOfferFacade.Get(id);

            if (id == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return View(job);
        }

        // POST: Job/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(int id, JobOfferDto jobOfferDto)
        {
            try
            {
                // TODO: Add update logic here
                this.jobOfferFacade.Update(id, jobOfferDto);
                return RedirectToAction("Details", new { id });
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Job/Delete/5
        public ActionResult Delete(int id)
        {
            this.jobOfferFacade.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: Job/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(int id, JobOfferDto collection)
        {
            try
            {
                // TODO: Add delete logic here
                this.jobOfferFacade.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }
    }
}
