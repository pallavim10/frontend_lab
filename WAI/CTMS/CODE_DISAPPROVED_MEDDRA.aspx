<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CODE_DISAPPROVED_MEDDRA.aspx.cs" Inherits="CTMS.CODE_DISAPPROVED_MEDDRA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        function pageLoad() {
            $('.select').select2();
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": false,
                "bDestroy": false,
                stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="DisApproved Code MedDRA"></asp:Label>

            </h3>
            <div class="pull-right" runat="server" style="margin-top: 5px;">
                <asp:LinkButton runat="server" ID="lblDisAppCodeMeddraExport" OnClick="lblDisAppCodeMeddraExport_Click"  CssClass="btn btn-info" Style="color: white;">
                        Export  &nbsp;&nbsp; <span class="glyphicon glyphicon-download 2x"></span>
                </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div class="form-group">
            <div class="has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
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
</asp:Content>
