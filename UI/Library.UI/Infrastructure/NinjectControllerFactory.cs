using Library.UI.Abstract;
using Ninject;
using System.Web.Mvc;

namespace Library.UI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKarnel;
        public NinjectControllerFactory()
        {
            ninjectKarnel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController) ninjectKarnel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKarnel.Bind<ICategoryServices>().To<CategoryServices>();
            ninjectKarnel.Bind<IBookServices>().To<BookServices>();
        }
    }
}