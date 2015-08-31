using MyDb;
using NTPCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginExtensionView : System.Web.UI.Page
{
    DataClassesDataContext ctx = new DataClassesDataContext();

    public NTPCLibrary.User LoginUser = null;
    NTPCLibrary.OpenID openId = new NTPCLibrary.OpenID();

    string user_identity = string.Empty;
    string rd = string.Empty;

    public LoginExtensionView()
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
                //一般User
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

                //管理端                
                List<RoleUser> rus = ctx.RoleUser.Where(r => r.user_id == LoginUser.Identity).OrderBy(r => r.role_id).ToList();

                if (rus.Count != 0)
                {
                    ListView2.DataSource = rus;
                    ListView2.DataBind();
                }
                else
                {
                    lblManagerTitle.Visible = false;
                }
            }
        }
    }

    //一般登入
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "login")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            NTPCLibrary.User user = LoginUser;
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
            Util.SetCookie<NTPCLibrary.User>(Util.OPENID_SELECT_USER_COOKIE, user);

            //清除role cookie
            //Util.CleanCookie(Util.OPENID_ROLE_COOKIE);
            Util.SetCookie(Util.OPENID_ROLE_COOKIE, "0");

            //重導
            Response.Redirect(rd);

            
        }
    }

    //擴充登入
    protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "login")
        {
            //設role_id cookie
            Util.SetCookie(Util.OPENID_ROLE_COOKIE, e.CommandArgument.ToString());

            //清除openidSelectUser cookie
            Util.CleanCookie(Util.OPENID_SELECT_USER_COOKIE);

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