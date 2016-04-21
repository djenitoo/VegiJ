﻿namespace VegiJ.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    public class TagMap : EntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            HasKey(t => t.ID);
            Property(t => t.Name);
            Property(t => t.CreatedDate);
            Property(t => t.LastModifiedDate);
            ToTable("Tags");
        }
    }
}
