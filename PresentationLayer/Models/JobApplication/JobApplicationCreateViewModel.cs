using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLayer.DTOs;

namespace PresentationLayer.Models.JobApplication
{
	public class JobApplicationCreateViewModel
	{
		public IEnumerable<SelectListItem> Educations { get; set; }

		public JobApplicationCreateDto JobApplicationCreateDto { get; set; }
	}
}