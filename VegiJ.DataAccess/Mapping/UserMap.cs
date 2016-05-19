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
            Property(t => t.FirstName);
            Property(t => t.LastName);
            Property(t => t.Password);
            Property(t => t.Email);
            Property(t => t.BirthDate);
            Property(t => t.CreatedDate);
            Property(t => t.Salt);
            Property(t => t.IsAdmin).IsOptional();
            Property(t => t.LastLoginDate).IsOptional();
            Property(t => t.LastModifiedDate);
            // TODO: is this even real
            HasOptional(t => t.Gender)
                .WithMany(g => g.Users)
                .HasForeignKey(t => t.GenderID)
                .WillCascadeOnDelete(false);
            HasMany(t => t.Recipes)
                .WithRequired(t => t.Author)
                .HasForeignKey(t => t.AuthorId);
            HasMany(t => t.Tips)
                .WithRequired(t => t.Author)
                .HasForeignKey(t => t.AuthorId);
            HasMany(t => t.Events)
                .WithRequired(t => t.Author)
                .HasForeignKey(t => t.AuthorId);
            ToTable("Users"); // primerno
        }
    }
}
