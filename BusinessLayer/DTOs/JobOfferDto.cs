using System.Collections.Generic;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class JobOfferDto : DtoBase
    {
        public string Name { get; set; }

        public EmployerDto Employer { get; set; }

        public int OfficeId { get; set; }

        public OfficeDto Office { get; set; }

        public string Description { get; set; }

        public List<QuestionDto> Questions { get; set; }

        public List<SkillDto> Skills { get; set; }

        public List<JobApplicationDto> JobApplications { get; set; }
	}
}
