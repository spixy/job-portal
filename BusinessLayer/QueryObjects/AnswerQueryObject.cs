using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BusinessLayer.QueryObjects
{
    public class AnswerQueryObject : QueryObjectBase<AnswerDto, Answer, AnswerFilterDto, IQuery<Answer>>
    {
        public AnswerQueryObject(IMapper mapper, IQuery<Answer> query) : base(mapper, query)
        {
        }

        protected override IQuery<Answer> ApplyWhereClause(IQuery<Answer> query, AnswerFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (filter.JobApplicationId != null)
                predicates.Add(new SimplePredicate(nameof(Answer.JobApplicationId), ValueComparingOperator.Equal, filter.JobApplicationId));

            if (filter.QuestionId != null)
                predicates.Add(new SimplePredicate(nameof(Answer.QuestionId), ValueComparingOperator.Equal, filter.QuestionId));

            return MergePredicates(predicates);
        }
    }
}
