using System.Collections.Generic;
using System.Linq;
using MarkoStudio.Twist.Application.Services;
using MarkoStudio.Twist.Generalization;
using MarkoStudio.Twist.SentimentAnalysis;
using MarkoStudio.Twist.TwitterClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarkoStudio.Twist.Application
{
    public static class Setup
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureTwitterClientServices(configuration);
            services.ConfigureSentimentAnalysisServices(configuration);
            services.ConfigureGeneralizationServices(configuration);

            services.AddScoped<StatisticsService>();
        }

        public static IEnumerable<string> GetRequiredParams()
        {
            return TwitterClient.Configuration.GetRequiredParams()
                .Concat(Generalization.Configuration.GetRequiredParams())
                .Concat(SentimentAnalysis.Configuration.GetRequiredParams());
        }
    }
}
