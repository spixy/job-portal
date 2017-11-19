using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using PresentationLayer.App_Start.Windsor;

namespace PresentationLayer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container = new WindsorContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void BootstrapContainer()
        {
            container.Install(new WebApiInstaller());
            container.Install(new BusinessLayer.Config.DiInstaller());

            // TODO: nehce to najst GlobalConfiguration
            /*System.Web.Http.GlobalConfiguration.Configuration.Services
                .Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));*/
        }

        public override void Dispose()
        {
            container.Dispose();
            base.Dispose();
        }
    }
}
