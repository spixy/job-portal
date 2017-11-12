using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class JobApplicationFacade : FacadeBase, IJobApplicationFacade
    {
        private readonly JobApplicationService jobApplicationService;
        private readonly CandidateService candidateService;

        public JobApplicationFacade(IUnitOfWorkProvider unitOfWorkProvider,
            JobApplicationService jobApplicationService) : base(unitOfWorkProvider)
        {
            this.jobApplicationService = jobApplicationService;
        }

        public async Task<int> Create(JobApplicationCreateDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                JobCandidate candidate = await this.candidateService.GetAsync(dto.applicationDto.JobCandidateId);

                // unregistered user?
                if (candidate == null && dto.candidateDto != null)
                {
                    this.candidateService.Create(dto.candidateDto);
                }

                return this.jobApplicationService.Create(dto.applicationDto);
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
                await this.jobApplicationService.ListAllAsync(dto);
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
