using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.RegisteredUsers
{
    public interface IRegisteredUserService : ICrudService<RegisteredUserDto, RegisteredUserFilterDto>
    {
        Task<IEnumerable<RegisteredUserDto>> GetBySkills(List<SkillDto> skills);
	    RegisteredUserDto Create(RegisteredUserCreateDto createDto);
	}
}