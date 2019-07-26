using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using baofengCrack.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace baofengCrack.Controllers
{
    public class TestController : Controller
    {
        [Route("/test/index")]
        public IActionResult Index()
        {
            byte[] bodyData = Convert.FromBase64String("cgo3Kzwge2V4aTViZmY8LHExImYyeg83LWgtK2BxbWtmITwwMzc1NyBgYSA9Nz5kIT0ULjF6LGVufW8xLDk6OT07X3gkOyBQK21te2piZW54alA1KCIzLwgpKD4oIj81Uzphdmd3c3FiMDs1XiYkMSY8PEouLyg8LCkmVCBsfmZNYiIjN1kgOjc9TjoyfisnRUg3PTcjST89Jis8LC0oMDFJNGABYik4Ji85VCo4S20tLj46LnQbcXd1eWV1eEcxYikxKSxabC0uKjoiJREtGX59f2N4WCAnLWw8P1t9PywtNC45SS58Z2htdnNxZ2UkJydEPiU4PSIwWGQkITk7OyFvfCx5f3Fzamp+U2A4LjtAPTY0Nz0qPz52Oj4zNyM9NygqMkdmLl5jIyYqMigrUjR9YmxmbXV9bGVxKDEheFM0JjclOTpNfjEkPDc2eQNxa2J1bmlvaG4ifW1HNTo/PSs+T28uYSJ/JVlgJy08XCV2L2t8a2x6ZV4zLSsqUDZ1bHNxYmpaOHQEKSYmJ1c8Y39jdXRibHtwZCw3GDomMjVFLTwnfTE/IkokWXhgVWE6J0ctJiwrZQ54aT8oI3wiNCQ0LGxvODNgJGxqYDc6LzggfX5uWj0kMCorIgcwdxQ5MSUWNTBqYT1+fn4jOiA/LGNqJiZuPGlzODFiLTkzJjM0YzwzMDc9Mi8ubBY/OTEnZS4Fcgg=");
            var decodedStr = 黑暗传说单机RPG.DecrptyPostData(bodyData);
            JObject jo = 黑暗传说单机RPG.JsonStringToJsonObj(decodedStr);
            if (jo == null)
            {
                return View(new TestModel() { msg="解密失败",RealJson=decodedStr});                
            }
            JObject data1 = (JObject)JsonConvert.DeserializeObject(jo["data1"].ToString());

            return View(new TestModel() { msg = "成功", RealJson = decodedStr, data1=data1.ToString(Formatting.None) });
        }
        [Route("/test/2")]
        public IActionResult action2()
        {
            int keyId;
            string aEncodeRespData = "2b6274317f125fe7c0c4832238b513838f73tdQ81tTrN4RRhUCSpTY96yr10uysTugtH5AepQiGi3PxlcAJSgUegYWVMOXmgbT5Kw41GAP7WgTUDZd5mN5wKZTvxBt6iwfwKWNytAfy46dyZm9KgbT5KwHQLlTUDZdyZWdnOin3DZT5est6qrTUDZNUTWfyOaNKgb23**";
            var respdata = 三国演义吞噬无界.DecodeResponseData(aEncodeRespData, out keyId);

            return View("index", new TestModel() { msg = "成功", RealJson = "", data1 = respdata.ToString(Formatting.None) });
        }
    }
}