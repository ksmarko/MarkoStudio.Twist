using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using MarkoStudio.Twist.SentimentAnalysis.Models;
using MarkoStudio.Twist.SentimentAnalysis.PerspectiveApi;
using MarkoStudio.Twist.SentimentAnalysis.PerspectiveApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.SentimentAnalysis
{
    public interface ISentimentService
    {
        Task<IEnumerable<TextSentiment>> DetectPositiveSentiments(IEnumerable<TextEntry> entries);

        Task<IEnumerable<TextToxicity>> DetectToxicSentiments(IEnumerable<TextEntry> entries);
    }

    public class SentimentService : ISentimentService
    {
        private readonly IAmazonComprehend _amazonComprehendClient;
        private readonly IPerspectiveClient _perspectiveClient;

        public SentimentService(IAmazonComprehend amazonComprehendClient, IPerspectiveClient perspectiveClient)
        {
            _amazonComprehendClient = amazonComprehendClient;
            _perspectiveClient = perspectiveClient;
        }

        public async Task<IEnumerable<TextSentiment>> DetectPositiveSentiments(IEnumerable<TextEntry> entries)
        {
            var response = await _amazonComprehendClient.BatchDetectSentimentAsync(new BatchDetectSentimentRequest
            {
                LanguageCode = LanguageCode.En,
                TextList = entries.Select(x => x.Text).ToList()
            });

            return entries.Zip(response.ResultList, (entry, result) => new TextSentiment
            {
                OriginId = entry.OriginId,
                Text = entry.Text,
                SentimentType = result.Sentiment.Value,
                SentimentScore = result.SentimentScore.GetScore(),
                SentimentIndex = result.Index
            }).ToList();
        }

        public async Task<IEnumerable<TextToxicity>> DetectToxicSentiments(IEnumerable<TextEntry> entries)
        {
            var resultList = new List<TextToxicity>();

            foreach (var entry in entries)
            {
                var response = await _perspectiveClient.Analyze(new PerspectiveAnalysisRequest
                {
                    Text = entry.Text
                });

                var result = new TextToxicity
                {
                    OriginId = entry.OriginId,
                    Text = entry.Text,
                    Score = response.AttributeScores[KnownPerspectiveAnalysisAttributes.Toxicity].Value
                };

                resultList.Add(result);
            }

            return resultList;
        }
    }
}
