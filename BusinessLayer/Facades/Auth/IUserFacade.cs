using System.Threading.Tasks;
using BusinessLayer.DTOs;

namespace BusinessLayer.Facades.Auth
{
	public interface IUserFacade
	{
		Task<UserBaseDto> GetUserByUsernameAsync(string username);
		Task<AuthResult> AuthorizeUserAsync(string username, string password);
	}

	public enum AuthResult
	{
		Fail,
		RegisteredUser,
		Employer
	}
}