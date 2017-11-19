using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace DAL.Tests.QueryTests
{
    [TestFixture]
    public class RegisteredUserPredicates
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        [Test]
        public async Task Test_SimplePredicate()
        {
            QueryResult<RegisteredUser> actualQueryResult;
            var query = Initializer.Container.Resolve<IQuery<RegisteredUser>>();

            var expectedQueryResult = new QueryResult<RegisteredUser>(new List<RegisteredUser>
            {
                new RegisteredUser {Email = "google@sasa.com", Name = "Sasa", Id = 1},
            }, 1, 10);

            var predicate = new SimplePredicate(nameof(UserBase.Email), ValueComparingOperator.Equal, "google@sasa.com");
            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await query.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }

        [Test]
        public async Task Test_CompositePredicate()
        {
            QueryResult<RegisteredUser> actualQueryResult;
            var query = Initializer.Container.Resolve<IQuery<RegisteredUser>>();

            var expectedQueryResult = new QueryResult<RegisteredUser>(new List<RegisteredUser>
            {
                new RegisteredUser {Email = "gg@sasa.com", Name = "Sasa", Id = 1},
                new RegisteredUser {Email = "ba@ba.com", Name = "Baba", Id = 2},
            }, 1, 10);

            var predicate = new CompositePredicate(new List<IPredicate>
            {
                new SimplePredicate(nameof(UserBase.Email), ValueComparingOperator.Equal, "ba@ba.com"),
                new SimplePredicate(nameof(UserBase.Name), ValueComparingOperator.Equal, "Baba")
            }, LogicalOperator.AND);

            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await query.Where(predicate).ExecuteAsync();
            }

            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }
    }
}
