using System.Collections.Generic;

namespace WebApi.Models
{
	public class JobOfferDto
	{
		public string Name { get; set; }

		public int OfficeId { get; set; }

		public string Description { get; set; }

		public IEnumerable<string> Questions { get; set; }

		public IEnumerable<string> Skills { get; set; }

		public IEnumerable<int> JobApplicationIds { get; set; }
	}
}