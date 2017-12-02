using System.Threading.Tasks;
using BusinessLayer.DTOs;

namespace BusinessLayer.Services.Auth
{
	public interface IUserService
	{
		string CreatePassword(string password, bool withSalt = true);
		Task<UserBaseDto> GetUserByUsernameAsync(string username);
		bool VerifyHashedPassword(string userHashedPassword, string inputPassword);
	}
}