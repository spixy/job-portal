
namespace DataAccessLayer.Infrastructure
{
    internal interface IUnitOfWorkProvider<out T> where T : IUnitOfWork, new()
    {
        T Create();
        T GetUnitOfWorkInstance();
    }
}
