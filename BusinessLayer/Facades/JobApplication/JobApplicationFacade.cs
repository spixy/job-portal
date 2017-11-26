using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Candidate;
using BusinessLayer.Services.JobApplications;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.JobApplication
{
    public class JobApplicationFacade : FacadeBase, IJobApplicationFacade
    {
        private readonly IJobApplicationService jobApplicationService;
        private readonly ICandidateService candidateServiceService;

        public JobApplicationFacade(IUnitOfWorkProvider unitOfWorkProvider, IJobApplicationService jobApplicationService, ICandidateService candidateServiceService)
            : base(unitOfWorkProvider)
        {
            this.jobApplicationService = jobApplicationService;
            this.candidateServiceService = candidateServiceService;
        }

        public async Task<int> Create(JobApplicationCreateDto dto)
        {
            using (var uow = this.UnitOfWorkProvider.Create())
            {
                JobCandidateDto candidate = await this.candidateServiceService.GetAsync(dto.ApplicationDto.JobCandidateId);

                // unregistered user?
                if (candidate == null && dto.CandidateDto != null)
                {
                    this.candidateServiceService.Create(dto.CandidateDto);
                }

                var created = this.jobApplicationService.Create(dto.ApplicationDto);
                uow.Commit();
                return created.Id;
            }
        }

        public async Task Get(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                await this.jobApplicationService.GetAsync(id);
            }
        }

        public async Task Get(JobApplicationFilterDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                await this.jobApplicationService.GetByFilterAsync(dto);
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
