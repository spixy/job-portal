using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.Account;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Auth;
using BusinessLayer.Facades.Employers;
using BusinessLayer.Facades.RegisteredUsers;
using PresentationLayer.Models.Account;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
	    private IEmployerFacade EmployerFacade => MvcApplication.Container.Resolve<EmployerFacade>();
	    private IRegisteredUserFacade RegisteredUserFacade => MvcApplication.Container.Resolve<RegisteredUserFacade>();
	    private IUserFacade UserFacade => MvcApplication.Container.Resolve<UserFacade>();

	    private static readonly Dictionary<Role, string> RoleControllerMap = new Dictionary<Role, string>
	    {
		    {Role.Employer, "EmployerAdmin"},
		    {Role.User, "RegisteredUserAdmin"}
	    };

		// GET: Account/Login
		public ActionResult Login()
        {
			return View();
        }

	    // POST: Account/RegisterUser
	    [HttpPost]
	    public async Task<ActionResult> Login(LoginModel loginModel, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			LoginDto result = await this.UserFacade.AuthorizeUserAsync(loginModel.Username, loginModel.Password);

		    if (result.Success)
		    {
				FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginModel.Username, DateTime.Now, DateTime.Now.AddHours(1), false, result.Roles.GetString());
			    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
			    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
			    HttpContext.Response.Cookies.Add(authCookie);

			    if (!string.IsNullOrEmpty(returnUrl))
			    {
				    string decodedUrl = Server.UrlDecode(returnUrl);

				    if (Url.IsLocalUrl(decodedUrl))
				    {
					    return Redirect(decodedUrl);
				    }
				}

			    string controllerName;
			    if (RoleControllerMap.TryGetValue(result.Roles, out controllerName))
			    {
				    return RedirectToAction("Index", controllerName);
			    }
			    else
			    {
					return RedirectToAction("Index", "Jobs");
				}
		    }
		    else
		    {
			    ModelState.AddModelError("", "Wrong username or password!");
			    return View();
		    }
		}

		// GET: Account/Login
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Index", "Jobs");
		}

		// GET: Account/RegisterEmployer
		public ActionResult RegisterEmployer()
	    {
		    return View();
	    }

		// POST: Account/RegisterEmployer
		[HttpPost]
        public async Task<ActionResult> RegisterEmployer(EmployerCreateDto employerDto)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			await this.EmployerFacade.Create(employerDto);

	        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, employerDto.Username, DateTime.Now, DateTime.Now.AddHours(1), false, Role.Employer.GetString());
	        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
	        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
	        HttpContext.Response.Cookies.Add(authCookie);

			string controllerName = RoleControllerMap[Role.Employer];
			return RedirectToAction("Index", controllerName);
		}

		// GET: Account/RegisterUser
		public ActionResult RegisterUser()
	    {
		    return View();
	    }

		// POST: Account/RegisterUser
		[HttpPost]
	    public async Task<ActionResult> RegisterUser(RegisteredUserCreateDto registeredUserDto)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			await this.RegisteredUserFacade.Create(registeredUserDto);

		    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, registeredUserDto.Username, DateTime.Now, DateTime.Now.AddHours(1), false, Role.User.GetString());
		    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
		    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
		    HttpContext.Response.Cookies.Add(authCookie);

			string controllerName = RoleControllerMap[Role.User];
			return RedirectToAction("Index", controllerName);
		}
    }
}
