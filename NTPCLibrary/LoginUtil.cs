using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// LoginUtil 的摘要描述
/// </summary>
public class LoginUtil
{
    public static string OPENID_THIS_WEBSITE_COOKIE = "openThisWebSiteCookie";//是否已在本網站登入過

	public LoginUtil()
	{
	}

    public static void Login()
    {
        NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();
        if (!openId.IsAuthenticated)
        {
            openId.Login();
        }
        else
        {
            SetCookie(OPENID_THIS_WEBSITE_COOKIE, "true");

            //多學校、多角色權限判斷
            //LoginMultiView(openId.User);
        }

    }

    public static void Logout()
    {
        NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();
        openId.Logout();

        CleanCookie(OPENID_THIS_WEBSITE_COOKIE);
    }

    private static void LoginMultiView(NTPCLibrary.User openIdUser)
    {
        //SNGNDBDataContext ctx = new SNGNDBDataContext();
        //Page page = (Page)HttpContext.Current.Handler;

        ////是否有2間以上學校
        //var isMultiSchool = openIdUser.Schools.Count() > 1;
        ////是否同1間學校有2個以上職位
        //var isMultiSchoolGroup = openIdUser.Schools.Where(s => s.Groups.Count >= 2).ToList().Count() > 0;
        ////是否DB RoleUser有管理者以上帳號(小於99)
        //var isManager = Util.isManager(openIdUser.Username);
        //if (isMultiSchool || isMultiSchoolGroup || isManager)
        //{
        //    string rdpath = page.Request["rd"];
        //    page.Response.Redirect("~/LoginMultiView.aspx?id=" + openIdUser.Username + "&rd=" + rdpath);
        //}
        //else
        //{
        //    //只有單一學校或群組
        //    SetNtpcCookie(NTPC_SCHOOL_COOKIE, openIdUser.Schools.FirstOrDefault().Identity);
        //    SetNtpcCookie(NTPC_SCHOOL_GROUP_COOKIE, openIdUser.Schools.FirstOrDefault().Groups.FirstOrDefault());
        //    SetNtpcCookie(NTPC_USER_COOKIE, openIdUser.Username);

        //    //1.validPage指定rd  2.在此導至rd
        //    string rdpath = page.Request["rd"] ?? "~/Default.aspx";
        //    page.Response.Redirect(rdpath);
        //}
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
}