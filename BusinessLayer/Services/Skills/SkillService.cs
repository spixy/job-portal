using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;

namespace BusinessLayer.Services.Skills
{
    public class SkillService : CrudQueryServiceBase<Skill, SkillDto, SkillFilterDto>, ISkillService
    {
        public SkillService(IMapper mapper, IRepository<Skill> repository,
            QueryObjectBase<SkillDto, Skill, SkillFilterDto, IQuery<Skill>> quoryObject)
            : base(mapper, repository, quoryObject)
        {
        }

        protected override async Task<Skill> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Skill.JobOffers));
        }

        public async Task<SkillDto> GetByName(string name)
        {
            var queryResult = await Query.ExecuteQuery(new SkillFilterDto { Names = new[] { name } });
            return queryResult.Items.SingleOrDefault();
        }
    }
}
