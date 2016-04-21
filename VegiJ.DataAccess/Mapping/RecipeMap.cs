namespace VegiJ.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RecipeMap : EntityTypeConfiguration<Recipe>
    {
        public RecipeMap()
        {
            //key
            HasKey(t => t.ID);
            Property(t => t.Title);
            Property(t => t.Content).HasColumnType("nvarchar(MAX)");
            Property(t => t.CreatedDate);
            Property(t => t.LastModifiedDate);
            HasMany(t => t.Tags);

            HasRequired(t => t.Author) // no idea dali e tai :D
                .WithMany(c => c.Recipes)
                .HasForeignKey(t => t.AuthorId)
                .WillCascadeOnDelete(false);
            
            HasRequired(t => t.Category)
                .WithMany(t => t.Recipes)
                .HasForeignKey(t => t.CategoryID)
                .WillCascadeOnDelete(false);
                        
            //Property(t => t.Photos).HasColumnType("image");
            ToTable("Recipes");
        }
    }
}
