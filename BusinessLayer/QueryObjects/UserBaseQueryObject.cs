using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;

namespace BusinessLayer.QueryObjects
{
    public class UserBaseQueryObject : QueryObjectBase<UserBaseDto, UserBase, UserBaseFilterDto, IQuery<UserBase>>
    {
        public UserBaseQueryObject(IMapper mapper, IQuery<UserBase> query) : base(mapper, query)
        {
        }

        protected override IQuery<UserBase> ApplyWhereClause(IQuery<UserBase> query, UserBaseFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicates.Add(new SimplePredicate(nameof(UserBase.Email), ValueComparingOperator.Equal,
                    filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicates.Add(new SimplePredicate(nameof(UserBase.Name), ValueComparingOperator.Equal,
                    filter.Name));
            }
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                predicates.Add(new SimplePredicate(nameof(UserBase.Phone), ValueComparingOperator.Equal,
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
