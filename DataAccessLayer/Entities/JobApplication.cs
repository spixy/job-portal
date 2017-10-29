using DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class JobApplication : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int JobOfferId { get; set; }

        [Required]
        public virtual JobOffer JobOffer { get; set; }

        [Required]
        public int JobCandidateId { get; set; }

        [Required]
        public virtual JobCandidate JobCandidate { get; set; }

        [Required]
        public Status Status { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}
