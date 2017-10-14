using System;

namespace DataAccessLayer.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
