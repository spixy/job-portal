using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;
using Infrastructure;

namespace DataAccessLayer.Entities
{
    public class JobOffer : IEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.JobOffers);

        [Required]
        public string Name { get; set; }

        [Required]
        public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        [Required]
        public int OfficeId { get; set; }

        public virtual Office Office { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<Question> Questions { get; set; }

        public virtual List<JobApplication> JobApplications { get; set; }

        public virtual List<Skill> Skills { get; set; }
    }
}
