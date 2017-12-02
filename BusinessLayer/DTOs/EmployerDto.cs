using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class EmployerDto : UserBaseDto
	{
		[Required]
		public string Username { get; set; }
		
	    public string Password { get; set; }
	}
}
