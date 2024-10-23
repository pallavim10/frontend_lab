<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_SiteUsersApproval.aspx.cs" Inherits="SpecimenTracking.UMT_SiteUsersApproval" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="Scripts/btnSave_Required.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="Style/Select2.css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>

    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
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

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'UMT_Users';

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Create Site User</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active"><a href="UserManagementDashboard.aspx">User Management</a></li>
                            <li class="breadcrumb-item active">Manage Sites</li>
                            <li class="breadcrumb-item active">Site Users</li>
                        </ol>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <div class="form-group has-warning">
                                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdnUserID" runat="server" />
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
                                <h3 class="card-title">Site Users Details</h3>
                                <div class="pull-right">

                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div>
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Site Id : &nbsp; </label>
                                                    <asp:Label ID="lblSiteID" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="Small">
                                                    </asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Study Role :&nbsp;</label>
                                                    <asp:Label ID="lblStudyRole" runat="server" Font-Size="Small" CssClass=" form-control"></asp:Label>
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
                                                    <label>First Name : &nbsp;</label>
                                                    <asp:Label ID="lblFirstName" runat="server" Font-Size="Small" CssClass=" form-control"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Last Name :&nbsp;</label>
                                                    <asp:Label ID="lblLastName" runat="server" Font-Size="Small" CssClass=" form-control"></asp:Label>
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
                                                    <label>Email Id :&nbsp;</label>
                                                    <asp:Label ID="lblEmailID" runat="server" Font-Size="Small" CssClass=" form-control"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Contact No :&nbsp;</label>
                                                    <asp:Label ID="lblContactNo" runat="server" Font-Size="Small" CssClass=" form-control"></asp:Label>
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
                                                    <label>Blinded/Unblinded :&nbsp;</label>
                                                    <asp:Label ID="lblUnblined" runat="server" Font-Size="Small" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Notes (If any):&nbsp;</label>
                                                    <asp:Label runat="server" ID="lblNotes" CssClass="form-control" TextMode="MultiLine" MaxLength="200"></asp:Label>
                                                    <label>&nbsp;</label>
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
                                                <label>Select Systems & Privileges: &nbsp;</label>
                                                <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*" Visible="false"></asp:Label>
                                                <div class="col-md-8">
                                                    <asp:UpdatePanel runat="server" ID="upnlSystems">
                                                        <ContentTemplate>
                                                            <table class="table table-bordered table-striped">
                                                                <tr>
                                                                    <th class="col-md-4">Systems
                                                                    </th>
                                                                    <th class="col-md-4 d-none">Privileges
                                                                    </th>
                                                                    <th class="col-md-4">Notes (If any)
                                                                    </th>
                                                                </tr>
                                                                <asp:Repeater runat="server" ID="repeatSystem" OnItemDataBound="repeatSystem_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td class="col-md-4">
                                                                                <asp:CheckBox ID="ChkSelect" runat="server" Enabled="false" />
                                                                                &nbsp;
                                                        <asp:Label ID="lblSystemName" runat="server" Text='<%# Bind("SystemName") %>'></asp:Label>
                                                                                <asp:Label ID="lblSystemID" runat="server" Text='<%# Bind("SystemID") %>' Visible="false"></asp:Label>
                                                                                <asp:HiddenField runat="server" ID="HiddenSubSytem" Value='<%# Eval("SubSystem") %>' />
                                                                            </td>
                                                                            <td class="col-md-4 d-none"></td>
                                                                            <td class="col-md-4">
                                                                                <asp:Label runat="server" ID="lblSystemNotes" CssClass="form-control w-100" TextMode="MultiLine" MaxLength="200" Enabled="false" Text='<%# Bind("Notes")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Action:&nbsp;</label>
                                                    <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300"></asp:Label>
                                                    <asp:DropDownList runat="server" ID="drpAction" CssClass="form-control required">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <asp:ListItem Value="Approve" Text="Approve"></asp:ListItem>
                                                        <asp:ListItem Value="Disapprove" Text="Disapprove"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Comment :&nbsp;</label>
                                                    <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" CssClass="form-control required"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <center>
                                    <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </section>
        <br />
    </div>
    <script type="text/javascript">

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
