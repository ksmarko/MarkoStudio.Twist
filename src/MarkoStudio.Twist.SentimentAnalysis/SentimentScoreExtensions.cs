using Amazon.Comprehend.Model;
using MarkoStudio.Twist.SentimentAnalysis.Models;
using System.Collections.Generic;

namespace MarkoStudio.Twist.SentimentAnalysis
{
    public static class SentimentScoreExtensions
    {
        public static Dictionary<string, double> GetScore(this SentimentScore sentimentScore)
        {
            var map = new Dictionary<string, double>
            {
                {KnownSentiment.Positive, sentimentScore.Positive},
                {KnownSentiment.Negative, sentimentScore.Negative},
                {KnownSentiment.Mixed, sentimentScore.Mixed},
                {KnownSentiment.Neutral, sentimentScore.Neutral}
            };

            return map;
        }
    }
}
