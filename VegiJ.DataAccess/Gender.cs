namespace VegiJ.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Gender : BaseEntity
    {
        public string Name { get; set; }
        private ICollection<User> _users;

        public virtual ICollection<User> Users
        {
            get { return this._users ?? (this._users = new Collection<User>()); }
            set { this._users = value; }
        }

        [Obsolete("Only needed for serialization and materialization", true)]
        public Gender()
        {
        }

        public Gender(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            this.Name = name;
        }
    }
}
