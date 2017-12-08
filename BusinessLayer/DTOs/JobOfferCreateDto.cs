using System.Collections.Generic;

namespace BusinessLayer.DTOs
{
	public class JobOfferCreateDto
	{
		public string Name { get; set; }

		public int EmployerId { get; set; }

		public int OfficeId { get; set; }

		public string Description { get; set; }

		public List<QuestionDto> Questions { get; set; }

		public List<SkillDto> Skills { get; set; }
	}
}
