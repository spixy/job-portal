using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.UnitOfWork;
using NUnit.Framework;
using static DAL.Tests.Config.DiInstaller;

namespace DAL.Tests.RepositoryTests
{
    [TestFixture]
    public class EmployerRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        private readonly IRepository<Employer> employerRepository = Initializer.Container.Resolve<IRepository<Employer>>();

        [Test]
        public async Task GetEmployerAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectEmployer()
        {
            // Arrange
            Employer employer;

            // Act
            using (unitOfWorkProvider.Create())
            {
                employer = await employerRepository.GetAsync(Google.Id);
            }

            // Assert
            Assert.NotNull(employer);
            Assert.AreEqual(employer.Id, Google.Id);
        }

        [Test]
        public async Task CreateEmployerAsync_EmployerIsNotPreviouslySeeded_CreatesNewEmployer()
        {
            var yahoo = new Employer
            {
                Name = "Yahoo! Inc.",
                Email = "yahoo@jobs.com"
            };

            using (var uow = unitOfWorkProvider.Create())
            {
                employerRepository.Create(yahoo);
                await uow.Commit();
            }

            Assert.IsFalse(yahoo.Id.Equals(0));
        }

        [Test]
        public async Task UpdateEmployerAsync_EmployerIsPreviouslySeeded_UpdatesEmployer()
        {
            Employer updatedEmployer;
            Employer newEmployer;

            using (var uow = unitOfWorkProvider.Create())
            {
                newEmployer = new Employer
                {
                    Id = Sony.Id,
                    Name = "Sony Slovakia",
                    Email = "sony-slovakia@jobs.com"
                };

                employerRepository.Update(newEmployer);
                await uow.Commit();
                updatedEmployer = await employerRepository.GetAsync(Sony.Id);
            }

            Assert.IsTrue(newEmployer.Name.Equals(updatedEmployer.Name));
            Assert.IsTrue(newEmployer.Email.Equals(updatedEmployer.Email));
        }

        [Test]
        public async Task DeleteEmployerAsync_EmployerIsPreviouslySeeded_DeletesEmployer()
        {
            Employer deletedEmployer;

            using (var uow = unitOfWorkProvider.Create())
            {
                employerRepository.Delete(Google.Id);
                await uow.Commit();
                deletedEmployer = await employerRepository.GetAsync(Google.Id);
            }

            Assert.Null(deletedEmployer);
        }
    }
}
