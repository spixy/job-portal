﻿using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace WebAPI
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<ApiController>()
                    .LifestylePerWebRequest()/*,
                Component.For<IMapper>()
					.OverWrite()
	                .Instance(new Mapper(new MapperConfiguration(config =>
					{
						BusinessLayer.Config.MappingConfig.ConfigureMapping(config);
						MappingConfig.ConfigureMapping(config);
	                })))
	                .LifestyleSingleton()*/
			);
        }
    }
}
