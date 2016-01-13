using Library.UI.Interfaces;

namespace Library.UI.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Library.UI.Services;
    using Ninject;

    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKarnel;

        public NinjectControllerFactory()
        {
            ninjectKarnel = new StandardKernel();
            
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController) ninjectKarnel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKarnel.Bind<ICategoryService>().To<CategoryService>();
            ninjectKarnel.Bind<IBookService>().To<BookService>();
            ninjectKarnel.Bind<IAuthenticationService>().To<AuthenticationService>();
            ninjectKarnel.Bind<IReportService>().To<ReportService>();
        }
    }
}