namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Security.Principal;

    public class User : BaseEntity, IPrincipal
    { 
        // TODO: Add user profile image
        public string UserName { get; set; }
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

        public User(string username, string password, string email)
        {
            this.UserName = username;
            this.Salt = PasswordHash.GenerateSalt();
            this.Password = PasswordHash.EncryptPassword(password, this.Salt);
            this.Email = email;
            this.IsAdmin = false;
        }

        public bool IsInRole(string role)
        {
            return role.Equals("admin", StringComparison.InvariantCultureIgnoreCase) && this.IsAdmin;
        }
    }
}
