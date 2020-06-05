using MarkoStudio.Twist.Application.Models;
using MarkoStudio.Twist.Generalization;
using MarkoStudio.Twist.SentimentAnalysis;
using MarkoStudio.Twist.SentimentAnalysis.Models;
using MarkoStudio.Twist.TwitterClient;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Application.Services
{
    public class StatisticsService
    {
        private readonly ITwitterAdapter _twitterAdapter;
        private readonly IGeneralizationService _generalizationService;
        private readonly ISentimentService _sentimentService;

        public StatisticsService(
            ITwitterAdapter twitterAdapter,
            IGeneralizationService generalizationService,
            ISentimentService sentimentService)
        {
            _twitterAdapter = twitterAdapter;
            _generalizationService = generalizationService;
            _sentimentService = sentimentService;
        }

        public async Task<ProfileStatistics> GetProfileStatistics(string userName)
        {
            var entities = await _twitterAdapter.GetAllTweets(userName);
            var tweets = entities.Select(x => x.Text).ToList();

            var topWordsTask = GetTopWords(tweets);

            var unifiedEntities = await _generalizationService.UnifyContent(tweets);

            var textEntries = unifiedEntities.Select(x => new TextEntry
            {
                OriginId = x.OriginId,
                Text = x.Unified
            }).ToList();

            var positiveSentimentTask = _sentimentService.DetectPositiveSentiments(textEntries);
            var toxicSentimentTask = _sentimentService.DetectToxicSentiments(textEntries);

            await Task.WhenAll(positiveSentimentTask, toxicSentimentTask, topWordsTask);

            var positiveSentiment = positiveSentimentTask.Result.ToDictionary(x => x.OriginId);
            var toxicSentiment = toxicSentimentTask.Result.ToDictionary(x => x.OriginId);
            var topWords = topWordsTask.Result;

            var responseRecords = unifiedEntities.Select(x => new Statistics
            {
                Text = x.Origin,
                SentimentScore = new SentimentScore
                {
                    Score = positiveSentiment[x.OriginId].SentimentScore.ToDictionary(t => t.Key, t => t.Value),
                    Label = positiveSentiment[x.OriginId].SentimentType
                },
                ToxicityScore = new ToxicityScore
                {
                    Value = toxicSentiment[x.OriginId].Score,
                    Label = toxicSentiment[x.OriginId].Score >= 0.5 ? KnownSentiment.Toxic : KnownSentiment.NonToxic
                }
            }).ToList();

            var profileSentiment = GetProfileLabel(responseRecords.Select(x => x.SentimentScore));
            var profileToxicity = GetProfileLabel(responseRecords.Select(x => x.ToxicityScore));

            var response = new ProfileStatistics
            {
                ProfileSentimentLabel = profileSentiment,
                ProfileToxicityLabel = profileToxicity,
                Records = responseRecords,
                TopWords = topWords
            };

            return response;
        }

        private static string GetProfileLabel(IEnumerable<ILabelled> records)
        {
            return records
                .GroupBy(p => p.Label)
                .Select(g => new { Label = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .First().Label;
        }

        private async Task<Dictionary<string, int>> GetTopWords(List<string> tweets)
        {
            return await Task.Run(() =>
            {
                var words = tweets.Select(GetWords).SelectMany(x => x);

                var result = words
                    .Where(x => x.Length > 3)
                    .GroupBy(x => x)
                    .Select(g => new { Key = g.Key, Count = g.Count()})
                    .OrderByDescending(x => x.Count)
                    .ThenByDescending(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Count);

                return result;
            });
        }

        private static string[] GetWords(string text)
        {
            var result = new List<string>();
            var words = text.Split(' ', '\n');

            var pattern = @"[^\w\d]";

            foreach(var word in words)
            {
                if (word.StartsWith("#") || word.StartsWith("http://") || word.StartsWith("https://"))
                    continue;

                var clear = Regex.Replace(word, pattern, string.Empty).Trim().ToLower();

                result.Add(clear);
            }

            return result.ToArray();
        }
    }
}
