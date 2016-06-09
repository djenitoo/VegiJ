using System.Web.Mvc;

namespace VegiJ.Web.MVC.Areas.Users
{
    using System.Web;

    public class UsersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Users";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "User_profile",
                "Users/{username}",
                new { controller = "User", action = "Index" }
            );
            context.MapRoute(
                "User_settings",
                "Users/{username}/Settings",
                new { controller = "User", action = "Settings" }
            );

        }
    }
}