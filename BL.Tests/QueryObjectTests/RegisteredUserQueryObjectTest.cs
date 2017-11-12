using System.Collections.Generic;
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
        public async Task SimplePredicate()
        {
            Skill cSharp = new Skill { Name = "C#" };

            QueryMockManager mockManager = new QueryMockManager();
            IPredicate expectedPredicate = new SimplePredicate(nameof(JobCandidate.Skills), ValueComparingOperator.EnumerableContains, cSharp);
            var mapperMock = mockManager.ConfigureMapperMock<RegisteredUser, RegisteredUserDto, RegisteredUserFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<RegisteredUser>();
            RegisteredUserQueryObject productQueryObject = new RegisteredUserQueryObject(mapperMock.Object, queryMock.Object);

            RegisteredUserFilterDto filterDto = new RegisteredUserFilterDto { Skills = new List<Skill> { cSharp } };
            QueryResultDto<RegisteredUserDto, RegisteredUserFilterDto> unused = await productQueryObject.ExecuteQuery(filterDto);

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }

        [Test]
        public async Task Test_CompositePredicate()
        {
            Skill cSharp = new Skill { Name = "C#" };
            Skill python = new Skill { Name = "Python" };

            QueryMockManager mockManager = new QueryMockManager();
            IPredicate expectedPredicate = new CompositePredicate(new List<IPredicate>
            {
                new SimplePredicate(nameof(JobCandidate.Skills), ValueComparingOperator.EnumerableContains, cSharp),
                new SimplePredicate(nameof(JobCandidate.Skills), ValueComparingOperator.EnumerableContains, python),
            });
            var mapperMock = mockManager.ConfigureMapperMock<RegisteredUser, RegisteredUserDto, RegisteredUserFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<RegisteredUser>();
            RegisteredUserQueryObject productQueryObject = new RegisteredUserQueryObject(mapperMock.Object, queryMock.Object);

            RegisteredUserFilterDto filterDto = new RegisteredUserFilterDto { Skills = new List<Skill> { cSharp, python } };
            QueryResultDto<RegisteredUserDto, RegisteredUserFilterDto> unused = await productQueryObject.ExecuteQuery(filterDto);

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }
    }
}
