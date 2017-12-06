using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.Common
{
    public abstract class DtoBase
    {
	    [ScaffoldColumn(false)]
		public int Id { get; set; }
    }
}
