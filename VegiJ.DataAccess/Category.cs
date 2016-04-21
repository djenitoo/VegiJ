namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }

        [Obsolete("Only needed for serialization and materialization", true)]
        public Category()
        {
        }

        public Category(string name)
        {
            this.Name = name;
        }
    }
}
