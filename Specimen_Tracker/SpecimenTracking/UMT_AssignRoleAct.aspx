<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_AssignRoleAct.aspx.cs" Inherits="SpecimenTracking.UMT_AssignRoleAct" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .ExternalName {
            font-weight:bold !important;
            margin-bottom:10px !important;
        }
         .SponsorName {
            font-weight:bold !important;
            margin-bottom:10px !important;
        }
        .text_center {
            text-align:center !important;
        }
    </style>
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
        $(".numeric").on("keypress keyup blur", function (event) {
            $(this).val($(this).val().replace(/[^\d].+/, ""));
            if ((event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        });

        //only for numeric value
        $('.numeric').keypress(function (event) {

            if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                // let it happen, don't do anything
                return true;
            }
            else {
                event.preventDefault();
            }
        });

        $('.select').select2();
        }
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             $('.noSpace').keypress(function (e) {
                 if (e.which === 32) {
                     return false;
                 }
             });
         });
     </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Assign User Roles</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active"><a href="UserManagementDashboard.aspx">User Management</a></li>
                            <li class="breadcrumb-item active">Manage Users</li>
                            <li class="breadcrumb-item active">Assign User Roles</li>
                        </ol>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <div class="form-group has-warning">
                                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                            </div>
                        </div>
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
                                <h3 class="card-title"></h3>
                                <asp:HiddenField ID="hdnACTIVE" runat="server" />
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label id="Label5">User Type: &nbsp;</label>
                                                    <asp:Label ID="lblUserType" runat="server" CssClass="form-control w-100"></asp:Label>
                                                </div>
                                            </div>
                                              <div class="col-md-6">
                                                <div class="form-group">
                                                    <div runat="server" id="divSite" visible="false">
                                                        <label class="label" id="label6">
                                                            Site Id: &nbsp;
                                                        </label>
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblSiteID" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="divExternal" visible="false">
                                                        <div class="label9" id="label9" style="margin-bottom:10px; font-weight:600;">
                                                            <asp:Label ID="lblDiv" runat="server"></asp:Label>: &nbsp;
                                                        </div>
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblCompany" runat="server" CssClass=" form-control"  Width="100%"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label id="Label1">First Name: &nbsp;</label>
                                                    <asp:Label ID="lblFirstName" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label id="Label2">Last Name:&nbsp;</label>
                                                    <asp:Label ID="lblLastName" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label id="Label3">Email Id: &nbsp;</label>
                                                    <asp:Label ID="lblEmailID" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label id="Label4">Contact No:&nbsp;</label>
                                                    <asp:Label ID="lblContactNo" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label id="Label7">Blinded/Unblinded: &nbsp;</label>
                                                    <asp:Label ID="lblUnblined" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label id="Label8">Study Role:&nbsp;</label>
                                                    <asp:Label ID="lblStudyRole" runat="server" CssClass=" form-control" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Notes (If any):&nbsp;</label>
                                                    <asp:Label runat="server" ID="lblNotes" CssClass="form-control" TextMode="MultiLine" MaxLength="200"></asp:Label>
                                                    <label>&nbsp;</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div style="width: 100%;">
                                        <div>
                                            <asp:GridView ID="grdAssignRolesAct" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                                                CssClass="table table-bordered table-striped Datatable" OnPreRender="grdUserDetails_PreRender" OnRowDataBound="grdAssignRolesAct_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Systems" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="text_center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSystem" runat="server" Text='<%# Bind("SystemName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Privileges" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="text_center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubSystem" runat="server" Text='<%# Bind("SubSystem") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notes" HeaderStyle-Width="40%" ItemStyle-CssClass="text_center" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSystemID" Text='<%# Bind("SystemID") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RoleID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRoleID" Text='<%# Bind("RoleID") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select Roles" ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpRoles" runat="server" class="form-control drpControl" Width="70%" ></asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                                <br />
                                <br />
                                <center>
                                    <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;
                                <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm" Visible="false"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

