<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RM_Risk_Tracker.aspx.cs" Inherits="CTMS.RM_Risk_Tracker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }

        function Print() {
            if ($("#<%=ddlStatus.ClientID%>").val() == "99") {
                $("#<%=ddlStatus.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectName = '<%= Session["PROJECTID"] %>'
            var Status = $("#<%=ddlStatus.ClientID%>").val();
            var test = "ReportRM_Risk_Tracker.aspx?ProjectName=" + ProjectName + "&Status=" + Status;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        Risk Tracker
                        <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                            CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
                    </h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </div>
                        <table>
                            <tr>
                                <td class="label">
                                    Select Status:
                                </td>
                                <td class="requiredSign">
                                    <asp:Label ID="lbl_Section" runat="server" Text="*"></asp:Label>
                                </td>
                                <td class="Control">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="100px" class="form-control drpControl"
                                                ValidationGroup="View" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="style10">
                                    &nbsp;
                                </td>
                                <td class="style10">
                                    &nbsp;
                                </td>
                                <td class="style10">
                                    <div class="dropdown" runat="server" visible="false">
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
                                            <hr style="margin: 5px;" />
                                            <li>
                                                <asp:LinkButton runat="server" CssClass="dropdown-item" ID="btnWord" OnClick="btnWord_Click"
                                                    ToolTip="Export to Word" Text="Export to Word" Style="color: #333333;">
                                                </asp:LinkButton></li>
                                        </ul>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <asp:GridView ID="gvCat" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped" OnRowDataBound="gvCat_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                    HeaderStyle-CssClass="txt_center">
                                    <HeaderTemplate>
                                        <a href="JavaScript:ManipulateAll('_Cat');" id="_Folder" style="color: #333333"><i
                                            id="img_Cat" class="icon-plus-sign-alt"></i></a>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div runat="server" id="anchor">
                                            <a href="JavaScript:divexpandcollapse('_Cat<%# Eval("Category_ID") %>');" style="color: #333333">
                                                <i id="img_Cat<%# Eval("Category_ID") %>" class="icon-plus-sign-alt"></i></a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category" ItemStyle-Width="100%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Category" Width="100%" ToolTip='<%# Bind("Category") %>' CssClass="label"
                                            runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Count" ItemStyle-Width="100%" HeaderStyle-CssClass="txt_center"
                                    ControlStyle-CssClass="txt_center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Count" Width="100%" ToolTip='<%# Bind("Count") %>' CssClass="label"
                                            runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%" style="padding: 2px;">
                                                <div style="float: right; font-size: 13px;">
                                                </div>
                                                <div>
                                                    <div class="rows">
                                                        <div class="col-md-12">
                                                            <div id="_Cat<%# Eval("Category_ID") %>" style="display: none; position: relative;
                                                                overflow: auto;">
                                                                <asp:GridView ID="gvSubCat" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                    CssClass="table table-bordered table-striped" OnRowDataBound="gvSubCat_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                            HeaderStyle-CssClass="txt_center">
                                                                            <HeaderTemplate>
                                                                                <a href="JavaScript:ManipulateAll('_SubCat');" id="_Folder" style="color: #333333"><i
                                                                                    id="img_SubCat" class="icon-plus-sign-alt"></i></a>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <div runat="server" id="anchor">
                                                                                    <a href="JavaScript:divexpandcollapse('_SubCat<%# Eval("SubCategory_ID") %>');" style="color: #333333">
                                                                                        <i id="img_SubCat<%# Eval("SubCategory_ID") %>" class="icon-plus-sign-alt"></i>
                                                                                    </a>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sub-Category" ItemStyle-Width="100%">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_SubCat" Width="100%" ToolTip='<%# Bind("SubCategory") %>' CssClass="label"
                                                                                    runat="server" Text='<%# Bind("SubCategory") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Count" ItemStyle-Width="100%" HeaderStyle-CssClass="txt_center"
                                                                            ControlStyle-CssClass="txt_center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_SubCount" Width="100%" ToolTip='<%# Bind("Count") %>' CssClass="label"
                                                                                    runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td colspan="100%" style="padding: 2px;">
                                                                                        <div style="float: right; font-size: 13px;">
                                                                                        </div>
                                                                                        <div>
                                                                                            <div class="rows">
                                                                                                <div class="col-md-12">
                                                                                                    <div id="_SubCat<%# Eval("SubCategory_ID") %>" style="display: none; position: relative;
                                                                                                        overflow: auto;">
                                                                                                        <asp:GridView ID="gvFactor" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                            CssClass="table table-bordered table-striped" OnRowDataBound="gvFactor_RowDataBound">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                                    <HeaderTemplate>
                                                                                                                        <a href="JavaScript:ManipulateAll('_Fact');" id="_Folder" style="color: #333333"><i
                                                                                                                            id="img_Fact" class="icon-plus-sign-alt"></i></a>
                                                                                                                    </HeaderTemplate>
                                                                                                                    <ItemTemplate>
                                                                                                                        <div runat="server" id="anchor">
                                                                                                                            <a href="JavaScript:divexpandcollapse('_Fact<%# Eval("Factor_ID") %>');" style="color: #333333">
                                                                                                                                <i id="img_Fact<%# Eval("Factor_ID") %>" class="icon-plus-sign-alt"></i></a>
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Factor" ItemStyle-Width="100%">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbl_SubCat" Width="100%" ToolTip='<%# Bind("Factor") %>' CssClass="label"
                                                                                                                            runat="server" Text='<%# Bind("Factor") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Probability" ItemStyle-Width="100%" HeaderStyle-CssClass="txt_center"
                                                                                                                    ControlStyle-CssClass="txt_center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbl_Probability" Width="100%" ToolTip='<%# Bind("Probability") %>'
                                                                                                                            CssClass="label" runat="server" Text='<%# Bind("Probability") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Severity" ItemStyle-Width="100%" HeaderStyle-CssClass="txt_center"
                                                                                                                    ControlStyle-CssClass="txt_center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbl_Severity" Width="100%" ToolTip='<%# Bind("Severity") %>' CssClass="label"
                                                                                                                            runat="server" Text='<%# Bind("Severity") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Detection" ItemStyle-Width="100%" HeaderStyle-CssClass="txt_center"
                                                                                                                    ControlStyle-CssClass="txt_center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbl_Detection" Width="100%" ToolTip='<%# Bind("Detection") %>' CssClass="label"
                                                                                                                            runat="server" Text='<%# Bind("Detection") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="RPN" ItemStyle-Width="100%" HeaderStyle-CssClass="txt_center"
                                                                                                                    ControlStyle-CssClass="txt_center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbl_RPN" Width="100%" ToolTip='<%# Bind("RPN") %>' CssClass="label"
                                                                                                                            runat="server" Text='<%# Bind("RPN") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Count" ItemStyle-Width="100%" HeaderStyle-CssClass="txt_center"
                                                                                                                    ControlStyle-CssClass="txt_center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbl_SubCount" Width="100%" ToolTip='<%# Bind("Count") %>' CssClass="label"
                                                                                                                            runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Days" ItemStyle-Width="100%" HeaderStyle-CssClass="txt_center"
                                                                                                                    ControlStyle-CssClass="txt_center">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lbl_Days" Width="100%" CssClass="label" runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <tr>
                                                                                                                            <td colspan="100%" style="padding: 2px;">
                                                                                                                                <div style="float: right; font-size: 13px;">
                                                                                                                                </div>
                                                                                                                                <div>
                                                                                                                                    <div class="rows">
                                                                                                                                        <div class="col-md-12">
                                                                                                                                            <div id="_Fact<%# Eval("Factor_ID") %>" style="display: none; position: relative;
                                                                                                                                                overflow: auto;">
                                                                                                                                                <asp:GridView ID="gvEvents" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                                                                    CssClass="table table-bordered table-striped" OnRowDataBound="gvEvents_RowDataBound">
                                                                                                                                                    <Columns>
                                                                                                                                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                                                                                                            HeaderStyle-CssClass="txt_center">
                                                                                                                                                            <HeaderTemplate>
                                                                                                                                                                <a href="JavaScript:ManipulateAll('_Event');" id="_Folder" style="color: #333333"><i
                                                                                                                                                                    id="img_Event" class="icon-plus-sign-alt"></i></a>
                                                                                                                                                            </HeaderTemplate>
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <div runat="server" id="anchor">
                                                                                                                                                                    <a href="JavaScript:divexpandcollapse('_Event<%# Eval("ID") %>');" style="color: #333333">
                                                                                                                                                                        <i id="img_Event<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                                                                                                                                </div>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField HeaderText="Description">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lbl_RiskCons" runat="server" Text='<%# Bind("REvent_Description") %>'></asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField HeaderText="Impacts">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lbl_Impact" runat="server" Text='<%# Bind("Risk_Impact") %>'></asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField HeaderText="Risk Type">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lbl_P" runat="server" Text='<%# Bind("Risk_Type") %>'></asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField HeaderText="Date Since">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lbl_D" runat="server" Text='<%# Bind("RDateIdentiFied1") %>'></asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField HeaderText="Days">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lbl_Days" runat="server" Text='<%# Bind("Days") %>'></asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField>
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <tr>
                                                                                                                                                                    <td colspan="100%" style="padding: 2px;">
                                                                                                                                                                        <div style="float: right; font-size: 13px;">
                                                                                                                                                                        </div>
                                                                                                                                                                        <div>
                                                                                                                                                                            <div class="rows">
                                                                                                                                                                                <div class="col-md-12">
                                                                                                                                                                                    <div id="_Event<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                                                                                                                                        <asp:GridView runat="server" ID="gvMitigation" AutoGenerateColumns="False" CssClass="table table-bordered table-striped">
                                                                                                                                                                                            <Columns>
                                                                                                                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                                                                                                                                                    HeaderText="ID">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>' CommandArgument='<%#Eval("Id") %>'></asp:Label>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:TemplateField HeaderText="Cause" ItemStyle-Width="8%" HeaderStyle-Width="8%">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <asp:Label ID="lblCause" runat="server" Text='<%# Bind("Cause") %>'></asp:Label>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:TemplateField HeaderText="Mitigation">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <asp:Label ID="lblMitigation" runat="server" Text='<%# Bind("Mitigation") %>'></asp:Label>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:TemplateField HeaderText="Primary Responsible Person" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <asp:Label ID="lblPrimary_RES" runat="server" Text='<%# Bind("Primary_RES") %>'></asp:Label>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:TemplateField HeaderText="Secondary Responsible Person" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <asp:Label ID="lblSecondary_RES" runat="server" Text='<%# Bind("Secondary_RES") %>'></asp:Label>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:TemplateField HeaderText="Date By" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                                <asp:TemplateField HeaderText="Date Complete" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                                        <asp:Label ID="lblDateComplete" Width="100%" runat="server" Text='<%# Bind("Date_Complete") %>'></asp:Label>
                                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                            </Columns>
                                                                                                                                                                                            <RowStyle ForeColor="Maroon" />
                                                                                                                                                                                            <HeaderStyle ForeColor="Maroon" />
                                                                                                                                                                                        </asp:GridView>
                                                                                                                                                                                    </div>
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
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                            <RowStyle ForeColor="Blue" />
                                                                                                            <HeaderStyle ForeColor="Blue" />
                                                                                                        </asp:GridView>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <RowStyle ForeColor="Maroon" />
                                                                    <HeaderStyle ForeColor="Maroon" />
                                                                </asp:GridView>
                                                            </div>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnPDF" />
            <asp:PostBackTrigger ControlID="btnWord" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
