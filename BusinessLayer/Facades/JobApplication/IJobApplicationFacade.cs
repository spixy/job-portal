using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.JobApplication
{
	public interface IJobApplicationFacade
	{
		Task<bool> AcceptApplication(int jobCandidateId);
		Task<int> Create(JobApplicationCreateDto dto);
		Task<bool> DeclineApplication(int jobCandidateId);
		void Delete(int id);
		Task<JobApplicationDto> Get(int id);
		Task<IEnumerable<JobApplicationDto>> Get(JobApplicationFilterDto dto);
		Task<IEnumerable<JobApplicationDto>> GetByFilterAsync(JobApplicationFilterDto filter);
		Task<IEnumerable<JobApplicationDto>> GetByJobCandidate(int id);
		Task<IEnumerable<JobApplicationDto>> GetByJobOffer(int id);
		Task Update(JobApplicationDto dto);
	}
}