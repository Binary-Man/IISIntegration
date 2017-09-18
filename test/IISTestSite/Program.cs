using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Logging;

namespace IISTestSite
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .ConfigureLogging((_, factory) =>
                {
                    factory.AddConsole();
                    factory.AddFilter("Console", level => level >= LogLevel.Information);
                })
                .UseNativeIIS()
                .UseStartup("IISTestSite")
                .Build();

            host.Run();
        }
    }
}
