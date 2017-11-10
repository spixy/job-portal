using BusinessLayer.DTOs.Common;
using DataAccessLayer.Enums;

namespace BusinessLayer.DTOs.Filters
{
    public class JobApplicationFilterDto : FilterDtoBase
    {
        public int? JobOfferId { get; set; }

        public int? JobCandidateId { get; set; }

        public Status? Status { get; set; }
    }
}
