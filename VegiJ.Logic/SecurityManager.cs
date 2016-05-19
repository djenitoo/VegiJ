namespace VegiJ.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
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
            username = username.Trim();
            password = password.Trim();
            //if (password.Contains(" "))
            //{
            //    throw new ArgumentException("Invalid password! Password cannot contain spaces!");
            //}
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Username and password cannot be empty!");
            }
            if (username.Length < GlobalConstants.UsernameMinLength || username.Length > GlobalConstants.UsernameMaxLength)
            {
                throw new ArgumentException(
                    string.Format("Incorrect length of username! Username length should be between {0} and {1} characters!",
                    GlobalConstants.UsernameMinLength,
                    GlobalConstants.UsernameMaxLength));
            }
            if (password.Length < GlobalConstants.UsernamePasswordMinLength || password.Length > GlobalConstants.UsernamePasswordMaxLength)
            {
                throw new ArgumentException(
                    string.Format("Incorrect length of password! Password length should be between {0} and {1} characters!",
                    GlobalConstants.UsernamePasswordMinLength, 
                    GlobalConstants.UsernamePasswordMaxLength));
            }
            var foundUser = userManager.GetUsers().AsEnumerable()
                .Where(
                u => 
                    string.Equals((u.UserName as string), username, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefault();
            if (foundUser == null) return false;
            //var hashedPassword = PasswordHash.EncryptPassword(password, foundUser.Salt);
            var areEqualPasswords = PasswordHash.ComparePasswords(password, foundUser.Salt, foundUser.Password);
            if (areEqualPasswords)
            {
                foundUser.LastLoginDate = DateTime.Now;
                userManager.UpdateUser(foundUser);
                currentUser = foundUser;
                var authMan = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = new GenericIdentity(currentUser.UserName, DefaultAuthenticationTypes.ApplicationCookie);
                userIdentity.AddClaims(PopulateClaims(currentUser));
                authMan.SignIn(new AuthenticationProperties() {IsPersistent = rememberMe}, userIdentity);
                
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
            var authMan = HttpContext.Current.GetOwinContext().Authentication;
            authMan.SignOut();
            currentUser = null;
            userManager = null;
        }
        
        private static IEnumerable<Claim> PopulateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
                new Claim(ClaimTypes.Gender, user.GenderID.ToString()),
                new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email),
                new Claim("LastLoginDate", user.LastLoginDate.ToString(), ClaimValueTypes.DateTime),
                new Claim("LastModifiedDate", user.LastModifiedDate.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.DateTime),
                new Claim(ClaimTypes.Role, "user")
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            return claims;
        }
    }
}
