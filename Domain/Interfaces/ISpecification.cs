using Domain.Models;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity,bool>> Criteria { get; }
        public List<Expression<Func<TEntity,object>>> Includes { get; }
        public Expression<Func<TEntity,object>> OrderByAsc { get; }
        public Expression<Func<TEntity,object>> OrderByDesc { get; }
        public int Skip { get; }
        public int Take { get; }
        public bool IsPaginated { get; }
    }
}
