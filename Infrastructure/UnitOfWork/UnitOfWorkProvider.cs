
namespace Infrastructure
{
    public class UnitOfWorkProvider<T> : IUnitOfWorkProvider where T : IUnitOfWork, new()
    {
        // TODO: v buducnosti pripadne pridat AsyncLocal
        protected IUnitOfWork localUowInstance;

        public IUnitOfWork Create()
        {
            localUowInstance = new T();
            return localUowInstance;
        }

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            return localUowInstance;
        }
    }
}
