namespace VegiJ.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    public class TipMap : EntityTypeConfiguration<Tip>
    {
        public TipMap()
        {
            HasKey(t => t.ID);
            Property(t => t.Title);
            Property(t => t.Content).HasColumnType("nvarchar(MAX)");
            Property(t => t.CreatedDate);
            Property(t => t.LastModifiedDate);
            HasRequired(t => t.Author)
                .WithMany(c => c.Tips)
                .HasForeignKey(t => t.AuthorId)
                .WillCascadeOnDelete(false);
            ToTable("Tips");
        }
    }
}