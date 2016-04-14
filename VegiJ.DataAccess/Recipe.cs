namespace VegiJ.DataAccess
{
    using System;

    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid OwnerId { get; set; }
        public byte[] Photos { get; set; }
        public string[] Tags { get; set; }
        public string[] Category { get; set; }

        // TODO: Add Recipe constructors
    }
}
