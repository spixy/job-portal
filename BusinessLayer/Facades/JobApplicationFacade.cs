﻿using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Infrastructure;
using Infrastructure.UnitOfWork;

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
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobApplicationService.Create(dto);
            }
        }

        public void Remove(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                this.jobApplicationService.Remove(id);
            }
        }
    }
}