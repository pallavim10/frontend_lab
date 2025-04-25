<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RM_Remove_AnticipatedProject_Risk.aspx.cs"
    Inherits="CTMS.RM_Remove_AnticipatedProject_Risk" %>

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
    </script>
    <script type="text/javascript">
        function ViewRECIDDETAILS(element) {

            var RiskId = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
            var TYPE = "UPDATE";

            var test = "RM_Risk_MitigationDetails.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=380,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }


        function Print() {
            if ($("#<%=Drp_Project_Name.ClientID%>").val() == "99") {
                $("#<%=Drp_Project_Name.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectName = $("#<%=Drp_Project_Name.ClientID%>").val();
            var test = "ReportRM_Remove_AnticipatedProject_Risk.aspx?ProjectName=" + ProjectName;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Anticipated Project Risks
                                        <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()" CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <table>
                    <tr>
                        <td class="label">
                            Select Project:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="Lbl_Sel_Dept" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:DropDownList ID="Drp_Project_Name" runat="server" class="form-control drpControl required">
                            </asp:DropDownList>
                        </td>
                        <td class="style10">
                            <asp:Button ID="Btn_Get_Fun" runat="server" OnClick="Btn_Get_Fun_Click" Text="Get Risks"
                                CssClass="btn btn-primary btn-sm cls-btnSave" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style10" runat="server" visible="false">
                            <div class="dropdown">
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
                    <tr>
                        <td class="label" colspan="5">
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box">
        <asp:GridView ID="gvCategory" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped" OnRowDataBound="gvCategory_RowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                    HeaderStyle-CssClass="txt_center">
                    <HeaderTemplate>
                        <a href="JavaScript:ManipulateAll('_Cat');" id="_Folder" style="color: #333333"><i
                            id="img_Cat" class="icon-plus-sign-alt"></i></a>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div runat="server" id="anchor">
                            <a href="JavaScript:divexpandcollapse('_Cat<%# Eval("ID") %>');" style="color: #333333">
                                <i id="img_Cat<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category" ItemStyle-Width="100%">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Category" Width="100%" ToolTip='<%# Bind("Description") %>' CssClass="label"
                            runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="width30px">
                    <HeaderTemplate>
                        <asp:Button ID="Btn_Add_Fun" runat="server" OnClick="Btn_Add_Fun_Click" Text="Remove"
                            CssClass="btn btn-primary btn-sm cls-btnSave" />
                    </HeaderTemplate>
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
                                            <div id="_Cat<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                <asp:GridView ID="gvSubCategory" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped" OnRowDataBound="gvSubCategory_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                            HeaderStyle-CssClass="txt_center">
                                                            <HeaderTemplate>
                                                                <a href="JavaScript:ManipulateAll('_SubCat');" id="_Folder" style="color: #333333"><i
                                                                    id="img_SubCat" class="icon-plus-sign-alt"></i></a>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div runat="server" id="anchor">
                                                                    <a href="JavaScript:divexpandcollapse('_SubCat<%# Eval("ID") %>');" style="color: #333333">
                                                                        <i id="img_SubCat<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub-Category" ItemStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SubCategory" Width="100%" ToolTip='<%# Bind("Description") %>'
                                                                    CssClass="label" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
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
                                                                                    <div id="_SubCat<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                            CssClass="table table-bordered table-striped">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Risk Id">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_RISK_ID" runat="server" CssClass="disp-none" CommandArgument='<%#Eval("Id") %>'
                                                                                                            Text='<%# Bind("Id") %>'></asp:Label>
                                                                                                        <asp:LinkButton ID="lnk_RISK_ID" Text='<%# Bind("RiskActualId") %>' CommandArgument='<%#Eval("id") %>'
                                                                                                            OnClientClick="return ViewRECIDDETAILS(this);" CommandName="Risk_ID" runat="server">
                                                                                                        </asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <%--<asp:TemplateField HeaderText="Risk Category">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Category" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                                                                                                        <asp:Label ID="lblCatId" runat="server" Text='<%# Bind("RCategory") %>' Visible="false"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Sub Category">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_SubCategory" runat="server" Text='<%# Bind("SubCategory") %>'></asp:Label>
                                                                                                        <asp:Label ID="lblSubCatId" runat="server" Text='<%# Bind("RSubCategory") %>' Visible="false"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>--%>
                                                                                                <asp:TemplateField HeaderText="Factor">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Factor" runat="server" Text='<%# Bind("Factor") %>'></asp:Label>
                                                                                                        <asp:Label ID="lblFactorId" runat="server" Text='<%# Bind("RFactor") %>' Visible="false"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Risk Considerations">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_RiskCons" runat="server" Text='<%# Bind("Risk_Description") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Impact">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Impact" runat="server" Text='<%# Bind("Impacts") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Risk Nature Static/Dynamic">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_RiskNature" runat="server" Text='<%# Bind("RiskNature") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="P" AccessibleHeaderText="Probability" ItemStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_P" runat="server" Text='<%# Bind("Probability") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="S" AccessibleHeaderText="Severity" ItemStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_S" runat="server" Text='<%# Bind("Severity") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="D" AccessibleHeaderText="Detection" ItemStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_D" runat="server" Text='<%# Bind("Detection") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="RPN" AccessibleHeaderText="RPN" ItemStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_RPN" runat="server" Text='<%# Bind("RPN") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Count" AccessibleHeaderText="Risk Count" ItemStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Count" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Upgrade" AccessibleHeaderText="Upgrade" ItemStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Upgrade" runat="server" Text='<%# Bind("Upgrade") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Downgrade" AccessibleHeaderText="Downgrade" ItemStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Downgrade" runat="server" Text='<%# Bind("Downgrade") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Added On" AccessibleHeaderText="Added On" ItemStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_RDate" runat="server" Text='<%# Bind("RDate1") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-CssClass="width30px txt_center" HeaderStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="Chk_Sel_Fun" ToolTip="Remove" runat="server" />
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
</asp:Content>
