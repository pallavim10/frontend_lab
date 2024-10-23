<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_StudyRole.aspx.cs" Inherits="SpecimenTracking.UMT_StudyRole" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script type="text/javascript">

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'UMT_StudyRole';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/showAuditTrail",
                data: JSON.stringify({ TABLENAME: TABLENAME, ID: ID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    } else {
                        $('#DivAuditTrail').html(response.d);
                        $('#modal-lg').modal('show'); // Show the modal after populating it
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching audit trail:', status, error);
                    alert("An error occurred. Please contact the administrator.");
                }
            });

            return false;
        }


        function confirm(event) {
            event.preventDefault();

            swal({
                title: "Warning!",
                text: "Are you sure you want to delete this Record?",
                icon: "warning",
                buttons: true,
                dangerMode: true
            }).then(function (isConfirm) {
                if (isConfirm) {
                    var linkButton = event.target;
                    if (linkButton.tagName.toLowerCase() === 'i') {
                        linkButton = linkButton.parentElement;
                    }
                    linkButton.onclick = null;
                    linkButton.click();
                } else {
                    Response.redirect(this);
                }
            });
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Study Roles</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active"><a href="UserManagementDashboard.aspx">User Management</a></li>
                            <li class="breadcrumb-item active">Manage Roles</li>
                            <li class="breadcrumb-item active">Study Roles</li>
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
                                <h3 class="card-title">Add Study Role</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbStudyRoleExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbStudyRoleExport_Click" ForeColor="Black">Export Study Roles&nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label>
                                                    Enter Study Role :</label>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:TextBox Style="width: 250px;" ID="txtStudyRole" ValidationGroup="section" runat="server"
                                                    CssClass="form-control required1 "></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label>
                                                    Applicable For:</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInternal" />&nbsp;&nbsp;
                                                    <label>Internal User</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSponsor" />&nbsp;&nbsp;
                                                    <label>Sponsor User</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSite" />&nbsp;&nbsp;
                                                    <label>Site User</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkExternal" />&nbsp;&nbsp;
                                                    <label>External User</label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <center>
                                        <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnUpdate" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpdate_Click" />&nbsp;&nbsp
                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnCancel_Click" />&nbsp;&nbsp;
                                    </center>
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
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Records</h3>
                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div style="width: 100%; overflow: auto; height: auto">
                                        <div>
                                            <asp:GridView ID="grdStudyRoles" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                                CssClass="table table-bordered table-striped Datatable1" OnPreRender="grdUserDetails_PreRender" OnRowCommand="grdStudyRoles_RowCommand" OnRowDataBound="grdStudyRoles_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="d-none"
                                                        ItemStyle-CssClass="d-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>' CssClass="btn-info btn-sm"
                                                                CommandName="EditStudy" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Study Role" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStudyRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Internal User" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInternal" runat="server" CommandArgument='<%# Eval("Internal") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconInternal" runat="server" class="fa fa-check"></i></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Site User" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSite" runat="server" CommandArgument='<%# Eval("Site") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSite" runat="server" class="fa fa-check"></i></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sponsor User" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSponsor" runat="server" CommandArgument='<%# Eval("Sponsor") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconSponsor" runat="server" class="fa fa-check"></i></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="External User" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExternal" runat="server" CommandArgument='<%# Eval("OtherExternal") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconExternal" runat="server" class="fa fa-check"></i></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" CssClass="btn-info btn-sm" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="DeleteStudyRole" CssClass="btn-danger btn-sm" OnClientClick="return confirm(event);" ToolTip="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
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
        </section>
    </div>
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
