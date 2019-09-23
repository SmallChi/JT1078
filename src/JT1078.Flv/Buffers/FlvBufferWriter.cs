using System;

namespace JT1078.Flv
{
    /// <summary>
    /// <see cref="System.Buffers.Writer"/>
    /// </summary>
    ref partial struct FlvBufferWriter
    {
        private Span<byte> _buffer;
        public FlvBufferWriter(Span<byte> buffer)
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
