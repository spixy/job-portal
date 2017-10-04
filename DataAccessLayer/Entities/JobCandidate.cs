using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class JobCandidate
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Education Education { get; set; }
        public List<JobApplication> JobApplications { get; set; }
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
