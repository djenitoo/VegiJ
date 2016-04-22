namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public virtual Category ParentCategory { get; set; }
        private ICollection<Category> _subCategories;
        private ICollection<Recipe> _recipes;

        public virtual ICollection<Category> SubCategories
        {
            get { return this._subCategories ?? (this._subCategories = new Collection<Category>()); }
            set { this._subCategories = value; }
        }

        public virtual ICollection<Recipe> Recipes
        {
            get { return this._recipes ?? (this._recipes = new Collection<Recipe>()); }
            set { this._recipes = value; }
        }

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
