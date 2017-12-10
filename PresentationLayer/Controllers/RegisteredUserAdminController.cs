using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.JobApplication;
using BusinessLayer.Facades.RegisteredUsers;

namespace PresentationLayer.Controllers
{
	/// <summary>
	/// Registruje userov, useri si mozu menit skilly, educations, atd
	/// </summary>
	[Authorize(Roles = "User" /*Role.User.GetString()*/)]
	public class RegisteredUserAdminController : Controller
	{
		public RegisteredUserFacade RegisteredUserFacade { get; set; }
		public JobApplicationFacade JobApplicationFacade { get; set; }

		// GET: RegisteredUser
		public async Task<ActionResult> Index(int page = 1)
		{
			var user = await RegisteredUserFacade.GetByUsername(User.Identity.Name);

			JobApplicationFilterDto filter = new JobApplicationFilterDto
			{
				PageSize = JobPortalSettings.DefaultPageSize,
				RequestedPageNumber = page,
				JobCandidateId = user.Id
			};

			var applications = await this.JobApplicationFacade.Get(filter);
			return this.View(applications);
		}
    }
}
