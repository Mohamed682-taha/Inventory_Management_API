using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly InventoryDbContext _dbContext;

        public GenericRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public async Task AddRangeAsync(List<TEntity> entities) => await _dbContext.Set<TEntity>().AddRangeAsync(entities);


        public async Task<int> CountAysnc(ISpecification<TEntity,TKey> specs)
        {
            var query = _dbContext.Set<TEntity>();
            return await SpecificationQueryBuilder.CreateQuery(query,specs).CountAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity,TKey> specs)
        {
            var query = _dbContext.Set<TEntity>();
            return await SpecificationQueryBuilder.CreateQuery(query,specs).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        public async Task<TEntity?> GetByIdAsync(int Id) => await _dbContext.Set<TEntity>().FindAsync(Id);

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity,TKey> specs)
        {
            var query = _dbContext.Set<TEntity>();
            return await SpecificationQueryBuilder.CreateQuery(query,specs).FirstOrDefaultAsync();
        }

        public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
    }
}
