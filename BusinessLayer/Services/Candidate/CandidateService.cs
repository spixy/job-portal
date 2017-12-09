using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;

namespace BusinessLayer.Services.Candidate
{
    public class CandidateService : CrudQueryServiceBase<JobCandidate, JobCandidateDto, JobCandidateFilterDto>, ICandidateService
    {
        public CandidateService(IMapper mapper, IRepository<JobCandidate> repository, QueryObjectBase<JobCandidateDto, JobCandidate, JobCandidateFilterDto, IQuery<JobCandidate>> query)
            : base(mapper, repository, query)
        {
        }

        protected override Task<JobCandidate> GetWithIncludesAsync(int entityId)
        {
            return this.Repository.GetAsync(entityId, nameof(JobCandidate.Skills));
        }

	    public async Task<JobCandidateDto> GetByEmailAsync(string email)
	    {
		    var result = await this.Query.ExecuteQuery(new JobCandidateFilterDto() {Email = email});
		    return result.Items.FirstOrDefault();
	    }
    }
}
