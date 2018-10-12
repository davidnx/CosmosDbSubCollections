using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CosmosSubCollections.Extensions;
using Microsoft.Azure.Documents.Linq;

namespace CosmosSubCollections
{
    public abstract class EntityClientBase<TEntity> : IEntityClient<TEntity>
          where TEntity : EntityBase
    {
        private readonly CollectionDescriptor descriptor;
        private readonly string kind;

        public EntityClientBase(CollectionDescriptor descriptor, string kind)
        {
            this.descriptor = descriptor ?? throw new ArgumentNullException(nameof(descriptor));
            this.kind = !string.IsNullOrEmpty(kind) ? kind : throw new ArgumentException(nameof(kind));
        }

        public async Task<IList<TEntity>> ListAllAsync(CancellationToken cancellation, Func<IQueryable<TEntity>, IQueryable<TEntity>> where = null)
        {
            var query = this.descriptor.Client
                .CreateDocumentQuery<TEntity>(this.descriptor.CollectionUri)
                .Where(e => e.Kind == this.kind);
            if (where != null)
            {
                query = where(query);
            }
            return await query
                .AsDocumentQuery()
                .ToListAsync(this.descriptor.Monitor, cancellation)
                .ConfigureAwait(false);
        }

        public async Task<TEntity> UpsertAsync(TEntity entity, CancellationToken cancellation)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Kind != this.kind)
            {
                throw new ArgumentException($"Invalid kind '{entity.Kind}' for entity of type {typeof(TEntity).FullName}, expected '{this.kind}'");
            }

            var resource = await this.descriptor.Client.
                UpsertDocumentAsync(this.descriptor.CollectionUri, entity, cancellationToken: cancellation)
                .ConfigureAwait(false);
            this.descriptor.Monitor.Increment(resource.RequestCharge);
            return (dynamic)resource.Resource;
        }
    }
}
