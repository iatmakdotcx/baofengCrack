using System;
using System.Collections.Generic;
using System.Text;

using SqlSugar;

namespace MakC.Data.Model
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("user")]
    public class BaofengUser
    {
        /// <summary>
        /// 
        /// </summary>
        public BaofengUser()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string logintoken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool is_cheat { get; set; }
     

        /// <summary>
        /// 
        /// </summary>
        public string account_platform { get; set; }
        public string usertoken { get; set; }

        public int player_id { get; set; }
        public string userName { get; set; }
        public string ServerName { get; set; }
        /// <summary>
        /// 是否使用本地存档
        /// </summary>
        public bool isHold { get; set; }
    }
}
