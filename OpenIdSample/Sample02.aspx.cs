using NTPCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[Authorize(Roles = "資訊組長")] //授權Authorization
public partial class Sample02 : OpenIdMultiValidPge //認證Authentication
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUserInfo_Click(object sender, EventArgs e)
    {
        Response.Write(Util.GetCookie(Util.OPENID_SELECT_USER_COOKIE));   
    }
    protected void btnMultiLogin_Click(object sender, EventArgs e)
    {
        string rdpath = System.IO.Path.GetFileName(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.Url.AbsolutePath));
        HttpContext.Current.Response.Redirect("~/LoginMultiView.aspx?id=" + LoginUser.Identity + "&rd=" + rdpath);
    }
}