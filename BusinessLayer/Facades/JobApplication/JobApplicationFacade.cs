using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Enums;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.JobApplications;
using BusinessLayer.Services.RegisteredUsers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.JobApplication
{
    public class JobApplicationFacade : FacadeBase, IJobApplicationFacade
	{
        private readonly IJobApplicationService jobApplicationService;
        private readonly IRegisteredUserService registeredUserService;

		public JobApplicationFacade(IUnitOfWorkProvider unitOfWorkProvider, IJobApplicationService jobApplicationService, IRegisteredUserService registeredUserService) : base(unitOfWorkProvider)
		{
			this.jobApplicationService = jobApplicationService;
			this.registeredUserService = registeredUserService;
		}

        public async Task<int> Create(JobApplicationCreateDto dto)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
                RegisteredUserDto registerUser = await this.registeredUserService.GetAsync(dto.CandidateDto.Id);

				// already registered user?
				if (registerUser != null)
                {
	                dto.CandidateDto = registerUser;
                }

				JobApplicationDto application = new JobApplicationDto
				{
					Answers = dto.Answers,
					JobOfferId = dto.JobOfferId,
					JobCandidate = dto.CandidateDto,
					Status = Status.Open
				};

				var created = this.jobApplicationService.Create(application);
                uow.SaveChanges();
                return created.Id;
            }
        }

        public async Task<JobApplicationDto> Get(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.GetAsync(id);
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
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.DeclineApplication(jobCandidateId);
            }
        }

        public async Task<bool> AcceptApplication(int jobCandidateId)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.AcceptApplication(jobCandidateId);
            }
        }

        public async Task Update(JobApplicationDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                await this.jobApplicationService.Update(dto);
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
