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
    using System.Web.Routing;
    public partial class Profile : Ninject.Web.PageBase
    {
        [Inject]
        public IUserManager UserManager { get; set; }
        public User currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["username"] == null && Request.QueryString["UserID"] == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect(GetRouteUrl("UserByNameRoute", new { username = User.Identity.Name }));
                }
                else
                {
                    Response.Redirect("Auth/Login.aspx");
                }
            }
            this.Title = "Profile of " + RouteData.Values["username"];
            this.currentUser = this.GetUser(null, RouteData.Values["username"].ToString()).FirstOrDefault();

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

        protected void RadListView1_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            RadListView1.DataSource = this.GetUser(null, RouteData.Values["username"].ToString()).FirstOrDefault().Recipes;
        }
    }
}