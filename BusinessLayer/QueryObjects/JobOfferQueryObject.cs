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
    public class JobOfferQueryObject : QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>>
    {
        public JobOfferQueryObject(IMapper mapper, IQuery<JobOffer> query) : base(mapper, query)
        {
        }

        protected override IQuery<JobOffer> ApplyWhereClause(IQuery<JobOffer> query, JobOfferFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                definedPredicates.Add(new SimplePredicate(nameof(JobOffer.Name), ValueComparingOperator.StringContains, filter.Name));

            if (filter.EmployerId != null)
                definedPredicates.Add(new SimplePredicate(nameof(JobOffer.EmployerId), ValueComparingOperator.Equal, filter.EmployerId));

            return MergePredicates(definedPredicates);
        }
    }
}
