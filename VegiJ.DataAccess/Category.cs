namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    public class Category : BaseEntity
    {
        public string _name;
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
        public string Name
        {
            get { return this._name; }
            set
            {
                if (value.Length < GlobalConstants.CategoryNameLength)
                {
                    throw new ArgumentException(string.Format(GlobalConstants.CategoryLenErrorMessage, GlobalConstants.CategoryNameLength));
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(GlobalConstants.CannotBeEmptyErrorMessage, "Category name"));
                }
                this._name = value;
            }
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
