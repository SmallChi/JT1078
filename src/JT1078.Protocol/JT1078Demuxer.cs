using JT1078.Protocol.Enums;
using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JT1078.Protocol
{
    public static class JT1078Demuxer
    {
        private readonly static ConcurrentDictionary<string, JT1078Package> JT1078PackageGroupDict = new ConcurrentDictionary<string, JT1078Package>(StringComparer.OrdinalIgnoreCase);
        public static JT1078Package Demuxer(JT1078Package jT1078Package)
        {
            string cacheKey = jT1078Package.GetKey();
            if (jT1078Package.Label3.SubpackageType == JT1078SubPackageType.分包处理时的第一个包)
            {
                JT1078PackageGroupDict.TryRemove(cacheKey, out _);
                JT1078PackageGroupDict.TryAdd(cacheKey, jT1078Package);
                return default;
            }
            else if (jT1078Package.Label3.SubpackageType == JT1078SubPackageType.分包处理时的中间包)
            {
                if (JT1078PackageGroupDict.TryGetValue(cacheKey, out var tmpPackage))
                {
                    tmpPackage.Bodies.Concat(jT1078Package.Bodies).ToArray();
                    JT1078PackageGroupDict[cacheKey] = tmpPackage;
                }
                return default;
            }
            else if (jT1078Package.Label3.SubpackageType == JT1078SubPackageType.分包处理时的最后一个包)
            {
                if (JT1078PackageGroupDict.TryGetValue(cacheKey, out var tmpPackage))
                {
                    tmpPackage.Bodies.Concat(jT1078Package.Bodies).ToArray();
                    return tmpPackage;
                }
                return default;
            }
            else
            {
                return jT1078Package;
            }
        }
    }
}
