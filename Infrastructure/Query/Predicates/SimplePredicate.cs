using System;
using Infrastructure.Query.Predicates.Operators;

namespace Infrastructure.Query.Predicates
{
    public class SimplePredicate : IEquatable<SimplePredicate>, IPredicate
    {
        public string TargetPropertyName { get; }

        public object ComparedValue { get; }

        public ValueComparingOperator ValueComparingOperator { get; }


        public SimplePredicate(string targetPropertyName, ValueComparingOperator valueComparingOperator, object comparedValue)
        {
            if (valueComparingOperator == ValueComparingOperator.None)
            {
                throw new ArgumentException("Simple predicate must use some sort of valueComparingOperator");
            }
            TargetPropertyName = !string.IsNullOrWhiteSpace(targetPropertyName) ? targetPropertyName : throw new ArgumentException("Target property name must be defined!");
            ValueComparingOperator = valueComparingOperator;
            ComparedValue = comparedValue;
        }

        public bool Equals(SimplePredicate other)
        {
            return string.Equals(TargetPropertyName, other.TargetPropertyName) && Equals(ComparedValue, other.ComparedValue) && ValueComparingOperator == other.ValueComparingOperator;
        }
    }
}
