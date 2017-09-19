using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Xunit;

namespace IISTestSite
{
    public class StartupRequestTests
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.Run(async ctx =>
            {
                var requestInfo = ctx.Features.Get<IHttpRequestFeature>();
                //Assert.Equal("GET", requestInfo.Method);
                //Assert.Equal(Stream.Null, requestInfo.Body);
                //Assert.NotNull(requestInfo.Headers);
                //Assert.Equal("http", requestInfo.Scheme);
                //Assert.Equal("/basepath", requestInfo.PathBase);
                //Assert.Equal("/SomePath", requestInfo.Path);
                //Assert.Equal("?SomeQuery", requestInfo.QueryString);
                //Assert.Equal("/basepath/SomePath?SomeQuery", requestInfo.RawTarget);
                //Assert.Equal("HTTP/1.1", requestInfo.Protocol);

                //var connectionInfo = ctx.Features.Get<IHttpConnectionFeature>();
                //Assert.Equal("::1", connectionInfo.RemoteIpAddress.ToString());
                //Assert.NotEqual(0, connectionInfo.RemotePort);
                //Assert.Equal("::1", connectionInfo.LocalIpAddress.ToString());
                //Assert.NotEqual(0, connectionInfo.LocalPort);
                //Assert.NotNull(connectionInfo.ConnectionId);

                //// Trace identifier
                //var requestIdentifierFeature = ctx.Features.Get<IHttpRequestIdentifierFeature>();
                //Assert.NotNull(requestIdentifierFeature);
                //Assert.NotNull(requestIdentifierFeature.TraceIdentifier);

                await ctx.Response.WriteAsync("Hello World");
            });
        }
    }
}
