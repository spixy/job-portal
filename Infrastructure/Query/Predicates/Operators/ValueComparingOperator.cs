
namespace Infrastructure.Query.Predicates
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
