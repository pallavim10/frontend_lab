<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_RPT_SysMaster.aspx.cs" Inherits="SpecimenTracking.UMT_RPT_SysMaster" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": false,
                "bDestroy": false,
                stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

            $(document).on("click", ".cls-btnSave1", function () {
                var test = "0";

                $('.required').each(function (index, element) {
                    var value = $(this).val();
                    var ctrl = $(this).prop('type');

                    if (ctrl == "select-one") {
                        if (value == "-1" || value == null || value == "-Select-" || value == "--Select--" || value == "0") {
                            $(this).addClass("brd-1px-redimp");
                            test = "1";
                        }
                    }
                });

                if (test == "1") {
                    return false;
                }
                return true;
            });
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
                        <h3>
                            <asp:Label runat="server" ID="lblHeader" Text="System Master Logs"></asp:Label></h3>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item">Logs</li>
                            <li class="breadcrumb-item active">System Master Logs</li>
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
                                <h3 class="card-title">System Master Logs</h3>
                                <div class="pull-right">
                                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" CssClass="btn btn-default" Style="color: #333333; font-size: 14px;">Export System Master Logs<span class="fa fa-download btn-xs"></span></asp:LinkButton>
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
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>From Date : &nbsp; </label>
                                                    <asp:TextBox runat="server" ID="txtDateFrom" CssClass="form-control  txtDate" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>To Date :&nbsp;</label>
                                                    <asp:TextBox runat="server" ID="txtDateTo" CssClass="form-control txtDate" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-4 align-content-center">
                                                <div class="d-inline-flex align-items-center">
                                                    <asp:Button ID="btnGet" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnGet_Click" />
                                                    <div id="divDownload" class="dropdown " runat="server" style="margin-left: 5px;">

                                                        <ul class="dropdown-menu dropdown-menu-sm">
                                                            <li></li>
                                                            <hr style="margin: 5px;" />
                                                        </ul>
                                                    </div>
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
                <div class="row" id="DivRecord" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Records</h3>
                                <div class="pull-right">
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div style="width: 100%; overflow: auto;">
                                                    <div>
                                                        <asp:GridView ID="grdData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                                            Width="100%" OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped">
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
            </section>
    </div>
</asp:Content>

