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
            Property(t => t.CreatedDate).IsOptional();
            Property(t => t.Salt);
            Property(t => t.IsAdmin).IsOptional();
            Property(t => t.LastLoginDate).IsOptional();
            Property(t => t.ModifiedDate).IsOptional();
            // table
            ToTable("Users"); // primerno
        }
    }
}
