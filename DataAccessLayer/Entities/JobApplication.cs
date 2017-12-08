using DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Contexts;
using Infrastructure;

namespace DataAccessLayer.Entities
{
    public class JobApplication : IEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.JobApplications);

        [Required]
        public int JobOfferId { get; set; }
		
        public virtual JobOffer JobOffer { get; set; }

        [Required]
        public int JobCandidateId { get; set; }
		
        public virtual JobCandidate JobCandidate { get; set; }

        [Required]
        public Status Status { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}
