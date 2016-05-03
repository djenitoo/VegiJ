namespace VegiJ.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tip : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        [Obsolete("Only needed for serialization and materialization", true)]
        public Tip()
        {
        }

        public Tip(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }
    }
}