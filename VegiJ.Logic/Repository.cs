namespace VegiJ.DataAccess
{
    using System;
    using System.Linq;
    using VegiJ.DataAccess.Contracts;
    using System.Data.Entity;
    using System.Data.Entity.Validation;

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _dbcontext;
        private IDbSet<T> _entities;

        public Repository(IDbContext context)
        {
            this._dbcontext = context;
        }

        public T GetById(Guid key)
        {
            return this.Entities.Find(key);
        }

        public void Create(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                this._dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException dbExp)
            {
                var msg = String.Empty;

                foreach (var validationErrors in dbExp.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}",
                                validationError.PropertyName, validationError.ErrorMessage) +
                                Environment.NewLine;
                    }
                }
                var fail = new Exception(msg, dbExp);
                throw fail;
            }
        }
                        
        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this._dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException dbExp)
            {
                var msg = String.Empty;

                foreach (var validationErrors in dbExp.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}",
                                validationError.PropertyName, validationError.ErrorMessage) +
                                Environment.NewLine;
                    }
                }
                var fail = new Exception(msg, dbExp);
                throw fail;
            }
        }
        
        public void Delete(Guid key)
        {
            try
            {
                if (key == null)
                {
                    throw new ArgumentNullException("entity");
                }
                var entity = this.Entities.Find(key);
                this.Entities.Remove(entity);
                this._dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException dbExp)
            {
                var msg = String.Empty;

                foreach (var validationErrors in dbExp.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}",
                                validationError.PropertyName, validationError.ErrorMessage) +
                                Environment.NewLine;
                    }
                }
                var fail = new Exception(msg, dbExp);
                throw fail;
            }
        }

        public IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        // TODO: Merge Entity with Table?
        // leave like that for now
        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _dbcontext.Set<T>();
                }

                return _entities;
            }
        }

    }
}
