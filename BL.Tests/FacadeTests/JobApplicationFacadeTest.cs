﻿using NUnit.Framework;

namespace BL.Tests.FacadeTests
{
    [TestFixture]
    internal class JobApplicationFacadeTest
    {
        //[Test]
        //public void Test_Get()
        //{
        //    const int id = 64;
        //    JobOfferDto expectedEmployerDto = new JobOfferDto
        //    {
        //        Id = id,
        //        Name = "Software Engineer",
        //        Description = "Work hard, play hard."
        //    };
        //    JobOffer expectedEmployer = new JobOffer
        //    {
        //        Id = id,
        //        Name = "Software Engineer",
        //        Description = "Work hard, play hard."
        //    };

        //    FacadeMockManager mockManager = new FacadeMockManager();
        //    var repositoryMock = mockManager.ConfigureGetRepositoryMock(expectedEmployer);
        //    var queryMock = mockManager.ConfigureQueryObjectMock<JobOfferDto, JobOffer, JobOfferFilterDto>(null);
        //    JobOfferFacade userfacade = CreateJobOfferFacade(repositoryMock, queryMock);

        //    int foundEmployer = userfacade.Create(expectedEmployerDto);

        //    Assert.AreEqual(foundEmployer, expectedEmployerDto.Id);
        //}

        //private static JobOfferFacade CreateJobOfferFacade(
        //    Mock<IRepository<JobOffer>> employerRepositoryMock,
        //    Mock<QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>>> employerQueryMock)
        //{
        //    Mock<IUnitOfWorkProvider> uowMock = FacadeMockManager.ConfigureUowMock();
        //    IMapper mapperMock = FacadeMockManager.ConfigureRealMapper();
        //    JobOfferService employerService = new JobOfferService(mapperMock, employerRepositoryMock.Object, employerQueryMock.Object);
        //    QuestionService questionService = new QuestionService(mapperMock, null, null);
        //    JobOfferFacade productFacade = new JobOfferFacade(uowMock.Object, employerService, questionService);
        //    return productFacade;
        //}
    }
}
