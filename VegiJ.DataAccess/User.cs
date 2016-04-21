namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
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
        public virtual ICollection<Recipe> Recipes { get; set; }

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
            return role.Equals("admin", StringComparison.InvariantCultureIgnoreCase) ? this.IsAdmin : false;
        }
    }
}
