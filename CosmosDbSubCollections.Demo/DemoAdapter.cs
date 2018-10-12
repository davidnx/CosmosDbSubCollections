using System;
using System.Threading;
using System.Threading.Tasks;
using CosmosSubCollections.Demo.EntityClients;
using Microsoft.Azure.Documents.Client;

namespace CosmosSubCollections.Demo
{
    public class DemoAdapter : IDemoAdapter
    {
        private readonly DocumentClient client;
        private readonly string databaseName;
        private readonly string collectionName;

        private readonly CosmosDbPerformanceMonitor monitor;

        public DemoAdapter(DocumentClient client, string databaseName, string collectionName)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            this.collectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));

            this.monitor = new CosmosDbPerformanceMonitor();

            var collectionUri = UriFactory.CreateDocumentCollectionUri(this.databaseName, this.collectionName);
            var descriptor = new CollectionDescriptor(this.monitor, client, collectionUri);

            this.Authors = new AuthorEntityClient(descriptor);
            this.BlogPosts = new BlogPostEntityClient(descriptor);
        }

        public IAuthorEntityClient Authors { get; }
        public IBlogPostEntityClient BlogPosts { get; }

        public double TotalCharge => this.monitor.Charge;
        public int TotalRequests => this.monitor.Requests;

        public async Task DeleteAsync(string id, CancellationToken cancellation)
        {
            var documentUri = UriFactory.CreateDocumentUri(this.databaseName, this.collectionName, id);
            var resource = await this.client
                .DeleteDocumentAsync(documentUri, cancellationToken: cancellation)
                .ConfigureAwait(false);
            this.monitor.Increment(resource.RequestCharge);
        }
    }
}
