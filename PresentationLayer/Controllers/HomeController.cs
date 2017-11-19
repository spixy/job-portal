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

        public IJobOfferFacade jobOfferFacade { get; set; }

        // GET: /
        public async Task<ActionResult> Index()
        {
            // TODO
            /*JobOfferFilterDto filter = new JobOfferFilterDto
            {
                PageSize = this.PageSize,
                RequestedPageNumber = 0
            };
            IEnumerable<JobOfferDto> jobs = await this.jobOfferFacade.Get(filter);*/
            IEnumerable<JobOfferDto> jobs = new JobOfferDto[0];

            return View(jobs);
        }

        // GET: Page/2
        public async Task<ActionResult> Page(int page)
        {
            JobOfferFilterDto filter = new JobOfferFilterDto
            {
                PageSize = this.PageSize,
                RequestedPageNumber = page
            };
            IEnumerable<JobOfferDto> jobs = await this.jobOfferFacade.Get(filter);

            return View(jobs);
        }

        // POST: Filter
        [HttpPost]
        public ActionResult Filter(JobOfferFilterDto filter)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}