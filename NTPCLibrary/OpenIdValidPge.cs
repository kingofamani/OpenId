using NTPCLibrary;
using System;
using System.Web;
using System.Web.UI;

/// <summary>
/// OpenIdValidPge 的摘要描述
/// </summary>
//[Authorize(Schools = "014792")]//可繼承
public class OpenIdValidPge :System.Web.UI.Page
{
    public NTPCLibrary.User LoginUser = null;
    NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();
	public OpenIdValidPge()
	{
        //授權Authorization (OpenID登入後才檢查)
        if (!AuthorizeCore.IsAuthorized(this))
        {
            //無權限處理(以下請自行修改)
            HttpContext.Current.Response.Redirect("Default.aspx");
        }
	}

    protected void Page_Init(object sender, EventArgs e)
    {
        //判斷是否Open ID登入
        if (!openId.IsAuthenticated)
        {
            LoginUtil.Login();
        }
        else
        {
            LoginUser = openId.User;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}