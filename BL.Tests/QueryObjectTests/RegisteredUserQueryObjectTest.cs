using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects;
using DataAccessLayer.Entities;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using NUnit.Framework;

namespace BL.Tests.QueryObjectTests
{
    [TestFixture]
    internal class RegisteredUserQueryObjectTest
    {
        [Test]
        public async Task Test_FindRegisteredUserWithSkill()
        {
            Skill cSharp = new Skill { Name = "C#" };

            var mockManager = new QueryMockManager();
            var expectedPredicate = new SimplePredicate(nameof(JobCandidate.Skills), ValueComparingOperator.EnumerableContains, cSharp);
            var mapperMock = mockManager.ConfigureMapperMock<RegisteredUser, RegisteredUserDto, RegisteredUserFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<RegisteredUser>();
            var productQueryObject = new RegisteredUserQueryObject(mapperMock.Object, queryMock.Object);

            var unused = await productQueryObject.ExecuteQuery(new RegisteredUserFilterDto { Skills = { cSharp }});

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }
    }
}
