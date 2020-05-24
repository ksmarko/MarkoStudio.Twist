using System.Collections.Generic;

namespace MarkoStudio.Twist.Client.Models
{
    public class ProfileStatisticsResponse
    {
        public string ProfileSentimentLabel { get; set; }

        public string ProfileToxicityLabel { get; set; }

        public List<StatisticsResponse> Records { get; set; }

        public List<TopWordItemResponse> TopWords { get; set; }
    }

    public class StatisticsResponse
    {
        public string Text { get; set; }

        public SentimentScoreResponse SentimentScore { get; set; }

        public ToxicityScoreResponse ToxicityScore { get; set; }
    }

    public class SentimentScoreResponse
    {
        public Dictionary<string, double> Score { get; set; }

        public string Label { get; set; }
    }

    public class ToxicityScoreResponse
    {
        public double Value { get; set; }

        public string Label { get; set; }
    }    
    
    public class TopWordItemResponse
    {
        public string Text { get; set; }

        public int Count { get; set; }
    }
}
