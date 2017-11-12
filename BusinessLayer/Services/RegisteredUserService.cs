using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services
{
    public class RegisteredUserService : CrudQueryServiceBase<RegisteredUser, RegisteredUserDto, FilterDtoBase>
    {
        public RegisteredUserService(IMapper mapper, IRepository<RegisteredUser> repository,
            QueryObjectBase<RegisteredUserDto, RegisteredUser, FilterDtoBase, IQuery<RegisteredUser>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<RegisteredUser> GetWithIncludesAsync(int entityId)
        {
            return await this.Repository.GetAsync(entityId, nameof(RegisteredUser.JobApplications));
        }

        public async Task<QueryResultDto<RegisteredUserDto, FilterDtoBase>> GetUsersWith(SkillDto skillDto)
        {
            return await this.Query.ExecuteQuery(null);
        }
    }
}
