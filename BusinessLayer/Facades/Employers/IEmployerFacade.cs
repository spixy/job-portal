using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.Employers
{
    public interface IEmployerFacade
    {
	    Task<int> Create(EmployerCreateDto dto);
        Task<IEnumerable<EmployerDto>> Get(EmployerFilterDto dto);
        Task<EmployerDto> Get(int id, bool withIncludes = true);
        Task<EmployerDto> GetByEmail(string email);
        Task<EmployerDto> GetByName(string name);
        Task<EmployerDto> GetByUsername(string name);
		Task<LoginDto> AuthorizeUserAsync(string username, string password);
    }
}