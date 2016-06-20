[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VegiJ.Web.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(VegiJ.Web.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace VegiJ.Web.MVC.App_Start
{
    using System;
    using System.Web;
    using System.Web.Security;
    using Areas.Administration.Models;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using DataAccess.Contracts;
    using DataAccess;
    using Helpers;
    using Logic;
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbContext>().To<DataContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<IRecipeManager>().To<RecipeManager>();
            kernel.Bind<ICategoryManager>().To<CategoryManager>();
            kernel.Bind<ITagManager>().To<TagManager>();
            kernel.Bind<ITipManager>().To<TipManager>();
            kernel.Bind<IEventManager>().To<EventManager>();
            //kernel.Bind<RoleProvider>().To<CustomRoleProvider>();
            //kernel.Bind<UserServices>().ToSelf();
        }
    }
}
