using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MakiseSharpServer.Models.Travis;
using MakiseSharpServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MakiseSharpServer.Controllers
{
    public class MakiseSharpController : Controller
    {
        private readonly IMakiseSharpPipe pipe;
        private readonly IMemoryCache cache;

        public MakiseSharpController(IMakiseSharpPipe pipe, IMemoryCache cache)
        {
            this.pipe = pipe;
            this.cache = cache;
        }

        // GET: MakiseSharp
        public ActionResult Index()
        {
            return Ok("Makise Sharp! <3");
        }

        // POST: MakiseSharp/TravisWebhook
        [HttpPost]
        public async Task<ActionResult> TravisWebhook([FromForm] TravisWebhookBodyModel data, [FromHeader] string signature)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var model = new TravisMessageModel(data.Payload, signature, cache, Encoding.ASCII, client);
                    if (await model.VerifySignature())
                    {
                        await pipe.SendAsync(data.Payload);
                    }
                    else
                    {
                        return BadRequest("Verifying signature failed.");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}