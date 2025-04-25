<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_KITS_UNUSED.aspx.cs" Inherits="CTMS.NIWRS_KITS_UNUSED" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true
            });
            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Unused Kits</h3>
                    <asp:Label runat="server" ID="lblHeader" Text="Unused Kits" Visible="false"></asp:Label>
                    <div class="pull-right">
                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Unused Kits&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        &nbsp;&nbsp;
                    </div>
                </div>
                <div class="form-group">
                    <div class="has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="rows">
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width60px">
                                    Filter By:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList runat="server" ID="ddlFilterBy" CssClass="form-control required width200px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlFilterBy_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Quarantined" Value="Unused_Quarantined"></asp:ListItem>
                                        <asp:ListItem Text="Damaged" Value="Unused_Damaged"></asp:ListItem>
                                        <asp:ListItem Text="Returned" Value="Unused_Returned"></asp:ListItem>
                                        <asp:ListItem Text="Destroyed" Value="Unused_Destroyed"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width60px">
                                    Country:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control required width200px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width60px">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList runat="server" ID="ddlSite" CssClass="form-control required width200px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvKits" runat="server" AutoGenerateColumns="true" OnPreRender="grd_data_PreRender"
                                    CssClass="table table-bordered Datatable txt_center table-striped notranslate">
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

