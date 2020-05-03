using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarkoStudio.Twist.TwitterApi
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

            services.AddSingleton<ITwitterClient, TwitterClient>();
        }
    }
}
