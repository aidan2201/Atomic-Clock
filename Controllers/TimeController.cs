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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (IsvalidUser(user.Username, user.Password))
            {
                return RedirectToAction("AtomicClock");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        private static bool IsvalidUser(string userName, string passWord)
        {
            return true;
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

                return atomicResponse.DateTime.AddHours(1);
            }
            else
            {
                var errorCode = response.StatusCode;
            }

            return new DateTime();
        }

        public async Task<JsonResult> GetJsonTime()
        {
            var time = await GetTime();

            var jsonTime = JsonConvert.SerializeObject(time);

            return Json(jsonTime, JsonRequestBehavior.AllowGet);
        }
    }
}