using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.JobApplications
{
    public interface IJobApplicationService : ICrudService<JobApplicationDto, JobApplicationFilterDto>
    {
        /// <summary>
        /// Find all applications for given job offer
        /// </summary>
        /// <param name="jobOfferId">jobOfferId</param>
        /// <returns>Applications for given job offer</returns>
        Task<IEnumerable<JobApplicationDto>> GetByJobOffer(int jobOfferId);

        /// <summary>
        /// Find all applications for given user
        /// </summary>
        /// <param name="jobCandidateId">jobCandidateId</param>
        /// <returns>Applications for given user</returns>
        Task<IEnumerable<JobApplicationDto>> GetByJobCandidate(int jobCandidateId);

        /// <summary>
        /// Find all applications for given filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Applications for given filter</returns>
        Task<IEnumerable<JobApplicationDto>> GetByFilterAsync(JobApplicationFilterDto filter);

        /// <summary>
        /// Decline application
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>True if update was successfull</returns>
        Task<bool> DeclineApplication(int applicationId);

        /// <summary>
        /// Accept application
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>True if update was successfull</returns>
        Task<bool> AcceptApplication(int applicationId);
    }
}
