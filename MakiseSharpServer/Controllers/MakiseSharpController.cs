using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MakiseSharpServer.Models;
using MakiseSharpServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakiseSharpServer.Controllers
{
    public class MakiseSharpController : Controller
    {
        private readonly IMakiseSharpPipe pipe;

        public MakiseSharpController(IMakiseSharpPipe pipe)
        {
            this.pipe = pipe;
        }

        // GET: MakiseSharp
        public ActionResult Index()
        {
            return Ok("Makise Sharp! <3");
        }

        // POST: MakiseSharp/TravisWebhook
        [HttpPost]
        public async Task<ActionResult> TravisWebhook([FromForm] TravisWebhookBodyModel data)
        {
            try
            {
                await pipe.SendAsync(data.Payload);
                return Ok("OK");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}