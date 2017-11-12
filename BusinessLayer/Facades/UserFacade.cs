using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.Services;
using BusinessLayer.Services.Employers;
using BusinessLayer.Services.RegisteredUsers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    // TODO: asi rozdelit na RegisteredUserFacade a EmployerFacade
    public class UserFacade : FacadeBase, IUserFacade
    {
        private readonly RegisteredUserService registeredUserService;
        private readonly EmployerService employerService;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, RegisteredUserService registeredUserService, EmployerService employerService) : base(unitOfWorkProvider)
        {
            this.registeredUserService = registeredUserService;
            this.employerService = employerService;
        }

        public int RegisterNewUser(RegisteredUserDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return this.registeredUserService.Create(dto);
            }
        }

        public int RegisterNewUser(EmployerDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return this.employerService.Create(dto);
            }
        }

        public async Task<RegisteredUserDto> GetRegisteredUserDto(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.registeredUserService.GetAsync(id);
            }
        }

        public async Task<QueryResultDto<RegisteredUserDto, FilterDtoBase>> GetUsersWith(SkillDto skillDto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.registeredUserService.GetUsersWith(skillDto);
            }
        }

        public void RemoveUser(RegisteredUserDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                this.registeredUserService.Delete(dto.Id);
            }
        }

        public async Task<EmployerDto> GetEmployer(int id)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await this.employerService.GetAsync(id);
            }
        }

        public void RemoveUser(EmployerDto dto)
        {
            using (this.UnitOfWorkProvider.Create())
            {
                this.employerService.Delete(dto.Id);
            }
        }
    }
}
