using System.Threading.Tasks;
using Infrastructure.UnitOfWork;

namespace BL.Tests.FacadeTests.Common
{
    internal class StubUow : IUnitOfWork
    {
        public void Commit()
        {
        }

        public Task CommitAsync()
        {
            return Task.Delay(15);
        }

        public void Dispose()
        {
        }
    }
}
