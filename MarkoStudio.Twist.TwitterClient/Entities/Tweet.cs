using Newtonsoft.Json;

namespace MarkoStudio.Twist.TwitterApi.Entities
{
    public class Tweet
    {
        public long Id { get; set; }

        public string Text { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
}
