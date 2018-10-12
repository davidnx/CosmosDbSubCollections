using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CosmosSubCollections.Demo.Entities;

namespace CosmosSubCollections.Demo.EntityClients
{
    public class AuthorEntityClient : EntityClientBase<AuthorEntity>, IAuthorEntityClient
    {
        internal AuthorEntityClient(CollectionDescriptor descriptor) :
            base(descriptor, AuthorEntity.KindConst)
        {
        }

        public async Task<AuthorEntity> GetAsync(string authorId, CancellationToken cancellation)
        {
            if (string.IsNullOrEmpty(authorId))
            {
                throw new ArgumentException(nameof(authorId));
            }

            var list = await this.ListAllAsync(
                cancellation,
                query => query.Where(e => e.Id == authorId)).ConfigureAwait(false);
            return list.Single();
        }
    }
}
