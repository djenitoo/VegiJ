namespace VegiJ.DataAccess
{
    using System;
    public class User : BaseEntity
    { 
        // privileges?
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        public byte[] Salt { get; set; }
        public bool IsAdmin { get; set; }

        public User(string username, string password)
        {
            this.UserName = username;
            var hash = new PasswordHash(password);
            this.Password = hash.ToString();
        }

        public User(string username, string password, string email)
        {
            this.UserName = username;
            var hash = new PasswordHash(password);
            this.Password = hash.ToString();
            this.Email = email;
        }        
    }
}
