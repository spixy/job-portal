using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services
{
    public class JobApplicationService : CrudQueryServiceBase<JobApplication, JobApplicationDto, JobApplicationFilterDto>
    {
        public JobApplicationService(IMapper mapper, IRepository<JobApplication> repository, QueryObjectBase<JobApplicationDto, JobApplication, JobApplicationFilterDto, IQuery<JobApplication>> query)
            : base(mapper, repository, query)
        {
        }

        protected override Task<JobApplication> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId, nameof(JobApplication.JobOffer), nameof(JobApplication.JobCandidate), nameof(JobApplication.Answers));
        }
    }
}
