using BusinessLayer.DTOs;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BusinessLayer.QueryObjects
{
    public class JobCandidateQueryObject : QueryObjectBase<JobCandidateDto, JobCandidate, JobCandidateFilterDto, IQuery<JobCandidate>>
    {
        public JobCandidateQueryObject(IMapper mapper, IQuery<JobCandidate> query) : base(mapper, query)
        {
        }

        protected override IQuery<JobCandidate> ApplyWhereClause(IQuery<JobCandidate> query, JobCandidateFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicates.Add(new SimplePredicate(nameof(JobCandidate.Email), ValueComparingOperator.Equal,
                    filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicates.Add(new SimplePredicate(nameof(JobCandidate.Name), ValueComparingOperator.Equal,
                    filter.Name));
            }
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                predicates.Add(new SimplePredicate(nameof(JobCandidate.Phone), ValueComparingOperator.Equal,
                    filter.Phone));
            }

            return MergePredicates(predicates);
        }
    }
}
