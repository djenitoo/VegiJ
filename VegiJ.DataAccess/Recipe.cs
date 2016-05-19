namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
        //public virtual ICollection<VegiJFile> Photos { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public Guid CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        // TODO: Extend Recipe constructors
        [Obsolete("Only needed for serialization and materialization", true)]
        public Recipe()
        {
        }
        //TODO: Validation for missing foreign IDs
        public Recipe(string name, string content)
        {
            this.Title = name;
            this.Content = content;
        }
    }
}
