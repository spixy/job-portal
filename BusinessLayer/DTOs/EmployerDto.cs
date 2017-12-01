namespace BusinessLayer.DTOs
{
    public class EmployerDto : UserBaseDto
    {
		public string Username { get; set; }
		
	    public string Password { get; set; }

	    public string Roles { get; set; }
	}
}
