using System.Net.Http;

namespace MarkoStudio.Twist.Client
{
    public interface ITwistClient
    {
        IStatisticsApi Statistics { get; }
    }

    public class TwistClient : ITwistClient
    {
        public IStatisticsApi Statistics { get; }

        public TwistClient()
        {
            var httpClient = new HttpClient();

            Statistics = new StatisticsApi(httpClient);
        }
    }
}
