using MarkoStudio.Twist.TwitterApi.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MarkoStudio.Twist.TwitterApi
{
    public interface ITwitterClient
    {
        Task<List<Tweet>> GetAllTweets(string userName, bool includeReplies = false, bool includeRetweets = false);
    }

    public class TwitterClient : ITwitterClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializer _serializer;

        public TwitterClient(IOptions<TwitterAuthOptions> options)
        {
            _httpClient = new HttpClient(new TwitterAuthHandler(options));
            _serializer = JsonSerializer.CreateDefault();
        }

        public async Task<List<Tweet>> GetAllTweets(string userName, bool includeReplies, bool includeRetweets)
        {
            var url = $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={userName}&exclude_replies={!includeReplies}&include_retweets={includeRetweets}";

            var message = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(message);

            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var reader = new StreamReader(stream))
                return _serializer.Deserialize<List<Tweet>>(new JsonTextReader(reader));
        }
    }
}
