using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Ninject;
using VegiJ.DataAccess;
using VegiJ.DataAccess.Contracts;
using VegiJ.Logic;

namespace VegiJ.Web
{
    public partial class _Default : Ninject.Web.PageBase
    {
        //[Inject]
        //IUserProvider userManager { get; set; }

        private IUserManager userManager;
        private User currentUser;
        [Inject]
        public void Setup(IRepository<User> userRepository)
        {
            // ?
            this.userManager = new UserManager(userRepository);
            SecurityManager.LoadUserRepository(userRepository);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            usersGridView.DataSource = userManager.GetUsers().ToList();
            usersGridView.DataBind();
        }

        protected void logOutButton_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.User = null;
            SecurityManager.LogOut();
            LogInButton.Visible = true;
            logOutButton.Visible = false;
            Response.Redirect(Request.RawUrl);
        }

        protected void LogInButton_Click(object sender, EventArgs e)
        {
            if (SecurityManager.LogIn("jenny", "012358"))
            {
                currentUser = SecurityManager.GetCurrentUser();
                HttpContext.Current.User = currentUser;                
            }
            
            if (Request.IsAuthenticated)
            {
                WelcomeBackMessage.Text = "Welcome back, " + HttpContext.Current.User.Identity.Name + "!";
                AuthenticatedMessagePanel.Visible = true;
                LogInButton.Visible = false;
                logOutButton.Visible = true;              
            }
        }
    }
}