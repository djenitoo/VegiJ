using VegiJ.MVC2;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWeb), "Start")]

namespace VegiJ.MVC2
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject.Web;
    using Ninject.Web.Common;

    public static class NinjectWeb 
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        }
    }
}
