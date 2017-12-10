using System.Collections.Generic;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs
{
	public class JobOfferUpdateDto : DtoBase
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public List<SkillDto> Skills { get; set; }

		public List<QuestionDto> Questions { get; set; }
	}
}
