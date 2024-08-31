<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_ChangeRole.aspx.cs" Inherits="SpecimenTracking.UMT_ChangeRole" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="Style/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
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
                data: '{TABLENAME: "' + TABLENAME + '",ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivAuditTrail').html(data.d)
                    }
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });

            $("#popup_AuditTrail").dialog({
                title: "Audit Trail",
                width: 900,
                height: 450,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
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
                        <h1 class="m-0 text-dark">Change Roles</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item active">Change Roles</li>
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
                                <h3 class="card-title">User Role Details</h3>
                                <div class="pull-right">
                                    <asp:LinkButton runat="server" Font-Size="14px" CssClass="btn btn-default" ForeColor="Black" ID="lblUserRoleExport" ToolTip="Export to Excel" OnClick="lblUserRoleExport_Click"> Export All Users &nbsp;<span class="fas fa-download btn-xs"></span>
                                    </asp:LinkButton>
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                     <div style="width: 100%; overflow: auto;">
                <div>
                    <asp:GridView ID="grdAssignRoles" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-bordered table-striped Datatable" OnPreRender="grdUserDetails_PreRender" OnRowDataBound="grdAssignRoles_RowDataBound" OnRowCommand="grdAssignRoles_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none"
                                HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtedituser" runat="server" CommandArgument='<%# Bind("UserID") %>'
                                        CommandName="EIDITUSER" ToolTip="Assign Roles"><i class="fa fa-edit"></i></asp:LinkButton>&nbsp;&nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Site ID">
                                <ItemTemplate>
                                    <asp:Label ID="SiteID" runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User ID">
                                <ItemTemplate>
                                    <asp:Label ID="UserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Fname")+" "+Eval("Lname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Study Role ">
                                <ItemTemplate>
                                    <asp:Label ID="lblRole" runat="server" Text='<%# Bind("StudyRole") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EmailID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact No">
                                <ItemTemplate>
                                    <asp:Label ID="lblContact" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="User Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Bind("UserType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-left">
                                <HeaderTemplate>
                                    <label>Request Details </label>
                                    <br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Requested By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server">
                                        <div>
                                            <asp:Label ID="REQUESTBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("REQUESTBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="REQUEST_CAL_DAT" runat="server" Text='<%# Bind("REQUEST_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="REQUEST_CAL_TZDAT" runat="server" Text='<%# Eval("REQUEST_CAL_TZDAT")+" , "+Eval("REQUEST_TZVAL") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-left">
                                <HeaderTemplate>
                                    <label>Review Details </label>
                                    <br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Reviewed By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div runat="server">
                                        <div>
                                            <asp:Label ID="REVIEWBYNAME" runat="server" Enabled="false" Font-Bold="true" Text='<%# Bind("REVIEWBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="REVIEW_CAL_DAT" runat="server" Text='<%# Bind("REVIEW_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="REVIEW_CAL_TZDAT" runat="server" Text='<%# Eval("REVIEW_CAL_TZDAT")+" , "+Eval("REVIEW_TZVAL") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-history" style="color:blue; font-size:15px"></i></asp:LinkButton>
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

</asp:Content>

