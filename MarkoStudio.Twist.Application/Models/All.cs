using System.Collections.Generic;

namespace MarkoStudio.Twist.Application.Models
{
    public class ProfileStatistics
    {
        public SentimentScore SentimentScore { get; set; }

        public ToxicityScore ToxicityScore { get; set; }

        public List<Statistics> Records { get; set; }
    }

    public class Statistics
    {
        public string Text { get; set; }

        public SentimentScore SentimentScore { get; set; }

        public ToxicityScore ToxicityScore { get; set; }
    }

    public class ToxicityScore
    {
        public double Value { get; }

        public string Label => GetLabel();

        public ToxicityScore(double value)
        {
            Value = value;
        }

        private string GetLabel()
        {
            return Value <= 50 ? "Non-toxic" : "Toxic";
        }
    }

    public class SentimentScore
    {
        public Dictionary<string, double> Score { get; set; }

        public string Label { get; set; }
    }
}
