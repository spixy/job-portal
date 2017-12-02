using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;

namespace BusinessLayer.Services.Employers
{
    public class EmployerService : CrudQueryServiceBase<Employer, EmployerDto, EmployerFilterDto>, IEmployerService
    {
        public EmployerService(IMapper mapper, IRepository<Employer> repository,
            QueryObjectBase<EmployerDto, Employer, EmployerFilterDto, IQuery<Employer>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<Employer> GetWithIncludesAsync(int entityId)
        {
            return await this.Repository.GetAsync(entityId, nameof(Employer.JobOffers));
        }

        public async Task<EmployerDto> GetByName(string name)
        {
            var queryResult = await Query.ExecuteQuery(new EmployerFilterDto { Name = name });
            return queryResult.Items.FirstOrDefault();
        }

        public async Task<EmployerDto> GetByEmail(string email)
        {
            var queryResult = await Query.ExecuteQuery(new EmployerFilterDto { Email = email });
            return queryResult.Items.FirstOrDefault();
        }

        public async Task<QueryResultDto<EmployerDto, EmployerFilterDto>> GetFiltered(EmployerFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

	    public EmployerDto Create(EmployerCreateDto createDto)
	    {
			EmployerDto employerDto = Mapper.Map<EmployerDto>(createDto);
		    return this.Create(employerDto);
	    }
    }
}
