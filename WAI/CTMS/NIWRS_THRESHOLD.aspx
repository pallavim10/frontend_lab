<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_THRESHOLD.aspx.cs" Inherits="CTMS.NIWRS_THRESHOLD" %>

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
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
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
                    <h3 class="box-title">
                        <asp:Label runat="server" ID="lblHeader" Text="Threshold Metrics"></asp:Label></h3>
                    <div class="pull-right">
                    <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" style="margin-top:3px;" CssClass="btn btn-info" ForeColor="White">Export Metrics&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>&nbsp;&nbsp;
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
                                <asp:GridView ID="gvKits" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="false"
                                    OnRowDataBound="gvKits_RowDataBound" CssClass="table table-bordered txt_center table-striped Datatable"
                                     OnPreRender="grd_data_PreRender" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="COUNTRYNAME" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site ID">
                                            <ItemTemplate>
                                                <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Treatment Code">
                                            <ItemTemplate>
                                                <asp:Label ID="TreatmentGroup" runat="server" Text='<%# Bind("TREAT_GRP") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Treatment Arms">
                                            <ItemTemplate>
                                                <asp:Label ID="Treatmengroupname" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Treatment Strength">
                                            <ItemTemplate>
                                                <asp:Label ID="TreatmentStrength" runat="server" Text='<%# Bind("TREAT_STRENGTH") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Available">
                                            <ItemTemplate>
                                                    <asp:Label ID="Available"  runat="server" Text='<%# Bind("Available") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trigger Value">
                                            <ItemTemplate>
                                                <asp:Label ID="TiggerValue" runat="server"  Text='<%# Bind("TRIGGER_VAL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Threshold Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblThreshold" runat="server" Visible="false" Text="Threshold exceeded for this Site." ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
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
