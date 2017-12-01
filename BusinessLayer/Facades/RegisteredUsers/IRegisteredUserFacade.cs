using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.RegisteredUsers
{
    public interface IRegisteredUserFacade
    {
	    Task<int> Create(UserCreateDto dto);
        Task<RegisteredUserDto> Get(int id);
        Task<IEnumerable<RegisteredUserDto>> Get(RegisteredUserFilterDto dto);
        Task<IEnumerable<RegisteredUserDto>> GetBySkills(List<SkillDto> skills);
	    Task<Tuple<bool, string>> AuthorizeUserAsync(string username, string password);
	}
}