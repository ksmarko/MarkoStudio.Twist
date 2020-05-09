using Google.Cloud.Translation.V2;
using MarkoStudio.Twist.Generalization.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Generalization
{
    public interface IGeneralizationService
    {
        Task<IEnumerable<GeneralizationResponse>> UnifyContent(IEnumerable<string> contentEntries);
    }

    public class GeneralizationService : IGeneralizationService
    {
        private readonly TranslationClientImpl _client;

        public GeneralizationService(TranslationClientImpl client)
        {
            _client = client;
        }
        
        public async Task<IEnumerable<GeneralizationResponse>> UnifyContent(IEnumerable<string> contentEntries)
        {
            var result = new List<GeneralizationResponse>();

            foreach (var text in contentEntries)
            {
                var translated = await _client.TranslateTextAsync(text, "en");

                result.Add(new GeneralizationResponse
                {
                    OriginId = Guid.NewGuid().ToString("N"),
                    Origin = translated.OriginalText,
                    Unified = translated.TranslatedText
                });
            }

            return result;
        }
    }
}
