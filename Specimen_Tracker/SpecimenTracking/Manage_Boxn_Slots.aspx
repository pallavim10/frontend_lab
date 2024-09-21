<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage_Boxn_Slots.aspx.cs" Inherits="SpecimenTracking.Manage_Boxn_Slots" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/2.1.4/css/dataTables.dataTables.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/2.1.4/js/dataTables.min.js"></script>
    <script src="Scripts/btnSave_Required.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Datatable").DataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                stateSave: true
            });
            $(".Datatable").parent().parent().addClass('fixTableHead');
        });
        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'BOX_MASTER';

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/SETUP_showAuditTrail",
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

        function VisitNameChanged(element) {
            event.preventDefault();
            $(element).closest('td').find("input[id*='btnvisitname_Changed']").click();

        }

        function CheckVisitNumber(element) {
            event.preventDefault();
            console.log("checking on input number");
            // Example of an immediate action on input change
            $(element).closest('td').find("input[id*='txtboxno']").click();
            __doPostBack('<%= txtBoxFrom.ClientID %>', 'TextChanged');
        }

        function VisitNameChangedOnInput(element) {
            event.preventDefault();
            console.log("checking on input");
            $(element).closest('td').find("input[id*='txtVistName']").click();
            // Example of an immediate action on input change
            //if (event.keyCode == 9 || event.keyCode == 13 || event.keyCode == 32 ) { // Tab key
            __doPostBack('<%= txtBoxTo.ClientID %>', 'TextChanged');
            // }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Manage Visit</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Manage Visit</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Define Visits</h3>
                                        <div class="pull-right">
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label for="txtSiteID">Site ID: &nbsp;</label>
                                                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <asp:DropDownList ID="dropdown_siteID" runat="server" Width="100%" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                                            <%--OnSelectedIndexChanged="dropdown_visit_SelectedIndexChanged"--%>
                                                            <asp:HiddenField ID="hdnSiteno" runat="server" />
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label for="txtBoxNo">No. of Boxes : &nbsp;</label>
                                                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <div class="row">
                                                                <div class="col-md-12 d-flex">
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="txtBoxFrom" runat="server" CssClass="form-control required  w-100" AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:Button runat="server" ID="btnBoxFrom_Changed" CssClass="d-none"></asp:Button>
                                                                        <asp:HiddenField ID="hdnBoxFrom" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="txtBoxTo" runat="server" CssClass="form-control required  w-100" AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:Button runat="server" ID="btnBoxTo_Changed" CssClass="d-none"></asp:Button>
                                                                        <asp:HiddenField ID="hdnBoxTo" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label for="txtBoxNo">No. of Slots : &nbsp;</label>
                                                            <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                            <div class="row">
                                                                <div class="col-md-12 d-flex">
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="txtSlotRow" runat="server" CssClass="form-control required  w-100" AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:Button runat="server" ID="btnSlowRow_Changed" CssClass="d-none"></asp:Button>
                                                                        <asp:HiddenField ID="hdnSlotRow" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <asp:TextBox ID="txtSlotColumn" runat="server" CssClass="form-control required  w-100" AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>
                                                                        <asp:Button runat="server" ID="SlotColumn_Changed" CssClass="d-none"></asp:Button>
                                                                        <asp:HiddenField ID="hdnSlotColumn" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <center>
                                                            <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Submit" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave"></asp:LinkButton>
                                                            <%--OnClick="lbtnSubmit_Click"--%>
                                                            &nbsp;&nbsp;
                                                            <asp:LinkButton runat="server" ID="lbtnUpdate" Text="Update" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" Visible="false"></asp:LinkButton>&nbsp;&nbsp;
                                                            <%-- OnClick="lbnUpdate_Click" --%>
                                                            <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                                                            <%-- OnClick="lbtnCancel_Click" --%>
                                                        </center>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card card-info">
                                    <div class="card-header">
                                        <h3 class="card-title">Records</h3>
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lbtnExport" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExport_Click" ForeColor="Black">Export Box n Slot &nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                            &nbsp;&nbsp;
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <asp:GridView ID="GrdBoxnSlot" AutoGenerateColumns="false" runat="server" class="table table-bordered table-striped responsive grid" DataKeyNames="ID" EmptyDataText="No Data Found!" Width="100%" AllowPaging="true" AllowSorting="true" PageSize="5" OnPageIndexChanging="GrdBoxnSlot_PageIndexChanging" >
                                                    <%--  OnRowCommand="GrdBoxnSlot_RowCommand" --%>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="BOXNSLOT_ID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBoxNSlotID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkedit" CommandName="EditVisit" CommandArgument='<%# Eval("ID") %>' runat="server" class="btn-info btn-sm"><i class="fas fa-edit"></i></asp:LinkButton>
                                                                <%--OnClick="lnkedit_Click"--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Site ID" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSiteID" runat="server" Text='<%# Eval("SITEID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Box From" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBoxesFrom" runat="server" Text='<%# Eval("BOX_FROM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Box To" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBoxesTo" runat="server" Text='<%# Eval("BOX_TO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Slot From" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlotFrom" runat="server" Text='<%# Eval("SLOTR_FROM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Slot To" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlotTo" runat="server" Text='<%# Eval("SLOTC_TO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Audit Trail" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkaudit_trail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail" CommandName="AuditTrailVisits" CommandArgument='<%# Eval("ID") %>'><i class="fas fa-history"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center align-middle" ItemStyle-CssClass="text-center align-middle">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkdelete" runat="server" class="btn-danger btn-sm" CommandName="DeleteVisit" CommandArgument='<%# Eval("ID") %>'><i class="fas fa-trash"></i></asp:LinkButton>
                                                                <%--OnClientClick="return confirmDelete(event);" OnClick="lnkDelete_Click" --%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
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

        });
        // Function to confirm deletion with SweetAlert
        function confirmDelete(event) {
            event.preventDefault();

            swal({
                title: "Warning!",
                text: "Are you sure you want to Delete this Records?",
                icon: "warning",
                buttons: true,
                dangerMode: true
            }).then(function (isConfirm) {
                console.log("Confirmation result: " + isConfirm);
                if (isConfirm) {
                    // Get the original link button element
                    var linkButton = event.target;

                    // If the target is an icon inside the link button, get the parent link button
                    if (linkButton.tagName.toLowerCase() === 'i') {
                        linkButton = linkButton.parentElement;
                    }

                    // Trigger the link button's click event
                    linkButton.onclick = null; // Remove the onclick event handler to avoid recursion
                    linkButton.click();
                } else {
                    Response.redirect(this);
                }
            });

            return false;
        }
    </script>
</asp:Content>

