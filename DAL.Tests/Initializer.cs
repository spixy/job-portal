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

        /// <summary>
        /// Initializes all Business Layer tests
        /// </summary>
        [OneTimeSetUp]
        public void InitializeBusinessLayerTests()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Database.SetInitializer(new DropCreateDatabaseAlways<JobPortalDbContext>());
            Container.Install(new Config.DiInstaller());
        }
    }
}
