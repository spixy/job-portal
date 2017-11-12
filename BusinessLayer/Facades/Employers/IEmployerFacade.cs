using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.Employers
{
    public interface IEmployerFacade
    {
        int Create(EmployerDto dto);
        Task<IEnumerable<EmployerDto>> Get(EmployerFilterDto dto);
        Task<EmployerDto> Get(int id);
        Task<EmployerDto> GetByEmail(string email);
        Task<EmployerDto> GetByName(string name);
    }
}