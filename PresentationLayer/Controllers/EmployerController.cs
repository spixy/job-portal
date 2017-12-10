using System;
using System.Collections.Generic;
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
		private IEmployerFacade EmployerFacade => MvcApplication.Container.Resolve<EmployerFacade>();
		private IJobOfferFacade JobOfferFacade => MvcApplication.Container.Resolve<JobOfferFacade>();

		// GET: Employer
		public ActionResult Index()
        {
            return View();
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

		// GET: Employer/Create
		public ActionResult Create()
        {
            return View();
        }

        // POST: Employer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: Employer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
