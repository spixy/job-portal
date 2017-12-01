using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
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
                new RegisteredUser
                {
                    Name = "David",
                    Email = "david@david.sk",
                    Education = Education.BachelorDegree,
                    Skills = new List<Skill> { new Skill { Name = "UNIX" } },
					Username = "David",
					Password = "****"
                }
            }, 1, 10, 1);

            SimplePredicate predicate = new SimplePredicate(nameof(UserBase.Email), ValueComparingOperator.Equal, "david@david.sk");
            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await query.Where(predicate).Page(1, 10).ExecuteAsync();
            }

            Assert.AreEqual(actualQueryResult.PageSize, expectedQueryResult.PageSize);
            Assert.AreEqual(actualQueryResult.RequestedPageNumber, expectedQueryResult.RequestedPageNumber);
            Assert.AreEqual(actualQueryResult.TotalItemsCount, expectedQueryResult.TotalItemsCount);
            Assert.AreEqual(actualQueryResult.Items.First().Email, expectedQueryResult.Items.First().Email);
        }

        [Test]
        public async Task Test_CompositePredicate()
        {
            QueryResult<RegisteredUser> actualQueryResult;
            var query = Initializer.Container.Resolve<IQuery<RegisteredUser>>();

            var expectedQueryResult = new QueryResult<RegisteredUser>(new List<RegisteredUser>
            {
                new RegisteredUser
                {
                    Name = "Lubos",
                    Email = "lubos@lubos.sk",
                    Education = Education.DoctoralDegree,
                    Skills = new List<Skill> { new Skill { Name = "HTML" } },
                    JobApplications = new List<JobApplication>(),
	                Username = "Lubos",
	                Password = "*****"
				}
            }, 1, 10, 1);

            CompositePredicate predicate = new CompositePredicate(new List<IPredicate>
            {
                new SimplePredicate(nameof(UserBase.Email), ValueComparingOperator.Equal, "lubos@lubos.sk"),
                new SimplePredicate(nameof(UserBase.Name), ValueComparingOperator.Equal, "Lubos")
            }, LogicalOperator.AND);

            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await query.Where(predicate).Page(1, 10).ExecuteAsync();
            }

            Assert.AreEqual(actualQueryResult.PageSize, expectedQueryResult.PageSize);
            Assert.AreEqual(actualQueryResult.RequestedPageNumber, expectedQueryResult.RequestedPageNumber);
            Assert.AreEqual(actualQueryResult.TotalItemsCount, expectedQueryResult.TotalItemsCount);
            Assert.AreEqual(actualQueryResult.Items.First().Email, expectedQueryResult.Items.First().Email);
        }
    }
}
