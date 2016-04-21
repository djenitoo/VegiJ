namespace VegiJ.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntity
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
