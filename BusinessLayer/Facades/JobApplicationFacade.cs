using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Enums;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Answers;
using BusinessLayer.Services.Candidate;
using BusinessLayer.Services.JobApplications;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.JobApplication
{
    public class JobApplicationFacade : FacadeBase
	{
        private readonly IJobApplicationService jobApplicationService;
		private readonly ICandidateService registeredUserService;
        private readonly IAnswerService answerService;

		public JobApplicationFacade(IUnitOfWorkProvider unitOfWorkProvider, IJobApplicationService jobApplicationService, ICandidateService registeredUserService, IAnswerService answerService) : base(unitOfWorkProvider)
		{
			this.jobApplicationService = jobApplicationService;
			this.registeredUserService = registeredUserService;
			this.answerService = answerService;
		}

        public int Create(JobApplicationCreateDto dto)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
				if (dto.CandidateDto.Id == 0)
				{
					dto.CandidateDto = this.registeredUserService.Create(dto.CandidateDto);
				}

	            int answersCount = dto.Answers?.Count ?? 0;

				JobApplicationDto application = new JobApplicationDto
				{
					Answers = new List<AnswerDto>(answersCount),
					JobOfferId = dto.JobOfferId,
					JobCandidateId = dto.CandidateDto.Id,
					Status = Status.Open
				};

	            for (int i = 0; i < answersCount; i++)
	            {
		            application.Answers.Add(this.answerService.Create(dto.Answers[i]));
	            }

				var created = this.jobApplicationService.Create(application);

				uow.SaveChanges();
                return created.Id;
            }
        }

        public async Task<JobApplicationDto> Get(int id, bool withIncludes = true)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.GetAsync(id, withIncludes);
            }
        }

        public async Task<IEnumerable<JobApplicationDto>> Get(JobApplicationFilterDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.GetByFilterAsync(dto);
            }
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobOffer(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.GetByJobOffer(id);
            }
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobCandidate(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.GetByJobCandidate(id);
            }
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByFilterAsync(JobApplicationFilterDto filter)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.GetByFilterAsync(filter);
            }
        }

        public async Task<bool> DeclineApplication(int jobCandidateId)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
                bool result = await this.jobApplicationService.DeclineApplication(jobCandidateId);
	            if (result)
		            await uow.SaveChangesAsync();
				return result;
            }
        }

        public async Task<bool> AcceptApplication(int jobCandidateId)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
				bool result = await this.jobApplicationService.AcceptApplication(jobCandidateId);
				if (result)
					await uow.SaveChangesAsync();
	            return result;
			}
        }

        public void Delete(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                this.jobApplicationService.Delete(id);
            }
        }
    }
}
