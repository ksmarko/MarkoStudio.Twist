using MarkoStudio.Twist.Client.Models;
using MarkoStudio.Twist.Common.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Client
{
    public interface IStatisticsApi
    {
        Task<ProfileStatisticsResponse> GetProfileStatistics(string userName);
    }

    public class StatisticsApi : IStatisticsApi
    {
        private readonly HttpClient _httpClient;

        public StatisticsApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProfileStatisticsResponse> GetProfileStatistics(string userName)
        {
            return await _httpClient.Get<ProfileStatisticsResponse>($"api/statistics/profile?userName={userName}");
        }
    }
}
