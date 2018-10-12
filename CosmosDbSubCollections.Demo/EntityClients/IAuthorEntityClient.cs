using System.Threading;
using System.Threading.Tasks;
using CosmosSubCollections.Demo.Entities;

namespace CosmosSubCollections.Demo.EntityClients
{
    public interface IAuthorEntityClient : IEntityClient<AuthorEntity>
    {
        Task<AuthorEntity> GetAsync(string authorId, CancellationToken cancellation);
    }
}