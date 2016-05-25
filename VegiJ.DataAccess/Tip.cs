namespace VegiJ.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tip : BaseEntity
    {
        private string _title;
        private string _content;
        public bool IsApproved { get; set; }
        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        [Obsolete("Only needed for serialization and materialization", true)]
        public Tip()
        {
        }

        public string Title
        {
            get
            { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tip title cannot be empty!");
                }
                _title = value;
            }
        }
        public string Content
        {
            get
            { return _content; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Tip title cannot be empty!");
                }
                _content = value;
            }
        }
        public Tip(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }
    }
}