using DataAccessLayer.Entities;

namespace BusinessLayer.DTOs
{
    public class UnregisteredUserDto : JobCandidateDto
    {
        public JobApplication JobApplication { get; set; }
    }
}
