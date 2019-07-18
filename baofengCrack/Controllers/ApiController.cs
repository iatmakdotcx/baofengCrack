using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MakC.Common;
using MakC.Data;
using MakC.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace baofengCrack.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private const int APIVERSION = 1;

        [HttpGet]
        public string Get()
        {
            return "nothing here";
        }
        [HttpGet("/api/test")]
        public JObject test()
        {
            JObject Rep = new JObject();
            Rep["ok"] = false;
            Rep["msg"] = "nothing";
            return Rep;
        }

        [HttpPost]
        public JObject Post([FromBody] JObject ReqJo)
        {
            JObject Rep = new JObject();
            Rep["ok"] = false;
            Rep["msg"] = "";
            try
            {
                switch (ReqJo["a"].ToString())
                {
                    case "v":
                        checkVersion(ReqJo, Rep);
                        break;
                    default:
                        gameAction(ReqJo, Rep);
                        break;
                }
            }
            finally
            {
                if ((bool)Rep["ok"] == false && Rep["msg"].ToString() == "")
                {
                    Rep["msg"] = "系统错误";
                }
            }
            return Rep;
        }

        private void checkVersion(JObject ReqJo, JObject Rep)
        {
            Rep["v"] = APIVERSION;
            Rep["ok"] = true;
            Rep["s"] = new JArray(GlobalSettings.gameServers.Select(i => i.Value));
        }
        private void gameAction(JObject ReqJo, JObject Rep)
        {
            string action;
            string urlHost = priseUrl(ReqJo["url"].ToString(), out action);
            if (string.IsNullOrEmpty(urlHost))
            {
                Rep["skip"] = "1";
                return;
            }
            //string serverName = GlobalSettings.getServerName(urlHost);
            //if (string.IsNullOrEmpty(serverName))
            //{
            //    Rep["skip"] = "1";
            //    return;
            //}
            switch (action.ToLower())
            {
                case "login":
                    gameAction_login(urlHost, ReqJo, Rep);
                    break;
                case "download_save":
                    gameAction_download_save(urlHost, ReqJo, Rep);
                    break;
                case "upload_save":
                    gameAction_upload_save(urlHost, ReqJo, Rep);
                    break;
                case "player_update_info":
                    gameAction_player_update_info(urlHost, ReqJo, Rep);
                    break;
                case "giftmanager_confirm_gift_code":
                    gameAction_giftmanager_confirm_gift_code(urlHost, ReqJo, Rep);
                    break;
                case "player_check_use_item":
                case "player_upload_use_item_info":
                    gameAction_player_check_use_item(urlHost, ReqJo, Rep);
                    break;
                default:
                    break;
            }
        }

        private void gameAction_login(string urlHost, JObject ReqJo, JObject Rep)
        {
            string bodyData = ReqJo["data"].ToString();
            string user_id = bodyData.GetQueryStringValue("user_id");
            var dbc = DbContext.Get();
            BaofengUser user = dbc.GetEntityDB<BaofengUser>().GetSingle(i => i.userid == user_id && i.ServerName == urlHost);
            if (user == null)
            {
                user = new BaofengUser();
            }
            //提交真实请求
            string errMsg = "";
            string repdata = comFunc.PostData(ReqJo["url"].ToString(), bodyData, out errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                Rep["skip"] = "1";
                return;
            }
            string player_id = repdata.GetQueryStringValue("player_id");
            int int_id;
            if (!int.TryParse(player_id, out int_id))
            {
                Rep["skip"] = "1";
                return;
            }
            user.player_id = int_id;
            user.is_cheat = repdata.GetQueryStringValue("is_cheat").Asbool();
            user.userName = repdata.GetQueryStringValue("user_name");

            user.logintoken = bodyData.GetQueryStringValue("login_token");
            if (user.id == 0)
            {
                user.userid = user_id;
                user.ServerName = urlHost;
                user.usertoken = bodyData.GetQueryStringValue("user_token");
                user.account_platform = bodyData.GetQueryStringValue("account_platform");
                dbc.Db.Insertable(user).ExecuteCommand();
            }
            else
            {
                dbc.Db.Updateable(user).ExecuteCommand();
            }
            //被ban的账号，删除ban标志
            if (user.is_cheat)
            {                
                repdata = repdata.Replace("is_cheat=1", "is_cheat=");
            }
            Rep["ok"] = true;
            Rep["data"] = repdata;
        }
        private void gameAction_download_save(string urlHost, JObject ReqJo, JObject Rep)
        {
            string bodyData = ReqJo["data"].ToString();
            string user_id = bodyData.GetQueryStringValue("user_id");
            var dbc = DbContext.Get();
            BaofengUser user = dbc.GetEntityDB<BaofengUser>().GetSingle(i => i.userid == user_id && i.ServerName == urlHost);
            if (user == null)
            {
                Rep["skip"] = "1";
                return;
            }
           
            StringBuilder localSaveData = new StringBuilder();
            localSaveData.Append("is_cheat=&result=1&server_time_stamp=");
            localSaveData.Append(((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString());
            bool force_download = bodyData.GetQueryStringValue("force_download").Asbool();
            if (user.isHold || force_download)
            {
                user.isHold = false;
                dbc.Db.Updateable(user).ExecuteCommand();
                //打包本地存档
                localSaveData.Append("&savefile_data=");
                BaofengUserdata userdata = dbc.GetEntityDB<BaofengUserdata>().GetSingle(i => i.userId == user.id);
                if (userdata == null)
                {
                    Rep["skip"] = "1";
                    return;
                }
                Dictionary<string, string> fileDict = new Dictionary<string, string>();
                fileDict.Add("actor", userdata.actor);
                fileDict.Add("amulet", userdata.amulet);
                fileDict.Add("bag", userdata.bag);
                fileDict.Add("level", userdata.level);
                fileDict.Add("mission", userdata.mission);
                fileDict.Add("other", userdata.other);
                fileDict.Add("player", userdata.player);
                fileDict.Add("practice", userdata.practice);
                fileDict.Add("setting", userdata.setting);
                fileDict.Add("store", userdata.store);
                fileDict.Add("uid", userdata.uid);
                var zipbytes = comFunc.Util__ZipFile(fileDict);
                localSaveData.Append(Convert.ToBase64String(zipbytes));
            }
            Rep["ok"] = true;
            Rep["data"] = localSaveData.ToString();
        }
        private void gameAction_upload_save(string urlHost, JObject ReqJo, JObject Rep)
        {
            string bodyData = ReqJo["data"].ToString();
            string user_id = bodyData.GetQueryStringValue("user_id");
            var dbc = DbContext.Get();
            BaofengUser user = dbc.GetEntityDB<BaofengUser>().GetSingle(i => i.userid == user_id && i.ServerName == urlHost);
            if (user == null)
            {
                Rep["skip"] = "1";
                return;
            }
            string datazipStr = bodyData.GetQueryStringValue("file");

            BaofengUserdata bfd;
            comFunc.ResaveUserData(HttpUtility.UrlDecode(datazipStr), user, out bfd);

            //StringBuilder sbreoStr = new StringBuilder();
            //sbreoStr.Append("is_cheat=&result=1&server_time_stamp=");
            //sbreoStr.Append(((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString());
            //Rep["ok"] = true;
            //Rep["data"] = sbreoStr.ToString();
        }
        private void gameAction_player_update_info(string urlHost, JObject ReqJo, JObject Rep)
        {
            string bodyData = ReqJo["data"].ToString();
            string user_id = bodyData.GetQueryStringValue("user_id");
            var dbc = DbContext.Get();
            BaofengUser user = dbc.GetEntityDB<BaofengUser>().GetSingle(i => i.userid == user_id && i.ServerName == urlHost);
            if (user == null || !user.isHold)
            {
                Rep["skip"] = "1";
                return;
            }

            Rep["ok"] = true;
            Rep["data"] = "data=/QgpzIVkygFBgBDv6ajAgiLHw==";
        }
        private void gameAction_giftmanager_confirm_gift_code(string urlHost, JObject ReqJo, JObject Rep)
        {
            string bodyData = ReqJo["data"].ToString();
            int player_id = bodyData.GetQueryStringValue("player_id").AsInt();
            string cd_key = bodyData.GetQueryStringValue("cd_key");
            var dbc = DbContext.Get();
            BaofengGifcode Gifcode = dbc.GetEntityDB<BaofengGifcode>().GetSingle(i => i.gifcode == cd_key && (i.player_id == player_id || i.player_id==0));
            if (Gifcode == null)
            {
                Rep["skip"] = "1";
                return;
            }
            Gifcode.usedCount++;
            if (Gifcode.canUseCount > 0)
            {
                Gifcode.player_id = player_id;
                if (Gifcode.usedCount > Gifcode.canUseCount)
                {
                    Rep["skip"] = "1";
                    return;
                }
            }
            dbc.Db.Updateable(Gifcode).ExecuteCommand();
            Rep["ok"] = true;
            Rep["data"] = "result=1&gift_id=" + Gifcode.gifid;
        }
        private void gameAction_player_check_use_item(string urlHost, JObject ReqJo, JObject Rep)
        {
            string bodyData = ReqJo["data"].ToString();
            string data = HttpUtility.UrlDecode(bodyData.GetQueryStringValue("data"));
            JObject obj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(comFunc.DecodeParams(data));
            int playerId = obj["pid"].ToString().AsInt();
            var dbc = DbContext.Get();
            BaofengUser user = dbc.GetEntityDB<BaofengUser>().GetSingle(i => i.player_id == playerId && i.ServerName == urlHost);
            if (user == null)
            {
                Rep["skip"] = "1";
                return;
            }

            Rep["ok"] = true;
            Rep["data"] = "data=/QgpzIVkyhB5b4tG5hkBwYAqTgeywkIix8=";
        }
        private string priseUrl(string url,out string action)
        {
            action = "";
            int splIdx = url.LastIndexOf("/");
            if (splIdx > 0)
            {
                action = url.Substring(splIdx + 1);
                return url.Substring(0, splIdx);
            }
            return "";
        }
    }
}
