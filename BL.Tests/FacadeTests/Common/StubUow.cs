using System.Threading.Tasks;
using Infrastructure.UnitOfWork;

namespace BL.Tests.FacadeTests.Common
{
    internal class StubUow : IUnitOfWork
    {
        public void SaveChanges()
        {
        }

        public Task SaveChangesAsync()
        {
            return Task.Delay(15);
        }

        public void Dispose()
        {
        }
    }
}
