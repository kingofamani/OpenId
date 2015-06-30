<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginMultiView.aspx.cs" Inherits="LoginMultiView" %>

<html>
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
</head>
<body>
    <form runat="server">
    <div>
        <center><asp:Label runat="server" ID="lblUserName" CssClass="label label-warning"></asp:Label> 使用者登入</center>
        <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand" >
            <ItemTemplate>                
                <tr runat="server">
                    <td><%# Eval("ID")%></td>
                    <td><%# Eval("Name")%></td>
                    <td><%# Eval("Group")%></td>
                    <td>
                        <asp:Button CssClass="btn btn-primary"  runat="server" Text="登入" CommandName="login" CommandArgument='<%# Eval("ID")+ ","+Eval("Name")+","+Eval("Group") %>'/>
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table border="1" style="width:80%;" runat="server">
                    <thead>
                        <tr runat="server">
                            <th runat="server">學校代碼</th>
                            <th runat="server">學校名稱</th>
                            <th runat="server">職位</th>
                            <th runat="server">操作</th>
                        </tr>
                    </thead>                 
                    <tr id="ItemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </div>    
    </form>
</body>
</html>
