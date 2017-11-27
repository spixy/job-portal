using System.Web.Http;
using BusinessLayer.Config;
using Castle.Windsor;
using WebAPI;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container = new WindsorContainer();
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            container.Install(new DiInstaller());
            container.Install(new WebApiInstaller());

            GlobalConfiguration.Configuration.DependencyResolver = new DependencyResolver(container.Kernel);
        }

        public override void Dispose()
        {
            container.Dispose();
            base.Dispose();
        }
    }

}
