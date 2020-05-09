namespace MarkoStudio.Twist.Application.Models
{
    public class Statistics
    {
        public string Text { get; set; }

        public SentimentScore SentimentScore { get; set; }

        public ToxicityScore ToxicityScore { get; set; }
    }
}
