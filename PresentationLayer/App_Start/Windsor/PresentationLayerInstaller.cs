﻿using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace PresentationLayer.Windsor
{
    public class PresentationLayerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .BasedOn<IController>()
                    .LifestylePerWebRequest()
            );

            container.Register(
                Classes.FromThisAssembly()
                    .InNamespace("PresentationLayer.Helpers")
                    .LifestyleTransient()
            );
        }
    }
}