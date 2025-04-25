<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NSAE_DETAILS_TRACKER.aspx.cs" Inherits="CTMS.NSAE_DETAILS_TRACKER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false, "ordering": false,
                "bDestroy": true, stateSave: true
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
                <asp:Label ID="lblHeader" runat="server" Text="SAE Details Tracker Report"></asp:Label>
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
                        <div class="form-group" style="display: inline-flex">
                            <label class="label">
                                SAE ID:
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="drpSAEID" runat="server" CssClass="form-control required" AutoPostBack="True"
                                    OnSelectedIndexChanged="drpSAEID_SelectedIndexChanged">
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
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="gridData" runat="server" AllowSorting="true" AutoGenerateColumns="true"
                                            CssClass="table table-bordered table-striped Datatable" OnPreRender="GridView1_PreRender">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
