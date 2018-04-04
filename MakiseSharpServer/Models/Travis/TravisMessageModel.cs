using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MakiseSharpServer.Services;
using MakiseSharpServer.Utility;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace MakiseSharpServer.Models.Travis
{
    public class TravisMessageModel
    {
        private readonly TravisType type;
        private readonly IMemoryCache cache;
        private readonly byte[] digestedMessage;
        private readonly byte[] signature;
        private readonly HttpClient client;

        public TravisMessageModel(string message, string signature, IMemoryCache cache, Encoding encoding, HttpClient client)
        {
            digestedMessage = Sha1.DigestMessage(message, encoding);
            type = GetTravisType(message);
            this.signature = Convert.FromBase64String(signature);
            this.cache = cache;
            this.client = client;
        }

        public static TravisType GetTravisType(string message)
        {
            dynamic json = JObject.Parse(message);
            string buildUrl = json.build_url;
            var match = Regex.Match(buildUrl, "https?:\\/\\/travis-ci.(com|org)\\/");
            return (TravisType)Enum.Parse(typeof(TravisType), match.Groups[1].ToString(), true);
        }

        public async Task<bool> VerifySignature() //Try from cache and fallback to downloading new pkey
        {
            var pkeyBytes = await cache.GetOrCreateAsync(type, async cacheEntry =>
                (await TravisPublicKeyService.Create(type, client)).KeyAsBytes());

            if (Sha1.VerifySignature(digestedMessage, signature, pkeyBytes))
            {
                return true;
            }

            pkeyBytes = (await TravisPublicKeyService.Create(type, client)).KeyAsBytes();

            if (!Sha1.VerifySignature(digestedMessage, signature, pkeyBytes))
            {
                return false;
            }

            cache.Set(type, pkeyBytes);
            return true;
        }
    }
}
