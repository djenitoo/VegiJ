namespace VegiJ.Logic
{
    using System.Linq;
    using System.Web.Security;
    using VegiJ.DataAccess;

    public static class SecurityManager //: ISecurityProvider
    {
        private static UserManager userManager;
        private static User currentUser;

        public static void LoadUserRepository(IRepository<User> userRepository)
        {
            userManager = new UserManager(userRepository);
        }

        // TODO: here implement the new user repository (start it from another method or here)
        public static bool LogIn(string username, string password)
        {
            username = username.Trim();
            password = password.Trim();
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                // TODO: Convert it to invalid input exception;  maybe add to UI lenght restriction
                return false;
            }

            var foundUser = userManager.GetUsers()
                .Where(u => string.Equals(u.UserName, username, System.StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (foundUser != null)
            {
                var hashedPassword = PasswordHash.EncryptPassword(password, foundUser.Salt);
                var areEqualPasswords = PasswordHash.ComparePasswords(hashedPassword, foundUser.Salt, foundUser.Password);
                if (areEqualPasswords)
                {
                    // TODO: also implement persistent cookie and make secure cookies                    
                    FormsAuthentication.SetAuthCookie(username, false);
                    currentUser = foundUser;
                    return true;
                }
            }
            return false;
        }
        // TODO: save somehow current user in http session contex in UI
        public static User GetCurrentUser()
        {
            return currentUser;
        }

        public static void LogOut()
        {
            FormsAuthentication.SignOut();
            currentUser = null;
            userManager = null;
            // TODO: Null sesion and everything
        }
    }
}
