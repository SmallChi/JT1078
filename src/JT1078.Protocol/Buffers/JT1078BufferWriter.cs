using System;

namespace JT1078.Protocol.Buffers
{
    /// <summary>
    /// <see cref="System.Buffers.Writer"/>
    /// </summary>
    ref partial struct JT1078BufferWriter
    {
        private Span<byte> _buffer;
        public JT1078BufferWriter(Span<byte> buffer)
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
