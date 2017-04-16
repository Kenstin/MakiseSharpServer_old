using System;
using Xunit;
using MakiseSharpServer.Tests.DataSources;
using MakiseSharpServer.Utility;

namespace MakiseSharpServer.Tests
{
    public class UtilitySha1Should
    {
        [Theory]
        [MemberData(nameof(UtilitySha1DataSource.TestData), MemberType = typeof(UtilitySha1DataSource))]
        public void VerifySignature(string hash, string signature, string publickey, bool pass)
        {
            Assert.Equal(pass, Sha1.VerifySignature(HexString.ToByteArray(hash), Convert.FromBase64String(signature), publickey));
        }
    }
}
