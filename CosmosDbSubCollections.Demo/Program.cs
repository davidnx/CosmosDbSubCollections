using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CosmosSubCollections.Demo.Entities;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace CosmosSubCollections.Demo
{
    public class Program
    {
        private const string MyCosmosUri = "https://<YOURACCOUNT>.documents.azure.com:443/";
        private const string MyCosmosKey = "<YOUR_PRIMARY_KEY>"; // For demo purposes only. Never store your keys in source code!
        private const string MyDatabaseName = "<YOUR_DATABASE_NAME>";
        private const string MyCollectionName = "<YOUR_COLLECTION_NAME>";

        public static void Main()
        {
            MainAsync(CancellationToken.None).Wait();
        }

        private static async Task MainAsync(CancellationToken cancellation)
        {
            try
            {
                var client = new DocumentClient(new Uri(MyCosmosUri), MyCosmosKey);
                var adapter = new DemoAdapter(client, MyDatabaseName, MyCollectionName);

                AuthorEntity author = await adapter.Authors.UpsertAsync(
                    new AuthorEntity
                    {
                        Name = "david"
                    },
                    cancellation);

                BlogPostEntity blogPost = await adapter.BlogPosts.UpsertAsync(
                    new BlogPostEntity
                    {
                        AuthorId = author.Id,
                        Text = "hello"
                    },
                    cancellation);

                IList<AuthorEntity> allAuthors = await adapter.Authors.ListAllAsync(cancellation);
                IList<BlogPostEntity> firstAuthorPosts = await adapter.BlogPosts.ListByAuthorAsync(allAuthors[0].Id, cancellation);

                Console.WriteLine($"Posts by author '{allAuthors[0].Id}':");
                Console.WriteLine(JsonConvert.SerializeObject(firstAuthorPosts, Formatting.Indented));

                Console.WriteLine();
                Console.WriteLine("Metrics:");
                Console.WriteLine($"  Total charge: {adapter.TotalCharge} RU's");
                Console.WriteLine($"  Total requests: {adapter.TotalRequests}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.Write(ex.ToString());
            }
        }
    }
}
