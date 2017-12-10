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
    public class JobOfferQueryObject : QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>>
    {
        public JobOfferQueryObject(IMapper mapper, IQuery<JobOffer> query) : base(mapper, query)
        {
        }

        protected override IQuery<JobOffer> ApplyWhereClause(IQuery<JobOffer> query, JobOfferFilterDto filter)
        {
            List<IPredicate> definedPredicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                definedPredicates.Add(new SimplePredicate(nameof(JobOffer.Name), ValueComparingOperator.StringContains, filter.Name));

			if (filter.EmployerId != null)
                definedPredicates.Add(new SimplePredicate(nameof(JobOffer.EmployerId), ValueComparingOperator.Equal, filter.EmployerId));

	        if (filter.Skills != null)
	        {
				var predicates = new List<IPredicate>();
		        foreach (SkillDto skillDto in filter.Skills)
		        {
			        var skill = Mapper.Map<Skill>(skillDto);
			        predicates.Add(new SimplePredicate(nameof(JobOffer.Skills), ValueComparingOperator.EnumerableContains, skill));
				}
		        definedPredicates.Add(new CompositePredicate(predicates, LogicalOperator.OR));
			}

			return MergePredicates(definedPredicates);
        }
    }
}
