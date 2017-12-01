using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Auth;
using BusinessLayer.Facades.Employers;
using BusinessLayer.Facades.RegisteredUsers;
using PresentationLayer.Models.Account;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
		private IEmployerFacade employerFacade { get; set; }
		private IRegisteredUserFacade registeredUserFacade { get; set; }
		private IUserFacade userFacade { get; set; }

		// GET: Account/Login
		public ActionResult Login()
        {
            return View();
        }

	    // POST: Account/RegisterUser
	    [HttpPost]
	    public async Task<ActionResult> Login(LoginModel loginModel)
	    {
		    try
		    {
			    AuthResult result = await this.userFacade.AuthorizeUserAsync(loginModel.Username, loginModel.Password);

			    switch (result)
			    {
				    case AuthResult.Fail:
					    return RedirectToAction("Login");

				    case AuthResult.RegisteredUser:
					    return RedirectToAction("Index", "RegisteredUser");

					case AuthResult.Employer:
						return RedirectToAction("Index", "Employer");

					default:
					    throw new ArgumentOutOfRangeException();
			    }
		    }
		    catch
		    {
			    return View();
		    }
	    }

		// GET: Account/Login
		public ActionResult Logout()
		{
			return RedirectToAction("Index", "Job");
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
            try
            {
	            await this.employerFacade.Create(employerDto);

				return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

		// GET: Account/RegisterUser
		public ActionResult RegisterUser()
	    {
		    return View();
	    }

		// POST: Account/RegisterUser
		[HttpPost]
	    public async Task<ActionResult> RegisterUser(UserCreateDto userDto)
	    {
		    try
		    {
			    await this.registeredUserFacade.Create(userDto);

			    return RedirectToAction("Index");
		    }
		    catch
		    {
			    return View();
		    }
	    }

		// GET: Account/Edit/5
		public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
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
    }
}
