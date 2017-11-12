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
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(this.TargetPropertyName, other.TargetPropertyName) && this.ComparedValue.Equals(other.ComparedValue) && this.ValueComparingOperator == other.ValueComparingOperator;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SimplePredicate);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.TargetPropertyName.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ComparedValue.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) this.ValueComparingOperator;
                return hashCode;
            }
        }
    }
}
