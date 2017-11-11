using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Infrastructure;

namespace DataAccessLayer
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public readonly DbContext Context;

        public EfUnitOfWork(Func<DbContext> dbContextFactory)
        {
            this.Context = dbContextFactory?.Invoke() ?? throw new ArgumentException("Db context factory cant be null!");
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public async Task CommitSync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
