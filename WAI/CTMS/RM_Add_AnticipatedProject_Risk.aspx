<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="RM_Add_AnticipatedProject_Risk.aspx.cs"
    Inherits="CTMS.RM_Add_AnticipatedProject_Risk" %>

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

            var test = "RM_Risk_Details.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=520,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Print() {
            if ($("#<%=Drp_Project_Name.ClientID%>").val() == "") {
                $("#<%=Drp_Project_Name.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectId = $("#<%=Drp_Project_Name.ClientID%>").val();
            var test = "ReportRM_Add_AnticipatedProject_Risk.aspx?ProjectId=" + ProjectId;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1500px";
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
                Risk Bank
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
                            Select Project:&nbsp;&nbsp;
                            <asp:Label ID="Lbl_Sel_Dept" CssClass="requiredSign" runat="server" Text="*"></asp:Label>
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
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr class="disp-none">
                        <td class="label">
                            Search By Category :
                        </td>
                        <td class="Control">
                            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" class="form-control drpControl"
                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="label">
                            Search By Sub Category :
                        </td>
                        <td class="Control">
                            <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="true" class="form-control drpControl"
                                OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="disp-none">
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
        <div class="box-header">
            <h3 class="box-title">
                Add Anticipated Project Risks
            </h3>
        </div>
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
                <asp:TemplateField ItemStyle-CssClass="width30px txt_center">
                    <HeaderTemplate>
                        <asp:Button ID="Btn_Add_Fun" runat="server" OnClick="Btn_Add_Fun_Click" Text="Add"
                            CssClass="btn btn-primary btn-sm cls-btnSave" />
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="width30px txt_center">
                    <HeaderTemplate>
                        <asp:Button ID="Btn_Del" OnClick="Btn_Del_Click" runat="server" Text="Delete" CssClass="btn btn-primary btn-sm cls-btnSave" />
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
                                                                                            CssClass="table table-bordered table-striped" OnRowDataBound="GridView1_RowDataBound">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="width20px">
                                                                                                    <ItemTemplate>
                                                                                                        <%--CssClass="disp-none"--%>
                                                                                                        <asp:Label ID="lblRISK_ID" runat="server" CssClass="disp-none" CommandArgument='<%#Eval("Id") %>'
                                                                                                            Text='<%# Bind("Id") %>'></asp:Label>
                                                                                                        <asp:LinkButton ID="lbtn_RISK_ID" Text='<%# Bind("Id") %>' CommandArgument='<%#Eval("id") %>'
                                                                                                            OnClientClick="return ViewRECIDDETAILS(this);" CommandName="Risk_ID" runat="server"></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Risk ID" ItemStyle-CssClass="width20px">
                                                                                                    <ItemTemplate>
                                                                                                        <%--CssClass="disp-none"--%>
                                                                                                        <asp:Label ID="lbl_RISK_ID" runat="server" CssClass="disp-none" CommandArgument='<%#Eval("Id") %>'
                                                                                                            Text='<%# Bind("Id") %>'></asp:Label>
                                                                                                        <asp:LinkButton ID="lnk_RISK_ID" Text='<%# Bind("RiskActualId") %>' CommandArgument='<%#Eval("id") %>'
                                                                                                            OnClientClick="return ViewRECIDDETAILS(this);" CommandName="Risk_ID" runat="server"></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Risk Category" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Category" runat="server" Text='<%# Bind("Category") %>'></asp:Label>
                                                                                                        <asp:Label ID="lblCatId" runat="server" Text='<%# Bind("CategoryId") %>' Visible="false"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Sub Category" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_SubCategory" runat="server" Text='<%# Bind("SubCategory") %>'></asp:Label>
                                                                                                        <asp:Label ID="lblSubCatId" runat="server" Text='<%# Bind("SubCategoryId") %>' Visible="false"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Factor">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Factor" runat="server" Text='<%# Bind("Factor") %>'></asp:Label>
                                                                                                        <asp:Label ID="lblFactorId" runat="server" Text='<%# Bind("TopicId") %>' Visible="false"></asp:Label>
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
                                                                                                <asp:TemplateField HeaderText="Suggested Mitigation">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_SugMitig" runat="server" Text='<%# Bind("Possible_Mitigations") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Suggested Risk category">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_SugRiskCat" runat="server" Text='<%# Bind("SugestedRiskCategory") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Risk Nature Static/Dynamic">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_RiskNature" runat="server" Text='<%# Bind("RiskNature") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Transcelerate Category">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_TransCat" runat="server" Text='<%# Bind("TranscelerateCategory") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Key Value">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblkeyvalue" runat="server" Text='<%# Bind("KeyValue") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-CssClass="width30px txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="Chk_Sel_Fun" ToolTip="Add" runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-CssClass="width30px txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="Chk_Del" ToolTip="Delete" runat="server" />
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
    <br />
</asp:Content>
