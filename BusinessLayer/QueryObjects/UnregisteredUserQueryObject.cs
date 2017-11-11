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
    public class UnregisteredUserQueryObject : QueryObjectBase<UnregisteredUserDto, UnregisteredUser, UnregisteredUserFilterDto, IQuery<UnregisteredUser>>
    {
        public UnregisteredUserQueryObject(IMapper mapper, IQuery<UnregisteredUser> query) : base(mapper, query)
        {
        }

        protected override IQuery<UnregisteredUser> ApplyWhereClause(IQuery<UnregisteredUser> query, UnregisteredUserFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicates.Add(new SimplePredicate(nameof(UnregisteredUser.Email), ValueComparingOperator.Equal,
                    filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicates.Add(new SimplePredicate(nameof(UnregisteredUser.Name), ValueComparingOperator.Equal,
                    filter.Name));
            }
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                predicates.Add(new SimplePredicate(nameof(UnregisteredUser.Phone), ValueComparingOperator.Equal,
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
