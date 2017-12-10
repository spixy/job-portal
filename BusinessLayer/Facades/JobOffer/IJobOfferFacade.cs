using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.JobOffer
{
    public interface IJobOfferFacade
    {
        int Create(JobOfferCreateDto dto);
        Task<JobOfferDto> Get(int id, bool withIncludes = true);
	    Task<TDto> Get<TDto>(int id, bool withIncludes = true) where TDto : DtoBase;
		Task<IEnumerable<JobOfferDto>> Get(JobOfferFilterDto dto);
        Task<IEnumerable<JobOfferDto>> GetByEmployer(int employerId);
        Task<IEnumerable<JobOfferDto>> GetByName(string name);
        Task<IEnumerable<JobOfferDto>> GetBySkillName(string skillName);
        Task<QueryResultDto<JobOfferDto, JobOfferFilterDto>> GetAllOffers();
        Task Update(int id, JobOfferUpdateDto jobOfferDto);
        Task Delete(int id);
    }
}