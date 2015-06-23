using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string rd = Request["rd"] ?? string.Empty;
        LoginUtil.Login();

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