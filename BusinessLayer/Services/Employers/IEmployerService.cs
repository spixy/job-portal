using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.Employers
{
    public interface IEmployerService : ICrudService<EmployerDto, EmployerFilterDto>
    {
        Task<EmployerDto> GetByName(string name);
        Task<EmployerDto> GetByEmail(string email);
        Task<QueryResultDto<EmployerDto, EmployerFilterDto>> GetFiltered(EmployerFilterDto filter);
	    EmployerDto Create(EmployerCreateDto createDto);
	}
}