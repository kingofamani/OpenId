using Newtonsoft.Json;
using NTPCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginMultiView : System.Web.UI.Page
{
    public NTPCLibrary.User LoginUser = null;
    NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();

    string user_identity = string.Empty;
    string rd = string.Empty;

    public LoginMultiView()
    {
        if (openId.IsAuthenticated)
        {
            LoginUser = openId.User;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        user_identity = Request["id"] ?? string.Empty;
        rd = Request["rd"] ?? string.Empty;

        if (!IsPostBack)
        {
            if (user_identity != string.Empty)
            {
                lblUserName.Text = LoginUser.FullName;

                List<TempDepartment> departments = new List<TempDepartment>();
                foreach (Department department in LoginUser.Departments)
                {
                    foreach (string group in department.Groups)
                    {
                        TempDepartment tempDepartment = new TempDepartment();
                        tempDepartment.ID = department.ID;
                        tempDepartment.Name = department.Name;
                        tempDepartment.Group = group;
                        departments.Add(tempDepartment);
                    }
                }
                ListView1.DataSource = departments;
                ListView1.DataBind();
            }
        }

    }

    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "login")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            User user = LoginUser;
            List<Department> departments = new List<Department>() 
            { 
                new Department()
                {
                    ID = commandArgs[0],
                    Name = commandArgs[1],
                    Groups = new List<string>()
                    {
                        commandArgs[2]
                    }
                }
            };
            user.Departments = departments;
            //設user cookie
            Util.SetCookie<User>(Util.OPENID_SELECT_USER_COOKIE, user);

            //重導
            Response.Redirect(rd);
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Page.Header.DataBind();
    }

    private class TempDepartment
    {
        public TempDepartment() { }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
    }
}