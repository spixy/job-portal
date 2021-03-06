﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.JobOffers;
using BusinessLayer.Services.Questions;
using BusinessLayer.Services.Skills;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.JobOffer
{
    public class JobOfferFacade : FacadeBase
    {
        private readonly IJobOfferService jobOfferService;
        private readonly IQuestionService questionService;
        private readonly ISkillService skillService;

        public JobOfferFacade(IUnitOfWorkProvider unitOfWorkProvider,
            IJobOfferService jobOfferService,
            IQuestionService questionService,
            ISkillService skillService) : base(unitOfWorkProvider)
        {
            this.jobOfferService = jobOfferService;
            this.questionService = questionService;
            this.skillService = skillService;
        }

        public int Create(JobOfferCreateDto dto)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
                var created = this.jobOfferService.Create(dto);
                uow.SaveChanges();
                return created.Id;
            }
        }

        public async Task<JobOfferDto> Get(int id, bool withIncludes = true)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobOfferService.GetAsync(id, withIncludes);
            }
        }

	    public async Task<TDto> Get<TDto>(int id, bool withIncludes = true) where TDto : DtoBase
	    {
		    using (this.UnitOfWorkProvider.Create())
			{
				return await this.jobOfferService.GetAsync<TDto>(id, withIncludes);
			}
	    }

		public async Task<IEnumerable<JobOfferDto>> GetByEmployer(int employerId)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobOfferService.GetByEmployer(employerId);
            }
        }

        public async Task<IEnumerable<JobOfferDto>> GetByName(string name)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobOfferService.GetByName(name);
            }
        }

        public async Task<IEnumerable<JobOfferDto>> GetBySkill(string skillName)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                var skillDto = await this.skillService.GetByName(skillName);
                if (skillDto == null)
                {
                    return new List<JobOfferDto>();
                }
                return await this.jobOfferService.GetBySkill(skillDto);
            }
        }

		public async Task Update(int id, JobOfferUpdateDto jobOfferDto)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
                await this.jobOfferService.Update(jobOfferDto);
                await uow.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
                JobOfferDto job = await this.jobOfferService.GetAsync(id, true);

                foreach (QuestionDto question in job.Questions)
				{
					this.questionService.Delete(question.Id);
				}

                this.jobOfferService.Delete(id);
                await uow.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<JobOfferDto>> Get(JobOfferFilterDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobOfferService.GetFilteredAsync(dto);
            }
        }

        public async Task<QueryResultDto<JobOfferDto, JobOfferFilterDto>> GetAllOffers()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.ListAllAsync();
            }
        }
    }
}
