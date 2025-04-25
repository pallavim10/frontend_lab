<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_DCF_LIST.aspx.cs" Inherits="CTMS.NIWRS_DCF_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">Pending Data Correction Requests
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="box-body">
                    <div class="rows">
                        <div style="width: 100%; overflow: auto;">
                            <div>
                                <asp:GridView ID="gridData" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="false"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable txt_center table-striped notranslate"
                                    OnRowCommand="gridData_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="DCF ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSite" runat="server" Text='<%# Bind("SITE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="Sub-Site ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubSite" runat="server" Text='<%# Bind("SUBSITE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Screening ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSUBJID" runat="server" Text='<%# Bind("SUBJID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Randomization No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRAND_NUM" runat="server" Text='<%# Bind("RAND_NUM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVISIT" runat="server" Text='<%# Bind("VISITNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Form">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFORM" runat="server" Text='<%# Bind("FORM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fieldname">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Old Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOLD_Val" runat="server" Text='<%# Bind("OLD_Val") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNEW_Val" runat="server" Text='<%# Bind("NEW_Val") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lblREASON" runat="server" Text='<%# Bind("REASON") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="align-left" HeaderStyle-CssClass="align-left">
                                            <HeaderTemplate>
                                                <label>DCF Details</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Raised By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <div>
                                                        <asp:Label ID="RAISE_BYNAME" runat="server" Text='<%# Bind("RAISE_BYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="RAISE_CAL_DAT" runat="server" Text='<%# Bind("RAISE_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="RAISE_CAL_TZDAT" runat="server" Text='<%# Bind("RAISE_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAction" runat="server" CommandName="Open" CommandArgument='<%# Bind("ID") %>'
                                                    Text="Open Request"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
