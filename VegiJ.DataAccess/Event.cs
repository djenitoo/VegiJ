namespace VegiJ.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime StartTime { get; set; }
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        // TODO: Add Event constuctors
    }
}