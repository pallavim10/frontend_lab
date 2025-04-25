<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_UserSites.aspx.cs" Inherits="CTMS.UMT_UserSites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Assign Sites
            </h3>
            <div id="Div3" class="pull-right" runat="server">
                <asp:LinkButton runat="server" ID="lbtnExport" ToolTip="Export Assigned Sites" OnClick="lbtnExport_Click"
                    CssClass="btn btn-info" Style="color: white;">
                    Export Assigned Sites &nbsp;&nbsp; <span class="glyphicon glyphicon-download 2x"></span>
                </asp:LinkButton>
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnID" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-5">
                                <div class="box box-primary" style="min-height: 264px;">
                                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                        <h4 class="box-title" align="left">Assign Site To User
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-4">
                                                            <label>
                                                                Select User:</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <asp:DropDownList ID="DrpUser" class="form-control drpControl required width200px"
                                                                runat="server" AutoPostBack="True" Style="margin-bottom: 1px" OnSelectedIndexChanged="DrpUser_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="rows">
                                                    <div style="width: 100%; height: 300px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site ID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSiteName" runat="server" Text='<%# Bind("SiteName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="ChkSITEID" />
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
                                <div class="box-body">
                                    <div style="min-height: 300px;">
                                        <div class="row txtCenter">
                                            <asp:LinkButton ID="lbtnAddFunctionName" ForeColor="White" Text="Add" runat="server"
                                                CssClass="btn btn-primary btn-sm cls-btnSave " OnClick="lbtnAddFunctionName_Click" />
                                        </div>
                                        <div class="row txtCenter">
                                            &nbsp;
                                        </div>
                                        <div class="row txtCenter">
                                            <asp:LinkButton ID="lbtnRemoveFunctionName" ForeColor="White" Text="Remove" runat="server"
                                                CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnRemoveFunctionName_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box box-primary" style="min-height: 300px;">
                                    <div class="box-header with-border" style="float: left;">
                                        <h4 class="box-title" align="left">Records
                                        </h4>
                                    </div>
                                    <div class="box-body">
                                        <div align="left" style="margin-left: 5px">
                                            <div>
                                                <div class="rows">
                                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                                        <div>
                                                            <asp:GridView ID="grdAddedUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                        ItemStyle-CssClass="disp-none">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site ID" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site Name" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSiteName" runat="server" Text='<%# Bind("SiteName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Options" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAddedSiteID" />
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
        <%--  <Triggers>
            <asp:PostBackTrigger ControlID="lbAssignINVUserExport" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
