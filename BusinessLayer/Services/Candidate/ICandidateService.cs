using System.Threading.Tasks;
using BusinessLayer.DTOs;
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
        Task<JobCandidateDto> GetByEmailAsync(string email);
    }
}
