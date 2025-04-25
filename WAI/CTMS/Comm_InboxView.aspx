<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Comm_InboxView.aspx.cs" Inherits="CTMS.Comm_InboxView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Sender
        {
            margin-left: 7%;
            font-size: small;
        }
        .Subject
        {
            margin-left: 7%;
            font-size: small;
        }
        .Body
        {
            margin-left: 7%;
        }
        .box
        {
            margin-bottom: 0%;
            margin-bottom: 0px;
            width: auto;
        }
        .fontBold
        {
            font-weight: bold;
        }
        .Margin7
        {
            margin-left: 3%;
            margin-right: 3%;
            font-size: larger;
            margin-top: 3%;
        }
        .color
        {
            color: #333333;
        }
        .list-group-item
        {
            display: unset;
            padding: 1px 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="form-group has-warning">
        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
    </div>
    <div>
        <div class="box box-info">
            <div class="box-header">
                <h3 class="box-title">
                    <asp:Label ID="lblSubject" runat="server"></asp:Label>
                </h3>
                <div class="pull-right" style="margin-right: 5%; font-size: initial;">
                    <asp:LinkButton runat="server" ID="lbtnReply" ToolTip="Reply" OnClick="lbtnReply_Click">
                <i class="fa fa-reply color"></i>
                    </asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lbtnReplyAll" ToolTip="Reply All" OnClick="lbtnReplyAll_Click">
                <i class="fa fa-reply-all color"></i>
                    </asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lbtnForward" ToolTip="Forward" OnClick="lbtnForward_Click">
                <i class="fa fa-share color"></i>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-md-12" style="font-size: 14px;">
                <div class="col-md-1">
                    <asp:Label ID="Label24" CssClass="fontBold" runat="server" Text="From"></asp:Label></div>
                <div class="col-md-5">
                    <asp:Label ID="lblFrom" runat="server"></asp:Label>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label26" CssClass="fontBold" runat="server" Text="To"></asp:Label>
                </div>
                <div class="col-md-5">
                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-12" runat="server" id="divCCs" style="font-size: 14px;">
                <div runat="server" id="divCc">
                    <div class="col-md-1">
                        <asp:Label ID="Label28" CssClass="fontBold" runat="server" Text="Cc"></asp:Label>
                    </div>
                    <div class="col-md-5">
                        <asp:Label ID="lblCc" runat="server"></asp:Label>
                    </div>
                </div>
                <div runat="server" id="divBcc" style="font-size: 14px;">
                    <div class="col-md-1">
                        <asp:Label ID="Label30" CssClass="fontBold" runat="server" Text="Bcc"></asp:Label>
                    </div>
                    <div class="col-md-5">
                        <asp:Label ID="lblBcc" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-12" runat="server" id="divAttach" style="font-size: 14px;">
                <div class="col-md-1">
                    <asp:Label ID="Label1" CssClass="fontBold" runat="server" Text="Attachments"></asp:Label></div>
                <div class="col-md-11" style="margin-top: 2px;">
                    <asp:Repeater runat="server" ID="repeatAttach" OnItemCommand="repeatAttach_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lbtnAttach" CssClass="list-group-item" ToolTip="Click here to Download"
                                CommandArgument='<%# Bind("ID") %>' Text='<%# Bind("FileName") %>'></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <br />
            <hr />
            <br />
            <div>
                <div class="Margin7">
                    <asp:Literal ID="litBody" runat="server" />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
