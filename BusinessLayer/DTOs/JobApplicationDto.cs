using System.Collections.Generic;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Enums;

namespace BusinessLayer.DTOs
{
    public class JobApplicationDto : DtoBase
    {
        public int JobOfferId { get; set; }

        public virtual JobOfferDto JobOffer { get; set; }

        public int JobCandidateId { get; set; }

	    public virtual JobCandidateDto JobCandidate { get; set; }

		public Status Status { get; set; }

        public virtual List<AnswerDto> Answers { get; set; }
    }
}
