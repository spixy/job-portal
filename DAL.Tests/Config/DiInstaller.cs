using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using DataAccessLayer.Repositories;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace DAL.Tests.Config
{
    public class DiInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "InMemoryTestDBDemoEshop";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EfUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EfRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EfQuery<>))
                    .LifestyleTransient()
            );
        }

        private DbContext InitializeDatabase()
        {
            var context = new JobPortalDbContext(new LocalDbConnectionFactory("mssqllocaldb").CreateConnection(TestDbConnectionString));
            context.RegisteredUsers.RemoveRange(context.RegisteredUsers);
            context.SaveChanges();

            RegisteredUser createdUser = new RegisteredUser { Id = 42, Education = Education.HighSchool, Email = "hu@hu.hu", Name = "Hugo" };
            context.RegisteredUsers.AddOrUpdate(createdUser);

            context.SaveChanges();
            return context;
        }
    }
}
