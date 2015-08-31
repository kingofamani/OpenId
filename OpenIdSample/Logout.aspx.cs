using System;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string rd = Request["rd"] ?? string.Empty;
        LoginUtil.Logout();

        if (rd == string.Empty)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            Response.Redirect(rd);
        }
    }
}