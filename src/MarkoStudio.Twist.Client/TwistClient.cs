using System.Net.Http;

namespace MarkoStudio.Twist.Client
{
    /// <summary>
    /// Represents API to access Twist application
    /// </summary>
    public interface ITwistClient
    {
        /// <summary>
        /// API to get Twitter profile statistics
        /// </summary>
        IStatisticsApi Statistics { get; }
    }

    /// <inheritdoc/>
    public class TwistClient : ITwistClient
    {
        /// <inheritdoc/>
        public IStatisticsApi Statistics { get; }

        public TwistClient(HttpClient httpClient)
        {
            Statistics = new StatisticsApi(httpClient);
        }
    }
}
