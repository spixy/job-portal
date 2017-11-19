using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using PresentationLayer.App_Start;
using PresentationLayer.App_Start.Windsor;

namespace PresentationLayer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly IWindsorContainer container = new WindsorContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            container.Install(new WebApiInstaller());
            container.Install(new BusinessLayer.Config.DiInstaller());

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
        }

        public override void Dispose()
        {
            container.Dispose();
            base.Dispose();
        }
    }
}
