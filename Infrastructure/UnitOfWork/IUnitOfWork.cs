using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitSync();
    }
}
