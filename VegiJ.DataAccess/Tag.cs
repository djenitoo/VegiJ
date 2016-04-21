namespace VegiJ.DataAccess
{
    using System;

    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        [Obsolete("Only needed for serialization and materialization", true)]
        public Tag()
        {
        }

        public Tag(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            
            this.Name = name;
        }
    }
}
