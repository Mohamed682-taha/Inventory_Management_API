using Domain.Interfaces;
using Domain.Models;
using System.Linq.Expressions;

namespace Service.Specifications
{
    abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity,bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        protected BaseSpecifications()
        {
            
        }

        public Expression<Func<TEntity,bool>> Criteria { get; private set; }
        public List<Expression<Func<TEntity,object>>> Includes { get; } = [];
        public Expression<Func<TEntity,object>> OrderByAsc { get; private set; }
        public Expression<Func<TEntity,object>> OrderByDesc { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void AddIncludes(Expression<Func<TEntity,object>> IncludeExpression) => Includes.Add(IncludeExpression);
        protected void OrderByAscending(Expression<Func<TEntity,object>> expression) => OrderByAsc = expression;
        protected void OrderByDescending(Expression<Func<TEntity,object>> expression) => OrderByDesc = expression;
        protected void AddPagination(int PageSize,int PageIndex)
        {
            IsPaginated = true;
            Skip = ( PageIndex - 1 ) * PageSize;
            Take = PageSize;
        }
    }
}
