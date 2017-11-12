using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.JobOffers
{
    public class JobOfferService : CrudQueryServiceBase<JobOffer, JobOfferDto, JobOfferFilterDto>, IJobOfferService
    {
        public JobOfferService(IMapper mapper, IRepository<JobOffer> repository,
            QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<JobOffer> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(JobOffer.Employer), nameof(JobOffer.Skills),
                nameof(JobOffer.Questions));
        }

        public async Task<IEnumerable<JobOfferDto>> GetByEmployer(int employerId)
        {
            var queryResult = await Query.ExecuteQuery(new JobOfferFilterDto { EmployerId = employerId });
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobOfferDto>> GetByName(string name)
        {
            var queryResult = await Query.ExecuteQuery(new JobOfferFilterDto { Name = name });
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobOfferDto>> GetFilteredAsync(JobOfferFilterDto filter)
        {
            var queryResult = await Query.ExecuteQuery(filter);
            return queryResult.Items;
        }
    }
}
