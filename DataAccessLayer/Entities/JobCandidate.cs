using DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class JobCandidate : UserBase
    {
        [Required]
        public Education Education { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
