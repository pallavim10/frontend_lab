<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="USER_REPORT_DOWNLOAD_LOGS.aspx.cs" Inherits="CTMS.USER_REPORT_DOWNLOAD_LOGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style>
        .select2-container .select2-selection--multiple
        {
            min-height: 20px;
        }
    </style>
    <script type="text/javascript">

        function pageLoad() {
            $('.select').select2();

            var transpose = $('#MainContent_hdntranspose').val();

            if (transpose == 'FieldNameVise') {

                $(".Datatable").dataTable({
                    "bSort": true,
                    "ordering": true,
                    "bDestroy": false,
                    stateSave: false,
                    aaSorting: [[1, 'asc']]
                });

            }
            else {

                $(".Datatable").dataTable({
                    "bSort": true,
                    "ordering": true,
                    "bDestroy": false,
                    stateSave: false
                });

            }

            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });

            $('.txtTime').each(function (index, element) {
                $(element).inputmask(
        "hh:mm", {
            placeholder: "HH:MM",
            insertMode: false,
            showMaskOnHover: false,
            hourFormat: "24"
        }
      );
            });

        }

        $(function () {
            $('.txtTime').inputmask(
        "hh:mm", {
            placeholder: "HH:MM",
            insertMode: false,
            showMaskOnHover: false,
            hourFormat: "24"
        }
      );
        });

    </script>
    <style type="text/css">
        .fontBlue
        {
            color: Blue;
            cursor: pointer;
        }
        .circleQueryCountRed
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Red;
        }
        .circleQueryCountGreen
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Green;
        }
        .YellowIcon
        {
            color: Yellow;
        }
        .GreenIcon
        {
            color: Green;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="lblHeader" Text="User Activity Log" runat="server"></asp:Label>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div style="display: inline-flex">
                    <label class="label ">
                        From
                    </label>
                    <div class="Control" style="display: inline-flex">
                        <asp:TextBox ID="txtDateFrom" CssClass="form-control required txtDate" runat="server"
                            autocomplete="off" Width="88px"></asp:TextBox>
                        <asp:TextBox ID="txtTimeFrom" CssClass="form-control required txtTime" runat="server"
                            autocomplete="off" Width="54px" Text="00:00"></asp:TextBox>
                    </div>
                </div>
                <div runat="server" id="Div1" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label ">
                            To
                        </label>
                        <div class="Control" style="display: inline-flex">
                            <asp:TextBox ID="txtDateTo" CssClass="form-control required txtDate" runat="server"
                                autocomplete="off" Width="88px"></asp:TextBox>
                            <asp:TextBox ID="txtTimeTo" CssClass="form-control required txtTime" runat="server"
                                autocomplete="off" Width="54px" Text="23:59"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div runat="server" id="Div2" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <asp:Button ID="btnGet" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btnGet_Click" />
                    </div>
                </div>
                <div runat="server" id="Div4" style="display: inline-flex">
                    <div style="display: inline-flex">
                        &nbsp;&nbsp;
                    </div>
                </div>
                <div runat="server" id="Div3" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <div id="divDownload" class="dropdown" runat="server" style="display: inline-flex">
                            <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                style="color: #333333" title="Export"></a>
                            <ul class="dropdown-menu dropdown-menu-sm">
                                <li>
                                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                        Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                    </asp:LinkButton></li>
                                <hr style="margin: 5px;" />
                                <li>
                                    <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnPDF" OnClick="btnPDF_Click"
                                        ToolTip="Export to PDF" Text="Export to PDF" Style="color: #333333;">
                                    </asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="box-body">
                    <div class="rows">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped notranslate"
                                    EmptyDataText="No Records Available">
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
