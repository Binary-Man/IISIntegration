using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http.Features;

namespace Microsoft.AspNetCore.Server.IIS
{
    public interface IIISHttpContext
    {
        string ConnectionId { get; set; }
        IFeatureCollection ConnectionFeatures { get; set; }
        PipeFactory PipeFactory { get; set; }
        IPEndPoint RemoteEndPoint { get; set; }
        IPEndPoint LocalEndPoint { get; set; }
        // TODO consider having ServiceContext like Kestrel (which contains Serveroptions, trace, and Threadpool?
    }
}
