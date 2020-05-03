using Amazon.Comprehend.Model;

namespace MarkoStudio.Twist.SentimentAnalysis.Entities
{
    public class TextSentiment
    {
        public string OriginId { get; set; }

        public string Text { get; set; }

        public string SentimentType { get; set; }

        public SentimentScore SentimentScore { get; set; }

        public int SentimentIndex { get; set; }
    }
}
