using System;
using Microsoft.Azure.Documents.Client;

namespace CosmosSubCollections
{
    public class CollectionDescriptor
    {
        public CollectionDescriptor(CosmosDbPerformanceMonitor monitor, DocumentClient client, Uri collectionUri)
        {
            this.Monitor = monitor ?? throw new ArgumentNullException(nameof(monitor));
            this.Client = client ?? throw new ArgumentNullException(nameof(client));
            this.CollectionUri = collectionUri ?? throw new ArgumentNullException(nameof(collectionUri));
        }

        public CosmosDbPerformanceMonitor Monitor { get; }
        public DocumentClient Client { get; }
        public Uri CollectionUri { get; }
    }
}
