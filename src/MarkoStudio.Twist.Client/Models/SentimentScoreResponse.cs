using System.Collections.Generic;

namespace MarkoStudio.Twist.Client.Models
{
    public class SentimentScoreResponse
    {
        public Dictionary<string, double> Score { get; set; }

        public string Label { get; set; }
    }
}
