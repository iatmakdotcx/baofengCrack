using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakC.Data.Model
{
    public class 黑暗传说单机RPG_Model
    {
        [SugarTable("黑暗传说单机RPG_账号")]
        public class 账号
        {
            [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
            public int id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string ime { get; set; }
            public string playerid { get; set; }
            public int versionCode { get; set; }
            public string data { get; set; }
            public DateTime addtime { get; set; }
        }

        [SugarTable("黑暗传说单机RPG_存档")]
        public class 存档
        {
            [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
            public int id { get; set; }

            public int accountid { get; set; }

            public string playerid { get; set; }
            public string data { get; set; }

            public DateTime uploadTime { get; set; }
        }
        [SugarTable("黑暗传说单机RPG_可兑换")]
        public class 可兑换
        {
            [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
            public int id { get; set; }
            public string gameId { get; set; }
            public bool enabled { get; set; }

        }

    }
}
