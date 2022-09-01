using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Atomic_Clock.Models;

namespace Atomic_Clock.Controllers
{
    public class TimeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> AtomicClock()
        {
            var time = await GetTime();

            ViewBag.Time = time;

            return View(time);
        }

        private async Task<DateTime> GetTime()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://worldtimeapi.org/api/timezone/Europe/London");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var atomicResponse = JsonConvert.DeserializeObject<AtomicClockResponse>(result);

                return atomicResponse.DateTime;
            }
            else
            {
                var errorCode = response.StatusCode;
            }

            return new DateTime();
        }
    }
}