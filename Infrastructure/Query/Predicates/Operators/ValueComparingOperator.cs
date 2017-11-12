
namespace Infrastructure.Query.Predicates.Operators
{
    public enum ValueComparingOperator
    {
        None,

        // All
        Equal,
        NotEqual,

        // Numbers
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,

        // Strings
        StringContains
    }
}
