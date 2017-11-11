using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class JobApplicationCreateDto : DtoBase
    {
        public JobApplicationDto applicationDto { get; set; }

        public JobCandidateDto candidateDto { get; set; }
    }
}
