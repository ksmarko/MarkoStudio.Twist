using System.Collections.Generic;
using Newtonsoft.Json;

namespace MarkoStudio.Twist.SentimentAnalysis.PerspectiveApi.Models
{
    internal class AnalyzeRequest
    {
        [JsonProperty("comment")]
        public AnalyzeComment Comment { get; set; }

        [JsonProperty("requestedAttributes")]
        public Dictionary<string, object> RequestedAttributes { get; set; }

        [JsonProperty("languages")]
        public string[] Languages { get; set; }
    }

    internal class AnalyzeComment
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    internal class AnalyzeResponse
    {
        public Dictionary<string, AttributeScores> AttributeScores { get; set; }
    }

    internal class AttributeScores
    {
        public Score SummaryScore { get; set; }
    }

    internal class Score
    {
        public float? Value { get; set; }
    }
}
