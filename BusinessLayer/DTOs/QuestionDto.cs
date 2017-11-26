using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class QuestionDto : DtoBase
    {
        [Required]
        public string Text { get; set; }

        public List<JobApplicationDto> JobApplications { get; set; }
    }
}
