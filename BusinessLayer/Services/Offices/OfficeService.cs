using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using BusinessLayer.Services.Skills;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;

namespace BusinessLayer.Services.Offices
{
    public class OfficeService : CrudQueryServiceBase<Office, OfficeDto, OfficeFilterDto>, IOfficeService
    {
        public OfficeService(IMapper mapper, IRepository<Office> repository, QueryObjectBase<OfficeDto, Office, OfficeFilterDto, IQuery<Office>> query) : base(mapper, repository, query)
        {
        }

        protected override async Task<Office> GetWithIncludesAsync(int entityId)
        {
            return await this.Repository.GetAsync(entityId);
        }
    }
}
