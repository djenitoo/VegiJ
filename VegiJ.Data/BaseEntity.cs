namespace VegiJ.Data
{
    using System;

    public abstract class BaseEntity
    {
        public Guid ID { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
