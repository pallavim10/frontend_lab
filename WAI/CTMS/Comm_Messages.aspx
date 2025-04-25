<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Comm_Messages.aspx.cs" Inherits="CTMS.Comm_Messages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script type="text/jscript">
        function pageLoad() {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        }
    </script>
    <style type="text/css">
        .Sender
        {
            margin-left: 7%;
            font-size: initial;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning" style="border: none;">
        <div class="box-header">
            <h3 class="box-title">
                Inbox
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="tabscontainer" runat="server">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab-1" data-toggle="tab">Inbox</a> </li>
                            <li><a href="#tab-2" data-toggle="tab">Sent Items</a> </li>
                            <li><a href="#tab-3" data-toggle="tab">Deleted Items</a> </li>
                        </ul>
                        <div class="tab">
                            <div id="tab-1" class="tab-content current">
                                <div class="list-group" style="padding-left: 0px;">
                                    <asp:Repeater runat="server" ID="repeatInbox" OnItemCommand="repeatInbox_ItemCommand"
                                        OnItemDataBound="repeatInbox_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="row list-group-item" runat="server" id="divMailItem">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Day") %>'></asp:Label><br />
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
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
                                                        <asp:LinkButton runat="server" ToolTip="Delete" CommandArgument='<%# Bind("ID") %>'
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
                            <div id="tab-2" class="tab-content">
                                <div class="list-group" style="padding-left: 0px;">
                                    <asp:Repeater runat="server" ID="repeatSent" OnItemCommand="repeatSent_ItemCommand"
                                        OnItemDataBound="repeatSent_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="row list-group-item" runat="server" id="divMailItem">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Day") %>'></asp:Label><br />
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Time") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="Sender">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("To") %>'></asp:Label>
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
                                                        <asp:LinkButton runat="server" ToolTip="Delete" CommandArgument='<%# Bind("ID") %>'
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
                            <div id="tab-3" class="tab-content">
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
                                                    <div class="col-md-4">
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
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
