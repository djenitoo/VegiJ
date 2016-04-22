namespace VegiJ.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            //key
            HasKey(t => t.ID);
            // properties
            Property(t => t.UserName);
            Property(t => t.Password);
            Property(t => t.Email);
            Property(t => t.CreatedDate);
            Property(t => t.Salt);
            Property(t => t.IsAdmin).IsOptional();
            Property(t => t.LastLoginDate).IsOptional();
            Property(t => t.LastModifiedDate);
            // TODO: is this even real
            HasMany(t => t.Recipes)
                .WithRequired(t => t.Author)
                .HasForeignKey(t => t.AuthorId);
            
            ToTable("Users"); // primerno
        }
    }
}
