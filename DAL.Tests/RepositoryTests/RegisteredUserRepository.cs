using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Infrastructure;
using Infrastructure.UnitOfWork;
using NUnit.Framework;

namespace DAL.Tests.RepositoryTests
{
    public class RegisteredUserRepository
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        private readonly IRepository<RegisteredUser> productRepository = Initializer.Container.Resolve<IRepository<RegisteredUser>>();

        [Test]
        public async Task TestAdd()
        {
            // Arrange
            const int TEST_ID = 42;
            RegisteredUser user = new RegisteredUser {Id = TEST_ID };
            RegisteredUser foundUser;

            // Act
            using (unitOfWorkProvider.Create())
            {
                productRepository.Create(user);
            }
            using (unitOfWorkProvider.Create())
            {
                foundUser = await productRepository.GetAsync(TEST_ID);
            }

            // Assert
            Assert.AreEqual(user, foundUser);
        }
    }
}
