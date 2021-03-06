﻿namespace VegiJ.DataAccess
{
    using System;
    using System.Linq;

    public interface IRepository<T> where T : BaseEntity
    {
        // TODO: implement delete by id/object?
        void Create(T obj);
        T GetById(Guid key);
        void Update(T obj);
        void Delete(Guid key);
        IQueryable<T> Table { get; }
        // that? void Save();        
    }
}
