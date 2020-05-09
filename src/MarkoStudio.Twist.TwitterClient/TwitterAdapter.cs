using MarkoStudio.Twist.Common.Http;
using MarkoStudio.Twist.TwitterClient.Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.TwitterClient
{
    public interface ITwitterAdapter
    {
        Task<IEnumerable<TweetResponse>> GetAllTweets(string userName, bool includeReplies = false, bool includeRetweets = false);
    }

    public class TwitterAdapter : ITwitterAdapter
    {
        private readonly HttpClient _httpClient;

        public TwitterAdapter(IOptions<TwitterAuthOptions> options)
        {
            _httpClient = new HttpClient(new TwitterAuthHandler(options));
        }

        public async Task<IEnumerable<TweetResponse>> GetAllTweets(string userName, bool includeReplies, bool includeRetweets)
        {
            var url = $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={userName}&exclude_replies={!includeReplies}&include_retweets={includeRetweets}";

            var response = await _httpClient.Get<List<TweetResponse>>(url);

            return response;
        }
    }
}
