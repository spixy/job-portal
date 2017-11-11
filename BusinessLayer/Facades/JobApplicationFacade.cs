using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Infrastructure;

namespace BusinessLayer.Facades
{
    public class JobApplicationFacade : FacadeBase
    {
        private readonly JobApplicationService jobApplicationService;

        public JobApplicationFacade(IUnitOfWorkProvider unitOfWorkProvider,
            JobApplicationService jobApplicationService) : base(unitOfWorkProvider)
        {
            this.jobApplicationService = jobApplicationService;
        }

        public async Task<int> ApplyToJob(JobApplicationCreateDto dto)
        {
            using (IUnitOfWork uow = this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.Create(dto);
            }
        }

        public void Remove(int id)
        {
            using (IUnitOfWork uow = this.UnitOfWorkProvider.Create())
            {
                this.jobApplicationService.Remove(id);
            }
        }
    }
}
