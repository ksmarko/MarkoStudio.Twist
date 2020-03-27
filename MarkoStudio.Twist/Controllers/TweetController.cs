using System.Collections.Generic;
using System.Threading.Tasks;
using MarkoStudio.Twist.TwitterApi;
using MarkoStudio.Twist.TwitterApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarkoStudio.Twist.Controllers
{
    [ApiController]
    [Route("api/tweets")]
    public class TweetController : ControllerBase
    {
        private readonly ITwitterClient _client;

        public TweetController(ITwitterClient client)
        {
            _client = client;
        }

        [HttpGet("/user/{userName}")]
        public async Task<List<Tweet>> GetTweets(string userName)
        {
            return await _client.GetAllTweets(userName);
        }
    }
}