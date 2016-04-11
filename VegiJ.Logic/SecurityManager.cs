namespace VegiJ.Logic
{
    using System.Linq;
    using System.Web.Security;
    using VegiJ.DataAccess;
    using VegiJ.DataAccess.Contracts;
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
            // TODO: here implement the new user repository (start it from another method or here)
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                // TODO: Convert it to invalid input exception
                // maybe add to UI lenght restriction
                // add trim
                return false;
            }

            var foundUser = this.userManager.GetUsers()
                .Where(u => string.Equals(u.UserName, username, System.StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (foundUser != null)
            {
                var hashedPassword = PasswordHash.EncryptPassword(password, foundUser.Salt);
                var areEqualPasswords = PasswordHash.ComparePasswords(hashedPassword, foundUser.Salt, foundUser.Password);
                if (areEqualPasswords)
                {
                    // TODO: also implement persistent cookie and make secure cookies                    
                    FormsAuthentication.SetAuthCookie(username, false);
                    this.currentUser = foundUser;
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
