using System.Threading.Tasks;
using BusinessLayer.DTOs;

namespace BusinessLayer.Facades.Auth
{
	public interface IUserFacade
	{
		Task<UserBaseDto> GetUserByUsernameAsync(string username);
		Task<LoginDto> AuthorizeUserAsync(string username, string password);
	}
}