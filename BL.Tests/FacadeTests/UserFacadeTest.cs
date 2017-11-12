using System.Threading.Tasks;
using AutoMapper;
using BL.Tests.FacadeTests.Common;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Employers;
using BusinessLayer.Services.RegisteredUsers;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;
using Moq;
using NUnit.Framework;
using Infrastructure.UnitOfWork;

namespace BL.Tests.FacadeTests
{
    [TestFixture]
    internal class UserFacadeTest
    {
        [Test]
        public void Test_RegisterNewEmployer()
        {
            const int id = 55;
            EmployerDto expectedProductDto = new EmployerDto
            {
                Id = id,
                Name = "Alibaba",
                Email = "mail@alibaba.cn"
            };

            FacadeMockManager mockManager = new FacadeMockManager();
            var employerRepositoryMock = mockManager.ConfigureRepositoryMock<Employer>();
            var employerQueryMock = mockManager.ConfigureQueryObjectMock<EmployerDto, Employer, FilterDtoBase>(null);
            UserFacade userfacade = CreateUserFacadeFacade(null, null, employerRepositoryMock, employerQueryMock);

            int createdId = userfacade.RegisterNewUser(expectedProductDto);

            Assert.AreEqual(createdId, id);
        }

        [Test]
        public async Task Test_GetEmployer()
        {
            const int id = 55;
            EmployerDto expectedProductDto = new EmployerDto
            {
                Id = id,
                Name = "Alibaba",
                Email = "mail@alibaba.cn"
            };
            Employer expectedProduct = new Employer
            {
                Id = id,
                Name = "Alibaba",
                Email = "mail@alibaba.cn"
            };

            FacadeMockManager mockManager = new FacadeMockManager();
            var employerRepositoryMock = mockManager.ConfigureGetRepositoryMock(expectedProduct);
            var employerQueryMock = mockManager.ConfigureQueryObjectMock<EmployerDto, Employer, FilterDtoBase>(null);
            UserFacade userfacade = CreateUserFacadeFacade(null, null, employerRepositoryMock, employerQueryMock);

            EmployerDto foundEmployer = await userfacade.GetEmployer(id);

            Assert.AreEqual(foundEmployer, expectedProductDto);
        }

        private static UserFacade CreateUserFacadeFacade(
            Mock<IRepository<RegisteredUser>> registeredUserRepositoryMock,
            Mock<QueryObjectBase<RegisteredUserDto, RegisteredUser, RegisteredUserFilterDto, IQuery<RegisteredUser>>> registeredUserQueryMock,
            Mock<IRepository<Employer>> employerRepositoryMock,
            Mock<QueryObjectBase<EmployerDto, Employer, EmployerFilterDto, IQuery<Employer>>> employerQueryMock)
        {
            Mock<IUnitOfWorkProvider> uowMock = FacadeMockManager.ConfigureUowMock();
            IMapper mapperMock = FacadeMockManager.ConfigureRealMapper();

            RegisteredUserService userService = (registeredUserRepositoryMock == null || registeredUserQueryMock == null)
                ? null
                : new RegisteredUserService(mapperMock, registeredUserRepositoryMock.Object, registeredUserQueryMock.Object);
            EmployerService employerService = (employerRepositoryMock == null || employerQueryMock == null)
                ? null
                : new EmployerService(mapperMock, employerRepositoryMock.Object, employerQueryMock.Object);

            UserFacade productFacade = new UserFacade(uowMock.Object, userService, employerService);
            return productFacade;
        }
    }
}
