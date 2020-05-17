using System.Collections.Generic;

namespace MarkoStudio.Twist.Application.Models
{
    public class ProfileStatistics
    {
        public string ProfileSentimentLabel { get; set; }

        public string ProfileToxicityLabel { get; set; }

        public List<Statistics> Records { get; set; }
    }

    public class Statistics
    {
        public string Text { get; set; }

        public SentimentScore SentimentScore { get; set; }

        public ToxicityScore ToxicityScore { get; set; }
    }

    public class SentimentScore
    {
        public Dictionary<string, double> Score { get; set; }

        public string Label { get; set; }
    }

    public class ToxicityScore
    {
        public double Value { get; set; }

        public string Label { get; set; }
    }
}
