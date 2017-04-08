using System;
using System.Linq;

namespace MakiseSharpServer.Utility
{
    public static class HexString
    {
        public static byte[] ToByteArray(string hexString)
        {
            return Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
