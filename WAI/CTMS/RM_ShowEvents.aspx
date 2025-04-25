<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_ShowEvents.aspx.cs"
    Inherits="CTMS.RM_ShowEvents" %>

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

            var test = "Risk_ProejctEvents.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;

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
                Project Risk Events Detail</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="box">
        <asp:GridView ID="gridprojevents" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped">
            <Columns>
                <asp:TemplateField HeaderText="Risk Id" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RISK_ID" runat="server" CssClass="disp-none" CommandArgument='<%#Eval("Id") %>'
                            Text='<%# Bind("id") %>'></asp:Label>
                        <asp:LinkButton ID="lnk_RISK_ID" Text='<%# Bind("id") %>' CommandArgument='<%#Eval("id") %>'
                            OnClientClick="return ViewRECIDDETAILS(this);" CommandName="Risk_ID" runat="server">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Risk Considerations">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RiskCons" runat="server" Text='<%# Bind("REvent_Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Impacts">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Impact" runat="server" Text='<%# Bind("Risk_Impact") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Risk Status">
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
                <asp:TemplateField HeaderText="Date IdentiFied">
                    <ItemTemplate>
                        <asp:Label ID="lbl_D" runat="server" Text='<%# Bind("RDateIdentiFied") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
