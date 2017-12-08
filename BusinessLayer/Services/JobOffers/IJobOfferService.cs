using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.JobOffers
{
    public interface IJobOfferService : ICrudService<JobOfferDto, JobOfferFilterDto>
    {
        JobOfferDto Create(JobOfferCreateDto jobOfferCreateDto);

        /// <summary>
        /// Find all offers for given employer
        /// </summary>
        /// <param name="employerId">employerId</param>
        /// <returns>Job offers for given employer</returns>
        Task<IEnumerable<JobOfferDto>> GetByEmployer(int employerId);

        /// <summary>
        /// Find all offers for given name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Job offers for given name</returns>
        Task<IEnumerable<JobOfferDto>> GetByName(string name);

        /// <summary>
        /// Find all offers for given skill
        /// </summary>
        /// <param name="skillDto">skill</param>
        /// <returns>Job offers for given skill</returns>
        Task<IEnumerable<JobOfferDto>> GetBySkill(SkillDto skillDto);

        /// <summary>
        /// Find all offers matching filter criteria
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Job offers matching filter criteria</returns>
        Task<IEnumerable<JobOfferDto>> GetFilteredAsync(JobOfferFilterDto filter);
    }
}
