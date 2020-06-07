using System.Collections.Generic;

namespace MarkoStudio.Twist.Client.Models
{
    /// <summary>
    /// Represents Twitter profile statistics response
    /// </summary>
    public class ProfileStatisticsResponse
    {
        /// <summary>
        /// The profile positivity label. Possible values are POSITIVE, NEGATIVE, NEUTRAL, MIXED
        /// </summary>
        public string ProfileSentimentLabel { get; set; }

        /// <summary>
        /// The profile toxicity label. Possible values are Toxic, NonToxic
        /// </summary>
        public string ProfileToxicityLabel { get; set; }

        /// <summary>
        /// Positivity and toxicity statistics per each tweet
        /// </summary>
        public List<StatisticsResponse> Records { get; set; }

        /// <summary>
        /// The most frequently used words list
        /// </summary>
        public List<TopWordItemResponse> TopWords { get; set; }
    }

    /// <summary>
    /// Positivity and toxicity statistics per each tweet
    /// </summary>
    public class StatisticsResponse
    {
        /// <summary>
        /// The tweet text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Tweet positivity
        /// </summary>
        public SentimentScoreResponse SentimentScore { get; set; }

        /// <summary>
        /// Tweet toxicity
        /// </summary>
        public ToxicityScoreResponse ToxicityScore { get; set; }
    }

    /// <summary>
    /// The positivity response
    /// </summary>
    public class SentimentScoreResponse
    {
        /// <summary>
        /// Map with values per each label type. Label values are POSITIVE, NEGATIVE, NEUTRAL, MIXED
        /// </summary>
        public Dictionary<string, double> Score { get; set; }

        /// <summary>
        /// Result label. Possible values are POSITIVE, NEGATIVE, NEUTRAL, MIXED
        /// </summary>
        public string Label { get; set; }
    }

    /// <summary>
    /// The toxicity response
    /// </summary>
    public class ToxicityScoreResponse
    {
        /// <summary>
        /// Result value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Result label. Possible values are Toxic, NonToxic
        /// </summary>
        public string Label { get; set; }
    }    
    
    /// <summary>
    /// The top words list item
    /// </summary>
    public class TopWordItemResponse
    {
        /// <summary>
        /// The word
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The entries count
        /// </summary>
        public int Count { get; set; }
    }
}
