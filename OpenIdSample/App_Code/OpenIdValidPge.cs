using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// OpenIdValidPge 的摘要描述
/// </summary>
public class OpenIdValidPge :System.Web.UI.Page
{
    public NTPCLibrary.User LoginUser = null;
	public OpenIdValidPge()
	{
	}

    protected void Page_Init(object sender, EventArgs e)
    {
        string rdpath = System.IO.Path.GetFileName(Server.MapPath(Request.Url.AbsolutePath));
        //判斷是否Open ID登入
        NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();

        //沒Open ID：如果在別的網站登出，就要重登
        if (!openId.IsAuthenticated)
        {
            LoginUtil.Logout();//清除Cookie
            Response.Redirect("~/Login.aspx?rd=" + rdpath);

        }
        //有Open ID，沒Cookie：在別的網站登入過
        else
        {
            if (LoginUtil.GetCookie(LoginUtil.OPENID_THIS_WEBSITE_COOKIE) != string.Empty)
            {
                LoginUser = openId.User;
            }
            else
            {
                Response.Redirect("~/Login.aspx?rd=" + rdpath);
            }
        }
    }
}