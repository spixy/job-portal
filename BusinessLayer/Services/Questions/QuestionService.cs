﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;

namespace BusinessLayer.Services.Questions
{
    public class QuestionService : CrudQueryServiceBase<Question, QuestionDto, QuestionFilterDto>, IQuestionService
    {
        public QuestionService(IMapper mapper, IRepository<Question> repository,
            QueryObjectBase<QuestionDto, Question, QuestionFilterDto, IQuery<Question>> quoryObject)
            : base(mapper, repository, quoryObject)
        {
        }

        protected override async Task<Question> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof (Question.JobApplications));
        }

        public async Task<IEnumerable<QuestionDto>> GetByWords(string[] words)
        {
            var queryResult = await Query.ExecuteQuery(new QuestionFilterDto { Keywords = words });
            return queryResult.Items;
        }
    }
}
