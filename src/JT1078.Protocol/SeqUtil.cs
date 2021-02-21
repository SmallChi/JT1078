using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol
{
    public static class SeqUtil
    {
        static readonly ConcurrentDictionary<string, int> counterDict;
        static SeqUtil()
        {
            counterDict = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }
        public static ushort Increment(string terminalPhoneNo)
        {
            return (ushort)counterDict.AddOrUpdate(terminalPhoneNo, 0, (id, count) => count + 1);
        }
        public static ushort Reset(string terminalPhoneNo)
        {
            return (ushort)counterDict.AddOrUpdate(terminalPhoneNo, 0, (id, count) => 0);
        }
    }
}
