using AutoMapper;
using BusinessLayer.Facades;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
namespace BusinessLayer.Config
{
    public class DiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Choose prefered DAL
            new DataAccessLayer.Config.DiInstaller().Install(container, store);

            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn(typeof(QueryObjectBase<,,,>))
                    .WithServiceBase()
                    .LifestyleTransient(),

                Classes.FromThisAssembly()
                    .BasedOn<ServiceBase>()
                    .WithServiceDefaultInterfaces()
                    .LifestyleTransient(),

                Classes.FromThisAssembly()
                    .BasedOn<FacadeBase>()
                    .LifestyleTransient(),

                Component.For<IMapper>()
                    .Instance(new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping)))
                    .LifestyleSingleton()
            );

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
        }
    }
}
