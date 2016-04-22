namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        private ICollection<Recipe> _recipes;
        public virtual ICollection<Recipe> Recipes
        {
            get { return this._recipes ?? (this._recipes = new Collection<Recipe>()); }
            set { this._recipes = value; }
        }

        [Obsolete("Only needed for serialization and materialization", true)]
        public Tag()
        {
        }

        public Tag(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            
            this.Name = name;
        }
    }
}
