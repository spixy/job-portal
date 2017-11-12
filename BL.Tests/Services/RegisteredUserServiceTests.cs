using AutoMapper;
using BL.Tests.FacadeTests.Common;
using BL.Tests.Services.Common;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.RegisteredUsers;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;
using Moq;
using NUnit.Framework;

namespace BL.Tests.Services
{
    [TestFixture]
    internal class RegisteredUserServiceTests
    {
        [Test]
        public void Test_RegisterNewEmployer()
        {
            const int id = 33;
            RegisteredUserDto expectedDto = new RegisteredUserDto
            {
                Id = id
            };

            ServiceMockManager mockManager = new ServiceMockManager();
            var repositoryMock = mockManager.ConfigureRepositoryMock<RegisteredUser>();
            var queryMock = mockManager.ConfigureQueryObjectMock<RegisteredUserDto, RegisteredUser, RegisteredUserFilterDto>(null);
            RegisteredUserService jobApplicationService = CreateRegisteredUserService(repositoryMock, queryMock);

            int createdId = jobApplicationService.Create(expectedDto);

            Assert.AreEqual(createdId, id);
        }

        private static RegisteredUserService CreateRegisteredUserService(
            Mock<IRepository<RegisteredUser>> repositoryMock,
            Mock<QueryObjectBase<RegisteredUserDto, RegisteredUser, RegisteredUserFilterDto, IQuery<RegisteredUser>>> queryMock)
        {
            IMapper mapperMock = FacadeMockManager.ConfigureRealMapper();

            RegisteredUserService jobApplicationService = new RegisteredUserService(mapperMock, repositoryMock.Object, queryMock.Object);
            return jobApplicationService;
        }
    }
}
