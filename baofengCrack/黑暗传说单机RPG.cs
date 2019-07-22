using MakC.Common;
using MakC.Data;
using MakC.Data.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace baofengCrack
{
    public class 黑暗传说单机RPG
    {

        public static void dooooo(string action, JObject ReqJo, JObject Rep)
        {
            if (action.EndsWith("account/account_upload.php"))
            {
                byte[] bodyData = System.Text.Encoding.ASCII.GetBytes(ReqJo["data"].ToString());
                JObject jo = JsonStringToJsonObj(DecrptyPostData(bodyData));
                JObject data1 = (JObject)JsonConvert.DeserializeObject(jo["data1"].ToString());
                var dbh = DbContext.Get();

                黑暗传说单机RPG_Model.账号 account = dbh.GetEntityDB<黑暗传说单机RPG_Model.账号>().GetSingle(ii => ii.username == data1["username"].ToString());
                if (account == null)
                {
                    account = new 黑暗传说单机RPG_Model.账号();
                    account.ime = data1["basevalue"]["ime"].ToString();
                    account.playerid = data1["basevalue"]["playerid"].ToString();
                    account.versionCode = data1["basevalue"]["versioncode"].ToString().AsInt();                   
                    account.data = data1["playersave"].ToString(/*Formatting.None*/);
                    dbh.Db.Insertable(account).ExecuteCommandIdentityIntoEntity();
                }
                else
                {
                    account.ime = data1["basevalue"]["ime"].ToString();
                    account.playerid = data1["basevalue"]["playerid"].ToString();
                    account.data = data1["playersave"].ToString(/*Formatting.None*/);
                    dbh.Db.Updateable(account).ExecuteCommand();
                }

                黑暗传说单机RPG_Model.存档 cd = new 黑暗传说单机RPG_Model.存档();
                cd.accountid = account.id;
                cd.playerid = data1["basevalue"]["playerid"].ToString();
                cd.data = data1["playersave"].ToString(Formatting.None);
                cd.uploadTime = DateTime.Now;
                DbContext.Get().Db.Insertable(cd).ExecuteCommand();

                Rep["ok"] = true;
                JObject data = new JObject();
                data["currtime"] = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                data["server_returncode"] = 131;
                Rep["data"] = PackResponseData(data.ToString(Formatting.None), account.ime, account.playerid );
            }
            else if (action.EndsWith("account/account_login2.php"))
            {
                byte[] bodyData = System.Text.Encoding.ASCII.GetBytes(ReqJo["data"].ToString());
                JObject jo = JsonStringToJsonObj(DecrptyPostData(bodyData));
                JObject data1 = (JObject)JsonConvert.DeserializeObject(jo["data1"].ToString());
                var dbh = DbContext.Get();
                黑暗传说单机RPG_Model.账号 account = dbh.GetEntityDB<黑暗传说单机RPG_Model.账号>().GetSingle(ii => ii.username == data1["username"].ToString());
                if (account == null)
                {
                    account = new 黑暗传说单机RPG_Model.账号();
                    account.ime = data1["basevalue"]["ime"].ToString();
                    account.playerid = data1["basevalue"]["playerid"].ToString();
                    account.versionCode = data1["basevalue"]["versioncode"].ToString().AsInt();
                    account.username = data1["username"].ToString();
                    account.password = data1["userpassword"].ToString();
                    account.addtime = DateTime.Now;
                    dbh.Db.Insertable(account).ExecuteCommand();
                }
                else
                {
                    account.ime = data1["basevalue"]["ime"].ToString();
                    account.playerid = data1["basevalue"]["playerid"].ToString();
                    dbh.Db.Updateable(account).ExecuteCommand();
                }
                Rep["ok"] = true;
                JObject data = new JObject();
                data["currtime"] = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                data["server_returncode"] = 125;  //127失败，125成功
                data["downloadnum"] = 101;
                data["activenum"] = 11;
                Rep["data"] = PackResponseData(data.ToString(Formatting.None), account.ime, account.playerid);
            }else if (action.EndsWith("account/account_download.php"))
            {
                byte[] bodyData = System.Text.Encoding.ASCII.GetBytes(ReqJo["data"].ToString());
                JObject jo = JsonStringToJsonObj(DecrptyPostData(bodyData));
                JObject data1 = (JObject)JsonConvert.DeserializeObject(jo["data1"].ToString());
                var dbh = DbContext.Get();
                string ime = data1["basevalue"]["ime"].ToString();
                string playerid = data1["basevalue"]["playerid"].ToString();
                黑暗传说单机RPG_Model.账号 account = dbh.GetEntityDB<黑暗传说单机RPG_Model.账号>().GetSingle(ii => ii.username == data1["username"].ToString());
                if (account == null || account.password != data1["userpassword"].ToString() || string.IsNullOrEmpty(account.data))
                {
                    Rep["ok"] = true;
                    JObject Errdata = new JObject();
                    Errdata["currtime"] = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                    Errdata["server_returncode"] = 135;  //135 下载失败
                    Rep["data"] = PackResponseData(Errdata.ToString(Formatting.None), ime, playerid);
                    return;
                }

                Rep["ok"] = true;
                JObject data = new JObject();
                data["currtime"] = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                data["server_returncode"] = 136;  //136下载成功
                data["playersave"] = account.data;
                data["md5"] = MD5Hash(account.data);
                Rep["data"] = PackResponseData(data.ToString(Formatting.None), ime, playerid);
            }else if (action.EndsWith("cdkey/usecdkey5.php"))
            {
                byte[] bodyData = System.Text.Encoding.ASCII.GetBytes(ReqJo["data"].ToString());
                JObject jo = JsonStringToJsonObj(DecrptyPostData(bodyData));
                JObject data1 = (JObject)JsonConvert.DeserializeObject(jo["data1"].ToString());
                var dbh = DbContext.Get();
                string ime = data1["basevalue"]["ime"].ToString();
                string playerid = data1["basevalue"]["playerid"].ToString();
                //黑暗传说单机RPG_Model.账号 account = dbh.GetEntityDB<黑暗传说单机RPG_Model.账号>().GetSingle(ii => ii.username == data1["username"].ToString());
                //if (account == null || account.password != data1["userpassword"].ToString() || string.IsNullOrEmpty(account.data))
                //{
                //    Rep["ok"] = true;
                //    JObject Errdata = new JObject();
                //    Errdata["currtime"] = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                //    Errdata["server_returncode"] = 135;  //135 下载失败
                //    Rep["data"] = PackResponseData(Errdata.ToString(Formatting.None), ime, playerid);
                //    return;
                //}

                Rep["ok"] = true;
                JObject data = new JObject();
                data["cdkvalue"] = 5086;
                data["server_returncode"] = 0;  //136下载成功
                data["tem"] = "";                
                Rep["data"] = PackResponseData(data.ToString(Formatting.None), ime, playerid);
            }else if (action.EndsWith("monster_new/exchange7.php"))
            {
                byte[] bodyData = System.Text.Encoding.ASCII.GetBytes(ReqJo["data"].ToString());
                JObject jo = JsonStringToJsonObj(DecrptyPostData(bodyData));
                JObject data1 = (JObject)JsonConvert.DeserializeObject(jo["data1"].ToString());
                var dbh = DbContext.Get();
                string ime = data1["basevalue"]["ime"].ToString();
                string playerid = data1["basevalue"]["playerid"].ToString();
                //黑暗传说单机RPG_Model.账号 account = dbh.GetEntityDB<黑暗传说单机RPG_Model.账号>().GetSingle(ii => ii.username == data1["username"].ToString());
                //if (account == null || account.password != data1["userpassword"].ToString() || string.IsNullOrEmpty(account.data))
                //{
                //    Rep["ok"] = true;
                //    JObject Errdata = new JObject();
                //    Errdata["currtime"] = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                //    Errdata["server_returncode"] = 135;  //135 下载失败
                //    Rep["data"] = PackResponseData(Errdata.ToString(Formatting.None), ime, playerid);
                //    return;
                //}

                Rep["ok"] = true;
                JObject data = new JObject();
                data["currtime"] = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                data["server_returncode"] = 112; //113失败
                data["bantype"] = 0;
                data["point"] = 0;                
                Rep["data"] = PackResponseData(data.ToString(Formatting.None), ime, playerid);
            }
        }

        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToLower();
            }
        }
        public static string PackResponseData(string Jsondata, string ime, string playerid)
        {
            string data1 = Jsondata;

            string data0 = MD5Hash(data1 + "dfs&dw42a)5idsa+jfslIfdmg50kma015kjimlv*daamwffxdkjs$50alf" + ime + playerid + "koiqnxlnm87951dhpamhgnxhalsid");
            string data2 = "b3c8ae906d087c5f951e7194780ba0c5";
            string data3 = MD5Hash(data0 + data1 + data2 + "aslfwoeri*)412348128:JFKDSOI");

            JObject Resdata = new JObject();
            Resdata["data0"] = data0;
            Resdata["data1"] = data1;
            Resdata["data2"] = data2;
            Resdata["data3"] = data3;
            string RepJsonStr = Resdata.ToString(Formatting.None);
            return EncrptyPostResult(RepJsonStr);
        }

        private static JObject JsonStringToJsonObj(string JsonStr)
        {
            JObject jo = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    jo = (JObject)JsonConvert.DeserializeObject(JsonStr);
                    break;
                }
                catch (Exception)
                {
                    JsonStr = JsonStr.Remove(JsonStr.Length - 1);
                }
            }
            return jo;
        }

        public static byte[] x_EncrptyData(byte[] data, int startIdx, int endIdx, string key)
        {
            var Tmpkeys = System.Text.Encoding.ASCII.GetBytes(key);
            for (int i = startIdx; i < Math.Min(data.Length, endIdx); i++)
            {
                data[i] ^= Tmpkeys[i % Tmpkeys.Length];
                if (data[i] == 0)
                {
                    data[i] = Tmpkeys[i % Tmpkeys.Length];
                }
            }
            return data;
        }

        public static string EncrptyPostData(byte[] data)
        {
            byte[] outdata = x_EncrptyData(data, 0, data.Length, "fasmlkjgamp912qifoxfesdifjqpe347rmlzsdjf*iasfomghauha+difojaflkas-dflksajdfi");
            outdata = x_EncrptyData(outdata, 0, data.Length, "oadfpiucmeuywnmgsjkdojkgsdlkgpihquhzpjfiojfkojdfkpkfpqjhifdahukxlmgphpsmckjaiuw");
            return System.Text.Encoding.ASCII.GetString(outdata);
        }

        public static string DecrptyPostData(byte[] data)
        {
            byte[] outdata = x_EncrptyData(data, 0, data.Length, "oadfpiucmeuywnmgsjkdojkgsdlkgpihquhzpjfiojfkojdfkpkfpqjhifdahukxlmgphpsmckjaiuw");
            outdata = x_EncrptyData(outdata, 0, data.Length, "fasmlkjgamp912qifoxfesdifjqpe347rmlzsdjf*iasfomghauha+difojaflkas-dflksajdfi");
            return System.Text.Encoding.ASCII.GetString(outdata);
        }

        public static string DecrptyPostResult(byte[] data)
        {
            byte[] outdata = x_EncrptyData(data, 0, data.Length, "fgoj89u432jizjdfdx9fowkrks8iuqp,dzp2&xcmufewu#2@(fdsamjxcioijiwojek9asdfj8fjsdmkzcvuhsudaifdsiojwqpke");
            outdata = x_EncrptyData(outdata, 0, data.Length, "pfdsijeruh389245u7uahrfj34uj53-9a89uhjfhusd238u4usdaji0fwmckli*)jdfaoi5897");
            return System.Text.Encoding.ASCII.GetString(outdata);
        }
        public static string EncrptyPostResult(string data)
        {
            var byteDta = Encoding.ASCII.GetBytes(data);
            byte[] outdata = x_EncrptyData(byteDta, 0, byteDta.Length, "pfdsijeruh389245u7uahrfj34uj53-9a89uhjfhusd238u4usdaji0fwmckli*)jdfaoi5897");
            outdata = x_EncrptyData(outdata, 0, byteDta.Length, "fgoj89u432jizjdfdx9fowkrks8iuqp,dzp2&xcmufewu#2@(fdsamjxcioijiwojek9asdfj8fjsdmkzcvuhsudaifdsiojwqpke");
            return System.Text.Encoding.ASCII.GetString(outdata);
        }

    }
}
