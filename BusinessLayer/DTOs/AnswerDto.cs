using DataAccessLayer.Entities;
using BusinessLayer.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs
{
    public class AnswerDto : DtoBase
    {
        [Required]
        public string Text { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int JobApplicationId { get; set; }
    }
}
