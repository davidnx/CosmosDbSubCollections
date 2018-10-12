using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CosmosSubCollections
{
    public interface IEntityClient<TEntity>
        where TEntity : EntityBase
    {
        Task<IList<TEntity>> ListAllAsync(CancellationToken cancellation, Func<IQueryable<TEntity>, IQueryable<TEntity>> where = null);
        Task<TEntity> UpsertAsync(TEntity entity, CancellationToken cancellation);
    }
}