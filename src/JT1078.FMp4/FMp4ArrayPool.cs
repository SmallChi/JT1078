using System.Buffers;

namespace JT1078.FMp4
{
    internal static class FMp4ArrayPool
    {
        private readonly static ArrayPool<byte> ArrayPool;

        static FMp4ArrayPool()
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
