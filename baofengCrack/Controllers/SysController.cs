using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace baofengCrack.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SysController : Controller
    {
        [Route("/login")]
        [AllowAnonymous]
        public IActionResult login()
        {

            return View();
        }
        [HttpPost("/login")]
        [AllowAnonymous]
        public IActionResult dologin()
        {
            var identity = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                    {
                            new Claim(ClaimTypes.Sid,"0123456789"),
                            new Claim(ClaimTypes.Role,"Admin"),
                            new Claim(ClaimTypes.Name,"admin's name"),
                            new Claim(ClaimTypes.WindowsAccountName,"admin"),
                            new Claim(ClaimTypes.UserData,"user.UpLoginDate.ToString()")
                    }, CookieAuthenticationDefaults.AuthenticationScheme)
            );
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, identity, 
                new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(7)) // 有效时间
            });
            return Redirect("/lst");
        }

        [Route("/login/test")]
        public IActionResult test()
        {

            return Content("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }
    }
}