namespace VegiJ.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            HasKey(c => c.ID);
            Property(c => c.Name);
            Property(c => c.CreatedDate);
            Property(c => c.LastModifiedDate);
            //HasOptional(c => c.ParentCategory)      // bruh?
            //    .WithMany(subc => subc.SubCategories)
            //    .HasForeignKey(c => c.ParentCategoryId)
            //    .WillCascadeOnDelete(false);
            HasMany(c => c.SubCategories)
                .WithOptional(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .WillCascadeOnDelete(false);
            HasMany(c => c.Recipes)
                .WithRequired(r => r.Category)
                .HasForeignKey(r => r.CategoryID)
                .WillCascadeOnDelete(false);
            ToTable("Categories");
        }
    }
}
