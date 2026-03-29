using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationQueryBuilder
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> entry,ISpecification<TEntity,TKey> specs)
            where TEntity : BaseEntity<TKey>
        {
            var query = entry;

            if ( specs.Criteria is not null )
            {
                query = query.Where(specs.Criteria);
            }

            if ( specs.OrderByAsc is not null )
            {
                query = query.OrderBy(specs.OrderByAsc);
            }

            if ( specs.OrderByDesc is not null )
            {
                query = query.OrderByDescending(specs.OrderByDesc);
            }

            if ( specs.Includes.Count > 0 )
            {
                query = specs.Includes.Aggregate(query,(CurrentQuery,IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            }

            if ( specs.IsPaginated )
            {
                query = query.Skip(specs.Skip).Take(specs.Take);
            }
            return query;
        }
    }
}
