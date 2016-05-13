using System;
using System.Collections.Generic;
using System.Linq;

namespace VegiJ.Web.Users
{
    using System.Web.ModelBinding;
    using DataAccess;
    using Microsoft.Ajax.Utilities;
    using Microsoft.Owin;
    using Ninject;

    public partial class Profile : Ninject.Web.PageBase
    {
        [Inject]
        public IUserManager UserManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["username"] == null && Request.QueryString["UserID"] == null)
            {
                Response.Redirect(GetRouteUrl("UserByNameRoute", new { username = User.Identity.Name }));
            }
            this.Title = "Profile of " + RouteData.Values["username"];
        }
        
        public IEnumerable<User> GetUser(
                        [QueryString("UserID")] Guid? userId,
                        [RouteData] string username)
        {
            IEnumerable<User> user = null;
            if (userId.HasValue)
            {
                user = UserManager.GetUsers().AsEnumerable().Where(u => u.ID == userId.Value);
            }
            else if (!String.IsNullOrEmpty(username))
            {
                user = UserManager.GetUsers().AsEnumerable().Where(
                    u =>
                        string.Equals((u.UserName as string), username, StringComparison.InvariantCultureIgnoreCase));
            }

            return user;
        }

        // TODO: if own profile show sensitive fields
        public bool IsOwnProfilePage()
        {
            return string.Equals(User.Identity.Name, RouteData.Values["username"].ToString(),
                StringComparison.InvariantCultureIgnoreCase);

        }
    }
}