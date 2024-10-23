<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_RoleRights.aspx.cs" Inherits="SpecimenTracking.UMT_RoleRights" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="Scripts/btnSave_Required.js"></script>
    <script type="text/javascript" language="javascript">

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'fa fa-minus-square';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'fa fa-plus-square';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'fa fa-plus-square') {
                img.className = 'fa fa-minus-square'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('fa fa-plus-square');
                $("i[id*='" + ID + "']").addClass('fa fa-minus-square');
            } else {
                img.className = 'fa fa-plus-square'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('fa fa-minus-square');
                $("i[id*='" + ID + "']").addClass('fa fa-plus-square');
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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">User Role Rights</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active"><a href="UserManagementDashboard.aspx">User Management</a></li>
                            <li class="breadcrumb-item active">Manage Roles</li>
                            <li class="breadcrumb-item active">User Role Rights</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">User Role Rights</h3>
                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label>Select Systems: &nbsp;</label>
                                                    <asp:DropDownList ID="drpSystems" runat="server" AutoPostBack="True" CssClass="form-control required" OnSelectedIndexChanged="drpSystems_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <div class="form-group">
                                                    <label>Select Roles:&nbsp; </label>
                                                    <asp:DropDownList ID="drpRoles" runat="server" CssClass="form-control required">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <asp:LinkButton runat="server" ID="lbtnGetRights" Text="Get Rights" ForeColor="White"
                                                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnGetRights_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <br />
        <section class="content">
            <div class="container-fluid">
                <div class="row" runat="server" id="divFunction" visible="false">
                    <div class="col-md-12">
                        <div class="rows">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="card card-info">
                                            <div class="card-header">
                                                <h3 class="card-title">Add User Role Rights</h3>
                                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                            </div>
                                            <div class="card-body">
                                                <div class="rows">
                                                    <div style="width: 100%; height: 100%; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdlevel1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-striped1"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="grdlevel1_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                        HeaderStyle-CssClass="txt_center">
                                                                        <HeaderTemplate>
                                                                            <a href="JavaScript:ManipulateAll('_Level1_');" id="_Folder" style="color: #333333"><i
                                                                                id="img_Level1_" class="fa fa-plus-square"></i></a>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <div runat="server" id="anchor">
                                                                                <a href="JavaScript:divexpandcollapse('_Level1_<%# Eval("ID") %>');" style="color: #333333">
                                                                                    <i id="img_Level1_<%# Eval("ID") %>" class="fa fa-plus-square"></i></a>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center d-none"
                                                                        ItemStyle-CssClass="txt_center d-none">
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
                                                                                                                id="img_Level2_" class="fa fa-plus-square"></i></a>
                                                                                                        </HeaderTemplate>
                                                                                                        <ItemTemplate>
                                                                                                            <div runat="server" id="anchor">
                                                                                                                <a href="JavaScript:divexpandcollapse('_Level2_<%# Eval("ID") %>');" style="color: #333333">
                                                                                                                    <i id="img_Level2_<%# Eval("ID") %>" class="fa fa-plus-square"></i></a>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center d-none"
                                                                                                        ItemStyle-CssClass="txt_center d-none">
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
                                                                                                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="txt_center d-none"
                                                                                                                                        ItemStyle-CssClass="txt_center d-none">
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
                                        <div class="card card-info">
                                            <div class="card-header">
                                                <h3 class="card-title">Added User Role Rights</h3>
                                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                            </div>
                                            <div class="card-body">
                                                <div class="rows">
                                                    <div style="width: 100%; height: 100%; overflow: auto;">
                                                        <aside class="sidebar-offcanvas" style="background: #333;">
                                                            <section class="sidebar">
                                                                <div id="sub" runat="server">
                                                                    <asp:ListView runat="server" ID="lstLevel1" OnItemDataBound="lstLevel1_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <ul class="sidebar-menu" data-widget="treeview" role="menu" data-accordion="true">
                                                                                <li id="lisub" runat="server"><a id="a1" runat="server" style="cursor: pointer;"><i class="fa fa-circle" style="color: white;"></i><span style="color: white;">
                                                                                    <%# Eval("Name") %></span> <i id="i1" style="color: white;" runat="server"></i></a>
                                                                                    <asp:LinkButton ID="lbtnName" Visible="false" CommandName="menu" CommandArgument='<%# Eval("Name") %>'
                                                                                        Text='<%# Eval("Name") %>' runat="server">
                                                                                    </asp:LinkButton>
                                                                                    <ul class="treeview-menu">
                                                                                        <asp:ListView runat="server" ID="lstLevel2" OnItemDataBound="lstLevel2_ItemDataBound">
                                                                                            <ItemTemplate>
                                                                                                <li id="li2" runat="server"><a id="a2" runat="server" style="cursor: pointer;" hello="test">
                                                                                                    <i class="fa fa-angle-double-right" style="color: white;"></i>
                                                                                                    <i id="i2" style="color: white;"  runat="server"></i>
                                                                                                    <span style="color: white;" commandargument='<%# Eval("Name") %>'><%# Eval("Name") %></span>
                                                                                                </a>
                                                                                                    <asp:LinkButton ID="lbtnName" Visible="false" Text='<%# Eval("Name") %>'
                                                                                                        CommandName="menu" CommandArgument='<%# Eval("Name") %>' runat="server"></asp:LinkButton>
                                                                                                    <ul class="treeview-menu">
                                                                                                        <asp:ListView runat="server" ID="lstLevel3" OnItemDataBound="lstLevel3_ItemDataBound">
                                                                                                            <ItemTemplate>
                                                                                                                <li id="li3" runat="server"><a id="a3" runat="server" style="cursor: pointer;">
                                                                                                                    <i id="i3" style="color: white;"  runat="server"></i><i class="fa fa-caret-right" style="color: white;"></i><span style="color: white;">
                                                                                                                        <%# Eval("Name") %></span> </a>
                                                                                                                    <asp:LinkButton ID="lbtnName" Visible="false" ForeColor="White" Text='<%# Eval("Name") %>'
                                                                                                                        CommandName="menu" CommandArgument='<%# Eval("Name") %>' runat="server"></asp:LinkButton>
                                                                                                                    <ul class="treeview-menu">
                                                                                                                        <asp:ListView runat="server" ID="lstLevel4" OnItemDataBound="lstLevel4_ItemDataBound">
                                                                                                                            <ItemTemplate>
                                                                                                                                <li id="li4" style="color: white;" runat="server"><a id="a4" runat="server" style="cursor: pointer;">
                                                                                                                                    <span style="color: white;">
                                                                                                                                        <%# Eval("Name") %> </span> <i id="i4" style="color: white;" runat="server"></i></a>
                                                                                                                                    <asp:LinkButton ID="lbtnmenu3" ForeColor="White" Text='<%# Eval("Name") %>' Visible="false"
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
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>>
</asp:Content>
