<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MM_LINKED_QUERY.aspx.cs" Inherits="CTMS.MM_LINKED_QUERY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="js/plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <script type="text/jscript">
        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Link MM Query To DM
            </h3>
        </div>
        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700; font-size: small;"></asp:Label>
        </div>
        <div class="box-group">
            <div class="form-group">
                <div class="rows">
                    <div style="width: 100%; overflow: auto;">
                        <asp:GridView ID="grdQueryDetailReports" runat="server" AutoGenerateColumns="False" Width="97%"
                            CssClass="table table-bordered table-striped Datatable" OnPreRender="grd_data_PreRender"
                            CellPadding="4" CellSpacing="2" EmptyDataText="No Query Available." OnRowCommand="grdQueryDetailReports_RowCommand"
                            OnRowDataBound="grdQueryDetailReports_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSelect" runat="server" Text="Link To DM" CommandName="Link"
                                            CommandArgument='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Site ID">
                                    <ItemTemplate>
                                        <asp:Label ID="INVID" runat="server" Text='<%# Bind("INVID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject ID">
                                    <ItemTemplate>
                                        <asp:Label ID="SUBJID" runat="server" Text='<%# Bind("SUBJID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Record No." ItemStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" Font-Size="Small" Text='<%# Bind("RECORDNO") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visit" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Module">
                                    <ItemTemplate>
                                        <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Field Name" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="FIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query ID" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rule description" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Text" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="QUERYTEXT" runat="server" Text='<%# Bind("QUERYTEXT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Status" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="STATUSTEXT" runat="server" Text='<%# Bind("STATUSTEXT") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Query Type" HeaderStyle-CssClass="align-left">
                                    <ItemTemplate>
                                        <asp:Label ID="QUERYTYPE" runat="server" Text='<%# Bind("QUERYTYPE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="align-left">
                                    <HeaderTemplate>
                                        <label>Generated</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Generated By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server" id="divGenerated">
                                            <div>
                                                <asp:Label ID="QRYGENBYNAME" runat="server" Text='<%# Bind("QRYGENBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="QRYGEN_CAL_DAT" runat="server" Text='<%# Bind("QRYGEN_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="QRYGEN_CAL_TZDAT" runat="server" Text='<%# Bind("QRYGEN_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="MULTIPLEYN" runat="server" Text='<%# Bind("MULTIPLEYN") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <div class="row" id="div12" runat="server">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            <asp:Button ID="btnBack" runat="server" Text="Back To MM Query Report" CssClass="btn btn-danger btn-sm"
                                OnClick="btnBack_Click" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
