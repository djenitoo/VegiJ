using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace VegiJ.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Add routes
            RegisterCustomRoutes(RouteTable.Routes);
        }

        void RegisterCustomRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "UserByNameRoute",
                "Users/{username}",
                "~/Users/Profile.aspx"
                );
            routes.MapPageRoute(
                "SettingsByUserNameRoute",
                "Users/{username}/Settings",
                "~/Users/Settings.aspx"
                );
        }
    }
}