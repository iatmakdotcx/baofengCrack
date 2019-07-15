
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;
using MakC.Common;
using MakC.Data;
using MakC.Data.Model;
using Neo.IronLua;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace baofengCrack
{
    public class comFunc
    {
        public static JObject BagLua2Json(string luaval)
        {
            LuaStrConvertor lsc = new LuaStrConvertor(luaval);
            JObject jo = lsc.doPrise();
            DecrptyJsonNumber(jo);
            return jo;
        }
        public static string Json2BagLua(JObject jObject)
        {
            EncrptyJsonNumber(jObject);
            return "do local " + Jobject2LuaTable(jObject) + " return ret end";
        }
        private static string Jobject2LuaTable(JObject jObject)
        {
            StringBuilder sbData = new StringBuilder();
            foreach (var item in jObject)
            {
                sbData.Append(",").Append(item.Key).Append("=");
                if (item.Value is JObject)
                {
                    sbData.Append("{");
                    sbData.Append(Jobject2LuaTable((JObject)item.Value));
                    sbData.Append("}");
                }
                else
                {
                    UInt64 tmpInt;
                    if (UInt64.TryParse(item.Value.ToString(), out tmpInt))
                    {
                        sbData.Append(tmpInt);
                    }
                    else
                        sbData.Append(item.Value);
                }
            }
            if (sbData.Length > 0)
            {
                return sbData.ToString().Substring(1);
            }
            return "";
        }
        public static void DecrptyJsonNumber(JObject jObject)
        {
            foreach (var item in jObject)
            {
                if (item.Key == "eds")
                {
                    foreach (var EncrptyItem in (JObject)item.Value)
                    {
                        UInt64 tmpInt;
                        if (UInt64.TryParse(EncrptyItem.Value.ToString(), out tmpInt))
                        {
                            item.Value[EncrptyItem.Key] = DecrptyNumber(tmpInt);
                        }
                    }
                }else if (item.Value is JObject)
                {
                    DecrptyJsonNumber((JObject)item.Value);
                }
            }
        }
        public static void EncrptyJsonNumber(JObject jObject)
        {
            foreach (var item in jObject)
            {
                if (item.Key == "eds")
                {
                    foreach (var EncrptyItem in (JObject)item.Value)
                    {
                        int tmpInt;
                        if (int.TryParse(EncrptyItem.Value.ToString(), out tmpInt))
                        {
                            item.Value[EncrptyItem.Key] = EncrptyNumber(tmpInt);
                        }
                    }
                }
                else if (item.Value is JObject)
                {
                    EncrptyJsonNumber((JObject)item.Value);
                }
            }
        }
        public static int DecrptyNumber(UInt64 itemCount)
        {
            if (itemCount < 0x10000)
            {
                itemCount ^= 0x6A2C;
            }
            else
            {
                //  A7 C100E558   ==>  C1A7E558 
                var tmp = (itemCount & 0xff00000000) >> 16;
                itemCount = (itemCount & 0xff00ffff) | tmp;
                itemCount ^= 0xC1A6E98B;
            }
            return (int)itemCount;
        }
        public static UInt64 EncrptyNumber(int itemCount)
        {
            if (itemCount < 0x10000)
            {
                return (ulong)(itemCount ^ 0x6A2C);
            }
            else
            {
                // C1A7E558  ==>  A7 C100E558
                var tmoLL = (itemCount ^ 0xC1A6E98B);
                ulong tmp = (ulong)(tmoLL & 0x00FF0000) << 16;
                return (ulong)(tmoLL & 0xff00ffff) | tmp;
            }
        }

        public static void ResaveUserData(string dataStr, BaofengUser user, out BaofengUserdata bfd)
        {
            bfd = null;

            byte[] bytes = Convert.FromBase64String(dataStr);
            var dicts = Util__UnzipFile(bytes);

            var dbc = DbContext.Get();
            bfd = dbc.GetEntityDB<BaofengUserdata>().GetSingle(i => i.userId == user.id);
            if (bfd == null)
            {
                bfd = new BaofengUserdata();
                bfd.userId = user.id;
            }
            else
            {
                BaofengUserdata_bak bfk = new BaofengUserdata_bak();
                bfk.userId = bfd.userId;
                bfk.actor = bfd.actor;
                bfk.amulet = bfd.amulet;
                bfk.bag = bfd.bag;
                bfk.level = bfd.level;
                bfk.mission = bfd.mission;
                bfk.other = bfd.other;
                bfk.player = bfd.player;
                bfk.practice = bfd.practice;
                bfk.setting = bfd.setting;
                bfk.store = bfd.store;
                bfk.uid = bfd.uid;
                bfk.cdate = bfd.updateDate;
                dbc.Db.Insertable(bfk).ExecuteCommand();
            }
            bfd.actor = dicts["actor"];
            bfd.amulet = dicts["amulet"];
            bfd.bag = dicts["bag"];
            bfd.level = dicts["level"];
            bfd.mission = dicts["mission"];
            bfd.other = dicts["other"];
            bfd.player = dicts["player"];
            bfd.practice = dicts["practice"];
            bfd.setting = dicts["setting"];
            bfd.store = dicts["store"];
            bfd.uid = dicts["uid"];
            bfd.updateDate = DateTime.Now;
            if (bfd.id > 0)
            {
                dbc.Db.Updateable(bfd).ExecuteCommand();
            }
            else
            {
                dbc.Db.Insertable(bfd).ExecuteCommand();
            }
        }
        public static string Req_Create_Login(BaofengUser user)
        {
            string url = user.ServerName + "/login"; 

            Dictionary<string, string> data = new Dictionary<string, string>();
            data["user_token"] = "";
            data["login_time_stamp"] = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
            data["login_token"] = user.logintoken;
            data["user_id"] = user.userid;
            data["account_platform"] = user.account_platform;
            string errMsg = "";
            string repdata = PostData(url, data, out errMsg);
            if (!string.IsNullOrEmpty(repdata))
            {
                string player_id = repdata.GetQueryStringValue("player_id");
                int int_id;
                if (int.TryParse(player_id, out int_id))
                {
                    user.player_id = int_id;
                    user.is_cheat = repdata.GetQueryStringValue("is_cheat").Asbool();
                    user.userName = repdata.GetQueryStringValue("user_name");
                    DbContext.Get().Db.Updateable(user).ExecuteCommand();
                }
            }
            return errMsg;
        }
        public static string Req_Create_download_save(BaofengUser user,out BaofengUserdata bfd)
        {
            bfd = null;
            string url = user.ServerName + "/download_save";

            Dictionary<string, string> data = new Dictionary<string, string>();
            data["login_time_stamp"] = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
            data["login_token"] = user.logintoken;
            data["user_id"] = user.userid;
            data["force_download"] = "1";
            string errMsg = "";
            string repdata = PostData(url, data, out errMsg);
            if (!string.IsNullOrEmpty(repdata))
            {
                string savefile_data = repdata.GetQueryStringValue("savefile_data");
                if (!string.IsNullOrEmpty(savefile_data))
                {                    
                    user.is_cheat = repdata.GetQueryStringValue("is_cheat").Asbool();
                    DbContext.Get().Db.Updateable(user).ExecuteCommand();

                    ResaveUserData(savefile_data, user, out bfd);
                    return "";
                }
                else
                    return repdata;
            }
            return errMsg;
        }
        public static string PostData(string url, string data, out string errMsg)
        {
            errMsg = "";
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, err) => true;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "idle/1900 CFNetwork/894 Darwin/17.4.0";                
                request.Headers["X-Unity-Version"] = "5.6.0f3";

                byte[] buffer = Encoding.UTF8.GetBytes(data);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                errMsg = ex.Message;
            }
            return "";
        }
        private static string PostData(string url, Dictionary<string,string> data, out string errMsg)
        {
            return PostData(url, string.Join("&", data.Select(i => i.Key + "=" + i.Value)), out errMsg);
        }

        #region 加解密含函数
        static byte[] Util__Decrypt2_key = new byte[] { 0x90, 0x83, 0x17, 0xcf, 0xf4, 0x20, 0xa7, 0x32, 0x15 };
        public static byte[] Util_Decrypt2(byte[] data)
        {
            for (int i = 0; i < data.Length / 2; i++)
            {
                var salt = Util__Decrypt2_key[i % Util__Decrypt2_key.Length];
                data[i] ^= salt;
                data[data.Length - 1 - i] ^= data[i];
            }
            return data;
        }
        public static byte[] Util_Encrypt2(byte[] data)
        {
            for (int i = 0; i < data.Length / 2; i++)
            {
                var salt = Util__Decrypt2_key[i % Util__Decrypt2_key.Length];
                data[data.Length - 1 - i] ^= data[i];
                data[i] ^= salt;
            }
            return data;
        }
        public static byte[] fixfile(byte[] alldatas)
        {
            int div2back = alldatas.Length / 2;
            for (int i = 0; i < 4; i++)
            {
                var tmpbyte = alldatas[i];
                alldatas[i] = alldatas[div2back];
                alldatas[div2back] = tmpbyte;
                div2back -= 2;
            }
            return alldatas;
        }
        static string KEY_64 = "Zwqt8XK0";
        static string IV_64 = "9tCsILM3";
        public static byte[] DecryptDataFile(byte[] data)
        {
            byte[] result = null;
            using (DES des = new DESCryptoServiceProvider() { Key = Encoding.UTF8.GetBytes(KEY_64) })
            {
                des.Mode = CipherMode.ECB;
                //des.Padding = PaddingMode.None;
                var tmobj = des.CreateDecryptor();
                result = tmobj.TransformFinalBlock(data, 0, data.Length);
            }
            return result;
        }
        public static byte[] EncryptDataFile(byte[] data)
        {
            byte[] result = null;
            using (DES des = new DESCryptoServiceProvider() { Key = Encoding.UTF8.GetBytes(KEY_64) })
            {
                des.Mode = CipherMode.ECB;
                //des.Padding = PaddingMode.None;
                var tmobj = des.CreateEncryptor();
                result = tmobj.TransformFinalBlock(data, 0, data.Length);
            }
            return result;
        }
        public static Dictionary<string, string> Util__UnzipFile(byte[] filebytes)
        {
            Dictionary<string, string> saveData = new Dictionary<string, string>();
            using (MemoryStream ms = new MemoryStream(filebytes))
            {
                ZipFile fastZip = new ZipFile(ms);
                foreach (ZipEntry item in fastZip)
                {
                    var data = fastZip.GetInputStream(item);
                    MemoryStream Iitemms = new MemoryStream();
                    data.CopyTo(Iitemms);
                    data.Close();
                    var strdata = Encoding.UTF8.GetString(DecryptDataFile(fixfile(Iitemms.ToArray())));
                    Iitemms.Close();
                    saveData.Add(item.Name, strdata);
                }
                fastZip.Close();
            }
            return saveData;
        }
        public static byte[] Util__ZipFile(Dictionary<string, string> fileDict)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipOutputStream zipOutput = new ZipOutputStream(ms))
                {
                    foreach (var item in fileDict)
                    {
                        ZipEntry ze = new ZipEntry(item.Key);
                        ze.DateTime = DateTime.Now;
                        zipOutput.PutNextEntry(ze);
                        var data = fixfile(EncryptDataFile(Encoding.UTF8.GetBytes(item.Value)));
                        zipOutput.Write(data, 0, data.Length);
                    }
                    zipOutput.CloseEntry();
                }
                return ms.ToArray();
            }
        }
        public static string Util__UnzipString(byte[] zipbytes)
        {
            using (var ms = new MemoryStream(zipbytes))
            {
                using (var gzipStream = new GZipInputStream(ms))
                {
                    using (StreamReader reader = new StreamReader(gzipStream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
        public static byte[] Util__zipString(string zipstr)
        {
            return Util__zipString(Encoding.UTF8.GetBytes(zipstr));
        }
        public static byte[] Util__zipString(byte[] zipstr)
        {
            using (var ms = new MemoryStream())
            {
                using (var gzipStream = new GZipOutputStream(ms))
                {
                    gzipStream.Write(zipstr, 0, zipstr.Length);
                    gzipStream.Finish();
                    return ms.ToArray();
                }
            }
        }
        public static string DecodeParams(string data)
        {
            data = "jwgfz" + data;
            byte[] bytes = Convert.FromBase64String(data);
            bytes = Util_Decrypt2(bytes);
            return Util__UnzipString(bytes);
        }
        public static string EecodeParams(string data)
        {
            var tmpstr = Convert.ToBase64String(Util_Encrypt2(Util__zipString(data)));
            return tmpstr.Substring(5);
        }
        #endregion
    }
    class LuaStrConvertor
    {
        private string data = "";
        private int readIdx = 0;
        public LuaStrConvertor(string Luadata)
        {
            readIdx = 0;
            if (Luadata.StartsWith("do local "))
            {
                data = Luadata.Substring(9);
            }
            else
                data = Luadata;
        }
        public JObject doPrise()
        {
            JObject resObj = new JObject();
            string keyNameStr = "";
            string tmpStr = "";
            while (readIdx < data.Length)
            {
                char chr = data[readIdx];
                readIdx++;
                switch (chr)
                {
                    case '=':
                        keyNameStr = tmpStr;
                        tmpStr = "";
                        break;
                    case ',':
                        if (!string.IsNullOrEmpty(keyNameStr) && !string.IsNullOrEmpty(tmpStr))
                        {
                            UInt64 tmpInt;
                            if (UInt64.TryParse(tmpStr, out tmpInt))
                            {
                                resObj[keyNameStr] = tmpInt;
                            }
                            else resObj[keyNameStr] = tmpStr;
                        }
                        tmpStr = "";
                        break;
                    case '{':
                        var nobj = doPrise();
                        if (string.IsNullOrEmpty(keyNameStr))
                        {
                            return nobj;
                        }
                        else
                            resObj[keyNameStr] = nobj;
                        break;
                    case '}':
                        if (!string.IsNullOrEmpty(keyNameStr) && !string.IsNullOrEmpty(tmpStr))
                        {
                            UInt64 tmpInt;
                            if (UInt64.TryParse(tmpStr, out tmpInt))
                            {
                                resObj[keyNameStr] = tmpInt;
                            }
                            else resObj[keyNameStr] = tmpStr;
                        }
                        return resObj;
                    case '"':
                        break;
                    case '\\':
                        tmpStr += data[readIdx];
                        break;
                    default:
                        tmpStr += chr;
                        break;
                }
            }
            return resObj;
        }
    }
}
