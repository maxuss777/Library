using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Library.UI.App_Start;
using Library.UI.Infrastructure;

namespace Library.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(new GlobalFilterCollection());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

        }
    }
}
