using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.RegisteredUsers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.Employers
{
    public class RegisteredUserFacade : FacadeBase, IRegisteredUserFacade
    {
        private readonly RegisteredUserService registeredUserService;

        public RegisteredUserFacade(IUnitOfWorkProvider unitOfWorkProvider, RegisteredUserService registeredUserService) : base(unitOfWorkProvider)
        {
            this.registeredUserService = registeredUserService;
        }

        public int Create(RegisteredUserDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return this.registeredUserService.Create(dto);
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
                var result = await this.registeredUserService.ListAllAsync(dto); ;
                return result.Items;
            }
        }
    }
}
