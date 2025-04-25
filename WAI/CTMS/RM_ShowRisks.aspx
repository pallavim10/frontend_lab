<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_ShowRisks.aspx.cs" Inherits="CTMS.RM_ShowRisks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Diagonsearch</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />--%>
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <link href="Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.alerts.js" type="text/javascript"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ViewRECIDDETAILS(element) {

            var RiskId = $(element).closest('tr').find('td:eq(0)').find('span').attr('commandargument');
            var TYPE = "UPDATE";

            var test = "RM_Risk_MitigationDetails.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Risk Bucket Details</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="box">
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
                <asp:TemplateField HeaderText="Risk Category">
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
                </asp:TemplateField>
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
                <asp:TemplateField HeaderText="Risk Nature">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RiskNature" runat="server" Text='<%# Bind("RiskNature") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="P" AccessibleHeaderText="Probability">
                    <ItemTemplate>
                        <asp:Label ID="lbl_P" runat="server" Text='<%# Bind("Probability") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="S" AccessibleHeaderText="Severity">
                    <ItemTemplate>
                        <asp:Label ID="lbl_S" runat="server" Text='<%# Bind("Severity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="D" AccessibleHeaderText="Detection">
                    <ItemTemplate>
                        <asp:Label ID="lbl_D" runat="server" Text='<%# Bind("Detection") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RPN" AccessibleHeaderText="RPN">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RPN" runat="server" Text='<%# Bind("RPN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Count" AccessibleHeaderText="Risk Count">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Count" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
