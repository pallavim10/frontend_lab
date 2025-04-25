<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UMT_RPT_RoleRights.aspx.cs" Inherits="CTMS.UMT_RPT_RoleRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function pageLoad() {
            $('.select').select2();
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": false,
                "bDestroy": false,
                stateSave: false
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="User Rights by Role"></asp:Label></h3>
        </div>
        <div class="form-group">
            <div class="has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="rows">
                <div style="display: inline-flex">
                    <label class="label ">
                        From Date :
                    </label>
                    <div class="Control" style="display: inline-flex">
                        <asp:TextBox ID="txtDateFrom" CssClass="form-control txtDate" runat="server" autocomplete="off"
                            Width="120px"></asp:TextBox>
                    </div>
                </div>
                <div runat="server" id="Div1" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label ">
                            To Date :
                        </label>
                        <div class="Control" style="display: inline-flex">
                            <asp:TextBox ID="txtDateTo" CssClass="form-control txtDate" runat="server" autocomplete="off"
                                Width="120px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div runat="server" id="Div2" style="display: inline-flex">
                    <div style="display: inline-flex">
                        <asp:Button ID="btnGet" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave"
                            OnClick="btnGet_Click" />
                    </div>
                </div>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <div id="divDownload" class="dropdown" runat="server" style="display: inline-flex">
                    <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                        style="color: #333333" title="Export"></a>
                    <ul class="dropdown-menu dropdown-menu-sm">
                        <li>
                            <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel"
                                Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                            </asp:LinkButton></li>
                        <hr style="margin: 5px;" />
                    </ul>
                </div>
                <br />
                <br />
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
</asp:Content>
