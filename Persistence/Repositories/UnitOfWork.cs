using Domain.Interfaces;
using Domain.Models;
using Persistence.Data.DbContexts;

namespace Persistence.Repositories
{
    public class UnitOfWork(InventoryDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string,object> repos = [];
        public IGenericRepository<TEntity,TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if ( repos.ContainsKey(typeName) )
                return (IGenericRepository<TEntity,TKey>)repos[typeName];
            else
            {
                var createdObject = new GenericRepository<TEntity,TKey>(_dbContext);
                repos[typeName] = createdObject;
                return createdObject;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
