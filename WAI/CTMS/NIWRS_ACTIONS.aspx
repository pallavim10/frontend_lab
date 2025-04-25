<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_ACTIONS.aspx.cs" Inherits="CTMS.NIWRS_ACTIONS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;
        font-size: 15px; margin-left: 7px"></asp:Label>
    <asp:HiddenField runat="server" ID="hfSTEPID" />
    <asp:HiddenField runat="server" ID="hfSTOPCLAUSE_MSG" />
    <asp:HiddenField runat="server" ID="hfSTOPCLAUSE_URL" />
</asp:Content>
