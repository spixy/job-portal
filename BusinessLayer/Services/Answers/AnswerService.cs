using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Answers
{
    public class AnswerService : CrudQueryServiceBase<Answer, AnswerDto, AnswerFilterDto>, IAnswerService
    {
        public AnswerService(IMapper mapper, IRepository<Answer> repository, QueryObjectBase<AnswerDto, Answer, AnswerFilterDto, IQuery<Answer>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<Answer> GetWithIncludesAsync(int entityId)
        {
            return await this.Repository.GetAsync(entityId, nameof(Answer.Question), nameof(Answer.JobApplication));
        }

        public async Task<IEnumerable<AnswerDto>> GetByQuestion(int questionId)
        {
            var queryResult = await Query.ExecuteQuery(new AnswerFilterDto { QuestionId = questionId });
            return queryResult.Items;
        }

        public async Task<IEnumerable<AnswerDto>> GetByApplication(int jobApplicationId)
        {
            var queryResult = await Query.ExecuteQuery(new AnswerFilterDto { JobApplicationId = jobApplicationId });
            return queryResult.Items;
        }

        public async Task<AnswerDto> GetByApplicationQuestion(int jobApplicationId, int questionId)
        {
            var queryResult = await Query.ExecuteQuery(new AnswerFilterDto { JobApplicationId = jobApplicationId, QuestionId = questionId });
            return queryResult.Items.SingleOrDefault();
        }
    }
}
