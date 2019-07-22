using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace baofengCrack
{
    public class CustAuthorizeAttribute: AuthorizeAttribute
    {
        public const string MakAuthenticationScheme = "MakAuthenticationScheme";

        public CustAuthorizeAttribute()
        {
            this.AuthenticationSchemes = MakAuthenticationScheme;
        }
    }
}
