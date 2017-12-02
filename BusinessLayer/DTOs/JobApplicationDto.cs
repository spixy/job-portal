﻿using System.Collections.Generic;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Enums;

namespace BusinessLayer.DTOs
{
    public class JobApplicationDto : DtoBase
    {
        public int JobOfferId { get; set; }

        public JobOfferDto JobOffer { get; set; }

        public int JobCandidateId { get; set; }

	    public JobCandidateDto JobCandidate { get; set; }

		public Status Status { get; set; }

        public List<AnswerDto> Answers { get; set; }
    }
}
