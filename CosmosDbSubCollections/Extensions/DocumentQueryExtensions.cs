using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Linq;

namespace CosmosSubCollections.Extensions
{
    internal static class DocumentQueryExtensions
    {
        internal static async Task<List<T>> ToListAsync<T>(this IDocumentQuery<T> query, CosmosDbPerformanceMonitor monitor, CancellationToken cancellation)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var results = new List<T>();
            while (query.HasMoreResults)
            {
                var feed = await query.ExecuteNextAsync<T>(cancellation);
                monitor.Increment(feed.RequestCharge);
                results.AddRange(feed);
            }

            return results;
        }
    }
}
