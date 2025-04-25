<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Comm_Delete.aspx.cs" Inherits="CTMS.Comm_Delete" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Sender
        {
            font-size: initial;
        }
        .Subject
        {
            font-size: small;
        }
        .Body
        {
            margin-left: 7%;
        }
        .Margin7
        {
            margin-left: 7%;
            margin-right: 7%;
            font-size: larger;
            margin-top: 1%;
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
        .color
        {
            color: #333333;
        }
        .colorRed
        {
            color: #ff0000;
        }
        .row
        {
            margin-left: 0;
            margin-right: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title" style="width: 100%">
                        Deleted Items
                        <div class="pull-right" style="display: inline-flex; width: 40%;">
                            <asp:AutoCompleteExtender ServiceMethod="GetSearch" MinimumPrefixLength="1" CompletionInterval="10"
                                EnableCaching="false" CompletionSetCount="10" TargetControlID="txtAutoCompText"
                                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                            </asp:AutoCompleteExtender>
                            <label class="label width60px">
                                Search:
                            </label>
                            <asp:TextBox ID="txtAutoCompText" CssClass="form-control" Style="width: 80%;" AutoPostBack="true"
                                runat="server" OnTextChanged="txtAutoCompText_TextChanged"></asp:TextBox>
                        </div>
                    </h3>
                </div>
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="list-group" style="padding-left: 0px;">
                        <asp:Repeater runat="server" ID="repeatDelete" OnItemCommand="repeatDelete_ItemCommand"
                            OnItemDataBound="repeatDelete_ItemDataBound">
                            <ItemTemplate>
                                <div class="row list-group-item" runat="server" id="divMailItem">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Day") %>'></asp:Label><br />
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="Sender">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("From") %>'></asp:Label>
                                            </div>
                                            <div class="Subject">
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("Subject") %>'></asp:Label></div>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton runat="server" ToolTip="Open Mail" CommandArgument='<%# Bind("ID") %>'
                                                CommandName="Open" ID="lbtnOpen">
                                            <i class="fa fa-search color"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton runat="server" ToolTip="Flag" CommandArgument='<%# Bind("ID") %>'
                                                CommandName="Flag" ID="lbtFlag">
                                            <i class="fa fa-flag-o color"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton runat="server" ToolTip="UnFlag" CommandArgument='<%# Bind("ID") %>'
                                                CommandName="UnFlag" ID="lbtnUnFlag">
                                            <i class="fa fa-flag colorRed"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton runat="server" ToolTip="Mark As Read" CommandArgument='<%# Bind("ID") %>'
                                                CommandName="Read" ID="lbtnRead">
                                            <i class="fa fa-envelope-o color"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton runat="server" ToolTip="Mark As UnRead" CommandArgument='<%# Bind("ID") %>'
                                                CommandName="UnRead" ID="lbtnUnRead">
                                            <i class="fa fa-envelope color"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton runat="server" ToolTip="Restore" CommandArgument='<%# Bind("ID") %>'
                                                CommandName="RestoreMail" ID="lbtnRestore">
                                            <i class="fa fa-undo color"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:LinkButton runat="server" ToolTip="Permanently Delete" CommandArgument='<%# Bind("ID") %>'
                                                CommandName="DeleteMail" ID="lbtnDelete">
                                            <i class="fa fa-trash-o color"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
