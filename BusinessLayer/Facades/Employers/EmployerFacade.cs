using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Employers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.Employers
{
    public class EmployerFacade : FacadeBase, IEmployerFacade
    {
        private readonly IEmployerService employerService;

        public EmployerFacade(IUnitOfWorkProvider unitOfWorkProvider, IEmployerService employerService) : base(unitOfWorkProvider)
        {
            this.employerService = employerService;
        }

        public int Create(EmployerDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return this.employerService.Create(dto);
            }
        }

        public async Task<EmployerDto> Get(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.employerService.GetAsync(id);
            }
        }

        public async Task<EmployerDto> GetByEmail(string email)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.employerService.GetByEmail(email);
            }
        }

        public async Task<EmployerDto> GetByName(string name)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.employerService.GetByName(name);
            }
        }

        public async Task<IEnumerable<EmployerDto>> Get(EmployerFilterDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                var result = await this.employerService.GetFiltered(dto);;
                return result.Items;
            }
        }
    }
}
