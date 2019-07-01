using System.Buffers;

namespace JT1078.Protocol
{
    internal static class JT1078ArrayPool
    {
        private readonly static ArrayPool<byte> ArrayPool;

        static JT1078ArrayPool()
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
