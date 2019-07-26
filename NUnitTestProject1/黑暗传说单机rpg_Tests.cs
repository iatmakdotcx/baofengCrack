using baofengCrack;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class 黑暗传说单机rpg_Tests
    {
        [Test]
        public void PostData_Decrypt()
        {
            int keyId;
            var postdata = 黑暗传说单机RPG.DecrptyPostData(System.IO.File.ReadAllBytes(@"C:\Users\Mak-pc\Desktop\a.bin"));
            //var postdata = 黑暗传说单机RPG.DecrptyPostData(Convert.FromBase64String("cgo3Kzwge2V4aTViZmY8LHExImYyeg83LWgtK2BxbWtmITwwMzc1NyBgYSA9Nz5kIT0ULjF6LGVufW8xLDk6OT07X3gkOyBQK21te2piZW54alA1KCIzLwgpKD4oIj81Uzphdmd3c3FiMDs1XiYkMSY8PEouLyg8LCkmVCBsfmZNYiIjN1kgOjc9TjoyfisnRUg3PTcjST89Jis8LC0oMDFJNGABYik4Ji85VCo4S20tLj46LnQbcXd1eWV1eEcxYikxKSxabC0uKjoiJREtGX59f2N4WCAnLWw8P1t9PywtNC45SS58Z2htdnNxZ2UkJydEPiU4PSIwWGQkITk7OyFvfCx5f3Fzamp+U2A4LjtAPTY0Nz0qPz52Oj4zNyM9NygqMkdmLl5jIyYqMigrUjR9YmxmbXV9bGVxKDEheFM0JjclOTpNfjEkPDc2eQNxa2J1bmlvaG4ifW1HNTo/PSs+T28uYSJ/JVlgJy08XCV2L2t8a2x6ZV4zLSsqUDZ1bHNxYmpaOHQEKSYmJ1c8Y39jdXRibHtwZCw3GDomMjVFLTwnfTE/IkokWXhgVWE6J0ctJiwrZQ54aT8oI3wiNCQ0LGxvODNgJGxqYDc6LzggfX5uWj0kMCorIgcwdxQ5MSUWNTBqYT1+fn4jOiA/LGNqJiZuPGlzODFiLTkzJjM0YzwzMDc9Mi8ubBY/OTEnZS4Fcgg="));
            JObject jo = 黑暗传说单机RPG.JsonStringToJsonObj(postdata);
            Assert.IsNotNull(jo);

            Assert.Pass();
        }

    }
}
