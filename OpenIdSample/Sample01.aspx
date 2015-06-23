<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample01.aspx.cs" Inherits="Sample01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        sample01
        基本OpenId驗證

        <asp:Button runat="server" ID="btnUserInfo" OnClick="btnUserInfo_Click" Text="User資訊" />
    </div>
    </form>
</body>
</html>
