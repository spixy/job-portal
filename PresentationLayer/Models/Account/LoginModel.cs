using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.Account
{
	public class LoginModel
	{
		[Required]
		public string Username { get; set; }

		[Required, StringLength(100)]
		public string Password { get; set; }
	}
}