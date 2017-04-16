using System.Collections.Generic;

namespace MakiseSharpServer.Tests.DataSources
{
    internal class UtilitySha1DataSource
    {
        private static readonly List<object[]> Data
            = new List<object[]>
            {
                new object[]
                {
                    "844562056b39db6d4b59403d6b6b69406f4b8e96",
                    "eNb8Iffr1pSdg5h3i4JdxHfmL+UbIrvB0ivXYRJJM2bfJBI7v+Ea47M6A6rZDhcsk+EhnowRCebnTGLvaXC2n5ultkVOaSwlsVL/snh/6+yHejJF7MsoousSDt2iSxFd/EOi/voV6LjvN/Sg2DCb9G6dHlTLJdak6yL0CKy6Nbt0p72jS9GPg2g+E77QgI88YtsjBJvbvvYu+rHfAfHYBxHQ+N3kDDRZdZnxENX5vG5AljWLZXkm8yH/f+Jto4eNaQgBfK7PX+W6AxGuFQUga5laOsIan+zzTawWEqfQnsrlAcb8AS++dKsmGq9Zol+TKv2Tp17lboY15Jl8PnLn8A==",
                    "-----BEGIN PUBLIC KEY-----\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAnQU2j9lnRtyuW36arNOc\ndzCzyKVirLUi3/aLh6UfnTVXzTnx8eHUnBn1ZeQl7Eh3J3qqdbIKl6npS27ONzCy\n3PIcfjpLPaVyGagIL8c8XgDEvB45AesC0osVP5gkXQkPUM3B2rrUmp1AZzG+Fuo0\nSAeNnS71gN63U3brL9fN/MTCXJJ6TvMt3GrcJUq5uq56qNeJTsiowK6eiiWFUSfh\ne1qapOdMFmcEs9J/R1XQ/scxbAnLcWfl8lqH/MjMdCMe0j3X2ZYMTqOHsb3cQGSS\ndMPwZGeLWV+OaxjJ7TrJ+riqMANOgqCBGpvWUnUfo046ACOx7p6u4fFc3aRiuqYK\nVQIDAQAB\n-----END PUBLIC KEY-----",
                    false
                },
                new object[]
                {
                    "944562056b39db6d4b59403d6b6b69406f4b8e96",
                    "eNb8Iffr1pSdg5h3i4JdxHfmL+UbIrvB0ivXYRJJM2bfJBI7v+Ea47M6A6rZDhcsk+EhnowRCebnTGLvaXC2n5ultkVOaSwlsVL/snh/6+yHejJF7MsoousSDt2iSxFd/EOi/voV6LjvN/Sg2DCb9G6dHlTLJdak6yL0CKy6Nbt0p72jS9GPg2g+E77QgI88YtsjBJvbvvYu+rHfAfHYBxHQ+N3kDDRZdZnxENX5vG5AljWLZXkm8yH/f+Jto4eNaQgBfK7PX+W6AxGuFQUga5laOsIan+zzTawWEqfQnsrlAcb8AS++dKsmGq9Zol+TKv2Tp17lboY15Jl8PnLn8A==",
                    "-----BEGIN PUBLIC KEY-----\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAnQU2j9lnRtyuW36arNOc\ndzCzyKVirLUi3/aLh6UfnTVXzTnx8eHUnBn1ZeQl7Eh3J3qqdbIKl6npS27ONzCy\n3PIcfjpLPaVyGagIL8c8XgDEvB45AesC0osVP5gkXQkPUM3B2rrUmp1AZzG+Fuo0\nSAeNnS71gN63U3brL9fN/MTCXJJ6TvMt3GrcJUq5uq56qNeJTsiowK6eiiWFUSfh\ne1qapOdMFmcEs9J/R1XQ/scxbAnLcWfl8lqH/MjMdCMe0j3X2ZYMTqOHsb3cQGSS\ndMPwZGeLWV+OaxjJ7TrJ+riqMANOgqCBGpvWUnUfo046ACOx7p6u4fFc3aRiuqYK\nVQIDAQAB\n-----END PUBLIC KEY-----",
                    true
                },
                new object[]
                {
                    "944562056b39db6d4b59403d6b6b69406f4b8e96",
                    "fNb8Iffr1pSdg5h3i4JdxHfmL+UbIrvB0ivXYRJJM2bfJBI7v+Ea47M6A6rZDhcsk+EhnowRCebnTGLvaXC2n5ultkVOaSwlsVL/snh/6+yHejJF7MsoousSDt2iSxFd/EOi/voV6LjvN/Sg2DCb9G6dHlTLJdak6yL0CKy6Nbt0p72jS9GPg2g+E77QgI88YtsjBJvbvvYu+rHfAfHYBxHQ+N3kDDRZdZnxENX5vG5AljWLZXkm8yH/f+Jto4eNaQgBfK7PX+W6AxGuFQUga5laOsIan+zzTawWEqfQnsrlAcb8AS++dKsmGq9Zol+TKv2Tp17lboY15Jl8PnLn8A==",
                    "-----BEGIN PUBLIC KEY-----\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAnQU2j9lnRtyuW36arNOc\ndzCzyKVirLUi3/aLh6UfnTVXzTnx8eHUnBn1ZeQl7Eh3J3qqdbIKl6npS27ONzCy\n3PIcfjpLPaVyGagIL8c8XgDEvB45AesC0osVP5gkXQkPUM3B2rrUmp1AZzG+Fuo0\nSAeNnS71gN63U3brL9fN/MTCXJJ6TvMt3GrcJUq5uq56qNeJTsiowK6eiiWFUSfh\ne1qapOdMFmcEs9J/R1XQ/scxbAnLcWfl8lqH/MjMdCMe0j3X2ZYMTqOHsb3cQGSS\ndMPwZGeLWV+OaxjJ7TrJ+riqMANOgqCBGpvWUnUfo046ACOx7p6u4fFc3aRiuqYK\nVQIDAQAB\n-----END PUBLIC KEY-----",
                    false
                }
            };

        public static IEnumerable<object[]> TestData => Data;
    }
}