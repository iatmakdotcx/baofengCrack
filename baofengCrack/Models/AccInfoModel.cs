using MakC.Data.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace baofengCrack.Models
{
    public class AccInfoModel
    {
        public string ErrMsg { get; set; }
        public JObject Bag { get; set; } = new JObject();
        public BaofengUser user { get; set; } = new BaofengUser();
    }
}
