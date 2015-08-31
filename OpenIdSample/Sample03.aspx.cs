using NTPCLibrary;
using System;
using System.Web;

//[Authorize(Users = "af3726")]
[Authorize(Schools="019998")]
[AuthorizeExtension(Roles = "管理者,教研網管組")] //授權Authorization
public partial class Sample03 : OpenIdExtensionValidPge //認證Authentication
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUserInfo_Click(object sender, EventArgs e)
    {
        Response.Write(Util.GetCookie(Util.OPENID_COOKIE));
    }
    protected void btnMultiLogin_Click(object sender, EventArgs e)
    {
        string rdpath = System.IO.Path.GetFileName(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.Url.AbsolutePath));
        HttpContext.Current.Response.Redirect("~/LoginExtensionView.aspx?id=" + LoginUser.Identity + "&rd=" + rdpath);
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Logout.aspx");
    }
}