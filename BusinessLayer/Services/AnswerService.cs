using System;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services
{
    public class AnswerService : CrudQueryServiceBase<Answer, AnswerDto, AnswerFilterDto>
    {
        public AnswerService(IMapper mapper, IRepository<Answer> repository, QueryObjectBase<AnswerDto, Answer, AnswerFilterDto, IQuery<Answer>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<Answer> GetWithIncludesAsync(int entityId)
        {
            return await this.Repository.GetAsync(entityId, nameof(Answer.Question), nameof(Answer.JobApplication));
        }
    }
}
