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
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.Services.JobOffers
{
    public class JobOfferService : CrudQueryServiceBase<JobOffer, JobOfferDto, JobOfferUpdateDto, JobOfferFilterDto>, IJobOfferService
    {
        private readonly IJobOfferRepository jobOfferRepository;
        private readonly ISkillRepository skillRepository;
        private readonly IRepository<Question> questionRepository;

        public JobOfferService(IMapper mapper, IJobOfferRepository jobOfferRepository,
            ISkillRepository skillRepository, IRepository<Question> questionRepository,
            QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>> query)
            : base(mapper, jobOfferRepository, query)
        {
            this.jobOfferRepository = jobOfferRepository;
            this.skillRepository = skillRepository;
            this.questionRepository = questionRepository;
        }

        protected override async Task<JobOffer> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(JobOffer.Employer), nameof(JobOffer.Skills),
                nameof(JobOffer.Questions));
        }

        public JobOfferDto Create(JobOfferCreateDto jobOfferCreateDto)
        {
            JobOffer jobOffer = Mapper.Map<JobOffer>(jobOfferCreateDto);
            JobOffer created = Repository.Create(jobOffer);
            return Mapper.Map<JobOfferDto>(created);
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

	    public async Task<TDto> GetAsync<TDto>(int id, bool withIncludes = true) where TDto : DtoBase
	    {
			JobOfferDto result = await this.GetAsync(id, withIncludes);
		    return this.Mapper.Map<TDto>(result);
		}
	}
}
