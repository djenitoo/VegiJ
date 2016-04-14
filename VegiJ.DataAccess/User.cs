namespace VegiJ.DataAccess
{
    using System;
    using System.Security.Principal;

    public class User : BaseEntity, IPrincipal
    { 
        // privileges?
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; set; }

        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.UserName);
            }
        }

        // TODO: Make the constructors etc. !!
        [Obsolete("Only needed for serialization and materialization", true)]
        public User()
        {
        }
        public User(string username, string password)
        {
            this.UserName = username;
            this.Salt = PasswordHash.GenerateSalt();
            this.Password = PasswordHash.EncryptPassword(password, this.Salt);
        }

        public User(string username, string password, string email)
        {
            this.ID = Guid.NewGuid();
            this.UserName = username;
            this.Salt = PasswordHash.GenerateSalt();
            this.Password = PasswordHash.EncryptPassword(password, this.Salt);
            this.Email = email;
            // TODO: Edit and resolve the data exceptions!
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
            this.LastLoginDate = DateTime.Now;
            this.IsAdmin = false;
        }

        public bool IsInRole(string role)
        {
            return role.Equals("admin", StringComparison.InvariantCultureIgnoreCase) ? this.IsAdmin : false;
        }
    }
}
