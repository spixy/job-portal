using DataAccessLayer.Entities;
using System.Collections.Generic;

namespace BusinessLayer.DTOs
{
    public class RegisteredUserDto : JobCandidateDto
    {
        public List<JobApplication> JobApplications { get; set; }
    }
}
