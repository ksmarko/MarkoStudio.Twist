using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MarkoStudio.Twist.TwitterClient
{
    public static class Configuration
    {
        public static List<string> GetRequiredParams()
        {
            return new List<string>
            {
                "Twist_Twitter_ConsumerKey",
                "Twist_Twitter_ConsumerSecret"
            };
        }

        public static void ConfigureTwitterClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var consumerKey = configuration.GetSection("Twist_Twitter_ConsumerKey").Value;
            var consumerSecret = configuration.GetSection("Twist_Twitter_ConsumerSecret").Value;

            services.AddOptions<TwitterAuthOptions>()
                .Configure(options =>
                {
                    options.ConsumerKey = consumerKey;
                    options.ConsumerSecret = consumerSecret;
                });

            services.AddSingleton<ITwitterAdapter, TwitterAdapter>();
        }
    }
}
