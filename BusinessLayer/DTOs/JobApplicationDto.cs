using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System.Collections.Generic;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class JobApplicationDto : DtoBase
    {
        public int JobOfferId { get; set; }

        public JobOffer JobOffer { get; set; } // TODO: nema byt toto virtual?

        public int JobCandidateId { get; set; }

        public Status Status { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}
