
namespace DataAccessLayer.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public enum Country
    {
        Unknown = 0,
        Austria,
        CzechRepublic,
        Germany,
        Japan,
        Slovakia,
        Switzerland,
        UnitedKingdom,
        USA
    }
}
