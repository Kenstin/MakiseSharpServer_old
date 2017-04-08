using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MakiseSharpServer.Utility
{
    public class StringStream
    {
        private const int LengthBytes = 4;
        private static readonly Encoding StreamEncoding;

        static StringStream()
        {
            StreamEncoding = new UnicodeEncoding();
        }

        public static async Task<string> ReadMessage(Stream ioStream)
        {
            var length = new byte[4];
            if (await ioStream.ReadAsync(length, 0, LengthBytes) != 4)
            {
                return string.Empty;
            }

            var len = BitConverter.ToInt32(length, 0);
            var message = new byte[len];
            int read = 0;
            do
            {
                read += await ioStream.ReadAsync(message, read, len - read);
            }
            while (read < len);

            return StreamEncoding.GetString(message);
        }

        public static async Task<int> WriteMessage(string message, Stream ioStream)
        {
            if (string.IsNullOrEmpty(message))
            {
                return 0;
            }

            var buffer = StreamEncoding.GetBytes(message);
            int len = buffer.Length; //overflow

            var length = BitConverter.GetBytes(len);
            await ioStream.WriteAsync(length, 0, LengthBytes);
            await ioStream.WriteAsync(buffer, 0, len);
            await ioStream.FlushAsync();

            return LengthBytes + len;
        }
    }
}
