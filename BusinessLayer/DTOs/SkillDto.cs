using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class SkillDto : DtoBase
    {
        [Required]
        public string Name { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }

        public virtual List<JobCandidate> JobCandidates { get; set; }
    }
}
