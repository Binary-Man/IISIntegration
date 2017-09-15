using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Xunit;

namespace Microsoft.AspNetCore.Server.IIS.Tests
{
    public class IISHttpServerTests
    {
        [Theory]
        [InlineData("http://localhost:5000")]
        public void StartWarnsWhenIgnoringIServerAddressesFeature(string ignoredAddress)
        {

            // Directly configuring an endpoint using Listen causes the IServerAddressesFeature to be ignored.
            using (var server = new IISHttpServer(applicationLifetime: null))
            {
                server.Features.Get<IServerAddressesFeature>().Addresses.Add(ignoredAddress);
                StartDummyApplication(server);
            }
        }

        private static void StartDummyApplication(IServer server)
        {
            server.StartAsync(new DummyApplication(context => Task.CompletedTask), CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
