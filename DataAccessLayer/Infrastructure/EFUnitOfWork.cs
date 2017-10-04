using DataAccessLayer.Infrastructure;

namespace DataAccessLayer.Contexts
{
    internal class EFUnitOfWork : IUnitOfWork
    {
        protected readonly JobPortalDbContext Context;

        public EFUnitOfWork()
        {
            Context = new JobPortalDbContext();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }
    }
}
