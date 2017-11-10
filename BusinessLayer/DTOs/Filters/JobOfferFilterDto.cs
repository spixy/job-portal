using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs.Filters
{
    public class JobOfferFilterDto : FilterDtoBase
    {
        public string Name { get; set; }

        public int? EmployerId { get; set; }

        public int? OfficeId { get; set; }

        public int? SkillId { get; set; }
    }
}
