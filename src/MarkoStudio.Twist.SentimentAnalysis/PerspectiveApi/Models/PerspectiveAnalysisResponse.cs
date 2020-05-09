using System.Collections.Generic;

namespace MarkoStudio.Twist.SentimentAnalysis.PerspectiveApi.Models
{
    public class PerspectiveAnalysisResponse
    {
        public string Text { get; set; }

        public Dictionary<string, float?> AttributeScores { get; set; }
    }
}
