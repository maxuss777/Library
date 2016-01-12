namespace Library.API
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Http;
    using Library.API.App_Start;
    using Library.API.Filters;
    using Library.API.Infrastructure;
    using Ninject;
    using Ninject.Modules;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthenticationHandler());

            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));

            NinjectModule registrations = new NinjectRegistrations();
            StandardKernel kernel = new StandardKernel(registrations);
            NinjectDependencyResolver ninjectResolver = new NinjectDependencyResolver(kernel);

            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver;
        }
    }
}