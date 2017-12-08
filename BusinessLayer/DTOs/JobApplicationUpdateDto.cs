using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Enums;

namespace BusinessLayer.DTOs
{
	public class JobApplicationUpdateDto : DtoBase
	{
		public Status Status { get; set; }
	}
}
