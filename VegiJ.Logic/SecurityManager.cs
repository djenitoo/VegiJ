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
                .Where(u => string.Equals(u.UserName, username, System.StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (searchResult != null)
            {
                var hashedPassword = new PasswordHash(password).ToString();

                if (string.Equals(searchResult.Password, hashedPassword, System.StringComparison.InvariantCulture))
                {
                    // TODO: also implement persistent cookie and make secure cookies
                    FormsAuthentication.SetAuthCookie(username, false);
                    this.currentUser = searchResult;
                    return true;
                }              
            }
            return false;
        }
        // TODO: maybe save current user in http session contex in UI
        public User GetCurrentUser()
        {
            return this.currentUser;
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
            this.currentUser = null;            
            // TODO: Null sesion and everything
        }
    }
}
