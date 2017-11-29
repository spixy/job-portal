using System.Collections.Generic;
using System.Linq;
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
    public class SkillQueryObject : QueryObjectBase<SkillDto, Skill, SkillFilterDto, IQuery<Skill>>
    {
        public SkillQueryObject(IMapper mapper, IQuery<Skill> query) : base(mapper, query)
        {
        }

        protected override IQuery<Skill> ApplyWhereClause(IQuery<Skill> query, SkillFilterDto filter)
        {
            if (filter.Names == null || !filter.Names.Any())
            {
                return query;
            }
            var predicates = new List<IPredicate>(filter.Names
                .Select(name => new SimplePredicate(
                    nameof(Skill.Name),
                    ValueComparingOperator.Equal,
                    name)));
            var predicate = new CompositePredicate(predicates, LogicalOperator.OR);
            return query.Where(predicate);
        }
    }
}
