using MarkoStudio.Twist.Client;
using MarkoStudio.Twist.Generalization;
using MarkoStudio.Twist.SentimentAnalysis;
using MarkoStudio.Twist.TwitterClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Tests
{
    public class TwistFixture
    {
        public TwistClient ApiClient { get; private set; }

        public Mock<ITwitterAdapter> TwitterAdapterMock { get; private set; }

        public Mock<IGeneralizationService> GeneralizationServiceMock { get; private set; }

        public Mock<ISentimentService> SentimentServiceMock { get; private set; }

        private TestServer _server;

        public TwistFixture()
        {
            TwitterAdapterMock = new Mock<ITwitterAdapter>();
            GeneralizationServiceMock = new Mock<IGeneralizationService>();
            SentimentServiceMock = new Mock<ISentimentService>();
        }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Tests.json", true, false)
                .AddEnvironmentVariables()
                .Build();

            var adminService = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .ConfigureServices(ConfigureServices)
                .UseEnvironment("Local");

            _server = new TestServer(adminService);

            ApiClient = GetClient();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITwitterAdapter>(p => TwitterAdapterMock.Object);

            services.AddScoped<IGeneralizationService>(p => GeneralizationServiceMock.Object);

            services.AddScoped<ISentimentService>(p => SentimentServiceMock.Object);
        }

        public TwistClient GetClient()
        {
            var client = _server.CreateClient();
            return new TwistClient(client);
        }

        public virtual Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
