using System.Collections.Generic;

namespace MarkoStudio.Twist.Application.Models
{
    public class SentimentScore
    {
        public Dictionary<string, double> Score { get; set; }

        public string Label { get; set; }
    }
}
