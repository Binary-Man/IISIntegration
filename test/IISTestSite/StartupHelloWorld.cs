using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IISTestSite
{
    public class StartupHelloWorld
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.Run(async ctx =>
            {
                if (ctx.Request.Path.Value.StartsWith("/Path"))
                {
                    await ctx.Response.WriteAsync(ctx.Request.Path.Value);
                    return;
                }
                if (ctx.Request.Path.Value.StartsWith("/Query"))
                {
                    await ctx.Response.WriteAsync(ctx.Request.QueryString.Value);
                    return;
                }
                //if (ctx.Request.Path.Value.StartsWith("/BodyLimit"))
                //{
                //    await ctx.Response.WriteAsync(
                //        ctx.Features.Get<IHttpMaxRequestBodySizeFeature>()?.MaxRequestBodySize?.ToString() ?? "null");
                //    return;
                //}

                //if (ctx.Request.Path.StartsWithSegments("/Auth"))
                //{
                //    var iisAuth = Environment.GetEnvironmentVariable("ASPNETCORE_IIS_HTTPAUTH");
                //    var authProvider = ctx.RequestServices.GetService<IAuthenticationSchemeProvider>();
                //    var authScheme = (await authProvider.GetAllSchemesAsync()).SingleOrDefault();
                //    if (string.IsNullOrEmpty(iisAuth))
                //    {
                //        await ctx.Response.WriteAsync("backcompat;" + (authScheme?.Name ?? "null"));
                //    }
                //    else
                //    {
                //        await ctx.Response.WriteAsync("latest;" + (authScheme?.Name ?? "null"));
                //    }
                //    return;
                //}

                await ctx.Response.WriteAsync("Hello World");
            });
        }
    }
}
