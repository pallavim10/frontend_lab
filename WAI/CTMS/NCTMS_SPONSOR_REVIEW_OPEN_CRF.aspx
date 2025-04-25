<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NCTMS_SPONSOR_REVIEW_OPEN_CRF.aspx.cs" Inherits="CTMS.NCTMS_SPONSOR_REVIEW_OPEN_CRF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .form-control
        {
            width: 100%;
            min-width: 100px;
        }
        .brd-1px-redimp
        {
            border: 2px solid !important;
            border-color: Red !important;
        }
        .brd-1px-maroonimp
        {
            border: 2px solid !important;
            border-color: Maroon !important;
        }
    </style>
    <script>

        //For ajax function call on update data button click
        function VIEW_ALL_COMMENT(element) {

            var INVID = $("#MainContent_hdnSITEID").val();
            var PROJECTID = '<%= Session["PROJECTID"] %>';
            var SVID = $("#MainContent_hdnSVID").val();
            var VISITID = $("#MainContent_hdnVISITID").val();
            var VISIT = $("#MainContent_hdnVISIT").val();
            var MODULEID = $(element).closest('tr').find('td:eq(3)').find('span').html();
            var MODULENAME = $(element).closest('tr').find('td:eq(1)').find('span').html();

            var test = "NCTMS_ViewComments.aspx?Section=" + VISIT + "&ProjectId=" + PROJECTID + "&INVID=" + INVID + "&SVID=" + SVID + "&SectionID=" + VISITID + "&SubSectionID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&ACTION=SPONSOR";

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1400";
            window.open(test, '_blank', strWinProperty);
            return false;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 86%; display: inline-flex; font-size: medium;">
                Review Reports
            </h3>
            <div align="right">
                <asp:Button ID="btnSponsorFinal" runat="server" Text="Final" CssClass="btn btn-DarkGreen btn-sm"
                    OnClick="btnSponsorFinal_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReturnToPM" runat="server" Text="Return To PM" CssClass="btn btn-danger btn-sm"
                    OnClick="btnReturnToPM_Click" />&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;"></asp:Label>
                </div>
                <asp:HiddenField ID="hdnSVID" runat="server" />
                <asp:HiddenField ID="hdnSITEID" runat="server" />
                <asp:HiddenField ID="hdnVISITID" runat="server" />
                <asp:HiddenField ID="hdnVISIT" runat="server" />
                <div class="box">
                    <asp:GridView ID="Grd_OpenCRF" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                        OnRowCommand="Grd_OpenCRF_RowCommand" OnRowDataBound="Grd_OpenCRF_RowDataBound"
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="VISIT">
                                <ItemTemplate>
                                    <asp:Label ID="txtVisitCode" runat="server" Text='<%# Bind("VISIT_NAME") %>' Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MODULE NAME">
                                <ItemTemplate>
                                    <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>' Enabled="false"
                                        Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Multiple YN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="MULTIPLEYN" runat="server" Text='<%# Bind("MULTIPLEYN") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MODULE ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="MODULEID" runat="server" Text='<%# Bind("MODULEID") %>' Enabled="false"
                                        Font-Bold="true" CssClass="align-center"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PAGESTATUS" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPAGESTATUS" runat="server" Font-Size="X-Small" Height="17px"
                                        Text='<%# Bind("PAGESTATUS") %>' Enabled="false" Width="20px" Font-Bold="true"
                                        CssClass="align-center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GO TO PAGE" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <%--  <asp:HyperLink runat="server" ID="lnkPAGENUM" CommandName="GOTOPAGE" Visible="false" ImageUrl="Images/Page_logo.jpg"></asp:HyperLink>--%>
                                    <asp:ImageButton ID="lnkPAGENUM" runat="server" Style="height: 20px;" CommandName="GOTOPAGE"
                                        ImageUrl="Images/New_Page.png"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblViewAllComment" CssClass="btn btn-DARKORANGE btn-sm" runat="server"
                                        OnClientClick="return VIEW_ALL_COMMENT(this);" ToolTip="View All Comments">
                                        <i class="fa fa-comments fa-2x" style="color: White;"></i>
                                        <asp:Label class="badge badge-info right" ID="lblAllCommentCount" runat="server"
                                            Text="0" Style="margin-left: 5px; background-color: White; color: Black;" Font-Bold="true"></asp:Label>
                                    </asp:LinkButton>&nbsp&nbsp&nbsp
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBacktoHomePage" runat="server" Text="Back to Main Page" CssClass="btn btn-sm btn-primary"
                        OnClick="btnBacktoHomePage_Click"></asp:Button></div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
