<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.aspx.cs" Inherits="SessionExpired" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Session Expired</title>
    <link href="FastERP_StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CommonStyle {
            font-family: Arial;
        }
    </style>
</head>
<body>
    <form id="frmSessionExpired" runat="server">
        <div>
            <table style="width: 490px;">
                <tr>
                    <td style="width: 294px">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="Red"
                            Text="Your Session has Expired." CssClass="ErrorMessage" Width="500px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 294px;">
                        <span style="font-size: 13pt">
                            <asp:Label ID="Label3" runat="server" Text="This error has occured for one of the following reasons :"
                                Width="500px" CssClass="CommonStyle"></asp:Label>
                            <asp:Label ID="Label6" runat="server" Text="1) You have used Back/Forward/Refresh button of your Browser." CssClass="CommonStyle" Width="500px"></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text="2) You have double clicked on any options/buttons." CssClass="CommonStyle" Width="500px"></asp:Label>
                            <asp:Label ID="Label5" runat="server" Text="3) You have kept the browser window idle for a long time." CssClass="CommonStyle" Width="500px"></asp:Label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 294px">
                        <asp:HyperLink ID="hLogOut" runat="server" NavigateUrl="~/Login.aspx" Target="_parent" CssClass="CommonStyle">Click here</asp:HyperLink></td>
                </tr>
                <tr>
                    <td style="width: 294px; height: 21px;">
                        <asp:Label
                            ID="Label2" runat="server" Text="   to go to Login Page" CssClass="CommonStyle" Width="424px"></asp:Label></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
