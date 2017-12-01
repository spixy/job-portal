using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;
using Infrastructure;

namespace DataAccessLayer.Entities
{
    public class UserBase : IEntity
	{
        public int Id { get; set; }

        [NotMapped]
        public virtual string TableName { get; } = nameof(JobPortalDbContext.UserBases);

	    [NotMapped]
		public virtual string Roles { get; } = "UserBase";

		[Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
	}
}
