using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.Offices
{
    public interface IOfficeFacade
    {
        Task<QueryResultDto<OfficeDto, OfficeFilterDto>> GetAllOffices();
    }
}
