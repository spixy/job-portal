using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Skill : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<JobOffer> JobOffers { get; set; }
        public virtual List<JobCandidate> JobCandidates { get; set; }
    }
}
