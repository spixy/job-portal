using System.Web.Mvc;

namespace PresentationLayer.Controllers
{
    /// <summary>
    /// Home page, bude tu asi zoznam jobov a moznost filtrovat
    /// </summary>
    public class HomeController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }
    }
}