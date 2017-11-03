
namespace Infrastructure
{
    public interface IEntity
    {
        /// <summary>
        /// Unique key
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Name of the database table
        /// </summary>
        string TableName { get; }
    }
}
