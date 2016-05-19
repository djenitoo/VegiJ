namespace VegiJ.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    public class GenderMap : EntityTypeConfiguration<Gender>
    {
        public GenderMap()
        {
            HasKey(t => t.ID);
            Property(t => t.Name);
            Property(t => t.CreatedDate);
            Property(t => t.LastModifiedDate);
            HasMany(t => t.Users)
                .WithOptional(u => u.Gender)
                .HasForeignKey(u => u.GenderID)
                .WillCascadeOnDelete(false);
            ToTable("Genders");
        }
    }
}
