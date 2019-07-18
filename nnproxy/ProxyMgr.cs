using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace nnproxy
{
    public static class ProxyMgr
    {
        public static ushort defaultPort = 8878;
        public static string SvrApiUrl = "http://localhost:11105/api";
        public static List<Fiddler.Session> oAllSessions;
        public static bool showInfo = true;
        public static List<string> whitelist = new List<string>();
        public static List<string> GameSvrHosts = new List<string>();

        public static void Start()
        {
            whitelist.Clear();
            string whitelistPath = System.AppDomain.CurrentDomain.BaseDirectory + "whitelist.txt";
            if (File.Exists(whitelistPath))
            {
                string tmpStr = File.ReadAllText(whitelistPath);
                string[] tmpArr = tmpStr.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in tmpArr)
                {
                    whitelist.Add(item.Trim());
                }
            }
            oAllSessions = new List<Fiddler.Session>();
            Fiddler.FiddlerApplication.BeforeRequest += delegate (Fiddler.Session oS)
            {
                oS.bBufferResponse = false;
                Monitor.Enter(oAllSessions);
                oAllSessions.Add(oS);
                Monitor.Exit(oAllSessions);

                if (GameSvrHosts.Exists(ii => oS.fullUrl.StartsWith(ii)))
                {
                    string postData = Encoding.UTF8.GetString(oS.RequestBody);

                    JObject rep;
                    string errmsg = gsvr(out rep, oS.fullUrl, postData);
                    if (string.IsNullOrEmpty(errmsg))
                    {
                        if ((bool)rep["ok"])
                        {
                            oS.utilCreateResponseAndBypassServer();
                            oS.oResponse.headers.SetStatus(200, "OK");
                            oS.utilSetResponseBody(rep["data"].ToString());
                            oS.oResponse.headers.Add("hook", "1");
                            //oS.oResponse.headers.Remove("Content-Length");
                        }
                    }
                }else if (whitelist.Count > 0 && !whitelist.Exists(ii => oS.fullUrl.StartsWith(ii)) && !oS.fullUrl.EndsWith(":443"))
                {
                    oS.utilCreateResponseAndBypassServer();
                    oS.oResponse.headers.SetStatus(200, "OK");
                    oS.oResponse["Content-Type"] = "text/html; charset=UTF-8";
                    oS.oResponse["Cache-Control"] = "private, max-age=0";
                    oS.utilSetResponseBody("<html><body>" + oS.fullUrl + "<br /><plaintext>" + oS.oRequest.headers.ToString());
                    return;
                }
            };
            Fiddler.FiddlerApplication.AfterSessionComplete += delegate (Fiddler.Session oS)
            {
                if (showInfo)
                {
                    Console.WriteLine("Finished session:\t" + oS.fullUrl);
                }                
            };

            CONFIG.bDebugSpew = true;
            CONFIG.bAutoProxyLogon = false;

            FiddlerApplication.Log.OnLogString += delegate (object loger, LogEventArgs e)
            {
                //throw new Exception(e.LogString);
                if (e.LogString.StartsWith("! "))
                {
                    ConsoleLog(e.LogString, ConsoleColor.Red);
                }
                else if(showInfo)
                    Console.WriteLine(e.LogString);
            };
            //var oRootCert = new X509Certificate2("sss.pfx", "", X509KeyStorageFlags.Exportable);
            //var z = (RSACryptoServiceProvider)oRootCert.PrivateKey;
            //var cc = DotNetUtilities.GetRsaKeyPair(z);
            //var PrivateKeyInfo = Org.BouncyCastle.Pkcs.PrivateKeyInfoFactory.CreatePrivateKeyInfo(cc.Private);
            //byte[] derEncoded = PrivateKeyInfo.ToAsn1Object().GetDerEncoded();
            //FiddlerApplication.Prefs.SetStringPref("fiddler.certmaker.bc.key", Convert.ToBase64String(derEncoded));
            //FiddlerApplication.Prefs.SetStringPref("fiddler.certmaker.bc.cert", Convert.ToBase64String(oRootCert.Export(X509ContentType.Cert)));

            var oRootCert = Fiddler.CertMaker.GetRootCertificate();
            if (oRootCert == null)
            {
                if (!Fiddler.CertMaker.createRootCert())
                {
                    throw new Exception("创建根证书失败！");
                }
                oRootCert = Fiddler.CertMaker.GetRootCertificate();
                if (oRootCert == null)
                {
                    throw new Exception("创建根证书失败！");
                }
                X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                certStore.Open(OpenFlags.ReadWrite);
                try
                {
                    certStore.Add(oRootCert);
                }
                finally
                {
                    certStore.Close();
                }
                Console.WriteLine("=============================save my ok=================================");
                X509Store x509Store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
                x509Store.Open(OpenFlags.ReadWrite);
                try
                {
                    x509Store.Add(oRootCert);
                }
                finally
                {
                    x509Store.Close();
                }
                Console.WriteLine("=============================save root ok=================================");
                oRootCert = Fiddler.CertMaker.GetRootCertificate();
                if (oRootCert == null)
                {
                    throw new Exception("保存根证书失败！");
                }
            }

            Console.WriteLine("==============================================================");
            Console.WriteLine("RootCertHasPrivateKey:" + oRootCert.HasPrivateKey);
            Console.WriteLine("rootCertExists:" + CertMaker.rootCertExists());
            Console.WriteLine("rootCertIsTrusted:" + CertMaker.rootCertIsTrusted());
            if (!CertMaker.rootCertIsTrusted())
            {
                CertMaker.trustRootCert();
                Console.WriteLine("**rootCertIsTrusted:" + CertMaker.rootCertIsTrusted());
            }
            Console.WriteLine("==============================================================");
            Console.WriteLine(oRootCert);
            Console.WriteLine("==============================================================");

            Fiddler.CONFIG.IgnoreServerCertErrors = true;
            FiddlerCoreStartupFlags oFCSF = /*FiddlerCoreStartupFlags.DecryptSSL | */FiddlerCoreStartupFlags.MonitorAllConnections | FiddlerCoreStartupFlags.OptimizeThreadPool | FiddlerCoreStartupFlags.AllowRemoteClients;
            Fiddler.FiddlerApplication.Startup(defaultPort, oFCSF);
            Console.WriteLine(Fiddler.FiddlerApplication.GetDetailedInfo());
        }
        public static void Stop()
        {
            Fiddler.FiddlerApplication.Shutdown();
        }
        public static int ProxyPort()
        {
            if (IsRunning())
            {
                return defaultPort;
            }
            return 0;
        }
        public static bool IsRunning()
        {
            return Fiddler.FiddlerApplication.IsStarted();
        }
        private static void ConsoleLog(string msg,ConsoleColor clr)
        {
            if (clr == Console.ForegroundColor)
            {
                Console.WriteLine(msg);
            }
            else
            {
                var olc = Console.ForegroundColor;
                Console.ForegroundColor = clr;
                Console.WriteLine(msg);
                Console.ForegroundColor = olc;
            }
        }
        private static string PostData(string url, string data, out string errMsg)
        {
            errMsg = "";
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, err) => true;
            try
            {
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";

                byte[] buffer = encoding.GetBytes(data);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
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

        private static string gsvr(out JObject obj, string url,string bodyData)
        {
            obj = null;
            JObject reqData = new JObject();
            reqData["a"] = "a";
            reqData["url"] = url;
            reqData["data"] = bodyData;
            string errmsg = "";
            string respData = PostData(SvrApiUrl, reqData.ToString(Formatting.None), out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return errmsg;
            }
            JObject RespJo;
            try
            {
                RespJo = (JObject)JsonConvert.DeserializeObject(respData);
            }
            catch (Exception exx)
            {
                return exx.Message;
            }
            if ((bool)RespJo["ok"])
            {
                obj = RespJo;
            }
            else
            {
                return RespJo["msg"].ToString();
            }
            return "";
        }

        public static string getApiVersion(out int version)
        {
            version = 0;
            JObject reqData = new JObject();
            reqData["a"] = "v";
            string errmsg = "";
            string respData = PostData(SvrApiUrl, reqData.ToString(Formatting.None), out errmsg);
            if (!string.IsNullOrEmpty(errmsg))
            {
                return errmsg;
            }
            JObject RespJo;
            try
            {
                RespJo = (JObject)JsonConvert.DeserializeObject(respData);
            }
            catch (Exception exx)
            {
                return exx.Message;
            }
            if ((bool)RespJo["ok"])
            {
                version = (int)RespJo["v"];
                foreach (var item in (JArray)RespJo["s"])
                {
                    GameSvrHosts.Add(item.ToString());
                }
            }
            else
            {
                return RespJo["msg"].ToString();
            }
            return "";
        }

    }
}
