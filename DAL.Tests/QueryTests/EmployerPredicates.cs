using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace DAL.Tests.QueryTests
{
    [TestFixture]
    public class EmployerPredicates
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        [Test]
        public async Task Test_SimplePredicate()
        {
            var query = Initializer.Container.Resolve<IQuery<Employer>>();
            // TODO
        }

        [Test]
        public async Task Test_CompositePredicate()
        {
            var query = Initializer.Container.Resolve<IQuery<Employer>>();
            // TODO
        }
    }
}
