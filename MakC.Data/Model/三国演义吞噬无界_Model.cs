using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakC.Data.Model
{
    public class 三国演义吞噬无界_Model
    {

        [SugarTable("三国演义吞噬无界_账号")]
        public class 账号
        {
            [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
            public int id { get; set; }            
            public int tmpid { get; set; }
            public int ss { get; set; }
            public string sid { get; set; } 
            public string deviceId { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string bufferData { get; set; }
            public int bufferId { get; set; }
            public string SaveData { get; set; }
            public string uid { get; set; }
        }

        [SugarTable("三国演义吞噬无界_兑换码")]
        public class 兑换码
        {
            [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
            public int id { get; set; }
            public string cdkey { get; set; }
            public string bufferData { get; set; }
            public DateTime cdate { get; set; }
            public int canUseCount { get; set; }
            public int usedCount { get; set; }
            public string playerId { get; set; }
        }
    }
}
