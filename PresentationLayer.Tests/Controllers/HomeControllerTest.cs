using System.Web.Mvc;
using NUnit.Framework;
using PresentationLayer.Controllers;

namespace PresentationLayer.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public async void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
