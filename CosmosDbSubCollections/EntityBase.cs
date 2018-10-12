using System;
using Newtonsoft.Json;

namespace CosmosSubCollections
{
    public abstract class EntityBase : Microsoft.Azure.Documents.Resource
    {
        public EntityBase(string kind)
        {
            if (string.IsNullOrEmpty(kind))
            {
                throw new ArgumentException(nameof(kind));
            }

            this.Kind = kind;
        }

        [JsonProperty("kind")]
        internal string Kind { get; set; }
    }
}
