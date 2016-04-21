namespace VegiJ.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Contracts;
    using System.Reflection;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Collections.Generic;
    public class DataContext : DbContext, IDbContext
    {
        public DataContext() : base("name=DbConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                        type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;

            //Find all Entities that are Added/Modified that inherit from my EntityBase
            IEnumerable<ObjectStateEntry> objectStateEntries =
                from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                where
                    e.IsRelationship == false &&
                    e.Entity != null &&
                    typeof(BaseEntity).IsAssignableFrom(e.Entity.GetType())
                select e;

            var currentTime = DateTime.Now;

            foreach (var entry in objectStateEntries)
            {
                var entityBase = entry.Entity as BaseEntity;

                if (entry.State == EntityState.Added)
                {
                    entityBase.ID = Guid.NewGuid();
                    entityBase.CreatedDate = currentTime;
                }
                
                entityBase.LastModifiedDate = currentTime;
            }


            return base.SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
    }
}
