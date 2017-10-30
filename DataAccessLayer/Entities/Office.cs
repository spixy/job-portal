using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;
using Infrastructure;

namespace DataAccessLayer.Entities
{
    public class Office : IEntity
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public Country Country { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        public override string ToString()
        {
            return Address + ", " + City + ", " + Country;
        }
    }
}
