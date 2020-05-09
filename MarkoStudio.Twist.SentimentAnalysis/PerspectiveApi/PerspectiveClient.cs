using MarkoStudio.Twist.Common.Http;
using MarkoStudio.Twist.SentimentAnalysis.PerspectiveApi.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.SentimentAnalysis.PerspectiveApi
{
    public interface IPerspectiveClient
    {
        Task<PerspectiveAnalysisResponse> Analyze(PerspectiveAnalysisRequest request);
    }

    public class PerspectiveClient : IPerspectiveClient
    {
        private readonly HttpClient _httpClient;

        private readonly PerspectiveApiOptions _perspectiveApiOptions;

        public PerspectiveClient(IOptions<PerspectiveApiOptions> options)
        {
            _httpClient = new HttpClient();

            _perspectiveApiOptions = options.Value;
        }

        public async Task<PerspectiveAnalysisResponse> Analyze(PerspectiveAnalysisRequest request)
        {
            var url = $"https://commentanalyzer.googleapis.com/v1alpha1/comments:analyze?key={_perspectiveApiOptions.ApiKey}";

            var analyzeRequest = new AnalyzeRequest
            {
                Comment = new AnalyzeComment
                {
                    Text = request.Text
                },
                RequestedAttributes = new Dictionary<string, object>
                {
                    {KnownPerspectiveAnalysisAttributes.Toxicity, new object()},
                    {KnownPerspectiveAnalysisAttributes.IdentityAttack, new object()},
                    {KnownPerspectiveAnalysisAttributes.SexuallyExplicit, new object()},
                },
                Languages = new [] {"en"}
            };

            var analyzeResponse = await _httpClient.Post<AnalyzeResponse>(url, analyzeRequest);

            var response = new PerspectiveAnalysisResponse
            {
                Text = request.Text,
                AttributeScores = analyzeResponse.AttributeScores.ToDictionary(
                    x => x.Key, 
                    x => x.Value.SummaryScore.Value)
            };

            return response;
        }
    }
}
