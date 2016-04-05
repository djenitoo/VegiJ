namespace VegiJ.DataAccess
{
    using System;
    using System.Linq;

    public interface IRepository<T> where T : BaseEntity
    {
        void Create(T obj);
        T Read(Guid key);
        void Update(T obj);
        void Delete(Guid key);
        IQueryable<T> Table { get; }
        //void Save();
    }
}
