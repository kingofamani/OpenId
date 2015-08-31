using Newtonsoft.Json;
using System;
using System.Web;

namespace NTPCLibrary
{
    public static class Util
    {
        public const string OPENID_COOKIE = "openid";//OpenID使用者所有資訊
        public static string OPENID_THIS_WEBSITE_COOKIE = "openidThisWebSite";//是否已在本網站登入過
        public static string OPENID_SELECT_USER_COOKIE = "openidSelectUser";//選取的登入職稱
        public static string OPENID_ROLE_COOKIE = "openidRole";

        public enum 角色權限 : int
        {
            ADMINISTRATORS = 1, ENCTCMANAGERS = 15, SNGNCOMPANYS = 20
        }

        public static string 角色名稱(角色權限 role)
        {
            if (role == 角色權限.ADMINISTRATORS)
            {
                return "管理者";
            }
            else if (role == 角色權限.ENCTCMANAGERS)
            {
                return "教研網管組";
            }
            else if (role == 角色權限.SNGNCOMPANYS)
            {
                return "SNGN廠商";
            }
            else
            {
                return string.Empty;
            }
        }

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

        public static void SetCookie<T>(string cookieName, T value,string domain)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = HttpUtility.UrlEncode(JsonConvert.SerializeObject(value));
            cookie.Domain = domain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SetCookie<T>(string cookieName, T value, string domain,bool http)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = HttpUtility.UrlEncode(JsonConvert.SerializeObject(value));
            cookie.Domain = domain;
            cookie.HttpOnly = http;
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
