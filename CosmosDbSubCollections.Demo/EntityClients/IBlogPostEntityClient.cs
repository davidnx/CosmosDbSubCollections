using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CosmosSubCollections.Demo.Entities;

namespace CosmosSubCollections.Demo.EntityClients
{
    public interface IBlogPostEntityClient : IEntityClient<BlogPostEntity>
    {
        Task<BlogPostEntity> GetAsync(string authorId, string blogPostId, CancellationToken cancellation);
        Task<IList<BlogPostEntity>> ListByAuthorAsync(string authorId, CancellationToken cancellation);
    }
}