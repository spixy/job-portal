using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.JobOffers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class JobOfferFacade : FacadeBase, IJobOfferFacade
    {
        private readonly IJobOfferService jobOfferService;

        public JobOfferFacade(IUnitOfWorkProvider unitOfWorkProvider, IJobOfferService jobOfferService) : base(unitOfWorkProvider)
        {
            this.jobOfferService = jobOfferService;
        }

        public int Create(JobOfferDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return this.jobOfferService.Create(dto);
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

        public async Task<IEnumerable<JobOfferDto>> Get(JobOfferFilterDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.jobOfferService.GetFilteredAsync(dto);
            }
        }

        public Task Create()
        {
            throw new NotImplementedException();
        }
    }
}
