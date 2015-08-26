<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button runat="server" Text="LOGIN" ID="btnLogin" OnClick="btnLogin_Click" />

    <asp:Button runat="server" Text="登出" ID="btnLogout" OnClick="btnLogout_Click"/>

    <a href="Sample01.aspx">Sample01</a>
    <a href="Sample02.aspx">Sample02</a>
    <a href="Sample03.aspx">Sample03</a>
    </div>
    </form>
</body>
</html>
