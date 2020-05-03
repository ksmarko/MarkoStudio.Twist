using System;
using Amazon;
using Amazon.Comprehend;
using MarkoStudio.Twist.SentimentAnalysis.PerspectiveApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MarkoStudio.Twist.SentimentAnalysis
{
    public static class Configuration
    {
        public static List<string> GetRequiredParams()
        {
            return new List<string>
            {
                "Twist_Perspective_ApiKey",
                "Twist_AWS_AccessKey",
                "Twist_AWS_SecretKey"
            };
        }

        public static void ConfigureSentimentAnalysisServices(this IServiceCollection services, IConfiguration configuration)
        {
            var awsAccessKey = configuration.GetSection("Twist_AWS_AccessKey").Value;
            var awsSecretKey = configuration.GetSection("Twist_AWS_SecretKey").Value;

            if (string.IsNullOrEmpty(awsAccessKey))
                throw new ArgumentException("Value cannot be null or empty", nameof(awsAccessKey));

            if (string.IsNullOrEmpty(awsSecretKey))
                throw new ArgumentException("Value cannot be null or empty", nameof(awsSecretKey));

            services.AddSingleton<IAmazonComprehend>(p => new AmazonComprehendClient(awsAccessKey, awsSecretKey, RegionEndpoint.USWest2));

            services.AddSingleton<IPerspectiveClient, PerspectiveClient>();

            var perspectiveApiKey = configuration.GetSection("Twist_Perspective_ApiKey").Value;

            if (string.IsNullOrEmpty(perspectiveApiKey))
                throw new ArgumentException("Value cannot be null or empty", nameof(perspectiveApiKey));

            services.AddOptions<PerspectiveApiOptions>()
                .Configure(options =>
                {
                    options.ApiKey = perspectiveApiKey;
                });
        }
    }
}
