using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace NTPCLibrary
{
    /// <summary>
    /// 加入某頁面的OpenID權限
    /// <para>加入[Authorize]特性後，需執行AuthorizeCore.IsAuthorized(this)</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute
    {
        public AuthorizeAttribute()
        {
        }
        public virtual string Roles { get; set; }
        public virtual string Users { get; set; }
        public virtual string Schools { get; set; }
    }
}
