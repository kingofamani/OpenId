using NTPCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Linq;

/// <summary>
/// LoginUtil 的摘要描述
/// </summary>
public class LoginUtil
{
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
            Util.SetCookie(Util.OPENID_THIS_WEBSITE_COOKIE, "true");
        }
    }

    public static void MultiLogin()
    {
        NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();
        if (!openId.IsAuthenticated)
        {
            openId.Login();
        }
        else
        {
            if (Util.GetCookie(Util.OPENID_SELECT_USER_COOKIE) == string.Empty)
            {
                Util.SetCookie(Util.OPENID_THIS_WEBSITE_COOKIE, "true");

                //多學校、多角色權限判斷            
                LoginMultiView(openId.User);
            }
        }
    }

    public static void ExtensionLogin(string[] roleUser)
    {
        NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();
        if (!openId.IsAuthenticated)
        {
            openId.Login();
        }
        else
        {
            if (Util.GetCookie(Util.OPENID_ROLE_COOKIE) == string.Empty)
            {
                Util.SetCookie(Util.OPENID_THIS_WEBSITE_COOKIE, "true");

                //擴充權限判斷            
                LoginExtensionView(openId.User, roleUser);
            }
        }
    }

    public static void Logout()
    {
        NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();
        openId.Logout();

        Util.CleanCookie(Util.OPENID_THIS_WEBSITE_COOKIE);
        Util.CleanCookie(Util.OPENID_SELECT_USER_COOKIE);
        Util.CleanCookie(Util.OPENID_ROLE_COOKIE);
    }

    private static void LoginMultiView(NTPCLibrary.User openIdUser)
    {
        //是否有2間以上學校
        var isMultiSchool = openIdUser.Departments.Count() > 1;
        //是否同1間學校有2個以上職稱
        var isMultiSchoolGroup = openIdUser.Departments.Where(s => s.Groups.Count() >= 2).ToList().Count() > 0;
        if (isMultiSchool || isMultiSchoolGroup)
        {
            string rdpath = System.IO.Path.GetFileName(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.Url.AbsolutePath));
            HttpContext.Current.Response.Redirect("~/LoginMultiView.aspx?id=" + openIdUser.Identity + "&rd=" + rdpath);
        }
        else
        {
            //只有單一學校或職稱
            Util.SetCookie<User>(Util.OPENID_SELECT_USER_COOKIE, openIdUser);
        }
    }

    private static void LoginExtensionView(NTPCLibrary.User openIdUser, string[] roleUser)
    {
        //是否有2間以上學校
        var isMultiSchool = openIdUser.Departments.Count() > 1;
        //是否同1間學校有2個以上職稱
        var isMultiSchoolGroup = openIdUser.Departments.Where(s => s.Groups.Count() >= 2).ToList().Count() > 0;
        //是否有擴充權限
        var isExtensionRole = roleUser.Contains(openIdUser.Identity);
        if (isExtensionRole)
        {
            string rdpath = System.IO.Path.GetFileName(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.Url.AbsolutePath));
            HttpContext.Current.Response.Redirect("~/LoginExtensionView.aspx?id=" + openIdUser.Identity + "&rd=" + rdpath);
        }
        else
        {
            if (isMultiSchool || isMultiSchoolGroup)
            {
                string rdpath = System.IO.Path.GetFileName(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.Url.AbsolutePath));
                HttpContext.Current.Response.Redirect("~/LoginExtensionView.aspx?id=" + openIdUser.Identity + "&rd=" + rdpath);
            }
            else
            {
                //只有單一學校或職稱
                Util.SetCookie<User>(Util.OPENID_SELECT_USER_COOKIE, openIdUser);
            }
        }
    }
}