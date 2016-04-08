namespace VegiJ.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    public class UserMap : EntityTypeConfiguration<User>
    {
        // TODO: Make generic!
        public UserMap()
        {
            //key
            HasKey(t => t.ID);
            // properties
            Property(t => t.UserName);
            Property(t => t.Password);
            Property(t => t.Email);
            Property(t => t.CreatedDate);
            Property(t => t.IsAdmin);
            Property(t => t.LastLoginDate);
            Property(t => t.ModifiedDate);
            // table
            ToTable("Users"); // primerno
        }
    }
}
