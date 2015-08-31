using System;

namespace NTPCLibrary
{
    /// <summary>
    /// 加入某頁面的OpenID延伸權限
    /// <para>加入[AuthorizeExtension]特性後，需執行AuthorizeCore.IsExtensionAuthorized(this)</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeExtensionAttribute:Attribute
    {
        public AuthorizeExtensionAttribute()
	    {
	    }

        public virtual string Roles { get; set; }
    }
}
