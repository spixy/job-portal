using System;

namespace DataAccessLayer.Infrastructure
{
    internal interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
