using DataAccessLayer.Contexts;

namespace DataAccessLayer.Infrastructure.EntityFramework
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
