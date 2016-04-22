namespace VegiJ.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Net;

    public class Tip : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
    }
}