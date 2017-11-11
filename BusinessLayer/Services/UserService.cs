using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.QueryObjects;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services
{
    public class UserService : CrudQueryServiceBase<RegisteredUser, RegisteredUserDto, FilterDtoBase>
    {
        protected UserService(IMapper mapper, IRepository<RegisteredUser> repository, QueryObjectBase<RegisteredUserDto, RegisteredUser, FilterDtoBase, IQuery<RegisteredUser>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<RegisteredUser> GetWithIncludesAsync(int entityId)
        {
            RegisteredUser user = await this.repository.GetAsync(entityId);
            return user;
        }
    }
}
