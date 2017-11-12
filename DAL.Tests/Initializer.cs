using System.Data.Entity;
using Castle.Windsor;
using DataAccessLayer.Contexts;
using NUnit.Framework;

namespace DAL.Tests
{
    [SetUpFixture]
    internal class Initializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        [OneTimeSetUp]
        public void InitializeBusinessLayerTests()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<JobPortalDbContext>());
            Container.Install(new Config.DiInstaller());
        }
    }
}
