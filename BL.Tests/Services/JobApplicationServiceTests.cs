using AutoMapper;
using BL.Tests.FacadeTests.Common;
using BL.Tests.Services.Common;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.JobApplications;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.Query;
using Moq;
using NUnit.Framework;

namespace BL.Tests.Services
{
    [TestFixture]
    internal class JobApplicationServiceTests
    {
        [Test]
        public void Test_Create()
        {
            const int id = 45;
            JobApplicationDto expectedDto = new JobApplicationDto
            {
                Id = id
            };
            JobApplication expectedApplication = new JobApplication
            {
                Id = id
            };

            ServiceMockManager mockManager = new ServiceMockManager();
            var repositoryMock = mockManager.ConfigureGetRepositoryMock(expectedApplication);
            var queryMock = mockManager.ConfigureQueryObjectMock<JobApplicationDto, JobApplication, JobApplicationFilterDto>(null);
            JobApplicationService jobApplicationService = CreateJobApplicationService(repositoryMock, queryMock);

            int createdId = jobApplicationService.Create(expectedDto);

            Assert.AreEqual(createdId, id);
        }

        [Test]
        public void Test_AcceptApplication()
        {
            /*const int id = 45;
            JobApplicationDto expectedDto = new JobApplicationDto
            {
                Id = id
            };

            ServiceMockManager mockManager = new ServiceMockManager();
            var repositoryMock = mockManager.ConfigureRepositoryMock<JobApplication>();
            var queryMock = mockManager.ConfigureQueryObjectMock<JobApplicationDto, JobApplication, JobApplicationFilterDto>(null);
            JobApplicationService jobApplicationService = CreateJobApplicationService(repositoryMock, queryMock);

            int createdId = jobApplicationService.Create(expectedDto);
            int createdId = jobApplicationService.AcceptApplication(expectedDto);

            Assert.AreEqual(createdId, id);*/
        }


        private static JobApplicationService CreateJobApplicationService(
            Mock<IRepository<JobApplication>> repositoryMock,
            Mock<QueryObjectBase<JobApplicationDto, JobApplication, JobApplicationFilterDto, IQuery<JobApplication>>> queryMock)
        {
            IMapper mapperMock = FacadeMockManager.ConfigureRealMapper();

            JobApplicationService jobApplicationService = new JobApplicationService(mapperMock, repositoryMock.Object, queryMock.Object);
            return jobApplicationService;
        }
    }
}
