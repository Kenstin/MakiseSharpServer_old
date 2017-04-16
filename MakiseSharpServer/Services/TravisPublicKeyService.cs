using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MakiseSharpServer.Models.Travis;
using Newtonsoft.Json.Linq;

namespace MakiseSharpServer.Services
{
    public class TravisPublicKeyService
    {
        public static readonly Dictionary<TravisType, Uri> TravisLinks = new Dictionary<TravisType, Uri>(2)
        {
            { TravisType.Com, new Uri("https://api.travis-ci.com/config") },
            { TravisType.Org, new Uri("https://api.travis-ci.org/config") }
        };

        private readonly TravisType type;
        private readonly HttpClient client;
        private string key;

        private TravisPublicKeyService(TravisType type, HttpClient client)
        {
            this.type = type;
            this.client = client;
        }

        public static async Task<TravisPublicKeyService> Create(TravisType travisType, HttpClient client)
        {
            var obj = new TravisPublicKeyService(travisType, client);
            await obj.FetchPublicKey();
            return obj;
        }

        public byte[] ToBytes() => Convert.FromBase64String(Utility.Keys.Dearmor(key));

        public async Task FetchPublicKey()
        {
                string body = await client.GetStringAsync(TravisLinks[type]);
                dynamic json = JObject.Parse(body);
                key = json.config.notifications.webhook.public_key;
        }
    }
}
