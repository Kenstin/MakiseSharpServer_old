using Xunit;
using MakiseSharpServer.Utility;
using MakiseSharpServer.Tests.DataSources;

namespace MakiseSharpServer.Tests
{
    public class UtilityKeysShould
    {
        [Theory]
        [MemberData(nameof(DearmorDataSource.TestData), MemberType = typeof(DearmorDataSource))]
        public void Dearmor(string input, string output)
        {
            Assert.Equal(Keys.Dearmor(input), output);
        }
    }
}
