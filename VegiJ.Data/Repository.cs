using System;
using System.Linq;

namespace VegiJ.Data
{
    class Repository<T> : IRepository<T> where T : BaseEntity
    {
        
        // TODO: Implement cruds, also dbConn
        public void Create(T obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid key)
        {
            throw new NotImplementedException();
        }

        public T Read(Guid key)
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Table
        {
            get
            {
                throw new NotImplementedException();
            }
        }

    }
}
