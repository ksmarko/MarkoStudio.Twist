using System.Collections.Generic;

namespace MarkoStudio.Twist.Client.Models
{
    public class ProfileStatisticsResponse
    {
        public SentimentScoreResponse SentimentScore { get; set; }

        public ToxicityScoreResponse ToxicityScore { get; set; }

        public List<StatisticsResponse> Records { get; set; }
    }
}
