namespace VegiJ.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    public class RecipeMap : EntityTypeConfiguration<Recipe>
    {
        public RecipeMap()
        {
            //key
            HasKey(t => t.ID);
            Property(t => t.Title);
            Property(t => t.Photos).HasColumnType("image");
            // TODO: finish!
            Property(t => t.CreatedDate).IsOptional();
            Property(t => t.ModifiedDate).IsOptional();
            // table
            ToTable("Recipes");
        }
    }
}
