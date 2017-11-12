using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.RegisteredUsers
{
    public class RegisteredUserService : CrudQueryServiceBase<RegisteredUser, RegisteredUserDto, RegisteredUserFilterDto>
    {
        public RegisteredUserService(IMapper mapper, IRepository<RegisteredUser> repository,
            QueryObjectBase<RegisteredUserDto, RegisteredUser, RegisteredUserFilterDto, IQuery<RegisteredUser>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<RegisteredUser> GetWithIncludesAsync(int entityId)
        {
            return await this.Repository.GetAsync(entityId, nameof(RegisteredUser.JobApplications));
        }
    }
}
