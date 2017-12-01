using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.JobApplication
{
    public interface IJobApplicationFacade
    {
        Task<int> Create(JobApplicationCreateDto dto);
        void Delete(int id);
	    Task<JobApplicationDto> Get(int id);
	    Task<IEnumerable<JobApplicationDto>> Get(JobApplicationFilterDto dto);
        Task Update(JobApplicationDto dto);
    }
}