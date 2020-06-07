using FluentAssertions;
using MarkoStudio.Twist.Generalization.Models;
using MarkoStudio.Twist.SentimentAnalysis.Models;
using MarkoStudio.Twist.TwitterClient.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Tests.Api
{
    public class StatisticsApiTests
    {
        private TwistFixture _fixture;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            _fixture = new TwistFixture();

            SetupMocks();

            await _fixture.InitializeAsync();
        }        
        
        [OneTimeTearDown]
        public async Task TearDown()
        {
            await _fixture.DisposeAsync();
        }

        [Test]
        public async Task Should_return_proper_statistics_for_user_profile()
        {
            // Arrange
            var userName = "someTwitterUser";

            // Act
            var profileStatistics = await _fixture.ApiClient.Statistics.GetProfileStatistics(userName);

            // Assert
            profileStatistics.Should().NotBeNull();

            profileStatistics.ProfileSentimentLabel.Should().Be(KnownSentiment.Positive);
            profileStatistics.ProfileToxicityLabel.Should().Be(KnownSentiment.NonToxic);

            profileStatistics.TopWords.Should().HaveCount(15);
            profileStatistics.TopWords.First(x => x.Text == "love").Count.Should().Be(2);
            profileStatistics.TopWords.First(x => x.Text == "together").Count.Should().Be(2);
            profileStatistics.TopWords.First(x => x.Text == "breakfast").Count.Should().Be(1);
            profileStatistics.TopWords.FirstOrDefault(x => x.Text == "we").Should().BeNull();

            profileStatistics.Records.Should().HaveCount(5);
        }

        private void SetupMocks()
        {
            var tweets = new[]
            {
                "This is my bunny, love it so much!",
                "Hate you",
                "Tomorrow we will have a breakfast together",
                "Love this place, but sometimes it sucks",
                "Together we are stronger"
            };

            var entries = tweets.Select(tweet => new TextEntry
            {
                OriginId = Guid.NewGuid().ToString(),
                Text = tweet
            }).ToArray();

            _fixture.TwitterAdapterMock.Setup(x => x.GetAllTweets(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(() =>
                {
                    return tweets.Select(tweet => new TweetResponse
                    {
                        Id = new Random().Next(),
                        Text = tweet
                    }).ToList();
                });

            _fixture.GeneralizationServiceMock.Setup(x => x.UnifyContent(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(() => 
                {
                    return entries.Select(entry => new GeneralizationResponse
                    {
                        Origin = entry.Text,
                        OriginId = entry.OriginId,
                        Unified = entry.Text
                    });
                });

            _fixture.SentimentServiceMock.Setup(x => x.DetectPositiveSentiments(It.IsAny<IEnumerable<TextEntry>>()))
                .ReturnsAsync(() =>
                {
                    return entries.Select(x => new TextSentiment
                    {
                        OriginId = x.OriginId,
                        SentimentScore = new Dictionary<string, double>
                        {
                            {KnownSentiment.Positive, 0.9 },
                            {KnownSentiment.Negative, 0 },
                            {KnownSentiment.Neutral, 0 },
                            {KnownSentiment.Mixed, 0 },
                        }
                    });
                });

            _fixture.SentimentServiceMock.Setup(x => x.DetectToxicSentiments(It.IsAny<IEnumerable<TextEntry>>()))
                .ReturnsAsync(() =>
                {
                    return entries.Select(x => new TextToxicity
                    {
                        OriginId = x.OriginId,
                        Score = 0.1f,
                        Text = KnownSentiment.NonToxic
                    });
                });
        }
    }
}
