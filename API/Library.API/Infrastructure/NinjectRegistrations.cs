namespace Library.API.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;
    using Library.API.Business;
    using Library.API.Business.Interfaces;
    using Library.API.DataAccess;
    using Library.API.DataAccess.Interfaces;
    using Ninject;
    using Ninject.Modules;
    using Ninject.Syntax;

    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookService>().To<BookService>();
            Bind<IBookRepository>().To<BookRepository>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<ICategoryRepository>().To<CategoryRepository>();
            Bind<IMemberRepository>().To<MemberRepository>();
            Bind<IReportService>().To<ReportService>();
        }
    }

    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel) : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }
    }

    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot _resolver;

        internal NinjectDependencyScope(IResolutionRoot resolver)
        {
            _resolver = resolver;
        }

        public void Dispose()
        {
            IDisposable disposable = _resolver as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }

            _resolver = null;
        }

        public object GetService(Type serviceType)
        {
            if (_resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed");
            }

            return _resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed");
            }

            return _resolver.GetAll(serviceType);
        }
    }
}