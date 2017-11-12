using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
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

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
