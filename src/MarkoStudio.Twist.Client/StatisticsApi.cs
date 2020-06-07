using MarkoStudio.Twist.Client.Models;
using MarkoStudio.Twist.Common.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Client
{
    /// <summary>
    /// API to get Twitter profile statistics
    /// </summary>
    public interface IStatisticsApi
    {
        /// <summary>
        /// Gets Twitter profile statistics
        /// </summary>
        /// <param name="userName">Twitter profile user name to get statistics for</param>
        /// <returns>Profile statistics - general positivity and toxicity, positivity and toxicity per each tweet</returns>
        Task<ProfileStatisticsResponse> GetProfileStatistics(string userName);
    }

    /// <inheritdoc/>
    public class StatisticsApi : IStatisticsApi
    {
        private readonly HttpClient _httpClient;

        public StatisticsApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc/>
        public async Task<ProfileStatisticsResponse> GetProfileStatistics(string userName)
        {
            return await _httpClient.Get<ProfileStatisticsResponse>($"api/statistics/profile?userName={userName}");
        }
    }
}
