using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
	public class UserCreateDto : UserBaseDto
	{
		[Required]
		public string Username { get; set; }

		[Required, StringLength(100)]
		public string Password { get; set; }
	}
}
