using BusinessLayer.DTOs.Common;

namespace BusinessLayer.DTOs.Filters
{
    public class UserBaseFilterDto : FilterDtoBase
    {        
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
