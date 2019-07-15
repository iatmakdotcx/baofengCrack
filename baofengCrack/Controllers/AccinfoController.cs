using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using baofengCrack.Models;
using Microsoft.Extensions.Options;
using MakC.Data.Model;
using MakC.Data;
using Newtonsoft.Json.Linq;
using MakC.Common;

namespace baofengCrack.Controllers
{
    public class AccinfoController : Controller
    {
        public AccinfoController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [Route("/[action]/{userid}/{servername}/{refresh=}")]
        public IActionResult Accinfo(string userid, string servername,string refresh="")
        {
            string svrHost = GlobalSettings.getServerHost(servername);

            var dbc = DbContext.Get();
            BaofengUser user = dbc.GetEntityDB<BaofengUser>().GetSingle(i => i.userid == userid && i.ServerName == svrHost);
            if (user == null)
            {
                return NotFound();
            }
            BaofengUserdata bfd = null;            
            if (!"refresh".Equals(refresh) || user.is_cheat)  //如果账号已被ban，服务器没有最新存档
            {
                bfd = dbc.GetEntityDB<BaofengUserdata>().GetSingle(i => i.userId == user.id);
            }
            if (bfd == null)
            {
                string errmsg = comFunc.Req_Create_download_save(user, out bfd);
                if (!string.IsNullOrEmpty(errmsg))
                {
                    return View(new AccInfoModel()
                    {
                        ErrMsg = errmsg,
                        user = user
                    });
                }
            }
            var Bag = comFunc.BagLua2Json(bfd.bag);

            return View(new AccInfoModel() {
                Bag = (JObject)Bag["ret"]["itemDatas"],
                user = user
            });
        }

        [HttpPost("/accinfo/{userid}/{servername}/save")]
        public JObject save([FromBody]JObject data, string userid, string servername)
        {
            JObject resObj = new JObject();
            resObj["ok"] = false;
            resObj["msg"] = "";
            string svrHost = GlobalSettings.getServerHost(servername);

            var dbc = DbContext.Get();
            BaofengUser user = dbc.GetEntityDB<BaofengUser>().GetSingle(i => i.userid == userid && i.ServerName == svrHost);
            if (user == null)
            {
                resObj["msg"] = "未找到用户";
                return resObj;
            }
            BaofengUserdata bfd = dbc.GetEntityDB<BaofengUserdata>().GetSingle(i => i.userId == user.id);
            if (bfd == null)
            {
                string errmsg = comFunc.Req_Create_download_save(user, out bfd);
                if (!string.IsNullOrEmpty(errmsg))
                {
                    resObj["msg"] = errmsg;
                    return resObj;
                }
            }
            JObject bagContext = comFunc.BagLua2Json(bfd.bag);
            foreach (var item in data)
            {
                bagContext["ret"]["itemDatas"][item.Key] = item.Value;
            }
            bfd.bag = comFunc.Json2BagLua(bagContext);

            user.isHold = true;
            dbc.Db.Updateable(user).ExecuteCommand();
            dbc.Db.Updateable(bfd).ExecuteCommand();
            resObj["ok"] = true;
            return resObj;
        }
        [HttpPost("/accinfo/{userid}/{servername}/checklogin")]
        public JObject checklogin([FromBody]JObject data, string userid, string servername)
        {
            JObject resObj = new JObject();
            resObj["ok"] = false;
            resObj["msg"] = "";
            string svrHost = GlobalSettings.getServerHost(servername);

            var dbc = DbContext.Get();
            BaofengUser user = dbc.GetEntityDB<BaofengUser>().GetSingle(i => i.userid == userid && i.ServerName == svrHost);
            if (user == null)
            {
                resObj["msg"] = "未找到用户";
                return resObj;
            }
            comFunc.Req_Create_Login(user);

            resObj["ok"] = true;
            return resObj;
        }

        [Route("/lst")]
        public IActionResult lst()
        {
            var dbc = DbContext.Get();
            List<BaofengUser> user = dbc.GetEntityDB<BaofengUser>().GetList();
            if (user == null)
            {
                return NotFound();
            }
            return View(new UserListModel()
            {
                users = user
            });
        }
    }
}
