using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CosmosSubCollections.Demo.Entities;

namespace CosmosSubCollections.Demo.EntityClients
{
    public class BlogPostEntityClient : EntityClientBase<BlogPostEntity>, IBlogPostEntityClient
    {
        internal BlogPostEntityClient(CollectionDescriptor descriptor) :
            base(descriptor, BlogPostEntity.KindConst)
        {
        }

        public async Task<IList<BlogPostEntity>> ListByAuthorAsync(string authorId, CancellationToken cancellation)
        {
            if (string.IsNullOrEmpty(authorId))
            {
                throw new ArgumentException(nameof(authorId));
            }

            return await this.ListAllAsync(
                cancellation,
                query => query.Where(e => e.AuthorId == authorId)).ConfigureAwait(false);
        }

        public async Task<BlogPostEntity> GetAsync(string authorId, string blogPostId, CancellationToken cancellation)
        {
            if (string.IsNullOrEmpty(authorId))
            {
                throw new ArgumentException(nameof(authorId));
            }

            if (string.IsNullOrEmpty(blogPostId))
            {
                throw new ArgumentException(nameof(blogPostId));
            }

            var list = await this.ListAllAsync(
                cancellation,
                query => query.Where(e => e.AuthorId == authorId &&
                                          e.Id == blogPostId)).ConfigureAwait(false);
            return list.Single();
        }
    }
}
