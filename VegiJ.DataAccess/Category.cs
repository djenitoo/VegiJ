namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Category
    {
        [Key]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid? ParentCategoryId { get; private set; }
        public virtual Category ParentCategory { get; private set; }
        public virtual ICollection<Category> SubCategories { get; private set; }
    }
}
