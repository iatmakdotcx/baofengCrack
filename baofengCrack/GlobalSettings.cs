using MakC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace baofengCrack
{
    public class GlobalSettings
    {
        //public static string gameSvrHost = ConfigExtensions.Configuration["game:gameSvrHost"];

        //private static bool isSqliteEnabled = ConfigExtensions.Configuration["game:gameSvrHost"].ObjToBool();

        public static Dictionary<string, string> gameServers = new Dictionary<string, string>();
        static GlobalSettings()
        {
            if (gameServers.Count == 0)
            {
                lock (gameServers)
                {
                    if (gameServers.Count == 0)
                    {
                        foreach (var item in ConfigExtensions.Configuration.GetSection("game:gameSvrs").GetChildren())
                        {
                            gameServers.Add(item.Key, item.Value);
                        }
                    }
                }
            }
        }
        public static string getServerHost(string ServerName)
        {
            string serverHost = "";
            if(gameServers.TryGetValue(ServerName, out serverHost))
            {
                return serverHost;
            }
            return "";
        }
        public static string getServerName(string ServerHost)
        {
            foreach (var item in gameServers)
            {
                if (item.Value== ServerHost)
                {
                    return item.Key;
                }
            }
            return "";
        }
    }
}
