using System;
using System.Data.Entity;
using System.Threading;
using DataAccessLayer;

namespace Infrastructure
{
    public class UnitOfWorkProvider : IUnitOfWorkProvider, IDisposable
    {
        protected readonly AsyncLocal<IUnitOfWork> UowLocalInstance = new AsyncLocal<IUnitOfWork>();
        protected readonly Func<DbContext> DbContextFactory;

        public UnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this.DbContextFactory = dbContextFactory;
        }

        public IUnitOfWork Create()
        {
            UowLocalInstance.Value = new EntityFrameworkUnitOfWork(this.DbContextFactory);
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
