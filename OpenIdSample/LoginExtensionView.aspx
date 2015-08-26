<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginExtensionView.aspx.cs" Inherits="LoginExtensionView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <!--一般-->
        <div>
            <center>
                <asp:Label runat="server" ID="lblUserName" CssClass="label label-warning"></asp:Label>
                <h3>一般登入</h3>
            </center>
            <asp:ListView ID="ListView1" runat="server" OnItemCommand="ListView1_ItemCommand">
                <ItemTemplate>
                    <tr runat="server">
                        <td><%# Eval("ID")%></td>
                        <td><%# Eval("Name")%></td>
                        <td><%# Eval("Group")%></td>
                        <td>
                            <asp:Button CssClass="btn btn-primary" runat="server" Text="登入" CommandName="login" CommandArgument='<%# Eval("ID")+ ","+Eval("Name")+","+Eval("Group") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table border="1" style="width: 80%;" runat="server">
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
        <!--一般-->

        <!--擴充-->
        <div>
            <center><h3 runat="server" id="lblManagerTitle">擴充權限登入</h3></center>
            <br />
            <asp:ListView ID="ListView2" runat="server" OnItemCommand="ListView2_ItemCommand">
                <ItemTemplate>

                    <tr runat="server">
                        <td><%# NTPCLibrary.Util.角色名稱((NTPCLibrary.Util.角色權限)Convert.ToInt16(Eval("role_id")))%></td>
                        <td>
                            <asp:Button CssClass="btn btn-danger" runat="server" Text="登入" CommandName="login" CommandArgument='<%# Eval("role_id")%>' />
                        </td>

                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table border="1" style="width: 80%;" runat="server">
                        <thead>
                            <tr runat="server">
                                <th runat="server">擴充角色</th>
                                <th runat="server">操作</th>
                            </tr>
                        </thead>
                        <tr id="ItemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>

        </div>
        <!--擴充-->

    </form>
</body>
</html>
