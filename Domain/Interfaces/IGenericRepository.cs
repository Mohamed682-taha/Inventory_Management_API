using Domain.Models;

namespace Domain.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity,TKey> specs);
        Task<TEntity?> GetById(int Id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
