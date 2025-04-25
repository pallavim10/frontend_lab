<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SAE_ACTIVITY_LOGS.aspx.cs" Inherits="CTMS.SAE_ACTIVITY_LOGS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: false
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
            <h3 class="box-title" style="width: 100%;">SAE Event Logs Report</h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300;"></asp:Label>
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
                                    <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
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
                                    <asp:DropDownList ID="ddlSUBJID" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlSUBJID_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divSAEID" style="display: inline-flex">
                            <label class="label">
                                SAE ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSAEID" runat="server" CssClass="form-control required"  AutoPostBack="True" OnSelectedIndexChanged="drpSAEID_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div runat="server" id="divgetdata" style="display: inline-flex">
                            <asp:Button ID="btngetdata" Text="Get Data" runat="server" OnClick="btngetdata_Click" CssClass="btn btn-primary btn-sm" />
                        </div>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" ToolTip="Export to Excel" CssClass="btn btn-info btn-sm"
                            Text="Export to Excel" ForeColor="White">Export to Excel&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        <br />
                        <div class="box-body">
                            <div class="rows">
                                <div>
                                    <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="true" Width="98%" CssClass="table table-bordered table-striped Datatable" OnPreRender="gridData_PreRender">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
