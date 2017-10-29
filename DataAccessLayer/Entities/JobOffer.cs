using DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class JobOffer : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int EmployerId { get; set; }

        [Required]
        public virtual Employer Employer { get; set; }

        [Required]
        public int OfficeId { get; set; }

        [Required]
        public virtual Office Office { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<Question> Questions { get; set; }

        public virtual List<JobApplication> JobApplications { get; set; }

        public virtual List<Skill> Skills { get; set; }
    }
}
