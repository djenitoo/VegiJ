namespace VegiJ.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            HasKey(t => t.ID);
            Property(t => t.Name);
            Property(t => t.CreatedDate);
            Property(t => t.LastModifiedDate);
            Property(t => t.Place);
            Property(t => t.StartTime).IsOptional();
            HasRequired(t => t.Author)
                .WithMany(c => c.Events)
                .HasForeignKey(t => t.AuthorId)
                .WillCascadeOnDelete(false);
            ToTable("Events");
        }
    }
}