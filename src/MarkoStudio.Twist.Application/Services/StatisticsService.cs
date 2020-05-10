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
                    Score = positiveSentiment[x.OriginId].SentimentScore,
                    Label = positiveSentiment[x.OriginId].SentimentType
                },
                ToxicityScore = new ToxicityScore(toxicSentiment[x.OriginId].Score)
            }).ToList();

            var avgProfileToxicity = responseRecords.Select(x => x.ToxicityScore.Value).Average();
            var profileToxicity = new ToxicityScore(avgProfileToxicity);

            var profileSentiment = GetProfileSentiment(responseRecords.Select(x => x.SentimentScore));

            var response = new ProfileStatistics
            {
                SentimentScore = profileSentiment,
                ToxicityScore = profileToxicity,
                Records = responseRecords
            };

            _cache.Set(cacheKey, response);

            return response;
        }

        private static SentimentScore GetProfileSentiment(IEnumerable<SentimentScore> records)
        {
            var label = records
                .GroupBy(p => p.Label)
                .Select(g => new {Label = g.Key, Count = g.Count()})
                .OrderByDescending(x => x.Count)
                .First().Label;

            var dictionaries = records.Select(x => x.Score);

            var positive = dictionaries.Select(x => x[KnownSentiment.Positive]).Average();
            var negative = dictionaries.Select(x => x[KnownSentiment.Negative]).Average();
            var neutral = dictionaries.Select(x => x[KnownSentiment.Neutral]).Average();

            return new SentimentScore
            {
                Label = label,
                Score = new Dictionary<string, double>
                {
                    {KnownSentiment.Positive, positive},
                    {KnownSentiment.Negative, negative},
                    {KnownSentiment.Neutral, neutral}
                }
            };
        }
    }
}
