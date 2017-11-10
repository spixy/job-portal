using DataAccessLayer.Entities;

namespace BusinessLayer.DTOs
{
    class UnregisteredUserDto : JobCandidateDto
    {
        public JobApplication JobApplication { get; set; }
    }
}
