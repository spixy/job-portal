using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Account;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Auth;
using BusinessLayer.Services.RegisteredUsers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.RegisteredUsers
{
    public class RegisteredUserFacade : FacadeBase
    {
        private readonly IRegisteredUserService registeredUserService;
	    private readonly IUserService userService;

	    public RegisteredUserFacade(IUnitOfWorkProvider unitOfWorkProvider, IRegisteredUserService registeredUserService, IUserService userService) : base(unitOfWorkProvider)
	    {
		    this.registeredUserService = registeredUserService;
		    this.userService = userService;
	    }

	    public async Task<int> Create(RegisteredUserCreateDto registeredUserCreateDto)
	    {
		    using (var uow = this.UnitOfWorkProvider.Create())
		    {
			    var queryResult = await this.registeredUserService.ListAllAsync(new RegisteredUserFilterDto { UserName = registeredUserCreateDto.Username });
			    if (queryResult.Items.Count() == 1)
			    {
				    throw new ArgumentException();
			    }

			    registeredUserCreateDto.Password = userService.CreatePassword(registeredUserCreateDto.Password);
			    var user = registeredUserService.Create(registeredUserCreateDto);
			    await uow.SaveChangesAsync();
			    return user.Id;
		    }
	    }

	    public async Task<RegisteredUserDto> Get(int id, bool withIncludes = true)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.registeredUserService.GetAsync(id, withIncludes);
            }
        }

        public async Task<IEnumerable<RegisteredUserDto>> GetBySkills(List<SkillDto> skills)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.registeredUserService.GetBySkills(skills);
            }
        }

	    public async Task<RegisteredUserDto> GetByUsername(string username)
		{
			if (string.IsNullOrEmpty(username))
			{
				return null;
			}

			using (this.UnitOfWorkProvider.Create())
		    {
			    var result = await this.registeredUserService.ListAllAsync(new RegisteredUserFilterDto {UserName = username});
			    return result.Items.FirstOrDefault();
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

	    public async Task<LoginDto> AuthorizeUserAsync(string username, string password)
		{
			using (this.UnitOfWorkProvider.Create())
			{
				var userResult = await this.registeredUserService.ListAllAsync(new RegisteredUserFilterDto { UserName = username });

				bool succ = false;
				Role role = Role.None;

				if (userResult.Items.Count() == 1)
				{
					RegisteredUserDto user = userResult.Items.SingleOrDefault();
					succ = this.userService.VerifyHashedPassword(user.Password, password);
					role = Role.User;
				}

				return new LoginDto(succ, role);
			}
		}
	}
}
