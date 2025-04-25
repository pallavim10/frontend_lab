<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SAE_DCF_Report.aspx.cs" Inherits="CTMS.SAE_DCF_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                stateSave: false,
                
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">SAE DCF Report</h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="box-body">
                    <div class="form-group">
                        <div runat="server" id="divSIte" style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width70px">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divSubject" style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width70px">
                                    Subject ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList ID="ddlSUBJID" runat="server" AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divSAEID" style="display: inline-flex">
                            <label class="label">
                                SAE ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSAEID" runat="server" CssClass="form-control required" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div runat="server" id="divgetdata" style="display: inline-flex">
                            <asp:Button ID="btngetdata" Text="Get Data" runat="server" CssClass="btn btn-primary btn-sm"/>
                        </div>
                        <div id="divDownload" class="dropdown" runat="server" style="display: inline-flex">
                            <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                style="color: #333333" title="Export"></a>
                            <ul class="dropdown-menu dropdown-menu-sm">
                                <li>
                                    <asp:LinkButton runat="server" ID="btnExport" ToolTip="Export to Excel"
                                        Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                    </asp:LinkButton></li>
                                <hr style="margin: 5px;" />
                                <li>
                                    <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnPDF"
                                        ToolTip="Export to PDF" Text="Export to PDF" Style="color: #333333;">
                                    </asp:LinkButton></li>
                            </ul>
                        </div>
                        <br />
                        <br />
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true" Width="98%" CssClass="table table-bordered table-striped  Datatable" OnPreRender="gridData_PreRender">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </ContentTemplate>
            <Triggers>
                <%--<asp:PostBackTrigger ControlID="btnExport" />
                <asp:PostBackTrigger ControlID="btnPDF" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>