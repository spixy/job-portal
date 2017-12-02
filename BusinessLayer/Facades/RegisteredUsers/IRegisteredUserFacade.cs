using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.RegisteredUsers
{
    public interface IRegisteredUserFacade
    {
	    Task<int> Create(RegisteredUserCreateDto dto);
        Task<RegisteredUserDto> Get(int id);
        Task<IEnumerable<RegisteredUserDto>> Get(RegisteredUserFilterDto dto);
        Task<IEnumerable<RegisteredUserDto>> GetBySkills(List<SkillDto> skills);
	    Task<LoginDto> AuthorizeUserAsync(string username, string password);
	}
}