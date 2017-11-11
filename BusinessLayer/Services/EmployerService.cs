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
    public class EmployerService : CrudQueryServiceBase<Employer, EmployerDto, FilterDtoBase>
    {
        public EmployerService(IMapper mapper, IRepository<Employer> repository,
            QueryObjectBase<EmployerDto, Employer, FilterDtoBase, IQuery<Employer>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<Employer> GetWithIncludesAsync(int entityId)
        {
            return await this.Repository.GetAsync(entityId, nameof(Employer.JobOffers));
        }
    }
}
