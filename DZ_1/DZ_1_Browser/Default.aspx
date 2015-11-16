<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DZ_1_Browser.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="DefualtStyle.css" rel="stylesheet"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><% =Title %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="round"></div>
    <div id="queryString" style="position: fixed; top: 30px; text-align: center; width: 70%; left: 15%; height: 3em">
    
        <table width="100%" style="top: 50%; height: 46px;">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="ButtonSubmit" runat="server" OnClick="ButtonSubmit_Click" Text="GO" Width="100%" Height="28px" Font-Names="Berlin Sans FB" Font-Size="Medium" />
                </td>
                <td style="width: 55%">
                    <asp:TextBox ID="InputUri" runat="server" Width="100%" Height="23px" TextMode="Url" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                </td>
                <td style="width: 15%">
                    <asp:CheckBox ID="ShowBody" runat="server" Text="Show body" />
                </td>
                <td style="width: 10%">
                    <asp:Label ID="OutputStatus" runat="server" Font-Names="Arial"></asp:Label>
                </td>
                <td style="width: 10%">
                    <asp:Label ID="OutputLength" runat="server" Font-Names="Arial"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:Label ID="Output" runat="server"></asp:Label>
    </form>
    </body>
</html>
