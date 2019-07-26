using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Jurassic;
using MakC.Common;
using MakC.Data;
using MakC.Data.Model;
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
    public class 三国演义吞噬无界
    {
        private static string DedeDataJsCode = @"var CHARS62Obj = {0: 0,1: 1,2: 2,3: 3,4: 4,5: 5,6: 6,7: 7,8: 8,9: 9,a: 10,b: 11,c: 12,d: 13,e: 14,f: 15,g: 16,h: 17,i: 18,j: 19,k: 20,l: 21,m: 22,n: 23,o: 24,p: 25,q: 26,r: 27,s: 28,t: 29,u: 30,v: 31,w: 32,x: 33,y: 34,z: 35,A: 36,B: 37,C: 38,D: 39,E: 40,F: 41,G: 42,H: 43,I: 44,J: 45,K: 46,L: 47,M: 48,N: 49,O: 50,P: 51,Q: 52,R: 53,S: 54,T: 55,U: 56,V: 57,W: 58,X: 59,Y: 60,Z: 61};

var this_UrlKey1 = 'YUDzDypmlTmpd4a0E9CWkWt1oLcIcRLk'
var this_UrlKey2 = 'OGW69fq2BUVEedmh2ogWLFafuqYSjNY7hkLPXmk8UwjpwTgp8hy1flh9wHAHgw'


	getCtKey = function(e) {
var i = getK1([ this_UrlKey1, e ]);
return getK1([ i, this_UrlKey2 ]);
}
getK1= function(t) {
	var e = '', 
	i = t[0], 
	a = t[1]; //随机62
	for (var n = 0; n < i.length; n++) 
		e += a.charAt(string62to10(i.charAt(n)));
	return e;
}
string62to10 =function(t) {
for (var e = (t = String(t)).length, i = 0, a = 0; i < e; ) a += Math.pow(62, i++) * CHARS62Obj[t.charAt(e - i)];
return a;
}
	_keyStr= 'KLMNOPQR_abcdefUVWFGHIJstqr345XYZ0wxyz12DEuvijklmnopgh6789-ABCST*',
	_getKey= function(t) {
		if (t) {
			for (var e = this._keyStr.split(''), i = 0; i < t.length; i++) {
				var a = string62to10(t[i]),
				n = i % 2 ? i: 63 - i,
				s = e[n];
				e[n] = e[a];
				e[a] = s;
			}
			return e.join('');
		}
		return this._keyStr;
	}
	encode= function(t, e) {
		if (void 0 != t) {
			var i, a, n, s, l, o, r, c = this._getKey(e),
			u = '',
			d = 0;
			t = this._utf8_encode(t);
			for (; d < t.length;) {
				i = t.charCodeAt(d++);
				a = t.charCodeAt(d++);
				n = t.charCodeAt(d++);
				s = i >> 2;
				l = (3 & i) << 4 | a >> 4;
				o = (15 & a) << 2 | n >> 6;
				r = 63 & n;
				isNaN(a) ? o = r = 64 : isNaN(n) && (r = 64);
				u = u + c.charAt(s) + c.charAt(l) + c.charAt(o) + c.charAt(r);
			}
			return u;
		}
	}
	decode= function(t, e) {
		if (void 0 != t) {
			for (var i, a, n, s, l, o, r, c = this._getKey(e), u = '', d = 0; d < t.length;) {
				s = c.indexOf(t.charAt(d++));
				l = c.indexOf(t.charAt(d++));
				o = c.indexOf(t.charAt(d++));
				r = c.indexOf(t.charAt(d++));
				i = s << 2 | l >> 4;
				a = (15 & l) << 4 | o >> 2;
				n = (3 & o) << 6 | r;
				u += String.fromCharCode(i);
				64 != o && (u += String.fromCharCode(a));
				64 != r && (u += String.fromCharCode(n));
			}
			return u = this._utf8_decode(u);
		}
	}

	_utf8_encode=function(t) {
		t = t.replace(/\r\n/g, '\n');
		for (var e = '',
		i = 0; i < t.length; i++) {
			var a = t.charCodeAt(i);
			if (a < 128) e += String.fromCharCode(a);
			else if (a > 127 && a < 2048) {
				e += String.fromCharCode(a >> 6 | 192);
				e += String.fromCharCode(63 & a | 128);
			} else {
				e += String.fromCharCode(a >> 12 | 224);
				e += String.fromCharCode(a >> 6 & 63 | 128);
				e += String.fromCharCode(63 & a | 128);
			}
		}
		return e;
	}
	_utf8_decode= function(t) {
		var e, i, a, n = '',
		s = 0;
		e = i = 0;
		for (; s < t.length;) if ((e = t.charCodeAt(s)) < 128) {
			n += String.fromCharCode(e);
			s++;
		} else if (e > 191 && e < 224) {
			i = t.charCodeAt(s + 1);
			n += String.fromCharCode((31 & e) << 6 | 63 & i);
			s += 2;
		} else {
			i = t.charCodeAt(s + 1);
			a = t.charCodeAt(s + 2);
			n += String.fromCharCode((15 & e) << 12 | (63 & i) << 6 | 63 & a);
			s += 3;
		}
		return n;
	}";
        public static void dooooo(string action, JObject ReqJo, JObject Rep)
        {
            JArray jArray = new JArray();
            jArray.Add(new JObject(new JProperty("k", "Cache-Control"), new JProperty("v", "private, max-age=0")));
            jArray.Add(new JObject(new JProperty("k", "Server"), new JProperty("v", "Microsoft-IIS/10.0")));
            jArray.Add(new JObject(new JProperty("k", "X-AspNet-Version"),new JProperty("v", "4.0.30319")));

            jArray.Add(new JObject(new JProperty("k", "X-Powered-By"),new JProperty("v", "ASP.NET")));
            jArray.Add(new JObject(new JProperty("k", "Access-Control-Allow-Origin"),new JProperty("v", "Access-Control-Allow-Origin")));
            jArray.Add(new JObject(new JProperty("k", "Access-Control-Allow-Credentials"), new JProperty("v", "true")));
            jArray.Add(new JObject(new JProperty("k", "Access-Control-Allow-Methods"),new JProperty("v", "PUT,POST,GET,DELETE,OPTIONS")));
            jArray.Add(new JObject(new JProperty("k", "Access-Control-Allow-Headers"),new JProperty("v", "Access-Control-Allow-Origin,Origin,X-Requested-With,Content-Type,Accept")));
            Rep["header"] = jArray;

            if (action.EndsWith("WS_Passport.asmx/Login"))
            {
                string postdata = ReqJo["data"].ToString();
                int keyid;
                JObject data1 = DecodeRequestData(postdata,out keyid);
            }else if (action.EndsWith("Service.asmx/UseCdkey"))
            {
                string postdata = ReqJo["data"].ToString();
                int keyid;
                JObject data1 = DecodeRequestData(postdata, out keyid);
                string val = data1["val"].ToString();
                string id = data1["id"].ToString();
                string ss = data1["ss"].ToString();
                string device = data1["device"].ToString();
                string sid = data1["sid"].ToString();
                var dbh = DbContext.Get();
                三国演义吞噬无界_Model.兑换码 cdkey = dbh.GetEntityDB<三国演义吞噬无界_Model.兑换码>().GetSingle(ii => ii.cdkey == val && (ii.playerId == null || ii.playerId == sid));
                if (cdkey == null)
                {
                    return;
                }
                cdkey.usedCount++;
                if (cdkey.canUseCount != -1)
                {
                    cdkey.playerId = sid;
                    if (cdkey.usedCount > cdkey.canUseCount)
                    {
                        return;
                    }
                }
                三国演义吞噬无界_Model.账号 account = dbh.GetEntityDB<三国演义吞噬无界_Model.账号>().GetSingle(ii => ii.sid == sid);
                if (account == null)
                {
                    return;
                }
                dbh.Db.Updateable(cdkey).ExecuteCommand();
                account.bufferData = cdkey.bufferData;
                account.bufferId = 0;
                dbh.Db.Updateable(account).ExecuteCommand();

                JObject ResData = new JObject();
                ResData.Add("result", 0);
                ResData.Add("type", 96);
                //ResData.Add("text", "11已经使用过此类型兑换码11");
                ResData.Add("echo", data1["echo"]);

                Rep["data"] = EncodeResponseData(ResData.ToString(Formatting.None), "OOHVcwo3Yb2Kob3YC38mVxwJ0aKIaWuBBLj7W8YiZRFLxBr3QuQFA1VghXV1MF", keyid);
                Rep["ok"] = true;
            }else if (action.EndsWith("Service.asmx/TouchV2"))
            {
                string postdata = ReqJo["data"].ToString();
                int keyid;
                JObject data1 = DecodeRequestData(postdata, out keyid);
                string sid = data1["sid"].ToString();
                int buffer = data1["buffer"].ToString().AsInt();
                var dbh = DbContext.Get();
                三国演义吞噬无界_Model.账号 account = dbh.GetEntityDB<三国演义吞噬无界_Model.账号>().GetSingle(ii => ii.sid == sid);
                if (account == null)
                {
                    account = new 三国演义吞噬无界_Model.账号();

                    account.tmpid = data1["id"].ToString().AsInt();
                    account.ss = data1["ss"].ToString().AsInt();
                    account.sid = data1["sid"].ToString();                    
                    account.deviceId = data1["device"].ToString();
                    account.username = "";
                    account.password = "";
                    account.bufferData = "";
                    dbh.Db.Insertable(account).ExecuteCommand();
                }
                if (string.IsNullOrEmpty(account.bufferData))
                {
                    return;
                }
                string errMsg;
                string ResponseData = PostData(ReqJo["url"].ToString(), postdata, out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                {
                    JObject data2 = DecodeResponseData(ResponseData, out keyid);
                    if (data2["buffer"] == null)
                    {
                        data2["buffer"] = new JObject();
                    }
                    else
                    {
                        foreach (var item in (JObject)data2["buffer"])
                        {
                            int tmpInt;
                            if(int.TryParse(item.Key,out tmpInt) && tmpInt > buffer)
                            {
                                buffer = tmpInt;
                            }
                        }
                    }
                    buffer += 1;
                    if (buffer < account.bufferId || account.bufferId == 0)
                    {
                        account.bufferId = buffer;
                        dbh.Db.Updateable(account).ExecuteCommand();
                        data2["buffer"][buffer.ToString()] = account.bufferData;   // "[1,3,292,2],[100,50,8,88888]"
                    }
                    data2.Remove("info");
                    Rep["data"] = EncodeResponseData(data2.ToString(Formatting.None), "OOHVcwo3Yb2Kob3YC38mVxwJ0aKIaWuBBLj7W8YiZRFLxBr3QuQFA1VghXV1MF", keyid);
                    Rep["ok"] = true;
                }
            }
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
                request.Accept = "*/*";
                request.UserAgent = "kingwar4-mobile/4.0.14 CFNetwork/894 Darwin/17.4.0";
                request.Headers["Access-Control-Allow-Origin"] = "*";

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
        private static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToLower();
            }
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

        public static JObject DecodeRequestData(string data, out int keyId)
        {
            string md5Str = data.Substring(3, 32);
            string encodePassTable = data.Substring(35, 62);
            string encodedData = data.Substring(97);
            return DecodeData(encodedData, encodePassTable,out keyId);
        }
        public static JObject DecodeResponseData(string data, out int keyId)
        {
            string md5Str = data.Substring(0, 32);
            string encodePassTable = data.Substring(32, 62);
            string encodedData = data.Substring(94);
            return DecodeData(encodedData, encodePassTable,out keyId);
        }
        private static JObject DecodeData(string data, string key, out int keyId)
        {
            keyId = 0;
            IJsEngineSwitcher engineSwitcher = JsEngineSwitcher.Current;
            engineSwitcher.EngineFactories.Add(new JurassicJsEngineFactory());
            engineSwitcher.DefaultEngineName = JurassicJsEngine.EngineName;
            using (IJsEngine engine = JsEngineSwitcher.Current.CreateDefaultEngine())
            {
                engine.Execute(DedeDataJsCode);
                var publickey = engine.CallFunction("getCtKey", key);
                string PostjsonStr = engine.CallFunction("decode", data, publickey).ToString();
                JObject jo;
                if (PostjsonStr.StartsWith("{"))
                {
                    jo = JsonStringToJsonObj(PostjsonStr);
                    if (jo != null)
                    {
                        return jo;
                    }
                }
                keyId = 1;
                engine.Execute("var this_UrlKey1 = 'db0FaVXtwixFUGGQ1Iq9dN7yMrJ9DFHQ';var this_UrlKey2 = 'R8nD2B0DRcT0IoFrA5UqHeHLeFsbrOBvXIVhKmgcXXcDLDrQemyyQLdDpAom9N'");
                publickey = engine.CallFunction("getCtKey", key);
                PostjsonStr = engine.CallFunction("decode", data, publickey).ToString();
                if (PostjsonStr.StartsWith("{"))
                {
                    jo = JsonStringToJsonObj(PostjsonStr);
                    if (jo != null)
                    {
                        return jo;
                    }
                }
                return null;
            }
        }


        public static string EncodeRequestData(string data, string keyStr, int keyId)
        {
            string ctkey;
            string encodeStr = EncodeData(data, keyStr, keyId, out ctkey);
            string md5Str = MD5Hash(keyStr + encodeStr + ctkey);
            return "100" + md5Str + keyStr + encodeStr;
        }
        public static string EncodeResponseData(string data, string keyStr, int keyId)
        {
            string ctkey;
            string encodeStr = EncodeData(data, keyStr, keyId, out ctkey);
            string md5Str = MD5Hash(keyStr + encodeStr + ctkey);
            return md5Str + keyStr + encodeStr;
        }
        private static string EncodeData(string data, string key, int keyId,out string Ctkey)
        {
            IJsEngineSwitcher engineSwitcher = JsEngineSwitcher.Current;
            engineSwitcher.EngineFactories.Add(new JurassicJsEngineFactory());
            engineSwitcher.DefaultEngineName = JurassicJsEngine.EngineName;
            using (IJsEngine engine = JsEngineSwitcher.Current.CreateDefaultEngine())
            {
                engine.Execute(DedeDataJsCode);
                if (keyId == 1)
                {
                    engine.Execute("var this_UrlKey1 = 'db0FaVXtwixFUGGQ1Iq9dN7yMrJ9DFHQ';var this_UrlKey2 = 'R8nD2B0DRcT0IoFrA5UqHeHLeFsbrOBvXIVhKmgcXXcDLDrQemyyQLdDpAom9N'");
                }
                var publickey = engine.CallFunction("getCtKey", key);
                Ctkey = publickey.ToString();
                string PostjsonStr = engine.CallFunction("encode", data, publickey).ToString();
                return PostjsonStr;
            }
        }
    }
}
