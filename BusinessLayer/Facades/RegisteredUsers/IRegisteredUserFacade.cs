using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.RegisteredUsers
{
    public interface IRegisteredUserFacade
    {
        int Create(RegisteredUserDto dto);
        Task<RegisteredUserDto> Get(int id);
        Task<IEnumerable<RegisteredUserDto>> Get(RegisteredUserFilterDto dto);
        Task<IEnumerable<RegisteredUserDto>> GetBySkills(List<SkillDto> skills);
    }
}