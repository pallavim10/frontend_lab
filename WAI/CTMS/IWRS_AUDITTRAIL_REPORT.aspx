<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IWRS_AUDITTRAIL_REPORT.aspx.cs" Inherits="CTMS.IWRS_AUDITTRAIL_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }

        .btn-info {
            background-repeat: repeat-x;
            border-color: #28a4c9;
            /*background-image: linear-gradient(to bottom, #5bc0de 0%, #2aabd2 100%);*/
        }

        .prevent-refresh-button {
            display: inline-block;
            padding: 5px 5px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.428571429;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border: 1px solid transparent;
            border-radius: 4px;
            width: 110pt;
            height: 20pt;
        }
    </style>

    <script type="text/javascript">

        function pageLoad() {
            $('.select').select2();
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">AuditTrail Report
            </h3>
            <br />
            <div class="pull-right">
                <%--<asp:LinkButton runat="server" ID="btnPDF" 
                    ToolTip="Export to PDF" Text="Export to PDF" ForeColor="White" OnClick="btnPDF_Click" Font-Size="12px" Style="margin-top: 3px; margin-right:3px;"  CssClass="btn btn-info">Export AuditTrail &nbsp;&nbsp;<span class="glyphicon glyphicon-download 2x"></span>&nbsp;&nbsp;
                </asp:LinkButton>--%>

                <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Kit Summary&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                        <ul class="dropdown-menu dropdown-menu-sm">
                            <li>
                                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" CommandName="Excel" ToolTip="Excel"
                                    Text="Excel" CssClass="dropdown-item" Style="color: #333333;">
                                </asp:LinkButton></li>
                            <hr style="margin: 5px;" />
                            <li>
                                <asp:LinkButton runat="server" ID="btnExportPDF" OnClick="btnPDF_Click" CssClass="dropdown-item"
                                    ToolTip="PDF" Text="PDF" Style="color: #333333;">
                                </asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>
             <div class="box-body">
<div class="form-group">
    <div class="form-group has-warning">
        <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
    </div>


    <div class="form-group" style="display: flex; flex-wrap: wrap;">
        <div class="form-group" style="display: inline-flex; flex: 1;">
            <label class="label">
                Site ID:
            </label>
            <div class="Control">
                <asp:DropDownList ID="drpSiteID" runat="server" AutoPostBack="True" CssClass="form-control required" OnSelectedIndexChanged="drpSiteID_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>

        <div class="form-group" style="display: inline-flex; flex: 1;">
            <label class="label">
                <asp:Label ID="lbSubject" runat="server" text=""></asp:Label>:
            </label>
            <div class="Control">
                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required select" AutoPostBack="True" OnSelectedIndexChanged="drpSubID_SelectedIndexChanged" SelectionMode="Single">
                </asp:DropDownList>
            </div>
        </div>

     
        <div class="form-group" style="display: inline-flex; flex: 1;">
            <label class="label" style="font-size: 14px;">
                DCF ID:
            </label>
            <div class="Control">
                <asp:TextBox ID="TxtDCFID" runat="server" CssClass="form-control required select" autocomplete="off" Width="118px" Style="font-size: 14px;" />
            </div>
        </div>
    </div>


    <div class="form-group" style="display: flex; flex-wrap: wrap;">
        <div class="form-group" style="display: inline-flex; flex: 1;">
            <label class="label">
                From Date:
            </label>
            <div class="Control" style="display: inline-flex">
                <asp:TextBox ID="txtDateFrom" CssClass="form-control txtDate" runat="server" autocomplete="off" Width="120px"></asp:TextBox>
            </div>
        </div>

        <div class="form-group" style="display: inline-flex; flex: 1;">
            <label class="label">
                To Date:
            </label>
            <div class="Control" style="display: inline-flex">
                <asp:TextBox ID="txtDateTo" CssClass="form-control txtDate" runat="server" autocomplete="off" Width="120px"></asp:TextBox>
            </div>
        </div>

        <div class="form-group" style="display: inline-flex; flex: 1;">
            <div runat="server" id="Div2" style="display: inline-flex">
                <div style="display: inline-flex">
                    <asp:Button ID="btnGetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnGetdata_Click" />
                </div>
            </div>
        </div>
    </div>
</div>
                    <div class="rows">
                        <div class="col_md-12">
                            <div style="width: 100%; overflow: auto;">
                                <div>
                                    <asp:GridView ID="GrdData" runat="server" AutoGenerateColumns="True"
                                        CssClass="table table-bordered table-striped Datatable" OnPreRender="GrdData_PreRender">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGetdata" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
