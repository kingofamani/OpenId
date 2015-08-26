<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample03.aspx.cs" Inherits="Sample03" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Sample03
    <br />
    擴充OpenId驗證，可依資料庫RoleUser選取角色

        <asp:Button runat="server" Text="登出" ID="btnLogout" OnClick="btnLogout_Click"/>

        <asp:Button runat="server" ID="btnUserInfo" OnClick="btnUserInfo_Click" Text="User資訊" />

        <asp:Button runat="server" ID="btnMultiLogin" OnClick="btnMultiLogin_Click" Text="切換學校或職稱" />
    </div>
    </form>
</body>
</html>
