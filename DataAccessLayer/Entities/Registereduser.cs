using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;

namespace DataAccessLayer.Entities
{
    public class RegisteredUser : JobCandidate
    {
        [NotMapped]
        public override string TableName { get; } = nameof(JobPortalDbContext.RegisteredUsers);

        public List<JobApplication> JobApplications { get; set; }
    }
}
