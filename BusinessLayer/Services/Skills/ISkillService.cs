using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.Skills
{
    public interface ISkillService : ICrudService<SkillDto, SkillFilterDto>
    {
        /// <summary>
        /// Get Skill by name 
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Skill with given name</returns>
        Task<SkillDto> GetByName(string name);
    }
}
