namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Mail;
    using System.Security.Principal;
    using System.Text.RegularExpressions;

    public class User : BaseEntity, IPrincipal
    { 
        // TODO: Add user profile image
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; set; }
        private ICollection<Recipe> _recipes;
        private ICollection<Tip> _tips;
        private ICollection<Event> _events;

        public virtual ICollection<Recipe> Recipes
        {
            get { return this._recipes ?? (this._recipes = new Collection<Recipe>()); }
            set { this._recipes = value; }
        }

        public virtual ICollection<Tip> Tips
        {
            get { return this._tips ?? (this._tips = new Collection<Tip>()); }
            set { this._tips = value; }
        }

        public virtual ICollection<Event> Events
        {
            get { return this._events ?? (this._events = new Collection<Event>()); }
            set { this._events = value; }
        }

        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.UserName);
            }
        }

        // TODO: Extend the constructors for User etc. !!
        [Obsolete("Only needed for serialization and materialization", true)]
        public User()
        {
        }

        public User(string username, string password, string email, string firstname = null, string lastname = null)
        {
            ValidateUser(username, password, email);
            this.UserName = username;
            this.Salt = PasswordHash.GenerateSalt();
            this.Password = PasswordHash.EncryptPassword(password, this.Salt);
            this.Email = email;
            this.IsAdmin = false;
            this.FirstName = firstname;
            this.LastName = lastname;
        }

        public bool IsInRole(string role)
        {
            return role.Equals("admin", StringComparison.InvariantCultureIgnoreCase) && this.IsAdmin;
        }

        private void ValidateUser(string username, 
                                  string password, 
                                  string email)
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

            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email!");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
