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
    public class JobApplicationQueryObject : QueryObjectBase<JobApplicationDto, JobApplication, JobApplicationFilterDto, IQuery<JobApplication>>
    {
        public JobApplicationQueryObject(IMapper mapper, IQuery<JobApplication> query) : base(mapper, query)
        {
        }

        protected override IQuery<JobApplication> ApplyWhereClause(IQuery<JobApplication> query, JobApplicationFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();

            if (filter.JobOfferId != null)
                definedPredicates.Add(new SimplePredicate(nameof(JobApplication.JobOfferId), ValueComparingOperator.Equal, filter.JobOfferId));

            if (filter.JobCandidateId != null)
                definedPredicates.Add(new SimplePredicate(nameof(JobApplication.JobCandidateId), ValueComparingOperator.Equal, filter.JobCandidateId));

            if (filter.Status != null)
                definedPredicates.Add(new SimplePredicate(nameof(JobApplication.Status), ValueComparingOperator.Equal, filter.Status));


            return MergePredicates(definedPredicates);
        }
    }
}
