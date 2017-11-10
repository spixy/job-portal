using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    class QuestionDto : DtoBase
    {
        [Required]
        public string Text { get; set; }
    }
}
