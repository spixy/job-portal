using System;
using System.Data.Entity;
using System.Threading;

namespace Infrastructure.UnitOfWork
{
    public class EfUnitOfWorkProvider : IUnitOfWorkProvider, IDisposable
    {
        protected readonly AsyncLocal<IUnitOfWork> UowLocalInstance = new AsyncLocal<IUnitOfWork>();
        protected readonly Func<DbContext> DbContextFactory;

        public EfUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this.DbContextFactory = dbContextFactory;
        }

        public IUnitOfWork Create()
        {
            UowLocalInstance.Value = new EfUnitOfWork(this.DbContextFactory);
            return UowLocalInstance.Value;
        }

        public IUnitOfWork GetUnitOfWorkInstance()
        {
            return UowLocalInstance != null ? UowLocalInstance.Value : throw new InvalidOperationException("UoW not created");
        }

        public void Dispose()
        {
            UowLocalInstance.Value?.Dispose();
            UowLocalInstance.Value = null;
        }
    }
}
