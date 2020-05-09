namespace MarkoStudio.Twist.Client.Models
{
    public class StatisticsResponse
    {
        public string Text { get; set; }

        public SentimentScoreResponse SentimentScore { get; set; }

        public ToxicityScoreResponse ToxicityScore { get; set; }
    }
}
