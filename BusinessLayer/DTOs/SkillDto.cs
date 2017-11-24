using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class SkillDto : DtoBase
    {
        [Required]
        public string Name { get; set; }
    }
}
