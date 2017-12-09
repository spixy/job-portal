using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class JobOfferCreateDto : DtoBase
    {
        [Required]
        public string Name { get; set; }

        public int EmployerId { get; set; }

        public int OfficeId { get; set; }

        [Required]
        public string Description { get; set; }

        public List<QuestionDto> Questions { get; set; }

        public List<SkillDto> Skills { get; set; }
    }
}
