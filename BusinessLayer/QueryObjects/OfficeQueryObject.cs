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
    public class OfficeQueryObject : QueryObjectBase<OfficeDto, Office, OfficeFilterDto, IQuery<Office>>
    {
        public OfficeQueryObject(IMapper mapper, IQuery<Office> query) : base(mapper, query) { }

        protected override IQuery<Office> ApplyWhereClause(IQuery<Office> query, OfficeFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Address))
            {
                predicates.Add(new SimplePredicate(nameof(Office.Address), ValueComparingOperator.Equal,
                    filter.Address));
            }
            if (!string.IsNullOrWhiteSpace(filter.City))
            {
                predicates.Add(new SimplePredicate(nameof(Office.City), ValueComparingOperator.Equal,
                    filter.City));
            }
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicates.Add(new SimplePredicate(nameof(Office.Email), ValueComparingOperator.Equal,
                    filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                predicates.Add(new SimplePredicate(nameof(Office.Phone), ValueComparingOperator.Equal,
                    filter.Phone));
            }

            if (predicates.Count > 0)
            {
                return query.Where(new CompositePredicate(predicates));
            }

            return query;
        }

    }
}
