using System.Collections.Generic;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class JobOfferDto : DtoBase
    {
        public string Name { get; set; }

        public virtual EmployerDto Employer { get; set; }

        public int OfficeId { get; set; }

        public virtual OfficeDto Office { get; set; }

        public string Description { get; set; }

        public virtual List<QuestionDto> Questions { get; set; }

        public virtual List<SkillDto> Skills { get; set; }

        public virtual List<JobApplicationDto> JobApplications { get; set; }
	}
}
