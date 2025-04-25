<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RM_RiskEvents.aspx.cs" Inherits="CTMS.RM_RiskEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function ViewRECIDDETAILS(element) {
            var RiskId = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
            var TYPE = "UPDATE";
            var test = "Risk_ProejctEvents.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=650,width=1200px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddRisk(element) {
            var RiskId = "";
            var TYPE = "NEW";
            var test = "Risk_ProejctEvents.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;
            var strWinProperty = "toolbar=no,menubar=no,scrollbars=yes,titlebar=no,height=570px,width=1200px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ViewBucket(element) {

            var RiskId = $(element).closest('tr').find('td:eq(1)').find('span').attr('commandargument');
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

            if ($("#<%=ddl_Analysed.ClientID%>").val() == "99") {
                $("#<%=ddl_Analysed.ClientID%>").addClass("brd-1px-redimp");
                return false;
            }

            var ProjectName = $("#<%=Drp_Project_Name.ClientID%>").val();
            var Type = $("#<%=ddl_Analysed.ClientID%>").val();
            var test = "ReportRM_RiskEvents.aspx?ProjectName=" + ProjectName + "&Type=" + Type;

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
                Project Events List
                <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                    CssClass="btn-sm">
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
                        <td>
                            &nbsp;
                        </td>
                        <td class="label">
                            Select Type:
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="Control">
                            <asp:DropDownList ID="ddl_Analysed" runat="server" class="form-control drpControl">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style10">
                            <asp:Button ID="Btn_Get_Fun" runat="server" Text="Get Risk" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="Btn_Get_Fun_Click" />
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
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box">
        <asp:GridView ID="gridprojevents" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped" OnRowCommand="gridprojevents_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Id" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RISK_ID" runat="server" CssClass="disp-none" CommandArgument='<%#Eval("Id") %>'
                            Text='<%# Bind("id") %>'></asp:Label>
                        <asp:LinkButton ID="lnk_RISK_ID" Text='<%# Bind("id") %>' CommandArgument='<%#Eval("id") %>'
                            OnClientClick="return ViewRECIDDETAILS(this);" CommandName="Risk_ID" runat="server">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="bucket" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                    <ItemTemplate>
                        <asp:Label ID="lbl_BucketID" runat="server" CommandArgument='<%#Eval("BucketID") %>'
                            Text='<%# Bind("BucketID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reference">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_Reference" Text='<%# Bind("RiskID") %>' CommandArgument='<%#Eval("RiskID") %>'
                            OnClientClick="return ViewBucket(this);" CommandName="Risk_ID" runat="server">
                        </asp:LinkButton>
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
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RiskNature" runat="server" Text='<%# Bind("Risk_Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Risk Type">
                    <ItemTemplate>
                        <asp:Label ID="lbl_P" runat="server" Text='<%# Bind("Risk_Type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Source">
                    <ItemTemplate>
                        <asp:Label ID="lbl_S" runat="server" Text='<%# Bind("Source") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_D" runat="server" Text='<%# Bind("RDateIdentiFied1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtndelete" runat="server" CommandArgument='<%# Bind("id") %>'
                            CommandName="Delete1" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
