<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_Assign_RiskIndic.aspx.cs"
    Inherits="CTMS.RM_Assign_RiskIndic" %>

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
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script type="text/javascript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false
            });

            $('.select').select2();

        }

        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        // Read a page's GET URL variables and return them as an associative array.
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        function OpenRiskTrigger(Id) {
            var TileID = Id;
            var test = "RM_INDIC_TRIGGER.aspx?TILEID=" + TileID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=449px,width=1024px";
            window.open(test, '_blank', strWinProperty);
            window.top.close();
            return false;
        }

        function AddNewRiskIndic() {
            var TileID = getUrlVars()["TILEID"];
            var test = "RM_Assign_NewRiskIndic.aspx?TILEID=" + TileID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=820px,width=1900px";
            window.open(test, '_blank', strWinProperty);
            window.top.close();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    Select Risk Indicator to Assign
                </h3>
                <div class="pull-right" style="margin-bottom: 10px; margin-right: 10px;">
                    <asp:LinkButton runat="server" ID="lbtnAddNew" Text="Add New" CssClass="btn btn-primary btn-sm "
                        ForeColor="White" OnClientClick="return AddNewRiskIndic();"></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Category :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpTabCategory" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                        OnSelectedIndexChanged="drpTabCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Sub-Category :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpTabSubCategory" runat="server" CssClass="form-control width250px"
                                        AutoPostBack="True" OnSelectedIndexChanged="drpTabSubCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Relative Value :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpTabRelativeValue" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                        OnSelectedIndexChanged="drpTabRelativeValue_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Frequency :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpTabFreq" runat="server" CssClass="form-control width250px"
                                        AutoPostBack="True" OnSelectedIndexChanged="drpTabFreq_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Level of Scrutiny :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpTabLevelSec" runat="server" AutoPostBack="True" CssClass="form-control width250px"
                                        OnSelectedIndexChanged="drpTabLevelSec_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Core or Specific :&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="drpTabCoreSpecific" runat="server" CssClass="form-control width250px"
                                        AutoPostBack="True" OnSelectedIndexChanged="drpTabCoreSpecific_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="grdRiskIndicators" runat="server" AutoGenerateColumns="false" OnPreRender="grd_data_PreRender"
                                            CssClass="table table-bordered Datatable table-striped notranslate" OnRowCommand="grdRiskIndicators_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Category" runat="server" Text='<%# Bind("Category") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub-Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SubCategory" runat="server" Text='<%# Bind("Sub_Category") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Risk Indicator">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RiskIndicator" runat="server" Text='<%# Bind("Risk_Indicator") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Experience">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Experience" runat="server" Text='<%# Bind("Experience") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discussion/Details">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DiscussionDetails" runat="server" Text='<%# Bind("Discussion_Details") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Relative Value" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RelativeValue" runat="server" Text='<%# Bind("Relative_Value") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Core or Specific" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CoreorSpecific" runat="server" Text='<%# Bind("Core_or_Specific") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Level of Scrutiny" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LevelofScrutiny" runat="server" Text='<%# Bind("Level_of_Scrutiny") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Frequency" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Frequency" runat="server" Text='<%# Bind("Frequency") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Threshold Basis" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ThresholdBasis" runat="server" Text='<%# Bind("Threshold_Basis") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Scorecard" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Scorecard" runat="server" Text='<%# Bind("Scorecard") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Weighting" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Weighting" runat="server" Text='<%# Bind("Weighting") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mitigation Actions" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MitigationActions" runat="server" Text='<%# Bind("Mitigation_Actions") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RACT Traceability" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RACTTraceability" runat="server" Text='<%# Bind("RACT_Traceability") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Primary PI" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PrimaryPI" runat="server" Text='<%# Bind("Primary_PI") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Secondary PI" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SecondaryPI" runat="server" Text='<%# Bind("Secondary_PI") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Assign" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAssign" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="Assign" ToolTip="Assign"><i class="fa fa-arrow-circle-right" style="color:Black;"></i></asp:LinkButton>
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
            </div>
        </div>
    </div>
    </form>
</body>
</html>
