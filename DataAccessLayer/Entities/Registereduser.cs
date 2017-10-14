using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class RegisteredUser : JobCandidate
    {
        public List<JobApplication> JobApplications { get; set; }
    }
}
