using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class JobApplicationCreateDto : DtoBase
    {
        public JobApplicationDto ApplicationDto { get; set; }

        public JobCandidateDto CandidateDto { get; set; }
    }
}
