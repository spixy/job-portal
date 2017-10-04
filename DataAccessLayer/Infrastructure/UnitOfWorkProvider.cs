
namespace DataAccessLayer.Infrastructure
{
    internal class EFUnitOfWorkProvider<T> : IUnitOfWorkProvider<T> where T : IUnitOfWork, new()
    {
        protected T localUowInstance;

        public T Create()
        {
            localUowInstance = new T();
            return localUowInstance;
        }

        public T GetUnitOfWorkInstance()
        {
            return localUowInstance;
        }
    }
}
