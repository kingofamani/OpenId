using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NTPCLibrary
{
    public static class Util
    {
        public static string OPENID_THIS_WEBSITE_COOKIE = "openidThisWebSite";//是否已在本網站登入過
        public static string OPENID_SELECT_USER_COOKIE = "openidSelectUser";//選取的登入職稱

        public static void CleanCookie(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SetCookie(string cookieName, string value)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = HttpUtility.UrlEncode(value);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SetCookie<T>(string cookieName, T value)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = HttpUtility.UrlEncode(JsonConvert.SerializeObject(value));
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                return HttpUtility.UrlDecode(cookie.Value);
            }
            else
            {
                return string.Empty;
            }
        }

        public static T GetCookie<T>(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                return JsonConvert.DeserializeObject<T>(HttpUtility.UrlDecode(cookie.Value));
            }
            else
            {
                return default(T);
            }
        }
    }
}
