using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs.Filters
{
    public class SkillFilterDto : FilterDtoBase
    {
        public string[] Names { get; set; }
    }
}
