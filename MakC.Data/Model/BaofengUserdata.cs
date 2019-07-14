using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace MakC.Data.Model
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("userdata")]
    public class BaofengUserdata
    {
        /// <summary>
        /// 
        /// </summary>
        public BaofengUserdata()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        /// <summary>
        ///  对应 表 user.id
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string actor { get; set; }

        /// <summary>
        /// 护身符
        /// </summary>
        public string amulet { get; set; }

        /// <summary>
        /// 背包数据
        /// </summary>
        public string bag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mission { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string other { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string player { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string practice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string setting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string store { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string uid { get; set; }

        public DateTime updateDate { get; set; }
    }
}
