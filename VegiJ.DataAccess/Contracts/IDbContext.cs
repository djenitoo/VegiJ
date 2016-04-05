namespace VegiJ.DataAccess.Contracts
{
    using System.Data.Entity;

    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
