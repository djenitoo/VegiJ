namespace VegiJ.Logic
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
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
        
        public static bool LogIn(string username, string password, bool rememberMe = false)
        {
            username = Regex.Replace(username, "\\s+", " ");
            password = password.Trim();
            if (password.Contains(" "))
            {
                throw new ArgumentException("Invalid password! Password cannot contain spaces!");
            }
            username = username.Trim();
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password cannot be empty!");
            }
            // TODO: add global const strings
            if (username.Length < 5)
            {
                throw new ArgumentException("Incorrect length of username!");
            }
            if (password.Length < 6)
            {
                throw new ArgumentException("Incorrect length of password!");
            }
            var foundUser = userManager.GetUsers().AsEnumerable()
                .Where(u => string.Equals((u.UserName as string), username, StringComparison.InvariantCultureIgnoreCase) == true)
                .FirstOrDefault();
            if (foundUser == null) return false;
            //var hashedPassword = PasswordHash.EncryptPassword(password, foundUser.Salt);
            var areEqualPasswords = PasswordHash.ComparePasswords(password, foundUser.Salt, foundUser.Password);
            if (areEqualPasswords)
            {                  
                //FormsAuthentication.SetAuthCookie(username, rememberMe);
                foundUser.LastLoginDate = DateTime.Now;
                userManager.UpdateUser(foundUser);
                currentUser = foundUser;
                return true;
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
