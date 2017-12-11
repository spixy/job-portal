using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Skills;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class SkillFacade : FacadeBase
    {
        private readonly ISkillService skillService;

        public SkillFacade(IUnitOfWorkProvider unitOfWorkProvider, ISkillService skillService) : base(unitOfWorkProvider)
        {
            this.skillService = skillService;
        }

        public async Task<QueryResultDto<SkillDto, SkillFilterDto>> GetAllSkills()
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await skillService.ListAllAsync();
            }
        }
    }
}
