using MarkoStudio.Twist.TwitterApi;
using MarkoStudio.Twist.Common.ParameterStore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace MarkoStudio.Twist
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var env = context.HostingEnvironment;
                    builder
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile($"appsettings.Secrets.json", true, reloadOnChange: false)
                        .AddJsonFile($"appsettings.Api.json", true, reloadOnChange: false)
                        .AddJsonFile($"appsettings.Api.{env.EnvironmentName}.json", true, reloadOnChange: false)
                        .AddParameterStoreConfig(Configuration.GetParameterStoreParams())
                        .AddEnvironmentVariables();
                })
                .UseStartup<Startup>();
    }
}
