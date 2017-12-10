using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Offices;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades.Offices
{
    public class OfficeFacade : FacadeBase
    {
        private readonly IOfficeService officeService;

        public OfficeFacade(IUnitOfWorkProvider unitOfWorkProvider, IOfficeService officeService) : base(unitOfWorkProvider)
        {
            this.officeService = officeService;
        }

        public async Task<QueryResultDto<OfficeDto, OfficeFilterDto>> GetAllOffices()
        {
            using (this.UnitOfWorkProvider.Create())
            {
                return await officeService.ListAllAsync();
            }
        }
    }
}
