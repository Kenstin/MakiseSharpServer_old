using System;
using MakiseSharpServer.Services;
using Xunit;
using MakiseSharpServer.Tests.DataSources;
using MakiseSharpServer.Utility;

namespace MakiseSharpServer.Tests
{
    public class SHA1ServiceShould
    {
        [Theory]
        [MemberData(nameof(SHA1ServiceDataSource.TestData), MemberType = typeof(SHA1ServiceDataSource))]
        public void VerifySignature(string hash, string signature, string publickey, bool pass)
        {
            Assert.Equal(pass, SHA1Service.VerifySignature(HexString.ToByteArray(hash), Convert.FromBase64String(signature), publickey));
        }
    }
}
