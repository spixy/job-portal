using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Castle.Windsor;
using PresentationLayer.Windsor;
using HttpContext = System.Web.HttpContext;

namespace PresentationLayer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly IWindsorContainer Container = new WindsorContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            Container.Install(new BusinessLayer.Config.BusinessLayerInstaller());
            Container.Install(new PresentationLayerInstaller());

            IControllerFactory controllerFactory = new WindsorControllerFactory(Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

	    protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
	    {
		    HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
		    if (authCookie != null)
		    {
			    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
			    if (authTicket != null && !authTicket.Expired)
			    {
				    var roles = authTicket.UserData.Split(','); // TODO
				    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
			    }
		    }
	    }
	}
}
