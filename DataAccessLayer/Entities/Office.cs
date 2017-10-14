
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Office : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
