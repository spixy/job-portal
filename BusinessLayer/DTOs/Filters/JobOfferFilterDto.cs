using System.Collections.Generic;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Enums;

namespace BusinessLayer.DTOs.Filters
{
    public class JobOfferFilterDto : FilterDtoBase
    {
        public string Name { get; set; }

        public int? EmployerId { get; set; }

        public int? OfficeId { get; set; }

        public List<SkillDto> Skills { get; set; }

		public Education Education { get; set; }
    }
}
