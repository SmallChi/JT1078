using System.Buffers;

namespace JT1078.Flv
{
    internal static class FlvArrayPool
    {
        private readonly static ArrayPool<byte> ArrayPool;

        static FlvArrayPool()
        {
            ArrayPool = ArrayPool<byte>.Create();
        }

        public static byte[] Rent(int minimumLength)
        {
            return ArrayPool.Rent(minimumLength);
        }

        public static void Return(byte[] array, bool clearArray = false)
        {
            ArrayPool.Return(array, clearArray);
        }
    }
}
