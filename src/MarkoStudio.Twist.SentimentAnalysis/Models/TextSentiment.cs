using System.Collections.Generic;

namespace MarkoStudio.Twist.SentimentAnalysis.Models
{
    public class TextSentiment
    {
        public string OriginId { get; set; }

        public string Text { get; set; }

        public string SentimentType { get; set; }

        public Dictionary<string, double> SentimentScore { get; set; }

        public int SentimentIndex { get; set; }
    }

    public class KnownSentiment
    {
        public const string Positive = "Positive";
        public const string Negative = "Negative";
        public const string Mixed = "Mixed";
        public const string Neutral = "Neutral";

        public const string Toxic = "Toxic";
        public const string NonToxic = "Non-toxic";
    }
}
