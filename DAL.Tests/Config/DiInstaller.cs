using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataAccessLayer.Repositories;
using Infrastructure;
using Infrastructure.Query;

namespace DAL.Tests.Config
{
    public class DiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<IUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityFrameworkRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(QueryBase<>)) // TODO: implementovat QueryBase
                    .LifestyleTransient()
            );
        }

        private static DbContext InitializeDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
