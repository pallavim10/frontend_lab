<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_ASSIGN_LABS.aspx.cs" Inherits="SpecimenTracking.UMT_ASSIGN_LABS" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-12">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active"><a href="UserManagementDashboard.aspx">User Management</a></li>
                            <li class="breadcrumb-item active">Manage Labs</li>
                            <li class="breadcrumb-item active">Assign labs</li>
                        </ol>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Assign Labs</h3>
                                <div class="pull-right">
                                    <asp:LinkButton runat="server" Font-Size="14px" CssClass="btn btn-default" ForeColor="Black" ID="lbtnExport" OnClick="lbtnExport_Click"> Export Assigned Labs &nbsp;<span class="fas fa-download btn-xs"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <ContentTemplate>
            <div class="container-fluid">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="display:flex">
                            <div class="col-md-5">
                                <div class="card card-info" style="min-height: 264px;">
                                    <div class="card-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="card-title" align="left">Assign Lab To User
                                        </h4>
                                        <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-12">
                                                            <asp:label runat="server" ID="lblSelectUser" Text="Select User:" CssClass="font-weight-bold"></asp:label>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <asp:DropDownList ID="DrpUser" class="form-control drpControl required width200px" runat="server" AutoPostBack="True" Style="margin-bottom: 1px" OnSelectedIndexChanged="DrpUser_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="rows">
                                                    <div style="width: 100%; height: 300px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdLab" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 97%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="d-none"
                                                                        ItemStyle-CssClass="d-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Lab ID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLabID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Lab Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLabName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="ChkLabID" />
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
                            </div>
                            <div class="col-md-1">
                                <br />
                                <br />
                                <br />
                                <br />
                                <div class="card-body">
                                    <div style="min-height: 300px; padding:0.5%">
                                        <div class="row txtCenter justify-content-center">
                                            <asp:LinkButton ID="lbtnAddLab" ForeColor="White" Text="Add" runat="server"
                                                CssClass="btn btn-primary btn-sm" OnClick="lbtnAddLab_Click" />
                                        </div>
                                        <div class="row txtCenter">
                                            &nbsp;
                                        </div>
                                        <div class="row txtCenter justify-content-center">
                                            <asp:LinkButton ID="lbtnRemoveLab" ForeColor="White" Text="Remove" runat="server"
                                                CssClass="btn btn-primary btn-sm justify-content-center" OnClick="lbtnRemoveLab_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card card-info" style="min-height: 300px;">
                                    <div class="card-header with-border" style="float: left;">
                                        <h4 class="card-title" align="left">Records
                                        </h4>
                                        <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                    </div>
                                    <div class="card-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdAddedgrdLab" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="d-none"
                                                                        ItemStyle-CssClass="d-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Lab ID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLabID" runat="server" Text='<%# Bind("LabID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Lab Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLabname" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAddedLabID" />
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </div>
</asp:Content>

