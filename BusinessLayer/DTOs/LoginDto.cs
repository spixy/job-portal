using BusinessLayer.Account;

namespace BusinessLayer.DTOs
{
	public class LoginDto
	{
		public bool Success { get; set; }

		public Role Roles { get; set; }

		public LoginDto(bool success, Role role)
		{
			this.Success = success;
			this.Roles = role;
		}
	}
}
