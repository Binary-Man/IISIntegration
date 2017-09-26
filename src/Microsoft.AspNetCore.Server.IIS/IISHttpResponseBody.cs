using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Server.IIS
{
    internal class IISHttpResponseBody : Stream
    {
        private readonly HttpProtocol _httpContext;

        public IISHttpResponseBody(HttpProtocol httpContext)
        {
            _httpContext = httpContext;
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => throw new NotSupportedException();

        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override void Flush()
        {
            FlushAsync(CancellationToken.None).GetAwaiter().GetResult();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public unsafe override void Write(byte[] buffer, int offset, int count)
        {
            WriteAsync(buffer, offset, count, CancellationToken.None).GetAwaiter().GetResult();
        }

        public override unsafe Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _httpContext.WriteAsync(new ArraySegment<byte>(buffer, offset, count), cancellationToken);
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return _httpContext.FlushAsync(cancellationToken);
        }
    }
}
