using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Server.IntegrationTesting;
using System.IO;
using System;
using Xunit.Sdk;
using Xunit.Abstractions;
using Microsoft.Extensions.Logging.Testing;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Server.IIS.Tests
{
    public class IISHttpServerTests : LoggedTest
    {
        public IISHttpServerTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public Task ANCMINPROCTEST()
        {
            return HelloWorld(RuntimeFlavor.Clr, ApplicationType.Portable);
        }

        private async Task HelloWorld(RuntimeFlavor runtimeFlavor, ApplicationType applicationType)
        {
            var serverType = ServerType.IISExpress;
            var architecture = RuntimeArchitecture.x64;
            var testName = $"HelloWorld_{runtimeFlavor}";
            using (StartLog(out var loggerFactory, testName))
            {
                var logger = loggerFactory.CreateLogger("HelloWorldTest");

                var deploymentParameters = new DeploymentParameters(Helpers.GetTestSitesPath(), serverType, runtimeFlavor, architecture)
                {
                    EnvironmentName = "HelloWorld", // Will pick the Start class named 'StartupHelloWorld',
                    ServerConfigTemplateContent = (serverType == ServerType.IISExpress) ? File.ReadAllText("Http.config") : null,
                    SiteName = "HttpTestSite", // This is configured in the Http.config
                    TargetFramework = runtimeFlavor == RuntimeFlavor.Clr ? "net461" : "netcoreapp2.0",
                    ApplicationType = applicationType
                };

                using (var deployer = ApplicationDeployerFactory.Create(deploymentParameters, loggerFactory))
                {
                    var deploymentResult = await deployer.DeployAsync();
                    deploymentResult.HttpClient.Timeout = TimeSpan.FromSeconds(5);

                    // Request to base address and check if various parts of the body are rendered & measure the cold startup time.
                    var response = await RetryHelper.RetryRequest(() =>
                    {
                        return deploymentResult.HttpClient.GetAsync(string.Empty);
                    }, logger, deploymentResult.HostShutdownToken, retryCount: 30);

                    var responseText = await response.Content.ReadAsStringAsync();
                    try
                    {
                        Assert.Equal("Hello World", responseText);

                        response = await deploymentResult.HttpClient.GetAsync("/Path%3F%3F?query");
                        responseText = await response.Content.ReadAsStringAsync();
                        Assert.Equal("/Path??", responseText);

                        response = await deploymentResult.HttpClient.GetAsync("/Query%3FPath?query?");
                        responseText = await response.Content.ReadAsStringAsync();
                        Assert.Equal("?query?", responseText);

                        response = await deploymentResult.HttpClient.GetAsync("/BodyLimit");
                        responseText = await response.Content.ReadAsStringAsync();
                        Assert.Equal("null", responseText);

                        response = await deploymentResult.HttpClient.GetAsync("/Auth");
                        responseText = await response.Content.ReadAsStringAsync();
                        Assert.True("backcompat;Windows".Equals(responseText) || "latest;null".Equals(responseText), "Auth");
                    }
                    catch (XunitException)
                    {
                        logger.LogWarning(response.ToString());
                        logger.LogWarning(responseText);
                        throw;
                    }
                }
            }
        }
    }
}
