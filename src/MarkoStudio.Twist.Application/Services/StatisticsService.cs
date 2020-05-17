using MarkoStudio.Twist.Application.Models;
using MarkoStudio.Twist.Generalization;
using MarkoStudio.Twist.SentimentAnalysis;
using MarkoStudio.Twist.SentimentAnalysis.Models;
using MarkoStudio.Twist.TwitterClient;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Application.Services
{
    public class StatisticsService
    {
        private readonly ITwitterAdapter _twitterAdapter;
        private readonly IGeneralizationService _generalizationService;
        private readonly ISentimentService _sentimentService;
        private readonly IMemoryCache _cache;

        public StatisticsService(
            ITwitterAdapter twitterAdapter,
            IGeneralizationService generalizationService,
            ISentimentService sentimentService,
            IMemoryCache cache)
        {
            _twitterAdapter = twitterAdapter;
            _generalizationService = generalizationService;
            _sentimentService = sentimentService;
            _cache = cache;
        }

        public async Task<ProfileStatistics> GetProfileStatistics(string userName)
        {
            var cacheKey = userName;
            _cache.TryGetValue(cacheKey, out ProfileStatistics existing);

            if (existing != null)
                return existing;

            var entities = await _twitterAdapter.GetAllTweets(userName);
            var tweets = entities.Select(x => x.Text).ToList();

            var unifiedEntities = await _generalizationService.UnifyContent(tweets);

            var textEntries = unifiedEntities.Select(x => new TextEntry
            {
                OriginId = x.OriginId,
                Text = x.Unified
            }).ToList();

            var positiveSentimentTask = _sentimentService.DetectPositiveSentiments(textEntries);
            var toxicSentimentTask = _sentimentService.DetectToxicSentiments(textEntries);

            await Task.WhenAll(positiveSentimentTask, toxicSentimentTask);

            var positiveSentiment = positiveSentimentTask.Result.ToDictionary(x => x.OriginId);
            var toxicSentiment = toxicSentimentTask.Result.ToDictionary(x => x.OriginId);

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
                    Label = toxicSentiment[x.OriginId].Score >= 0.5? "Toxic" : "Non-toxic"
                }
            }).ToList();

            var profileSentiment = GetProfileLabel(responseRecords.Select(x => x.SentimentScore));
            var profileToxicity = GetProfileLabel(responseRecords.Select(x => x.ToxicityScore));

            var response = new ProfileStatistics
            {
                ProfileSentimentLabel = profileSentiment,
                ProfileToxicityLabel = profileToxicity,
                Records = responseRecords
            };

            _cache.Set(cacheKey, response);

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
    }
}
