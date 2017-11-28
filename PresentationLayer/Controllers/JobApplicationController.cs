using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.JobApplication;

namespace PresentationLayer.Controllers
{
    /// <summary>
    /// Vytvara a vybavuje job applications
    /// </summary>
    [Route("ApplyJob")]
    public class JobApplicationController : Controller
    {
        public IJobApplicationFacade JobApplicationFacade { get; set; }

        // GET: ApplyJob
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApplyJob/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplyJob/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplyJob/Create
        [HttpPost]
        public ActionResult Create(JobApplicationCreateDto jobApplicationDto)
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

        // GET: ApplyJob/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplyJob/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, JobApplicationDto jobApplicationDto)
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

        // GET: ApplyJob/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplyJob/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, JobApplicationDto jobApplicationDto)
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
