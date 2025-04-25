<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_ENROLL_LOG.aspx.cs" Inherits="CTMS.NIWRS_ENROLL_LOG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .fontBlue
        {
            color: Blue;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                <asp:Label ID="lblHeader" runat="server"></asp:Label>
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
            <asp:HiddenField runat="server" ID="hfSTEPID" />
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true"
                                            CssClass="table table-bordered Datatable txt_center table-striped notranslate"
                                            OnPreRender="grd_data_PreRender">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
