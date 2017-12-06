﻿using System.Collections.Generic;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
    public class JobApplicationCreateDto : DtoBase
	{
		public int JobOfferId { get; set; }

		public JobCandidateDto CandidateDto { get; set; }
		
		public List<AnswerDto> Answers { get; set; }
	}
}
