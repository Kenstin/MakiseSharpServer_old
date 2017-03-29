using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakiseSharpServer.Models;
using MakiseSharpServer.Services;
using Microsoft.AspNetCore.Http;
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

       /* // GET: MakiseSharp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }*/

        // POST: MakiseSharp/TravisWebhook
        [HttpPost]
        public async Task<ActionResult> TravisWebhook([FromForm] TravisWebhookBodyModel data)
        {
            try
            {
                await pipe.SendAsync(data.Payload);
                return Ok("OK");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /*
        // POST: MakiseSharp/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}