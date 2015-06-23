using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sample01 : OpenIdValidPge
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnUserInfo_Click(object sender, EventArgs e)
    {
        Response.Write(JsonConvert.SerializeObject(LoginUser));      
    }
}