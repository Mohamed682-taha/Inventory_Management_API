using Domain.Models;

namespace Domain.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity,TKey> specs);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<int> CountAysnc(ISpecification<TEntity,TKey> specs);
        Task<TEntity?> GetByIdAsync(int Id);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity,TKey> specs);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
