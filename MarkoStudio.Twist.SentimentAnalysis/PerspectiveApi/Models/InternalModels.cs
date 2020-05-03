using System.Collections.Generic;

namespace MarkoStudio.Twist.SentimentAnalysis.PerspectiveApi.Models
{
    internal class AnalyzeRequest
    {
        public AnalyzeComment Comment { get; set; }

        public Dictionary<string, object> RequestedAttributes { get; set; }
    }

    internal class AnalyzeComment
    {
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
