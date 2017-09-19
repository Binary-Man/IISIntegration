using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Microsoft.AspNetCore.Server.IIS.Tests
{
    public class IISServerMiddlewareTests
    {
        [Fact]
        public void UrlDelayRegisteredAndPreferHostingUrlsSetNative()
        {
            // TODO decide if we actually need this.
            var builder = new WebHostBuilder()
                .UseSetting("TOKEN", "TestToken")
                .UseSetting("PORT", "12345")
                .UseSetting("APPL_PATH", "/")
                .UseNativeIIS()
                .Configure(app =>
                {
                    app.Run(context => Task.FromResult(0));
                });

            Assert.Null(builder.GetSetting(WebHostDefaults.ServerUrlsKey));
            Assert.Null(builder.GetSetting(WebHostDefaults.PreferHostingUrlsKey));

            // Adds a server and calls Build()
            var server = new TestServer(builder);

            Assert.Equal("http://localhost:12345", builder.GetSetting(WebHostDefaults.ServerUrlsKey));
            Assert.Equal("true", builder.GetSetting(WebHostDefaults.PreferHostingUrlsKey));
        }
    }
}
