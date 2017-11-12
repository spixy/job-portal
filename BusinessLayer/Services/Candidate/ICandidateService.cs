using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.Candidate
{
    public interface ICandidateService : ICrudService<JobCandidateDto, JobCandidateFilterDto>
    {
        /// <summary>
        /// Gets user with given email address
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>User with given email address</returns>
        Task<RegisteredUserDto> GetByEmailAsync(string email);


        /// <summary>
        /// Find all users matching filter criteria
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Users matching filter criteria</returns>
        Task<QueryResultDto<RegisteredUserDto, RegisteredUserFilterDto>> GetFilteredAsync(RegisteredUserFilterDto filter);
    }
}
