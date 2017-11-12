using System;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
        Task CommitAsync();
    }
}
