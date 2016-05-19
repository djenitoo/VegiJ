namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;
    using System.Net.Mail;
    using System.Security.Principal;
    using System.Text.RegularExpressions;
    using System.Web.Security;

    public class User : BaseEntity, IPrincipal
    {
        // TODO: Add user profile image
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return this.FirstName + " " + this.LastName; } }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; set; }
        [ForeignKey("Gender")]
        public Guid? GenderID { get; set; }        
        public virtual Gender Gender { get; set; }
        private ICollection<Recipe> _recipes;
        private ICollection<Tip> _tips;
        private ICollection<Event> _events;
        private IIdentity _identity;

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
                return this._identity ?? (this._identity = new GenericIdentity(this.UserName, "Forms"));
            }
            set { this._identity = value; }
        }

        // TODO: Extend the constructors for User etc. !!
        [Obsolete("Only needed for serialization and materialization", true)]
        public User()
        {
        }

        public User(string username,
                    string password,
                    string email,
                    string birthDate,
                    string genderID,
                    string firstname = null,
                    string lastname = null)
        {
            ValidateUser(username, password, email, birthDate);
            this.UserName = username;
            this.Salt = PasswordHash.GenerateSalt();
            this.Password = PasswordHash.EncryptPassword(password, this.Salt);
            this.Email = email;
            this.IsAdmin = false;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.GenderID = Guid.Parse(genderID);
        }

        public bool IsInRole(string role)
        {
            return Roles.Provider.IsUserInRole(this.UserName, role);
        }

        private void ValidateUser(string username,
                                  string password,
                                  string email,
                                  string birthDate)
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

            if (username.Length < GlobalConstants.UsernameMinLength || username.Length > GlobalConstants.UsernameMaxLength)
            {
                throw new ArgumentException(string.Format("Incorrect length of username! Username length should be between {0} and {1} characters!",
                    GlobalConstants.UsernameMinLength,
                    GlobalConstants.UsernameMaxLength));
            }
            if (password.Length < GlobalConstants.UsernamePasswordMinLength || password.Length > GlobalConstants.UsernamePasswordMaxLength)
            {
                // TODO: Make messages global constants
                throw new ArgumentException(string.Format("Incorrect length of password! Password length should be between {0} and {1} characters!",
                    GlobalConstants.UsernamePasswordMinLength,
                    GlobalConstants.UsernamePasswordMaxLength));
            }

            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email!");
            }

            try
            {
                this.BirthDate = DateTime.Parse(birthDate);
            }
            catch (Exception)
            {
                
                throw new ArgumentException("Invalid birth date!");
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
