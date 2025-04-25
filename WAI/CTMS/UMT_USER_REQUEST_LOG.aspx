<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_USER_REQUEST_LOG.aspx.cs" Inherits="CTMS.UMT_USER_REQUEST_LOG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="User Request Logs"></asp:Label>
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <div style="display: inline-flex">
                    <label class="label ">
                        User Name :
                    </label>
                    <div class="Control" style="display: inline-flex">
                        <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" Width="120px"></asp:TextBox>
                    </div>
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
                &nbsp&nbsp;&nbsp;
                <div id="divDownload" class="dropdown" runat="server" style="display: inline-flex">
                    <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                        style="color: #333333" title="Export"></a>
                    <ul class="dropdown-menu dropdown-menu-sm">
                        <li>
                            <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                            </asp:LinkButton></li>
                        <hr style="margin: 5px;" />
                        </li>
                    </ul>
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
</asp:Content>
