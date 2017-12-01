using System.Threading.Tasks;
using AutoMapper;
using BL.Tests.FacadeTests.Common;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Employers;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Auth;
using BusinessLayer.Services.Employers;
using DataAccessLayer.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;
using Moq;
using NUnit.Framework;
using Infrastructure.UnitOfWork;

namespace BL.Tests.FacadeTests
{
    [TestFixture]
    internal class RegisteredUserFacadeTest
    {
        [Test]
        public async Task Test_GetById()
        {
            const int id = 55;
            EmployerDto expectedEmployerDto = new EmployerDto
            {
                Id = id,
                Name = "Alibaba",
                Email = "mail@alibaba.cn"
            };
            Employer expectedEmployer = new Employer
            {
                Id = id,
                Name = "Alibaba",
                Email = "mail@alibaba.cn"
            };

            FacadeMockManager mockManager = new FacadeMockManager();
            var employerRepositoryMock = mockManager.ConfigureGetRepositoryMock(expectedEmployer);
            var employerQueryMock = mockManager.ConfigureQueryObjectMock<EmployerDto, Employer, EmployerFilterDto>(null);
            EmployerFacade userfacade = CreateEmployerFacade(employerRepositoryMock, employerQueryMock);

            EmployerDto foundEmployer = await userfacade.Get(id);

            Assert.AreEqual(foundEmployer, expectedEmployerDto);
        }

        [Test]
        public async Task Test_GetByName()
        {
            const int id = 1337;
            const string name = "Amazon";
            EmployerDto expectedEmployerDto = new EmployerDto
            {
                Id = id,
                Name = name,
                Email = "mail@amazon.com"
            };
            Employer expectedEmployer = new Employer
            {
                Id = id,
                Name = name,
                Email = "mail@amazon.com"
            };
            var queryResult = new QueryResultDto<EmployerDto, EmployerFilterDto>()
            {
                Filter = new EmployerFilterDto {Name = name},
                Items = new[] {expectedEmployerDto},
                TotalItemsCount = 1
            };

            FacadeMockManager mockManager = new FacadeMockManager();
            var employerRepositoryMock = mockManager.ConfigureGetRepositoryMock(expectedEmployer);
            var employerQueryMock = mockManager.ConfigureQueryObjectMock<EmployerDto, Employer, EmployerFilterDto>(queryResult);
            EmployerFacade userfacade = CreateEmployerFacade(employerRepositoryMock, employerQueryMock);

            EmployerDto foundEmployer = await userfacade.GetByName(name);

            Assert.AreEqual(foundEmployer, expectedEmployerDto);
        }

        private static EmployerFacade CreateEmployerFacade(
            Mock<IRepository<Employer>> employerRepositoryMock,
            Mock<QueryObjectBase<EmployerDto, Employer, EmployerFilterDto, IQuery<Employer>>> employerQueryMock)
        {
            Mock<IUnitOfWorkProvider> uowMock = FacadeMockManager.ConfigureUowMock();
            IMapper mapperMock = FacadeMockManager.ConfigureRealMapper();
            EmployerService employerService = new EmployerService(mapperMock, employerRepositoryMock.Object, employerQueryMock.Object);
	        UserService userService = new UserService(mapperMock, null);
			EmployerFacade productFacade = new EmployerFacade(uowMock.Object, employerService, userService);
            return productFacade;
        }
    }
}
