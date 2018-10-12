using System.Threading;
using System.Threading.Tasks;
using CosmosSubCollections.Demo.EntityClients;

namespace CosmosSubCollections.Demo
{
    public interface IDemoAdapter
    {
        IAuthorEntityClient Authors { get; }
        IBlogPostEntityClient BlogPosts { get; }

        double TotalCharge { get; }
        int TotalRequests { get; }

        Task DeleteAsync(string id, CancellationToken cancellation);
    }
}