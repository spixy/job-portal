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
    public class RegisteredUserQueryObject : QueryObjectBase<RegisteredUserDto, RegisteredUser, RegisteredUserFilterDto, IQuery<RegisteredUser>>
    {
        public RegisteredUserQueryObject(IMapper mapper, IQuery<RegisteredUser> query) : base(mapper, query)
        {
        }

        protected override IQuery<RegisteredUser> ApplyWhereClause(IQuery<RegisteredUser> query, RegisteredUserFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicates.Add(new SimplePredicate(nameof(RegisteredUser.Email), ValueComparingOperator.Equal, filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicates.Add(new SimplePredicate(nameof(RegisteredUser.Name), ValueComparingOperator.Equal, filter.Name));
            }
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                predicates.Add(new SimplePredicate(nameof(RegisteredUser.Phone), ValueComparingOperator.Equal, filter.Phone));
			}
			if (!string.IsNullOrWhiteSpace(filter.UserName))
			{
				predicates.Add(new SimplePredicate(nameof(RegisteredUser.Username), ValueComparingOperator.Equal, filter.UserName));
			}
			if (filter.Skills != null)
            {
                foreach (Skill skill in filter.Skills)
                {
                    predicates.Add(new SimplePredicate(nameof(JobCandidate.Skills), ValueComparingOperator.EnumerableContains, skill));
                }
            }

            return MergePredicates(predicates);
        }
    }
}
