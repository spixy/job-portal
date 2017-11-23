using System.Web.Mvc;
using BusinessLayer.Facades.Employers;

namespace PresentationLayer.Controllers
{
    /// <summary>
    /// Registruje userov, useri si mozu menit skilly, educations, atd
    /// </summary>
    public class RegisteredUserController : Controller
    {
        public IRegisteredUserFacade registeredUserFacade { get; set; }

        // GET: RegisteredUser
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegisteredUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegisteredUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisteredUser/Create
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

        // GET: RegisteredUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegisteredUser/Edit/5
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

        // GET: RegisteredUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegisteredUser/Delete/5
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
