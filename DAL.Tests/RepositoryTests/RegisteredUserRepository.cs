using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Infrastructure;
using Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace DAL.Tests.RepositoryTests
{
    [TestFixture]
    public class RegisteredUserRepository
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        private readonly IRepository<RegisteredUser> userRepository = Initializer.Container.Resolve<IRepository<RegisteredUser>>();

        private const int StoredUserId = 42;

        [Test]
        public async Task Test_Add()
        {
            // Arrange
            const int testId = 7;
            RegisteredUser createdUser = new RegisteredUser {Id = testId, Education = Education.HighSchool, Email = "hu@hu.hu", Name = "Hugo"};
            RegisteredUser foundUser;

            // Act
            using (var uow = unitOfWorkProvider.Create())
            {
                userRepository.Create(createdUser);
                await uow.CommitAsync();
                foundUser = await userRepository.GetAsync(testId);
            }

            // Assert
            Assert.AreEqual(foundUser, createdUser);
        }

        [Test]
        public async Task Test_GetAsync()
        {
            // Arrange
            RegisteredUser foundUser;

            // Act
            using (unitOfWorkProvider.Create())
            {
                foundUser = await userRepository.GetAsync(StoredUserId);
            }

            // Assert
            Assert.AreEqual(foundUser.Id, StoredUserId);
        }

        [Test]
        public async Task Test_Delete()
        {
            // Arrange
            RegisteredUser foundUser;

            // Act
            using (var uow = unitOfWorkProvider.Create())
            {
                userRepository.Delete(StoredUserId);
                await uow.CommitAsync();
                foundUser = await userRepository.GetAsync(StoredUserId);
            }

            // Assert
            Assert.AreEqual(foundUser, null);
        }
    }
}
