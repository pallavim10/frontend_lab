<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_SysUser.aspx.cs" Inherits="SpecimenTracking.UMT_SysUser" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="Style/Select2.css" rel="stylesheet" type="text/css" />
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
            var TABLENAME = 'UMT_SysMaster';

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
                        <h1 class="m-0 text-dark">Assign Master Users</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">Manage Users</li>
                            <li class="breadcrumb-item active">Assign Roles</li>
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
                                <h3 class="card-title">Assign Master Users</h3>
                                <div class="pull-right">
                                    <asp:LinkButton runat="server" ID="lblMasterUsersExport" ToolTip="Export to Excel" OnClick="lblMasterUsersExport_Click" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" ForeColor="Black">
			Export Assigned Master Users &nbsp;<span class="fas fa-download btn-xs"></span>
                                    </asp:LinkButton>&nbsp;&nbsp;
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="rows">
                                        <div style="width: 100%; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="grdSysUser" runat="server" AutoGenerateColumns="false"
                                                    CssClass="table table-bordered table-striped" OnRowDataBound="grdSysUser_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"
                                                            HeaderText="ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Systems" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSystem" runat="server" Text='<%# Bind("SystemName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="txt_center d-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSystemID" Text='<%# Bind("SystemID") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Select User" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="drpInternalUser" runat="server" class="form-control drpControl select" SelectionMode="Single" Width="50%"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" CssClass="btn-info btn-sm" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <br />
                                    <div class="row d-block">
                                        <center>
                                            <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lbnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm" Visible="false"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" OnClick="lbtnCancel_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                                        </center>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
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

