
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Entities
{
    public class UnregisteredUser : JobCandidate
    {
        [NotMapped]
        public override string TableName { get; } = nameof(JobPortalDbContext.UnregisteredUsers);

        public JobApplication JobApplication { get; set; }
    }
}
