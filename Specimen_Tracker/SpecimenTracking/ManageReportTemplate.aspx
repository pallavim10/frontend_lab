<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageReportTemplate.aspx.cs" Inherits="SpecimenTracking.ManageReportTemplate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">

        function showAuditTrail(element) {

            var ID = $(element).closest('tr').find('td').eq(0).text().trim();
            var TABLENAME = 'SETUP_REPORT_TEMPLATE';

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
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Manage Repoert Templates</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active"><a href="SETUPDashboard.aspx">Setup</a></li>
                            <li class="breadcrumb-item active">Manage Repoert Templates</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-6">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Shipment Manifest(Word)</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Upload  File : &nbsp;</label>
                                                    <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:FileUpload ID="Manifestfilename" runat="server" CssClass="form-control w-75" /><br />
                                                    <label style="color: red; font-weight: normal;">[Note : Please Select Word File Only]</label>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <center>
                                                    <asp:LinkButton runat="server" ID="lbtnUpload" Text="Upload" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnUpload_Click"></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>
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
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="rows">
                                        <div class="col-md-12">
                                            <div style="width: 100%; max-height: 352px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="GrdShipment" AutoGenerateColumns="false" runat="server" class="table table-bordered table-striped responsive" EmptyDataText="No Data Found!" Width="100%" OnRowCommand="GrdShipment_RowCommand">

                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" ToolTip='<%# Eval("SHIPMENT_COLUMNS") %>' Text='<%# Bind("FILENAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDownload" runat="server" CommandName="Download" CommandArgument='<%# Bind("ID") %>' class="btn-info btn-sm" ToolTip="Download"><i class="fa fa-download"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-6">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Shipment Manifest(Excel)</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Upload  File : &nbsp;</label>
                                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:FileUpload ID="FileUploadExcel" runat="server" CssClass="form-control w-75" /><br />
                                                    <label style="color: red; font-weight: normal;">[Note : Please Select Excel File Only]</label>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <center>
                                                    <asp:LinkButton runat="server" ID="btnSubexcel" Text="Upload" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="btnSubexcel_Click"></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton runat="server" ID="btnCalexcel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="btnCalexcel_Click"></asp:LinkButton>
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
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="rows">
                                        <div class="col-md-12">
                                            <div style="width: 100%; max-height: 352px; overflow: auto;">
                                                <div>
                                                    <asp:GridView ID="grd_data" AutoGenerateColumns="false" runat="server" class="table table-bordered table-striped responsive" EmptyDataText="No Data Found!" Width="100%" OnRowCommand="grd_data_RowCommand">

                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="d-none" ItemStyle-CssClass="d-none" HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="File Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FILENAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDownload" runat="server" CommandName="Download" CommandArgument='<%# Bind("ID") %>' class="btn-info btn-sm" ToolTip="Download"><i class="fa fa-download"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" class="btn-info btn-sm" OnClientClick="return showAuditTrail(this);" ToolTip="Audit Trail"><i class="fa fa-history"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
