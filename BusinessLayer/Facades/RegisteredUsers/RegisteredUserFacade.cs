using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Auth;
using BusinessLayer.Services.RegisteredUsers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.RegisteredUsers
{
    public class RegisteredUserFacade : FacadeBase, IRegisteredUserFacade
    {
        private readonly IRegisteredUserService registeredUserService;
	    private readonly IUserService userService;

	    public RegisteredUserFacade(IUnitOfWorkProvider unitOfWorkProvider, IRegisteredUserService registeredUserService, IUserService userService) : base(unitOfWorkProvider)
	    {
		    this.registeredUserService = registeredUserService;
		    this.userService = userService;
	    }

	    public async Task<int> Create(UserCreateDto userCreateDto)
	    {
		    using (var uow = this.UnitOfWorkProvider.Create())
		    {
			    var queryResult = await this.registeredUserService.ListAllAsync(new RegisteredUserFilterDto { UserName = userCreateDto.Username });
			    if (queryResult.Items.Count() == 1)
			    {
				    throw new ArgumentException();
			    }

			    RegisteredUserDto employer = Mapper.Map<RegisteredUserDto>(userCreateDto);
			    employer.Password = userService.CreatePassword(userCreateDto.Password);
			    this.registeredUserService.Create(employer);
			    await uow.SaveChangesAsync();
				return employer.Id;
		    }
	    }

	    public async Task<RegisteredUserDto> Get(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.registeredUserService.GetAsync(id);
            }
        }

        public async Task<IEnumerable<RegisteredUserDto>> GetBySkills(List<SkillDto> skills)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.registeredUserService.GetBySkills(skills);
            }
        }

        public async Task<IEnumerable<RegisteredUserDto>> Get(RegisteredUserFilterDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                var result = await this.registeredUserService.ListAllAsync(dto);
                return result.Items;
            }
        }

	    public async Task<Tuple<bool, string>> AuthorizeUserAsync(string username, string password)
		{
			using (this.UnitOfWorkProvider.Create())
			{
				var userResult = await this.registeredUserService.ListAllAsync(new RegisteredUserFilterDto { UserName = username });

				bool succ = false;
				string roles = "";

				if (userResult.Items.Count() == 1)
				{
					RegisteredUserDto user = userResult.Items.SingleOrDefault();
					succ = this.userService.VerifyHashedPassword(user.Password, password);
					roles = user.Roles;
				}

				return new Tuple<bool, string>(succ, roles);
			}
		}
	}
}
