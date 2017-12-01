using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Auth;
using BusinessLayer.Services.Employers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.Employers
{
    public class EmployerFacade : FacadeBase, IEmployerFacade
    {
        private readonly IEmployerService employerService;
	    private readonly IUserService userService;

	    public EmployerFacade(IUnitOfWorkProvider unitOfWorkProvider, IEmployerService employerService, IUserService userService) : base(unitOfWorkProvider)
	    {
		    this.employerService = employerService;
		    this.userService = userService;
	    }

	    public async Task<int> Create(EmployerCreateDto employerDto)
	    {
		    using (var uow = this.UnitOfWorkProvider.Create())
		    {
				var queryResult = await this.employerService.ListAllAsync(new EmployerFilterDto {UserName = employerDto.Username});
				if (queryResult.Items.Count() == 1)
				{
					throw new ArgumentException();
				}

				EmployerDto employer = Mapper.Map<EmployerDto>(employerDto);
				employer.Password = userService.CreatePassword(employerDto.Password);
				this.employerService.Create(employer);
			    await uow.SaveChangesAsync();
				return employer.Id;
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
                var result = await this.employerService.GetFiltered(dto);
                return result.Items;
            }
        }

	    public async Task<Tuple<bool, string>> AuthorizeUserAsync(string username, string password)
	    {
		    using (this.UnitOfWorkProvider.Create())
		    {
			    var employerResult = await this.employerService.ListAllAsync(new EmployerFilterDto {UserName = username});

			    bool succ = false;
			    string roles = "";

			    if (employerResult.Items.Count() == 1)
			    {
				    EmployerDto employer = employerResult.Items.SingleOrDefault();
				    succ = this.userService.VerifyHashedPassword(employer.Password, password);
				    roles = employer.Roles;
			    }

			    return new Tuple<bool, string>(succ, roles);
		    }
	    }
	}
}
