using NTPCLibrary;
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
    public static string OPENID_THIS_WEBSITE_COOKIE = "openidThisWebSite";//是否已在本網站登入過
    public static string OPENID_SELECT_USER_COOKIE = "openidSelectUser";

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
            Util.SetCookie(OPENID_THIS_WEBSITE_COOKIE, "true");

            //多學校、多角色權限判斷
            //LoginMultiView(openId.User);
        }

    }

    public static void MuitlLogin()
    {
        NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();
        if (!openId.IsAuthenticated)
        {
            openId.Login();
        }
        else
        {
            Util.SetCookie(OPENID_THIS_WEBSITE_COOKIE, "true");

            //多學校、多角色權限判斷
            //LoginMultiView(openId.User);
        }

    }

    public static void Logout()
    {
        NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();
        openId.Logout();

        Util.CleanCookie(OPENID_THIS_WEBSITE_COOKIE);
    }

    private static void LoginMultiView(NTPCLibrary.User openIdUser)
    {
        //Page page = (Page)HttpContext.Current.Handler;

        ////是否有2間以上學校
        //var isMultiSchool = openIdUser.Departments.Count() > 1;
        ////是否同1間學校有2個以上職位
        //var isMultiSchoolGroup = openIdUser.Departments.Where(s => s.Groups.Count() >= 2).ToList().Count() > 0;
        //if (isMultiSchool || isMultiSchoolGroup)
        //{
        //    string rdpath = page.Request["rd"];
        //    page.Response.Redirect("~/LoginMultiView.aspx?id=" + openIdUser.Username + "&rd=" + rdpath);
        //}
        //else
        //{
        //    //只有單一學校或群組
        //    SetCookie(OPENID_SELECT_USER_COOKIE, openIdUser.Schools.FirstOrDefault().Identity);
        //    SetCookie(NTPC_SCHOOL_GROUP_COOKIE, openIdUser.Schools.FirstOrDefault().Groups.FirstOrDefault());
        //    SetCookie(NTPC_USER_COOKIE, openIdUser.Username);

        //    //1.validPage指定rd  2.在此導至rd
        //    string rdpath = page.Request["rd"] ?? "~/Default.aspx";
        //    page.Response.Redirect(rdpath);
        //}
    }    
}