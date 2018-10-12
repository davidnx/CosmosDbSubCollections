using Newtonsoft.Json;

namespace CosmosSubCollections.Demo.Entities
{
    public class AuthorEntity : EntityBase
    {
        internal const string KindConst = "author";

        public AuthorEntity() : base(KindConst)
        {
        }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
