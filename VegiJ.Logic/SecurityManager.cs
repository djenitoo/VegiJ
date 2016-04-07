namespace VegiJ.Logic
{
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using VegiJ.DataAccess;
    using VegiJ.DataAccess.Contracts;

    public class SecurityManager : ISecurityProvider
    {
        private UserManager userManager;
        // TODO: or better put UserManager
        public SecurityManager(IRepository<User> userRepository)
        {
            this.userManager = new UserManager(userRepository);
        }

        // TODO: maybe not bool, but void with redirections
        // TODO: leave redirections to the UI!!!
        public bool LogIn(string username, string password)
        {
            var searchResult = this.userManager.GetUsers().ToList()
                .Find(u => u.Password.Equals(password) && u.UserName.Equals(username));
            if (searchResult != null)
            {
                // TODO: also implement persistend cookie
                // add hash cookies? move redirecting to ui
                FormsAuthentication.SetAuthCookie(username, false);
                return true;
            }

            return false;
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
            // TODO: bruh, check if exist that page?
            //HttpContext.Current.Response.Redirect(FormsAuthentication.DefaultUrl);
        }
    }
}
