using System;

using SqlSugar;

namespace MakC.Data.Model
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("userdata_bak")]
    public class BaofengUserdata_bak
    {
        /// <summary>
        /// 
        /// </summary>
        public BaofengUserdata_bak()
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
        public int userId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string actor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string amulet { get; set; }

        /// <summary>
        /// 
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

        /// <summary>
        /// 
        /// </summary>
        public DateTime cdate { get; set; }
    }
}
