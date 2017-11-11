using Castle.Windsor;
using NUnit.Framework;

namespace DAL.Tests
{
    [SetUpFixture]
    public class Initializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [OneTimeSetUp]
        public void InitializeBusinessLayerTests()
        {
        }
    }
}
