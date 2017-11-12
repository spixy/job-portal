using AutoMapper;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BusinessLayer.QueryObjects
{
    public class EmployerQueryObject : QueryObjectBase<EmployerDto, Employer, EmployerFilterDto, IQuery<Employer>>
    {
        public EmployerQueryObject(IMapper mapper, IQuery<Employer> query) : base(mapper, query)
        {
        }

        protected override IQuery<Employer> ApplyWhereClause(IQuery<Employer> query, EmployerFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicates.Add(new SimplePredicate(nameof(Employer.Email), ValueComparingOperator.Equal,
                    filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicates.Add(new SimplePredicate(nameof(Employer.Name), ValueComparingOperator.Equal,
                    filter.Name));
            }
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                predicates.Add(new SimplePredicate(nameof(Employer.Phone), ValueComparingOperator.Equal,
                    filter.Phone));
            }

            return MergePredicates(predicates);
        }
    }
}
