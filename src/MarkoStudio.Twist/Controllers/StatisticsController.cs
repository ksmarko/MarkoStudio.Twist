using MarkoStudio.Twist.Application.Models;
using MarkoStudio.Twist.Application.Services;
using MarkoStudio.Twist.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _service;

        public StatisticsController(StatisticsService service)
        {
            _service = service;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<ProfileStatisticsResponse>> GetProfileStatistics([FromQuery] string userName)
        {
            var record = await _service.GetProfileStatistics(userName);

            return new ProfileStatisticsResponse
            {
                SentimentScore = Map(record.SentimentScore),
                ToxicityScore = Map(record.ToxicityScore),
                Records = record.Records.Select(x => new StatisticsResponse
                {
                    Text = x.Text,
                    SentimentScore = Map(x.SentimentScore),
                    ToxicityScore = Map(x.ToxicityScore)
                }).ToList()
            };
        }

        private static SentimentScoreResponse Map(SentimentScore score)
        {
            return new SentimentScoreResponse
            {
                Label = score.Label,
                Score = score.Score
            };
        }

        private static ToxicityScoreResponse Map(ToxicityScore score)
        {
            return new ToxicityScoreResponse
            {
                Label = score.Label,
                Score = score.Value
            };
        }
    }
}
