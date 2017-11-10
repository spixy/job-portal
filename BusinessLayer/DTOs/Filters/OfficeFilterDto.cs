using BusinessLayer.DTOs.Common;
using DataAccessLayer.Enums;

namespace BusinessLayer.DTOs.Filters
{
    public class OfficeFilterDto : FilterDtoBase
    {
        public string Address { get; set; }

        public string City { get; set; }

        public Country? Country { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
