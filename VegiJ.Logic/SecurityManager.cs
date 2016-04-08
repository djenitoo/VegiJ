namespace VegiJ.Logic
{
    using System.Linq;
    using System.Web.Security;
    using VegiJ.DataAccess;
    using VegiJ.DataAccess.Contracts;
    // TODO: ! put validations
    // TODO: make static
    public class SecurityManager : ISecurityProvider
    {
        private UserManager userManager;
        private User currentUser;
        // TODO: userRepository move out of constructor
        public SecurityManager(IRepository<User> userRepository)
        {
            this.userManager = new UserManager(userRepository);
        }
        
        public bool LogIn(string username, string password)
        {
            var searchResult = this.userManager.GetUsers()
                .Where(u => u.UserName == username && u.Password == password).FirstOrDefault();
            if (searchResult != null)
            {
                // TODO: also implement persistent cookie and make secure cookies
                FormsAuthentication.SetAuthCookie(username, false);
                this.currentUser = searchResult;
                return true;
            }

            return false;
        }
        // Make method(?) that returns current user object for UI; maybe save current user in http session contex
        public User GetCurrentUser()
        {
            return this.currentUser;
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
            this.currentUser = null;
            // TODO: Hash passwords with md5
            // TODO: Null sesion
        }
    }
}
