using DataAccessLayer.Contexts;
using Infrastructure;

namespace DataAccessLayer
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        public readonly JobPortalDbContext Context;

        public EntityFrameworkUnitOfWork()
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
