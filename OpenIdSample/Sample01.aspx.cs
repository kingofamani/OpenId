using Newtonsoft.Json;
using System;
using NTPCLibrary;

//[Authorize]
//[Authorize(Users = "amani,test1234")]
//[Authorize(Schools="014792")] 
[Authorize(Roles = "資訊組長")] //授權Authorization
public partial class Sample01 : OpenIdValidPge //認證Authentication
{
    protected void btnUserInfo_Click(object sender, EventArgs e)
    {
        Response.Write(JsonConvert.SerializeObject(LoginUser));      
    }
}