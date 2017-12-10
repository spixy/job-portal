using AutoMapper;
using BL.Tests.FacadeTests.Common;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.RegisteredUsers;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;
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
            /*const int id = 33;
            RegisteredUserDto expectedDto = new RegisteredUserDto
            {
                Id = id,
	            Username = "La",
	            Password = "*****"
			};
            RegisteredUser expectedUser = new RegisteredUser
            {
                Id = id,
				Username = "La",
				Password = "*****"
            };

            ServiceMockManager mockManager = new ServiceMockManager();
            var repositoryMock = mockManager.ConfigureGetRepositoryMock(expectedUser);
            var queryMock = mockManager.ConfigureQueryObjectMock<RegisteredUserDto, RegisteredUser, RegisteredUserFilterDto>(null);
            RegisteredUserService jobApplicationService = CreateRegisteredUserService(repositoryMock, queryMock);

            var createdId = jobApplicationService.Create(expectedDto);
            
            Assert.AreEqual(createdId?.Id, id);*/
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
