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

    public enum Education
    {
        HighSchool,
        AssociateDegree,
        BachelorDegree,
        MasterDegree,
        DoctoralDegree,
        HigherDegree
    }
}
