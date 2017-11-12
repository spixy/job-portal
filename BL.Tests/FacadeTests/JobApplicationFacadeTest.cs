using System.Threading.Tasks;
using AutoMapper;
using BL.Tests.FacadeTests.Common;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Facades;
using BusinessLayer.Facades.Employers;
using BusinessLayer.Facades.JobApplication;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Employers;
using BusinessLayer.Services.JobApplications;
using BusinessLayer.Services.JobOffers;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;
using Moq;
using NUnit.Framework;
using Infrastructure.UnitOfWork;

namespace BL.Tests.FacadeTests
{
    [TestFixture]
    internal class JobApplicationFacadeTest
    {
        [Test]
        public void Test_Create()
        {
            const int id = 64;
            JobOfferDto expectedEmployerDto = new JobOfferDto
            {
                Id = id,
                Name = "SuperJob",
                Description = "bla"
            };
            JobOffer expectedEmployer = new JobOffer
            {
                Id = id,
                Name = "SuperJob"
            };

            FacadeMockManager mockManager = new FacadeMockManager();
            var repositoryMock = mockManager.ConfigureGetRepositoryMock(expectedEmployer);
            var queryMock = mockManager.ConfigureQueryObjectMock<JobOfferDto, JobOffer, JobOfferFilterDto>(null);
            JobOfferFacade userfacade = CreateJobOfferFacade(repositoryMock, queryMock);

            int foundEmployer = userfacade.Create(expectedEmployerDto);

            Assert.AreEqual(foundEmployer, expectedEmployerDto);
        }

        private static JobOfferFacade CreateJobOfferFacade(
            Mock<IRepository<JobOffer>> employerRepositoryMock,
            Mock<QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>>> employerQueryMock)
        {
            Mock<IUnitOfWorkProvider> uowMock = FacadeMockManager.ConfigureUowMock();
            IMapper mapperMock = FacadeMockManager.ConfigureRealMapper();
            JobOfferService employerService = new JobOfferService(mapperMock, employerRepositoryMock.Object, employerQueryMock.Object);
            JobOfferFacade productFacade = new JobOfferFacade(uowMock.Object, employerService);
            return productFacade;
        }
    }
}
