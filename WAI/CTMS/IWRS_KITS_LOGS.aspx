<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IWRS_KITS_LOGS.aspx.cs" Inherits="CTMS.IWRS_KITS_LOGS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": false,
                "bDestroy": false,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true
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
            width: 100pt;
            height: 20pt;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box-body">
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="box box-warning">
            <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="Kit Summary"></asp:Label>
            </h3>
          
            <%--</div>--%>
            <%--<div style="display: inline-flex; margin-left: 85%; margin-top: 5px;">--%>
                <%--<asp:LinkButton runat="server" ID="btnExportExcel" OnClick="btnExportExcel_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White" Text="Export Kit Logs">Export Kit Logs&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>--%>
                <div class="pull-right">
                <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Kit Summary&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                        <ul class="dropdown-menu dropdown-menu-sm">
                            <li>
                                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExportExcel_Click" CommandName="Excel" ToolTip="Excel"
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
            <br />
            <div class="rows">
                <div class="col-md-12">
                    <div class="col-md-1" style="width: 131px;">
                        <label>Enter Kit Number:</label>
                    </div>
                    <div class="col-md-2" style="width: 160px;">
                        <asp:TextBox ID="Txtkitnumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btngetdata" Text="Get Data" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btngetdata_Click" />
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="box-body">
                    <div align="left">
                        <div>
                            <div class="rows">
                                <div class="col-md-12">
                                    <div style="width: 100%; overflow: auto;">
                                        <asp:GridView ID="grdKitLogs" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="True" Width="97%"
                                            CssClass="table table-bordered txt_center table-striped Datatable" OnPreRender="grdKitLogs_PreRender">
                                        </asp:GridView>
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
</asp:Content>
