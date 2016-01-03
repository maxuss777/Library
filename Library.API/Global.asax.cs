using System.Web;
using System.Web.Http;
using Library.API.App_Start;
using Library.API.Filters;
using Library.API.Infrastructure;
using Ninject;
using Ninject.Modules;

namespace Library.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new BasicAuthenticationMessageHandler()
);

            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            var ninjectResolver = new NinjectDependencyResolver(kernel);

            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver;
        }
    }
}
