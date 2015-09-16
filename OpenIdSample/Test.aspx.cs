using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    string name = "GenerateSample";
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string[] aspxLines = {"<%@ Page Language=\"C#\" AutoEventWireup=\"true\"CodeFile=\""+name+".aspx.cs\" Inherits=\"generate_page_runtime."+name+"\" %>",
        "<!DOCTYPE html>",
        "<head>",
        "<title>The New Page</title>",
        "</head>",
        "<body>",
        "   <form id=\"form1\" runat=\"server\">",
        "       <div>",
        "           <asp:literal id=\"output\" runat=\"server\"/>",
        "       </div>",
        "   </form>",
        "</body>",
        "</html>"};
        string[] csLines = {"using System;",
        "using System.Web.UI.WebControls;",
        "namespace generate_page_runtime {",
        "    public partial class "+name+" : System.Web.UI.Page {",
        "        protected void Page_Load(object sender, EventArgs e) {",
        "            output.Text = \"Our new page\";",
        "        }",
        "    }",
        "}"};
        File.WriteAllLines(Server.MapPath("" + name + ".aspx"), aspxLines);
        File.WriteAllLines(Server.MapPath("" + name + ".aspx.cs"), csLines);
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        Response.Redirect("" + name + ".aspx");
    }
    protected void btnCreateStream_Click(object sender, EventArgs e)
    {
        string fielName = Server.MapPath("~/NtpcOpenIDTempErrorPage.aspx");
        TextWriter tw = new StreamWriter(fielName);
        tw.WriteLine
            (@"<%@ Page Language=""C#"" AutoEventWireup=""true"" CodeFile=""Error.aspx.cs"" Inherits=""Error"" %>
            <!DOCTYPE html>
            <html>
            <head runat=""server"">
            <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
            </head>
            <body>
                <form runat=""server"">
                <div style=""margin:50px;""></div>
                <div>
                    <center><asp:Label runat=""server"" ID=""lblMsg""></asp:Label></center>
                </div>
                    <center>
                        <asp:Button runat=""server"" ID=""btnOk"" Text=""Home"" OnClick=""btnOk_Click""/>
                        <asp:Button runat=""server"" ID=""btnLogout"" Text=""Logout"" OnClick=""btnLogout_Click""/>
                    </center>
                </form>
            </body>
            </html>");
        tw.Close();
        tw = new StreamWriter(fielName + ".cs");

        tw.WriteLine
            (@"using System;
            using System.Collections.Generic;
            using System.Linq;
            using System.Web;
            using System.Web.UI;
            using System.Web.UI.WebControls;

            public partial class Error : System.Web.UI.Page
            {
                protected void Page_Load(object sender, EventArgs e)
                {
                    string msg = Request[""msg""] == null ? ""不明原因錯誤！"" : Request[""msg""].ToString();

                    if (!IsPostBack)
                    {
                        lblMsg.Text = HttpUtility.UrlDecode(msg);
                    }
                }
    
                protected void btnOk_Click(object sender, EventArgs e)
                {
                    string rd = Request[""rd""] == null ? string.Empty : Request[""rd""].ToString();
                    if (rd == string.Empty)
                    {
                        Response.Redirect(""~/Default.aspx"");
                    }
                    else
                    {
                        Response.Redirect(rd);
                    }
                }
                protected void btnLogout_Click(object sender, EventArgs e)
                {
                    LoginUtil.Logout();
                    Response.Redirect(""~/Default.aspx"");
                }
            }");
        tw.Close();
    }
    protected void btnGoStream_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/NtpcOpenIDTempErrorPage.aspx");
    }

    protected void btnCreateTemp_Click(object sender, EventArgs e)
    {
        //CreateTmpFile();
        HttpContext.Current.Response.Write(Path.GetTempPath());
    }
    protected void btnGoTemp_Click(object sender, EventArgs e)
    {

    }

    private static string CreateTmpFile()
    {
        string fileName = string.Empty;

        try
        {
            // Get the full name of the newly created Temporary file. 
            // Note that the GetTempFileName() method actually creates
            // a 0-byte file and returns the name of the created file.
            fileName = Path.GetTempFileName();

            // Craete a FileInfo object to set the file's attributes
            FileInfo fileInfo = new FileInfo(fileName);

            // Set the Attribute property of this file to Temporary. 
            // Although this is not completely necessary, the .NET Framework is able 
            // to optimize the use of Temporary files by keeping them cached in memory.
            fileInfo.Attributes = FileAttributes.Temporary;

            HttpContext.Current.Response.Write("TEMP file created at: " + fileName);
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write("Unable to create TEMP file or set its attributes: " + ex.Message);
        }

        return fileName;
    }

    protected void btnAllCookie_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        HttpCookie aCookie;
        for (int i = 0; i < Request.Cookies.Count; i++)
        {
            aCookie = Request.Cookies[i];
            output.Append("<br />");
            output.Append("Cookie name = " + Server.HtmlEncode(aCookie.Name)
                + "<br />");
            output.Append("Cookie value = " + Server.HtmlEncode(aCookie.Value)
                + "<br /><br />");
        }
        Label1.Text = output.ToString();
    }
}