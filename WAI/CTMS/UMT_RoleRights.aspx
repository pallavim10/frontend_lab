<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_RoleRights.aspx.cs" Inherits="CTMS.UMT_RoleRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">

        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }

    </script>
    <script type="text/javascript" language="javascript">

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }
    </script>
    <style type="text/css">
        .menu {
            text-align: center;
        }

        .fontstyle {
            font-size: medium;
        }
    </style>
    <style>
        .fa.pull-right {
            margin-right: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" id="header" runat="server">User Role Rights
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label">
                            Select Systems:
                        </label>
                        <div class="Control">
                            <asp:DropDownList ID="drpSystems" runat="server" AutoPostBack="True" CssClass="form-control required" OnSelectedIndexChanged="drpSystems_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <label class="label">
                        Select Roles:
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpRoles" runat="server" CssClass="form-control required">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group" style="display: inline-flex">
                    <div class="form-group" style="display: inline-flex">
                        <asp:LinkButton runat="server" ID="lbtnGetRights" Text="Get Rights" ForeColor="White"
                            CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnGetRights_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="divFunction" class="row" visible="false">
        <div class="col-md-6">
            <div class="box box-primary" style="min-height: 300px;">
                <div class="box-header with-border" style="float: left;">
                    <h4 class="box-title" align="left">Add User Role Rights
                    </h4>
                </div>
                <div class="box-body">
                    <div align="left" style="margin-left: 5px">
                        <div class="row">
                            <div style="width: 96%; height: 100%; overflow: auto;">
                                <div>
                                    <asp:GridView ID="grdlevel1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-striped1"
                                        Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="grdlevel1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                HeaderStyle-CssClass="txt_center">
                                                <HeaderTemplate>
                                                    <a href="JavaScript:ManipulateAll('_Level1_');" id="_Folder" style="color: #333333"><i
                                                        id="img_Level1_" class="icon-plus-sign-alt"></i></a>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div runat="server" id="anchor">
                                                        <a href="JavaScript:divexpandcollapse('_Level1_<%# Eval("ID") %>');" style="color: #333333">
                                                            <i id="img_Level1_<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center disp-none"
                                                ItemStyle-CssClass="txt_center disp-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSeleect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Function">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                            <div style="float: right; font-size: 13px;">
                                                            </div>
                                                            <div>
                                                                <div id="_Level1_<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                    <asp:GridView ID="grdlevel2" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                        CssClass="table table-bordered table-striped table-striped1" OnRowDataBound="grdlevel2_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                                HeaderStyle-CssClass="txt_center">
                                                                                <HeaderTemplate>
                                                                                    <a href="JavaScript:ManipulateAll('_Level2_');" id="_Folder" style="color: #333333"><i
                                                                                        id="img_Level2_" class="icon-plus-sign-alt"></i></a>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <div runat="server" id="anchor">
                                                                                        <a href="JavaScript:divexpandcollapse('_Level2_<%# Eval("ID") %>');" style="color: #333333">
                                                                                            <i id="img_Level2_<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center disp-none"
                                                                                ItemStyle-CssClass="txt_center disp-none">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSeleect" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Function">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblName" Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                            <div style="float: right; font-size: 13px;">
                                                                                            </div>
                                                                                            <div>
                                                                                                <div id="_Level2_<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                                                    <asp:GridView ID="grdlevel3" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                                        CssClass="table table-bordered table-striped table-striped1" OnRowDataBound="grdlevel3_RowDataBound">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center disp-none"
                                                                                                                ItemStyle-CssClass="txt_center disp-none">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkSeleect" runat="server" />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Function">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblName" Text='<%# Eval("Name") %>' runat="server"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </asp:GridView>
                                                                                                </div>
                                                                                            </div>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-1">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="box-body">
                <div style="min-height: 300px;">
                    <div class="row txtCenter">
                        <asp:LinkButton ID="lbtnAddFunctionName" ForeColor="White" Text="Add / Remove" runat="server"
                            CssClass="btn btn-primary btn-sm wrapperLable" OnClick="lbtnAddFunctionName_Click" />
                    </div>
                    <div class="row txtCenter">
                        &nbsp;
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-5">
            <div class="box box-primary" style="min-height: 300px;">
                <div class="box-header with-border" style="float: left;">
                    <h4 class="box-title" align="left">Added User Role Rights
                    </h4>
                </div>
                <div class="box-body">
                    <div align="left" style="margin-left: 17px">
                        <div class="row">
                            <div style="width: 96%; height: 100%; overflow: auto;">
                                <aside class="sidebar-offcanvas" style="background: #333;">
                                    <section class="sidebar">
                                        <div id="sub" runat="server">
                                            <asp:ListView runat="server" ID="lstLevel1" OnItemDataBound="lstLevel1_ItemDataBound">
                                                <ItemTemplate>
                                                    <ul class="sidebar-menu">
                                                        <li id="lisub" runat="server"><a id="a1" runat="server" style="cursor: pointer;"><i class="fa fa-circle" style="color: white;"></i><span>
                                                            <%# Eval("Name") %></span> <i id="i1" runat="server"></i></a>
                                                            <asp:LinkButton ID="lbtnName" Visible="false" CommandName="menu" CommandArgument='<%# Eval("Name") %>'
                                                                Text='<%# Eval("Name") %>' runat="server">
                                                            </asp:LinkButton>
                                                            <ul class="treeview-menu">
                                                                <asp:ListView runat="server" ID="lstLevel2" OnItemDataBound="lstLevel2_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <li id="li2" runat="server"><a id="a2" runat="server" style="cursor: pointer;" hello="test">
                                                                            <i class="fa fa-angle-double-right" style="color: white;"></i>
                                                                            <i id="i2" runat="server"></i>
                                                                            <span commandargument='<%# Eval("Name") %>'><%# Eval("Name") %></span>
                                                                        </a>
                                                                            <asp:LinkButton ID="lbtnName" Visible="false" Text='<%# Eval("Name") %>'
                                                                                CommandName="menu" CommandArgument='<%# Eval("Name") %>' runat="server"></asp:LinkButton>
                                                                            <ul class="treeview-menu">
                                                                                <asp:ListView runat="server" ID="lstLevel3" OnItemDataBound="lstLevel3_ItemDataBound">
                                                                                    <ItemTemplate>
                                                                                        <li id="li3" runat="server"><a id="a3" runat="server" style="cursor: pointer;">
                                                                                            <i id="i3" runat="server"></i><i class="fa fa-caret-right" style="color: white;"></i><span>
                                                                                                <%# Eval("Name") %></span> </a>
                                                                                            <asp:LinkButton ID="lbtnName" Visible="false" Text='<%# Eval("Name") %>'
                                                                                                CommandName="menu" CommandArgument='<%# Eval("Name") %>' runat="server"></asp:LinkButton>
                                                                                            <ul class="treeview-menu">
                                                                                                <asp:ListView runat="server" ID="lstLevel4" OnItemDataBound="lstLevel4_ItemDataBound">
                                                                                                    <ItemTemplate>
                                                                                                        <li id="li4" runat="server"><a id="a4" runat="server" style="cursor: pointer;">
                                                                                                            <span>
                                                                                                                <%# Eval("Name") %></span> <i id="i4" runat="server"></i></a>
                                                                                                            <asp:LinkButton ID="lbtnmenu3" Text='<%# Eval("Name") %>' Visible="false"
                                                                                                                CommandName="menu" CommandArgument='<%# Eval("Name") %>' runat="server"><i class="fa fa-angle-left pull-right" style="color:white;"></i> </asp:LinkButton>
                                                                                                        </li>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>
                                                                                            </ul>
                                                                                        </li>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </ul>
                                                                        </li>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </ul>
                                                        </li>
                                                    </ul>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </section>
                                </aside>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
