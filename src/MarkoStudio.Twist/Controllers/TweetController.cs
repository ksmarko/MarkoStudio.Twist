using MarkoStudio.Twist.TwitterClient;
using MarkoStudio.Twist.TwitterClient.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Controllers
{
    [ApiController]
    [Route("api/tweets")]
    public class TweetController : ControllerBase
    {
        private readonly ITwitterAdapter _adapter;

        public TweetController(ITwitterAdapter adapter)
        {
            _adapter = adapter;
        }

        [HttpGet("/user/{userName}")]
        public async Task<IEnumerable<TweetResponse>> GetTweets(string userName)
        {
            return await _adapter.GetAllTweets(userName);
        }
    }
}