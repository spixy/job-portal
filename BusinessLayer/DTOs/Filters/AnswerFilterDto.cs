using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs.Filters
{
    public class AnswerFilterDto : FilterDtoBase
    {
        public int? JobApplicationId { get; set; }

        public int? QuestionId { get; set; }
    }
}
