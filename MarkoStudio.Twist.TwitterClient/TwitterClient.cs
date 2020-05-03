using MarkoStudio.Twist.Common.Http;
using MarkoStudio.Twist.TwitterApi.Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.TwitterApi
{
    public interface ITwitterClient
    {
        Task<List<Tweet>> GetAllTweets(string userName, bool includeReplies = false, bool includeRetweets = false);
    }

    public class TwitterClient : ITwitterClient
    {
        private readonly HttpClient _httpClient;

        public TwitterClient(IOptions<TwitterAuthOptions> options)
        {
            _httpClient = new HttpClient(new TwitterAuthHandler(options));
        }

        public async Task<List<Tweet>> GetAllTweets(string userName, bool includeReplies, bool includeRetweets)
        {
            var url = $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={userName}&exclude_replies={!includeReplies}&include_retweets={includeRetweets}";

            var response = await _httpClient.Get<List<Tweet>>(url);

            return response;
        }
    }
}
