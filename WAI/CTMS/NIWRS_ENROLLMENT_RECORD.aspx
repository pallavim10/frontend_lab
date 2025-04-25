<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_ENROLLMENT_RECORD.aspx.cs" Inherits="CTMS.NIWRS_ENROLLMENT_RECORD" %>

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

        $(document).ready(function () {

            $('#MainContent_btnExport').mousedown(function (event) {
                switch (event.which) {
                    case 3:
                        $('#MainContent_btnExportPDF').click();
                        break;
                }
            });

        });

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
            width: 120pt;
            height: 20pt;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label runat="server" ID="lblHeader" Text="Enrollment Record"></asp:Label>&nbsp;&nbsp;&nbsp;
                    </h3>
                    <div class="pull-right">
                        <%--<asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Report&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="btnExportPDF" Text="Export as PDF" OnClick="btnExportPDF_Click"></asp:LinkButton>--%>
                        <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Enrollment Record&nbsp;&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                        <ul class="dropdown-menu dropdown-menu-sm">
                            <li>
                                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" CommandName="Excel" ToolTip="Excel"
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
                    <div class="has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="rows">
                        <br />
                        <br />
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gvVisits" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                            OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable txt_center table-striped notranslate"
                                            OnRowDataBound="gvVisits_RowDataBound">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnExportPDF" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
