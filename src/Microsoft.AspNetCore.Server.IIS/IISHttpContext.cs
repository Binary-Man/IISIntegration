// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http.Features;

namespace Microsoft.AspNetCore.Server.IIS
{
    public class IISHttpContext : IIISHttpContext // TODO rename this as IISHttpContext once everything has been abstracted out
    {
        public string ConnectionId { get; set; }
        public IFeatureCollection ConnectionFeatures { get; set; }
        public PipeFactory PipeFactory { get; set; }
        public IPEndPoint RemoteEndPoint { get; set; }
        public IPEndPoint LocalEndPoint { get; set; }
    }
}
