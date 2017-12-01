using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Entities
{
    public class Employer : UserBase
    {
        [NotMapped]
        public override string TableName { get; } = nameof(JobPortalDbContext.Employers);

	    [NotMapped]
		public override string Roles { get; } = "Employer";

		public virtual List<JobOffer> JobOffers { get; set; }

	    [Required]
	    public string Username { get; set; }

	    [Required, StringLength(100)]
	    public string Password { get; set; }
	}
}
