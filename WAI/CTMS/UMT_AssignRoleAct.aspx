<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_AssignRoleAct.aspx.cs" Inherits="CTMS.UMT_AssignRoleAct" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Assign User Roles
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnUserID" runat="server" />
                    <asp:HiddenField ID="hdnUserName" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title"></h3>
            <asp:HiddenField runat="server" ID="hdnACTIVE" />
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            User Type: &nbsp;             
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblUserType" runat="server" CssClass="form-control" Width="100%"></asp:Label>
                        </div>
                        <div runat="server" id="divSite" visible="false">
                            <div class="label col-md-2">
                                Site Id: &nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblSiteID" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                            </div>
                        </div>
                        <div runat="server" id="divExternal" visible="false">
                            <div class="label col-md-2">
                                <asp:Label ID="lblDiv" runat="server"></asp:Label>: &nbsp;
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblCompany" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            First Name: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFirstName" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                        </div>
                        <div class="label col-md-2">
                            Last Name:&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblLastName" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Email Id: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblEmailID" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                        </div>
                        <div class="label col-md-2">
                            Contact No:&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblContactNo" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Blinded/Unblinded: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblUnblined" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                        </div>
                        <div class="label col-md-2">
                            Study Role:&nbsp;
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblStudyRole" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Notes: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblNotes" runat="server" CssClass="form-control" Height="100%" TextMode="Multiline" Width="100%"></asp:Label>
                        </div>
                        <div class="label col-md-2">
                            &nbsp;             
                        </div>
                        <div class="col-md-4">
                            &nbsp;             
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="rows">
                    <div style="width: 100%;">
                        <div>
                            <asp:GridView ID="grdAssignRolesAct" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                                CssClass="table table-bordered table-striped Datatable" OnPreRender="grdUserDetails_PreRender" OnRowDataBound="grdAssignRolesAct_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Systems" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSystem" runat="server" Text='<%# Bind("SystemName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Priviledges" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubSystem" runat="server" Text='<%# Bind("SubSystem") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notes" HeaderStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSystemID" Text='<%# Bind("SystemID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RoleID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoleID" Text='<%# Bind("RoleID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select Roles" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpRoles" runat="server" class="form-control drpControl" Width="70%"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <br />
                    <div class="row txt_center">
                        <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                        &nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm" Visible="false"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

