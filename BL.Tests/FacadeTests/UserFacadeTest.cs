using System.Threading.Tasks;
using AutoMapper;
using BL.Tests.FacadeTests.Common;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades.Employers;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Employers;
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
            EmployerDto expectedEmployerDto = new EmployerDto
            {
                Id = id,
                Name = "Alibaba",
                Email = "mail@alibaba.cn"
            };

            FacadeMockManager mockManager = new FacadeMockManager();
            var employerRepositoryMock = mockManager.ConfigureRepositoryMock<Employer>();
            var employerQueryMock = mockManager.ConfigureQueryObjectMock<EmployerDto, Employer, EmployerFilterDto>(null);
            EmployerFacade userfacade = CreateEmployerFacade(employerRepositoryMock, employerQueryMock);

            int createdId = userfacade.Create(expectedEmployerDto);

            Assert.AreEqual(createdId, id);
        }

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
            const int id = 54;
            const string name = "Alibaba";
            EmployerDto expectedEmployerDto = new EmployerDto
            {
                Id = id,
                Name = name,
                Email = "mail@alibaba.cn"
            };
            Employer expectedEmployer = new Employer
            {
                Id = id,
                Name = name,
                Email = "mail@alibaba.cn"
            };

            FacadeMockManager mockManager = new FacadeMockManager();
            var employerRepositoryMock = mockManager.ConfigureGetRepositoryMock(expectedEmployer);
            var employerQueryMock = mockManager.ConfigureQueryObjectMock<EmployerDto, Employer, EmployerFilterDto>(null);
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
            EmployerFacade productFacade = new EmployerFacade(uowMock.Object, employerService);
            return productFacade;
        }
    }
}
