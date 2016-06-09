//namespace VegiJ.MVC
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Web.Http.Dependencies;
//    using System.Web.Mvc;
//    using DataAccess;
//    using DataAccess.Contracts;
//    using Logic;
//    using Ninject;
//    using Ninject.Activation;
//    using Ninject.Parameters;
//    using Ninject.Syntax;
//    using Ninject.Web.Common;

//    public class NinjectResolver : NinjectScope, System.Web.Mvc.IDependencyResolver
//    {
//        private IKernel _kernel;
//        public NinjectResolver(IKernel kernel)
//            : base(kernel)
//        {
//            _kernel = kernel;
//        }
//        public IDependencyScope BeginScope()
//        {
//            return new NinjectScope(_kernel.BeginBlock());
//        }
//    }

//    public class NinjectScope : IDependencyScope
//    {
//        protected IResolutionRoot resolutionRoot;

//        public NinjectScope(IResolutionRoot kernel)
//        {
//            resolutionRoot = kernel;
//        }

//        public object GetService(Type serviceType)
//        {
//            IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
//            return resolutionRoot.Resolve(request).SingleOrDefault();
//        }

//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
//            return resolutionRoot.Resolve(request).ToList();
//        }

//        public void Dispose()
//        {
//            IDisposable disposable = (IDisposable)resolutionRoot;
//            if (disposable != null) disposable.Dispose();
//            resolutionRoot = null;
//        }
//    }

//    public class NinjectDependencyResolver : System.Web.Mvc.IDependencyResolver
//    {
//        private readonly IKernel _kernel;

//        public NinjectDependencyResolver()
//        {
//            _kernel = new StandardKernel();
//            AddBindings();
//        }

//        public object GetService(Type serviceType)
//        {
//            return _kernel.TryGet(serviceType);
//        }

//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            return _kernel.GetAll(serviceType);
//        }

//        private void AddBindings()
//        {
//            _kernel.Bind<IDbContext>().To<DataContext>().InRequestScope();
//            _kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
//            _kernel.Bind<IUserManager>().To<UserManager>();
//        }
//    }
//}