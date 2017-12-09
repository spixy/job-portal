using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.Offices
{
    public interface IOfficeService : ICrudService<OfficeDto, OfficeFilterDto>
    {
    }
}
