using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Query
{
	public class QueryResult<TEntity> : IEquatable<QueryResult<TEntity>> where TEntity : IEntity
	{
		public long TotalItemsCount { get; }

		public int? RequestedPageNumber { get; }

		public int PageSize { get; }

		public IList<TEntity> Items { get; }

		public bool PagingEnabled => RequestedPageNumber != null;

		public QueryResult(IList<TEntity> items, long totalItemsCount = long.MaxValue, int pageSize = Config.DefaultPageSize, int? requestedPageNumber = null)
		{
			TotalItemsCount = totalItemsCount;
			RequestedPageNumber = requestedPageNumber;
			PageSize = pageSize;
			Items = items;
		}

		public bool Equals(QueryResult<TEntity> other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}
			return TotalItemsCount == other.TotalItemsCount &&
			       RequestedPageNumber == other.RequestedPageNumber &&
			       PageSize == other.PageSize &&
			       Items.All(entity => other.Items.Select(item => item.Id).Contains(entity.Id));
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryResult<TEntity>);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = TotalItemsCount.GetHashCode();
				hashCode = (hashCode * 397) ^ RequestedPageNumber.GetHashCode();
				hashCode = (hashCode * 397) ^ PageSize;
				hashCode = (hashCode * 397) ^ (Items != null ? Items.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
