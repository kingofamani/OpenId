<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnCreate" Text="產生" OnClick="btnCreate_Click"/>
        <asp:Button runat="server" ID="btnGo" Text="導至" OnClick="btnGo_Click" />
        <br />
        <asp:Button runat="server" ID="btnCreateStream" Text="Stream方式產生" OnClick="btnCreateStream_Click"/>
        <asp:Button runat="server" ID="btnGoStream" Text="Stream導至" OnClick="btnGoStream_Click"/>
        <br />
         <asp:Button runat="server" ID="btnCreateTemp" Text="Temp方式產生" OnClick="btnCreateTemp_Click"/>
        <asp:Button runat="server" ID="btnGoTemp" Text="Temp導至" OnClick="btnGoTemp_Click"/>
        <asp:Button runat="server" ID="btnAllCookie" Text="列舉所有可用的COOKIE" OnClick="btnAllCookie_Click" />
        <asp:Label runat="server" ID="Label1"></asp:Label>
    </div>
    </form>
</body>
</html>
