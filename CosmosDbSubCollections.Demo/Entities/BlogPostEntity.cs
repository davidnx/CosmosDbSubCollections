using Newtonsoft.Json;

namespace CosmosSubCollections.Demo.Entities
{
    public class BlogPostEntity : EntityBase
    {
        internal const string KindConst = "blogPost";

        public BlogPostEntity() : base(KindConst)
        {
        }

        [JsonProperty("authorId")]
        public string AuthorId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
