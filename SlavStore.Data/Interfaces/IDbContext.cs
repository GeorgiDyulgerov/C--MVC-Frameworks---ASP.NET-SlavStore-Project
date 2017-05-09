using System.Data.Entity;
using SlavStore.Models;

namespace SlavStore.Data.Interfaces
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}