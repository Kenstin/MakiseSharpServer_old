using System;
using System.Text;
using System.Threading.Tasks;
using MakiseSharpServer.Models.Travis;
using MakiseSharpServer.Tests.DataSources;
using Microsoft.Extensions.Caching.Memory;
using RichardSzalay.MockHttp;
using Xunit;

namespace MakiseSharpServer.Tests
{
    public class TravisMessageVerifySignatureShould : IDisposable
    {
        private readonly IMemoryCache cache;
        private readonly MockHttpMessageHandler mockHttp;

        public TravisMessageVerifySignatureShould()
        {
            var memoryCacheOptions = new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.MaxValue,
                CompactOnMemoryPressure = false
            };
            cache = new MemoryCache(memoryCacheOptions);
            mockHttp = new MockHttpMessageHandler();
        }

        [Theory]
        [MemberData(nameof(TravisMessageVerifySignatureDataSource.TestData), MemberType = typeof(TravisMessageVerifySignatureDataSource))]
        public async Task WorkWithoutCache(string message, string signature, string response, bool expected)
        {
            mockHttp.When("/config")
                .Respond("application/json", response);
            using (var client = mockHttp.ToHttpClient())
            {
                var model = new TravisMessageModel(message, signature, cache, Encoding.ASCII, client);
                Assert.Equal(await model.VerifySignature(), expected);
            }
        }

        [Theory]
        [MemberData(nameof(TravisMessageVerifySignatureDataSource.TestData), MemberType = typeof(TravisMessageVerifySignatureDataSource))]
        public async Task UseCache(string message, string signature, string response, bool expected)
        {
            var pkey = new byte[] {48, 130, 1, 34, 48, 13, 6, 9, 42, 134, 72, 134, 247, 13, 1, 1, 1, 5, 0, 3, 130, 1, 15, 0, 48, 130, 1, 10, 2, 130, 1, 1, 0, 157, 5, 54, 143, 217, 103, 70, 220, 174, 91, 126, 154, 172, 211, 156, 119, 48, 179, 200, 165, 98, 172, 181, 34, 223, 246, 139, 135, 165, 31, 157, 53, 87, 205, 57, 241, 241, 225, 212, 156, 25, 245, 101, 228, 37, 236, 72, 119, 39, 122, 170, 117, 178, 10, 151, 169, 233, 75, 110, 206, 55, 48, 178, 220, 242, 28, 126, 58, 75, 61, 165, 114, 25, 168, 8, 47, 199, 60, 94, 0, 196, 188, 30, 57, 1, 235, 2, 210, 139, 21, 63, 152, 36, 93, 9, 15, 80, 205, 193, 218, 186, 212, 154, 157, 64, 103, 49, 190, 22, 234, 52, 72, 7, 141, 157, 46, 245, 128, 222, 183, 83, 118, 235, 47, 215, 205, 252, 196, 194, 92, 146, 122, 78, 243, 45, 220, 106, 220, 37, 74, 185, 186, 174, 122, 168, 215, 137, 78, 200, 168, 192, 174, 158, 138, 37, 133, 81, 39, 225, 123, 90, 154, 164, 231, 76, 22, 103, 4, 179, 210, 127, 71, 85, 208, 254, 199, 49, 108, 9, 203, 113, 103, 229, 242, 90, 135, 252, 200, 204, 116, 35, 30, 210, 61, 215, 217, 150, 12, 78, 163, 135, 177, 189, 220, 64, 100, 146, 116, 195, 240, 100, 103, 139, 89, 95, 142, 107, 24, 201, 237, 58, 201, 250, 184, 170, 48, 3, 78, 130, 160, 129, 26, 155, 214, 82, 117, 31, 163, 78, 58, 0, 35, 177, 238, 158, 174, 225, 241, 92, 221, 164, 98, 186, 166, 10, 85, 2, 3, 1, 0, 1};
            cache.Set(TravisType.Com, pkey);
            if (expected == false)
            {
                mockHttp.Expect("/config")
                    .Respond("application/json", response);
            }
            using (var client = mockHttp.ToHttpClient())
            {
                var model = new TravisMessageModel(message, signature, cache, Encoding.ASCII, client);
                Assert.Equal(await model.VerifySignature(), expected);
                mockHttp.VerifyNoOutstandingExpectation();
            }
        }

        [Theory]
        [MemberData(nameof(TravisMessageVerifySignatureDataSource.TestData), MemberType = typeof(TravisMessageVerifySignatureDataSource))]
        public async Task FetchNewKeyGivenOutOfDateCache(string message, string signature, string response, bool expected)
        {
            var pkey = new byte[] {48, 130, 1, 34, 48, 13, 6, 9, 42, 134, 72, 134, 247, 13, 1, 1, 1, 5, 0, 3, 130, 1, 15, 0, 48, 130, 1, 10, 2, 130, 1, 1, 0, 157, 5, 54, 143, 217, 103, 70, 220, 174, 91, 126, 154, 172, 211, 156, 119, 48, 179, 200, 165, 98, 172, 181, 34, 223, 246, 139, 135, 165, 31, 157, 53, 87, 205, 57, 241, 241, 225, 212, 156, 25, 245, 101, 228, 37, 236, 72, 119, 39, 122, 170, 117, 178, 10, 151, 169, 233, 75, 110, 206, 55, 48, 178, 220, 242, 28, 126, 58, 76, 61, 165, 114, 25, 168, 8, 47, 199, 60, 94, 0, 196, 188, 30, 57, 1, 235, 2, 210, 139, 21, 63, 152, 36, 93, 9, 15, 80, 205, 193, 218, 186, 212, 154, 157, 64, 103, 49, 190, 22, 234, 52, 72, 7, 141, 157, 46, 245, 128, 222, 183, 83, 118, 235, 47, 215, 205, 252, 196, 194, 92, 146, 122, 78, 243, 45, 220, 106, 220, 37, 74, 185, 186, 174, 122, 168, 215, 137, 78, 200, 168, 192, 174, 158, 138, 37, 133, 81, 39, 225, 123, 90, 154, 164, 231, 76, 22, 103, 4, 179, 210, 127, 71, 85, 208, 254, 199, 49, 108, 9, 203, 113, 103, 229, 242, 90, 135, 252, 200, 204, 116, 35, 30, 210, 61, 215, 217, 150, 12, 78, 163, 135, 177, 189, 220, 64, 100, 146, 116, 195, 240, 100, 103, 139, 89, 95, 142, 107, 24, 201, 237, 58, 201, 250, 184, 170, 48, 3, 78, 130, 160, 129, 26, 155, 214, 82, 117, 31, 163, 78, 58, 0, 35, 177, 238, 158, 174, 225, 241, 92, 221, 164, 98, 186, 166, 10, 85, 2, 3, 1, 0, 1};
            cache.Set(TravisType.Com, pkey);

            mockHttp.Expect("/config")
                .Respond("application/json", response);

            using (var client = mockHttp.ToHttpClient())
            {
                var model = new TravisMessageModel(message, signature, cache, Encoding.ASCII, client);
                Assert.Equal(await model.VerifySignature(), expected);
                mockHttp.VerifyNoOutstandingExpectation();
            }
        }

        public void Dispose()
        {
            cache.Dispose();
            mockHttp.Dispose();
        }
    }
}