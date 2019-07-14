using System;
using System.Collections.Generic;
using System.Text;

using SqlSugar;

namespace MakC.Data.Model
{
    /// <summary>
    /// 
    /// </summary> 
    [SugarTable("userdata_bak")]
    public class BaofengGifcode
    {
        /// <summary>
        /// 
        /// </summary>
        public BaofengGifcode()
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
        public string gifcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int gifid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime cdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int canUseCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int usedCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string servername { get; set; }
        public int player_id { get; set; }
    }
}
