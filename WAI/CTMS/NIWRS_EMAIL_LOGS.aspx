<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_EMAIL_LOGS.aspx.cs" Inherits="CTMS.NIWRS_EMAIL_LOGS" %>

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
            width: 90pt;
            height: 20pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">Email Logs</h3>
            <div class="pull-right">

                <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Email Logs&nbsp;<span class="glyphicon glyphicon-download"></span>
                </a>
                <ul class="dropdown-menu dropdown-menu-sm">
                    <li>
                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" CommandName="Excel" ToolTip="Excel"
                            Text="Excel" CssClass="dropdown-item" Style="color: #333333;">
                        </asp:LinkButton>
                    </li>
                    <hr style="margin: 5px;" />
                    <li>
                        <asp:LinkButton runat="server" ID="btnExportPDF" OnClick="btnExportPDF_Click" CssClass="dropdown-item"
                            ToolTip="PDF" Text="PDF" Style="color: #333333;">
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </div>
        <div style="display: inline-flex; margin-bottom: 10px;">
            <label class="label" for="txtDateFrom">
                From Date :
            </label>
            <div class="Control">
                <asp:TextBox ID="txtDateFrom" CssClass="form-control txtDate" runat="server" autocomplete="off" Width="120px"></asp:TextBox>
            </div>
        </div>
        <div style="display: inline-flex; margin-bottom: 10px;">
            <label class="label" for="txtDateTo">
                To Date :
            </label>
            <div class="Control">
                <asp:TextBox ID="txtDateTo" CssClass="form-control txtDate" runat="server" autocomplete="off" Width="120px"></asp:TextBox>
            </div>
        </div>
        <div style="display: inline-flex;">
            <asp:Button ID="btnGetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnGetdata_Click" />
        </div>
        <div class="box-header">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <div>
                        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="true" Width="96%"
                            CssClass="table table-bordered table-striped Datatable" OnPreRender="grdUserDetails_PreRender">
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
