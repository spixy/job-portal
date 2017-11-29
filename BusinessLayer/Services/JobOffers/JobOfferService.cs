using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Infrastructure.Query;
using Infrastructure.Repository;

namespace BusinessLayer.Services.JobOffers
{
    public class JobOfferService : CrudQueryServiceBase<JobOffer, JobOfferDto, JobOfferFilterDto>, IJobOfferService
    {
        private IJobOfferRepository jobOfferRepository;

        public JobOfferService(IMapper mapper, IJobOfferRepository jobOfferRepository,
            QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>> query)
            : base(mapper, jobOfferRepository, query)
        {
            this.jobOfferRepository = jobOfferRepository;
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

        public async Task<IEnumerable<JobOfferDto>> GetBySkill(SkillDto skillDto)
        {
            Skill skill = Mapper.Map<Skill>(skillDto);
            return Mapper.Map<IList<JobOfferDto>>(await jobOfferRepository.GetBySkill(skill));
        }
    }
}
