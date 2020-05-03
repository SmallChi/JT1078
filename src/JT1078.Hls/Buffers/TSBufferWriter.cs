using System;

namespace JT1078.Hls
{
    /// <summary>
    /// <see cref="System.Buffers.Writer"/>
    /// </summary>
    ref partial struct TSBufferWriter
    {
        private Span<byte> _buffer;
        public TSBufferWriter(Span<byte> buffer)
        {
            _buffer = buffer;
            WrittenCount = 0;
        }
        public Span<byte> Free => _buffer.Slice(WrittenCount);
        public Span<byte> Written => _buffer.Slice(0, WrittenCount);
        public int WrittenCount { get; private set; }
        public void Advance(int count)
        {
            WrittenCount += count;
        }
    }
}
