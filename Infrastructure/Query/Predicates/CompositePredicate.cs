using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Query.Predicates.Operators;

namespace Infrastructure.Query.Predicates
{
    public class CompositePredicate : IEquatable<CompositePredicate>, IPredicate
    {
        public CompositePredicate(List<IPredicate> predicates, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            Predicates = predicates;
            Operator = logicalOperator;
        }

        public List<IPredicate> Predicates { get; }

        public LogicalOperator Operator { get; }

        public bool Equals(CompositePredicate other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return new HashSet<IPredicate>(Predicates.Where(predicate => predicate is SimplePredicate))
                .SetEquals(new HashSet<IPredicate>(other.Predicates.Where(predicate => predicate is SimplePredicate)))
                && Operator == other.Operator;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CompositePredicate);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Predicates != null ? Predicates.GetHashCode() : 0) * 397) ^ (int) Operator;
            }
        }
    }
}