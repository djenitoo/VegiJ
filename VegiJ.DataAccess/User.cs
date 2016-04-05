namespace VegiJ.DataAccess
{
    using System;
    using Microsoft.AspNet.Identity;

    public class User : BaseEntity
    {
        // privileges?
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsAdmin { get; set; }

        public User(string username, string password)
        {
            this.UserName = username;
            this.Password = password;
        }

        public User(string username, string password, string email)
        {
            this.UserName = username;
            this.Password = password;
            this.Email = email;
        }
    }
}
