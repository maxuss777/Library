using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Dependencies;
using Library.API.Business;
using Library.API.Business.Abstract;
using Ninject;
using Ninject.Modules;
using Ninject.Syntax;

namespace Library.API.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookServices>().To<BookServices>();

        }
    }
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel kernel;
        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }
        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot resolver;
        internal NinjectDependencyScope(IResolutionRoot resolver)
        {
            Contract.Assert(resolver != null);

            this.resolver = resolver;
        }
        public void Dispose()
        {
            var disposable = resolver as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }

            resolver = null;
        }
        public object GetService(Type serviceType)
        {
            if (resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed");
            }

            return resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (this.resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed");
            }

            return resolver.GetAll(serviceType);
        }
     }
}