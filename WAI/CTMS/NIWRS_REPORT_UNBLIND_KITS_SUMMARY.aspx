<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_REPORT_UNBLIND_KITS_SUMMARY.aspx.cs" Inherits="CTMS.NIWRS_REPORT_UNBLIND_KITS_SUMMARY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .fontBlue {
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

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);

            if (div.style.display == "none") {
                div.style.display = "inline";

            } else {
                div.style.display = "none";
            }
        }

    </script>
    <style type="text/css">
        .btn-info {
            background-repeat: repeat-x;
            border-color: #28a4c9;
            /*background-image: linear-gradient(to bottom, #5bc0de 0%, #2aabd2 100%);*/
        }
        
        .prevent-refresh-button {
            display: inline-block;
            padding: 5px 5px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.428571429;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border: 1px solid transparent;
            border-radius: 4px;
            width: 100pt;
            height: 20pt;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label runat="server" ID="lblHeader" Text="Kit Summary"></asp:Label></h3>
                
                    <div class="pull-right">
                        <%--<a href="#" class="dropdown-toggle btn btn-info" data-toggle="dropdown"
                            style="color: #333333" onclick="return false;">Export Kit Summary &nbsp;<span class="glyphicon glyphicon-download-alt"></span>
                        </a>--%>
                        <a href="#" class="dropdown-toggle btn-info prevent-refresh-button" data-toggle="dropdown" style="color: #FFFFFF">Export Kit Summary&nbsp;<span class="glyphicon glyphicon-download"></span></a>
                        <ul class="dropdown-menu dropdown-menu-sm">
                            <li>
                                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" CommandName="Excel" ToolTip="Excel"
                                    Text="Excel" CssClass="dropdown-item" Style="color: #333333;">
                                </asp:LinkButton></li>
                            <hr style="margin: 5px;" />
                            <li>
                                <asp:LinkButton runat="server" ID="btnExportPDF" OnClick="btnExportPDF_Click" CssClass="dropdown-item"
                                    ToolTip="PDF" Text="PDF" Style="color: #333333;">
                                </asp:LinkButton>
                            </li>
                        </ul>

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
                                    OnRowDataBound="gvKits_RowDataBound" CssClass="table table-bordered txt_center table-striped notranslate"
                                    OnRowCommand="gvKits_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Export Available">
                                            <ItemTemplate>
                                                <div id="divDownload" class="dropdown disp-none" runat="server" style="display: inline-flex">
                                                    <a href="#" class="dropdown-toggle glyphicon glyphicon-download-alt" data-toggle="dropdown"
                                                        style="color: #333333" title="Export"></a>
                                                    <ul class="dropdown-menu dropdown-menu-sm">
                                                        <li>
                                                            <asp:LinkButton runat="server" ID="btnExport" CommandName="Excel" ToolTip="Export to Excel"
                                                                Text="Export to Excel" CssClass="dropdown-item" Style="color: #333333;">
                                                            </asp:LinkButton></li>
                                                        <hr style="margin: 5px;" />
                                                        <li>
                                                            <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnPDF" CommandName="PDF"
                                                                ToolTip="Export to PDF" Text="Export to PDF" Style="color: #333333;">
                                                            </asp:LinkButton></li>
                                                    </ul>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="COUNTRYNAME" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site ID">
                                            <ItemTemplate>
                                                <asp:Label ID="SITEID" runat="server" Text='<%# Bind("[Site ID]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Treatment Code">
                                            <ItemTemplate>
                                                <asp:Label ID="TreatmentGroup" runat="server" Text='<%# Bind("[Treatment Group]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received">
                                            <ItemTemplate>
                                                <asp:Label ID="Received" runat="server" Text='<%# Bind("[Received]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dispensed">
                                            <ItemTemplate>
                                                <asp:Label ID="Dispensed" runat="server" Text='<%# Bind("[Dispensed]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quarantined">
                                            <ItemTemplate>
                                                <asp:Label ID="Quarantined" runat="server" Text='<%# Bind("[Quarantined]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Damaged">
                                            <ItemTemplate>
                                                <asp:Label ID="Damaged" runat="server" Text='<%# Bind("[Damaged]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Blocked">
                                            <ItemTemplate>
                                                <asp:Label ID="Blocked" runat="server" Text='<%# Bind("[Blocked]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejected">
                                            <ItemTemplate>
                                                <asp:Label ID="Rejected" runat="server" Text='<%# Bind("[Rejected]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Destroyed">
                                            <ItemTemplate>
                                                <asp:Label ID="Destroyed" runat="server" Text='<%# Bind("[Destroyed]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Returned">
                                            <ItemTemplate>
                                                <asp:Label ID="Returned" runat="server" Text='<%# Bind("[Returned]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Back-Up Kit Utilised">
                                            <ItemTemplate>
                                                <asp:Label ID="BAKUP" runat="server" Text='<%# Bind("[Back-Up Kit Utilised]") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Available">
                                            <ItemTemplate>
                                                <a href="JavaScript:divexpandcollapse('_KIT_<%# Container.DataItemIndex + 1 %>');"
                                                    style="color: #333333">
                                                    <asp:Label ID="Available" CssClass="fontBlue" runat="server" Text='<%# Bind("[Available]") %>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                        <div style="float: right; font-size: 13px;">
                                                        </div>
                                                        <div>
                                                            <div id="_KIT_<%# Container.DataItemIndex + 1 %>" style="display: none; position: relative; overflow: auto;">
                                                                <asp:GridView ID="gvAVLKits" runat="server" CellPadding="3" AutoGenerateColumns="true"
                                                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable table-striped table-striped1">
                                                                    <Columns>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        </div>
                                                    </td>
                                                </tr>
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
            <asp:PostBackTrigger ControlID="btnExportPDF" />
            <asp:PostBackTrigger ControlID="gvKits" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
