<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_RPT_SponsorUsers.aspx.cs" Inherits="SpecimenTracking.UMT_RPT_SponsorUsers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="Style/Select2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pageLoad() {
            $('.select').select2();
            //$(".Datatable").dataTable({
            //    "bSort": true,
            //    "ordering": false,
            //    "bDestroy": false,
            //    stateSave: true
            //});
            

            $('.Datatable').parent().parent().addClass('fixTableHead');


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
                        <h3><asp:Label runat="server" ID="lblHeader" Text="Sponsor Users Report"></asp:Label></h3>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home </a></li>
                            <li class="breadcrumb-item">Reports</li>
                            <li class="breadcrumb-item active">Sponsor Users Report</li>
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
                                <h3 class="card-title">Sponsor Users Report</h3>
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
                                                    <div id="divDownload" class="dropdown " runat="server" style="margin-left:5px;">
                                                <a href="#" class="dropdown-toggle fa fa-download" data-toggle="dropdown"
                                                    style="color: #333333" title="Export"></a>
                                                <ul class="dropdown-menu dropdown-menu-sm">
                                                    <li>
                                                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                                        </asp:LinkButton></li>
                                                    <hr style="margin: 5px;" />
                                                </ul>
                                            </div>
                                                </div>
                                                </div>
                                          
                                                
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
    <script type="text/javascript" src="plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript">

        function pageLoad() {
            Sys.Application.add_load(function () {
                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                        // firstDay: 1,
                        //position: 'top right',
                        // minDate: new Date('2000-01-01'),
                        // maxDate: new Date('9999-12-31'),
                        format: 'DD-MMM-YYYY',
                        //  defaultDate: new Date(''),
                        //setDefaultDate: false,
                        yearRange: [1910, 2050]
                    });
                });
            });

            Sys.Application.add_load(function () {
                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050]
                    });
                });
            });
            var picker = new Pikaday({
                field: document.getElementById('testDate'),
                format: 'DD-MMM-YYYY',
                yearRange: [1910, 2050]
            });

        }
    </script>
</asp:Content>
