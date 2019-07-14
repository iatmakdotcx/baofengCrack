using System;
using System.Collections.Generic;
using System.Text;

namespace MakC.Common
{
    public static class objectExtensions
    {
        public static bool AsBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
    }
}
