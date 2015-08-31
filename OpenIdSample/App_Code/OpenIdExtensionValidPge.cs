using NTPCLibrary;
using System;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Configuration;
using MyDb;

/// <summary>
/// OpenIdExtensionValidPge
/// </summary>
public class OpenIdExtensionValidPge : System.Web.UI.Page
{
    DataClassesDataContext ctx = new DataClassesDataContext();

    public NTPCLibrary.User LoginUser = null;
    NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();
    public OpenIdExtensionValidPge()
	{
        //先)認證Authentication：判斷是否OpenID登入
        string[] roleUser = ctx.RoleUser.Select(r => r.user_id).ToArray();
        LoginUtil.ExtensionLogin(roleUser);
        if (openId.IsAuthenticated)
        {
            LoginUser = openId.User;
        }
	}

    protected void Page_Init(object sender, EventArgs e)
    {
        //後)授權Authorization
        //1)第一次OpenID登入，由於抓不到OPENID_COOKIE
        if (HttpContext.Current.Request["dnoa.userSuppliedIdentifier"] == openId.Url)
        {
            //在此抓不到GetCookie(OPENID_COOKIE)，要用openId.Login()再Set一次OPENID_COOKIE；然後讓2)可以直接用openId.IsAuthenticated驗證
            openId.Login();

            //去掉dnoa.userSuppliedIdentifier
            string rdpath = System.IO.Path.GetFileName(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.Url.AbsolutePath));
            HttpContext.Current.Response.Redirect(rdpath);
        }
        //2)已登入OpenID，抓得到OPENID_COOKIE，直接用openId.IsAuthenticated驗證
        if (!AuthorizeCore.IsMultiExtensionAuthorized(this) && openId.IsAuthenticated)
        {
            //無權限處理↓↓↓↓以下請自行修改↓↓↓↓
            //HttpContext.Current.Response.Redirect("/Default.aspx");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alertreturn", "alert('您沒有擴充權限！');", true);
        }
    }
}
