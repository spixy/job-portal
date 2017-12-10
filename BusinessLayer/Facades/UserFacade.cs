using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Common;
using BusinessLayer.Facades.Employers;
using BusinessLayer.Facades.RegisteredUsers;
using BusinessLayer.Services.Auth;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.Auth
{
	public class UserFacade : FacadeBase
	{
		private readonly IUserService userService;
		private readonly EmployerFacade employerFacade;
		private readonly RegisteredUserFacade registeredUserFacade;

		public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService userService, EmployerFacade employerFacade, RegisteredUserFacade registeredUserFacade) : base(unitOfWorkProvider)
		{
			this.userService = userService;
			this.employerFacade = employerFacade;
			this.registeredUserFacade = registeredUserFacade;
		}

		public async Task<UserBaseDto> GetUserByUsernameAsync(string username)
		{
			using (this.UnitOfWorkProvider.Create())
			{
				return await this.userService.GetUserByUsernameAsync(username);
			}
		}

		public async Task<LoginDto> AuthorizeUserAsync(string username, string password)
		{
			var ret = await this.employerFacade.AuthorizeUserAsync(username, password);
			if (ret.Success)
			{
				return ret;
			}

			ret = await this.registeredUserFacade.AuthorizeUserAsync(username, password);
			if (ret.Success)
			{
				return ret;
			}

			return ret;
		}
	}
}
