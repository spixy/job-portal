using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades
{
    public interface IJobOfferFacade
    {
        int Create(JobOfferDto dto);
        Task<JobOfferDto> Get(int id);
        Task<IEnumerable<JobOfferDto>> Get(JobOfferFilterDto dto);
        Task<IEnumerable<JobOfferDto>> GetByEmployer(int employerId);
        Task<IEnumerable<JobOfferDto>> GetByName(string name);
        void Update(int id, JobOfferDto jobOfferDto);
        Task Delete(int id);
    }
}