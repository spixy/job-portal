using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Enums;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;

namespace BusinessLayer.Services.JobApplications
{
    public class JobApplicationService :
        CrudQueryServiceBase<JobApplication, JobApplicationDto, JobApplicationFilterDto>, IJobApplicationService
    {
        public JobApplicationService(IMapper mapper, IRepository<JobApplication> repository,
            QueryObjectBase<JobApplicationDto, JobApplication, JobApplicationFilterDto, IQuery<JobApplication>>
                query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<JobApplication> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(JobApplication.JobCandidate), nameof(JobApplication.Answers));
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobOffer(int jobOfferId)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDto
            {
                JobOfferId = jobOfferId
            });
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobCandidate(int jobCandidateId)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDto
            {
                JobCandidateId = jobCandidateId
            });
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByFilterAsync(JobApplicationFilterDto filter)
        {
            var queryResult = await ListAllAsync(filter);
            return queryResult.Items;
        }

        public async Task<bool> DeclineApplication(int jobCandidateId)
        {
            return await ChangeApplicationStatus(jobCandidateId, job => job.Status = Status.Declined);
        }

        public async Task<bool> AcceptApplication(int jobCandidateId)
        {
            return await ChangeApplicationStatus(jobCandidateId, job => job.Status = Status.Accepted);
        }

        private async Task<bool> ChangeApplicationStatus(int jobCandidateId, Action<JobApplicationDto> changeFunction)
        {
            JobApplicationDto application = await GetAsync(jobCandidateId, false);
            if (application == null)
            {
                return false;
            }
            changeFunction(application);
            await Update(application);
            return true;
        }
    }
}
