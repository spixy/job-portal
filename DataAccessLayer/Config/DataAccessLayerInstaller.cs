using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataAccessLayer.Contexts;
using DataAccessLayer.Repositories;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace DataAccessLayer.Config
{
    public class DataAccessLayerInstaller : IWindsorInstaller
    {
        internal const string ConnectionString = "Data source=(localdb)\\mssqllocaldb;Database=my_db;Trusted_Connection=True;MultipleActiveResultSets=true";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(() => new JobPortalDbContext())
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EfUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For<IJobOfferRepository>()
                    .ImplementedBy<JobOfferRepository>()
                    .LifestyleTransient(),
                Component.For<ISkillRepository>()
                    .ImplementedBy<SkillRepository>()
                    .LifestyleTransient(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EfRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EfQuery<>))
                    .LifestyleTransient()
            );
        }
    }
}
