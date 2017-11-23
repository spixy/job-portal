using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades;
using PresentationLayer.App_Start;

namespace PresentationLayer.Controllers
{
    /// <summary>
    /// Home page, bude tu asi zoznam jobov a moznost filtrovat
    /// </summary>
    public class HomeController : Controller
    {
        private int PageSize { get; set; } = JobPortalSettings.DefaultPageSize;

        //public IJobOfferFacade jobOfferFacade { get; set; }

        // TODO: docasne riesenie
        public IJobOfferFacade jobOfferFacade => MvcApplication.container.Resolve<JobOfferFacade>();

        // GET: /
        public async Task<ActionResult> Index(int page = 0)
        {
            // TODO
            JobOfferFilterDto filter = new JobOfferFilterDto
            {
                PageSize = this.PageSize,
                RequestedPageNumber = page
            };
            //IEnumerable<JobOfferDto> jobs = await this.jobOfferFacade.Get(filter);
            IEnumerable<JobOfferDto> jobs = new JobOfferDto[0];

            return View(jobs);
        }

        // POST: Filter
        [HttpPost]
        public async Task<ActionResult> Filter(JobOfferFilterDto filter)
        {
            try
            {
                // TODO: Add insert logic here
                IEnumerable<JobOfferDto> jobs = await this.jobOfferFacade.Get(filter);

                return RedirectToAction("Index", jobs);
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}