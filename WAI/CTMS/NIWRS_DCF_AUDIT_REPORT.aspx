<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_DCF_AUDIT_REPORT.aspx.cs" Inherits="CTMS.NIWRS_DCF_AUDIT_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            <h3 class="box-title">DCF Audit Trail
            </h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                        </div>
                        <div runat="server" id="DivINV" class="form-group" style="display: inline-flex">
                            <div class="form-group" style="display: inline-flex">
                                <label class="label">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="drpInvID" runat="server" AutoPostBack="True" CssClass="form-control "
                                        OnSelectedIndexChanged="drpInvID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                Subject ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSubID" runat="server" CssClass="form-control required" AutoPostBack="True"
                                    OnSelectedIndexChanged="drpSubID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div style="display: inline-flex">
                            <div id="Div1" class="dropdown" runat="server" style="display: inline-flex">
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
                        <div class="box">
                            <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped Datatable"
                                AllowSorting="true" Width="100%" OnPreRender="grd_data_PreRender">
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
