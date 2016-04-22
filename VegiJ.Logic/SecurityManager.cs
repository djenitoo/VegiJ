namespace VegiJ.Logic
{
    using System;
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
        
        public static bool LogIn(string username, string password)
        {
            username = username.Trim();
            password = password.Trim();
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                // TODO: Convert it to invalid input exception;  maybe add to UI lenght restriction
                return false;
            }

            var foundUser = userManager.GetUsers().AsEnumerable()
                .Where(u => string.Equals((u.UserName as string), username, System.StringComparison.InvariantCultureIgnoreCase) == true).FirstOrDefault();
            if (foundUser != null)
            {
                //var hashedPassword = PasswordHash.EncryptPassword(password, foundUser.Salt);
                var areEqualPasswords = PasswordHash.ComparePasswords(password, foundUser.Salt, foundUser.Password);
                if (areEqualPasswords)
                {
                    // TODO: also implement persistent cookie
                    // TODO: move to the UI or include persistent value here                   
                    FormsAuthentication.SetAuthCookie(username, false);
                    foundUser.LastLoginDate = DateTime.Now;
                    userManager.UpdateUser(foundUser);
                    currentUser = foundUser;
                    return true;
                }
            }
            return false;
        }

        public static User GetCurrentUser()
        {
            return currentUser;
        }

        public static void LogOut()
        {
            FormsAuthentication.SignOut();
            currentUser = null;
            userManager = null;
        }
    }
}
