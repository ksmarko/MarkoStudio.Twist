using System.Collections.Generic;

namespace MarkoStudio.Twist.Application.Models
{
    public class ProfileStatistics
    {
        public SentimentScore SentimentScore { get; set; }

        public ToxicityScore ToxicityScore { get; set; }

        public List<Statistics> Records { get; set; }
    }
}
