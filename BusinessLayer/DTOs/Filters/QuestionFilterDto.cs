using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs.Filters
{
    public class QuestionFilterDto : FilterDtoBase
    {
        public string[] Keywords { get; set; }
    }
}
