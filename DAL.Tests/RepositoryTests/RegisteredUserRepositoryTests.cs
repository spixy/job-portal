using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Infrastructure;
using Infrastructure.UnitOfWork;
using NUnit.Framework;
using static DAL.Tests.Config.DiInstaller;

namespace DAL.Tests.RepositoryTests
{
    [TestFixture]
    public class RegisteredUserRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        private readonly IRepository<RegisteredUser> userRepository = Initializer.Container.Resolve<IRepository<RegisteredUser>>();


        [Test]
        public async Task GetUserAsync_AlreadyStoredInDBNoIncludes_ReturnsCorrectUser()
        {
            // Arrange
            RegisteredUser foundUser;

            // Act
            using (unitOfWorkProvider.Create())
            {
                foundUser = await userRepository.GetAsync(David.Id);
            }

            // Assert
            Assert.AreEqual(foundUser.Id, David.Id);
        }

        [Test]
        public async Task CreateUserAsync_UserIsNotPreviouslySeeded_CreatesNewUser()
        {
            var viktor = new RegisteredUser
            {
                Name = "Viktor",
                Email = "viktor@viktor.sk",
                Education = Education.HighSchool,
                Skills = new List<Skill> { DotNet, CSharp, Html }
            };

            using (var uow = unitOfWorkProvider.Create())
            {
                userRepository.Create(viktor);
                await uow.CommitAsync();
            }

            Assert.IsFalse(viktor.Id.Equals(0));
        }

        [Test]
        public async Task UpdateUserAsync_UserIsPreviouslySeeded_UpdatesUser()
        {
            RegisteredUser updatedUser;
            RegisteredUser newUser;

            using (var uow = unitOfWorkProvider.Create())
            {
                newUser = new RegisteredUser
                {
                    Id = David.Id,
                    Name = "Jozko",
                    Email = "jozko@jozko.sk"
                };

                userRepository.Update(newUser);
                await uow.CommitAsync();
                updatedUser = await userRepository.GetAsync(David.Id);
            }

            Assert.IsTrue(newUser.Name.Equals(updatedUser.Name));
            Assert.IsTrue(newUser.Email.Equals(updatedUser.Email));
        }

        [Test]
        public async Task DeleteUserAsync_UserIsPreviouslySeeded_DeletesUser()
        {
            RegisteredUser deletedUser;

            using (var uow = unitOfWorkProvider.Create())
            {
                userRepository.Delete(David.Id);
                await uow.CommitAsync();
                deletedUser = await userRepository.GetAsync(David.Id);
            }

            Assert.Null(deletedUser);
        }
    }
}
