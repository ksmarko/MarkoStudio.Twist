using Google.Apis.Services;
using Google.Apis.Translate.v2;
using Google.Cloud.Translation.V2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MarkoStudio.Twist.Generalization
{
    public static class Configuration
    {
        public static List<string> GetParameterStoreParams()
        {
            return new List<string>
            {
                "Twist_Generalization_ApiKey"
            };
        }

        public static void ConfigureGeneralizationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var generalizationApiKey = configuration.GetSection("Twist_Generalization_ApiKey").Value;

            if (string.IsNullOrEmpty(generalizationApiKey))
                throw new ArgumentException("Value cannot be null or empty", nameof(generalizationApiKey));

            services.AddSingleton<TranslationClientImpl>(p =>
            {
                var service = new TranslateService(new BaseClientService.Initializer {ApiKey = generalizationApiKey});
                var client = new TranslationClientImpl(service, TranslationModel.ServiceDefault);

                return client;
            });
        }
    }
}
