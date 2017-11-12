using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.Employers
{
    public interface IEmployerService : ICrudService<EmployerDto, EmployerFilterDto>
    {
        /// <summary>
        /// Find employer by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Employer for given name</returns>
        Task<EmployerDto> GetByName(string name);

        /// <summary>
        /// Find employer by mail
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Employer for given email</returns>
        Task<EmployerDto> GetByEmail(string email);

        /// <summary>
        /// Gets employers according to given filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Employers matching filter criteria</returns>
        Task<QueryResultDto<EmployerDto, EmployerFilterDto>> GetFilteredAsync(EmployerFilterDto filter);
    }
}
