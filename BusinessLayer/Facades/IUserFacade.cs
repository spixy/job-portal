using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.Facades
{
    public interface IUserFacade
    {
        int RegisterNewUser(EmployerDto userDto);
        int RegisterNewUser(RegisteredUserDto userDto);

        void RemoveUser(EmployerDto userDto);
        void RemoveUser(RegisteredUserDto userDto);

        Task<EmployerDto> GetEmployer(int id);
        Task<RegisteredUserDto> GetRegisteredUserDto(int id);

        Task<QueryResultDto<RegisteredUserDto, FilterDtoBase>> GetUsersWith(SkillDto skillDto);
    }
}