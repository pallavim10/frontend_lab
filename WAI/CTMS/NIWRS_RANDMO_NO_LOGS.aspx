<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_RANDMO_NO_LOGS.aspx.cs" Inherits="CTMS.NIWRS_RANDMO_NO_LOGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>

    <style type="text/css">
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
            width: 160pt;
            height: 20pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Iwrs Randomization No Logs</h3>
          
            <div class="pull-right">
                <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Iwrs Randomization No. Logs&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                <ul class="dropdown-menu dropdown-menu-sm">
                    <li>
                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExcel_Click" CommandName="Excel" ToolTip="Excel"
                            Text="Excel" CssClass="dropdown-item" Style="color: #333333;">
                        </asp:LinkButton></li>
                    <hr style="margin: 5px;" />
                    <li>
                        <asp:LinkButton runat="server" ID="btnExportPDF" OnClick="btnExportPDF_Click" CssClass="dropdown-item"
                            ToolTip="PDF" Text="PDF" Style="color: #333333;">
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            </div>
            <div style="display: inline-flex">
                <label class="label ">
                    From Date :
                </label>
                <div class="Control" style="display: inline-flex">
                    <asp:TextBox ID="txtDateFrom" CssClass="form-control  txtDate" runat="server" autocomplete="off"
                        Width="120px"></asp:TextBox>
                </div>
            </div>
            <div runat="server" id="Div1" style="display: inline-flex">
                <div style="display: inline-flex">
                    <label class="label ">
                        To Date :
                    </label>
                    <div class="Control" style="display: inline-flex">
                        <asp:TextBox ID="txtDateTo" CssClass="form-control  txtDate" runat="server" autocomplete="off"
                            Width="120px"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div runat="server" id="Div2" style="display: inline-flex">
                <div style="display: inline-flex">
                    <asp:Button ID="btnGetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnGetdata_Click" />
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
</asp:Content>
