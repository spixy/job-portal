using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.JobOffers;
using BusinessLayer.Services.Questions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class JobOfferFacade : FacadeBase, IJobOfferFacade
    {
        private readonly IJobOfferService jobOfferService;
        private readonly IQuestionService questionService;

        public JobOfferFacade(IUnitOfWorkProvider unitOfWorkProvider, IJobOfferService jobOfferService, IQuestionService questionService) : base(unitOfWorkProvider)
        {
            this.jobOfferService = jobOfferService;
            this.questionService = questionService;
        }

        public int Create(JobOfferDto dto)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
                var created = this.jobOfferService.Create(dto);
                uow.Commit();
                return created.Id;
            }
        }

        public async Task<JobOfferDto> Get(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobOfferService.GetAsync(id);
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

        public async Task Update(int id, JobOfferDto jobOfferDto)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
                await this.jobOfferService.Update(jobOfferDto);
                await uow.CommitAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
                JobOfferDto job = await this.jobOfferService.GetAsync(id, true);

                foreach (QuestionDto question in job.Questions)
                {
                    if (question.JobApplications.Count == 1)
                    {
                        this.questionService.Delete(question.Id);
                    }
                }

                this.jobOfferService.Delete(id);
                await uow.CommitAsync();
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
