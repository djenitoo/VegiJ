namespace VegiJ.DataAccess
{
    using System;

    public abstract class BaseEntity
    {
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
