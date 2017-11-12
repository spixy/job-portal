using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs.Common;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BusinessLayer.QueryObjects.Common
{
    public abstract class QueryObjectBase<TDto, TEntity, TFilter, TQuery>
		where TFilter : FilterDtoBase
		where TQuery : IQuery<TEntity>
		where TEntity : class, IEntity, new()
	{
	    protected readonly IMapper Mapper;

		protected readonly IQuery<TEntity> Query;

		protected QueryObjectBase(IMapper mapper, TQuery query)
		{
			this.Mapper = mapper;
			this.Query = query;
		}

		protected abstract IQuery<TEntity> ApplyWhereClause(IQuery<TEntity> query, TFilter filter);

		public virtual async Task<QueryResultDto<TDto, TFilter>> ExecuteQuery(TFilter filter)
		{
			IQuery<TEntity> query = ApplyWhereClause(this.Query, filter);
			if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
			{
				query = query.SortBy(filter.SortCriteria, filter.SortAscending);
			}
			if (filter.RequestedPageNumber.HasValue)
			{
				query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
			}

			QueryResult<TEntity> queryResult = await query.ExecuteAsync();

			var queryResultDto = this.Mapper.Map<QueryResultDto<TDto, TFilter>>(queryResult);
			queryResultDto.Filter = filter;
			return queryResultDto;
		}

	    protected IQuery<TEntity> MergePredicates(List<IPredicate> predicates, LogicalOperator logicalOperator = LogicalOperator.AND)
	    {
	        switch (predicates.Count)
	        {
	            case 0:
	                return this.Query;
	            case 1:
	                return this.Query.Where(predicates.First());
                default:
                    return this.Query.Where(new CompositePredicate(predicates, logicalOperator));
            }
	    }
    }
}
