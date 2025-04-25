<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_Assign_NewRiskIndic.aspx.cs"
    Inherits="CTMS.RM_Assign_NewRiskIndic" %>

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
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonFunction.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <%--  <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>--%>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- fullCalendar -->
    <script src="js/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <!-- jQuery Knob Chart -->
    <script src="js/plugins/jqueryKnob/jquery.knob.js" type="text/javascript"></script>
    <!-- daterangepicker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="js/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="js/AdminLTE/app.js" type="text/javascript"></script>
    <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
    <link href="Styles/graph.css" rel="stylesheet" type="text/css" />
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

        $(function () {

            var avaTag = $("#<%=hfCats.ClientID%>").val().split("^");
            $(".txtCategory").autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });

            avaTag = $("#<%=hfSubCats.ClientID%>").val().split("^");
            $(".txtSubCategory").autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        });

        function bindCats() {
            var avaTag = $("#<%=hfCats.ClientID%>").val().split("^");
            $('.txtCategory').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });


            avaTag = $("#<%=hfSubCats.ClientID%>").val().split("^");
            $('.txtSubCategory').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        }

        function bindSubCats() {
            var avaTag = $("#<%=hfCats.ClientID%>").val().split("^");
            $('.txtCategory').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });


            avaTag = $("#<%=hfSubCats.ClientID%>").val().split("^");
            $('.txtSubCategory').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
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
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <div class="box box-warning">
                    <div class="box-header">
                        <h3 class="box-title">
                            Create New Risk Indicator to Assign
                        </h3>
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
                                            <asp:HiddenField runat="server" ID="hfCats" />
                                            <asp:TextBox ID="txtCategory" runat="server" TabIndex="1" AutoPostBack="true" onkeyup="return bindSubCats();"
                                                CssClass="form-control txtCategory required width250px" OnTextChanged="txtCategory_TextChanged">
                                            </asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                            Sub-Category :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:HiddenField runat="server" ID="hfSubCats" />
                                            <asp:TextBox ID="txtSubCategory" runat="server" TabIndex="3" onfocus="return bindSubCats();"
                                                onkeydown="return bindSubCats();" CssClass="form-control txtSubCategory required width250px">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Risk Indicator :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtRiskIndi" CssClass="form-control required width250px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                            Experience :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="drpExp" runat="server" CssClass="form-control required width250px">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Discussion/Details :&nbsp;
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtDetails" Width="727px" TextMode="MultiLine" CssClass="form-control required"> 
                                            </asp:TextBox>
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
                                            <asp:DropDownList ID="drpRelativeValue" runat="server" CssClass="form-control required width250px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-2">
                                            Core or Specific :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpCoreSpecific" runat="server" CssClass="form-control required width250px">
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
                                            <asp:DropDownList ID="drpLevelSec" runat="server" CssClass="form-control required width250px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-2">
                                            Frequency :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpFreq" runat="server" CssClass="form-control required width250px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Threshold Basis : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:ListBox ID="lstThreshold" runat="server" CssClass="select" SelectionMode="Multiple"
                                                Width="727px"></asp:ListBox>
                                        </div>
                                        <div class="label col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Scorecard :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlScorecard" runat="server" CssClass="form-control required width250px">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-2">
                                            Weighting :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlWeighting" runat="server" CssClass="form-control required width250px">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Mitigation Actions :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlmitigation" runat="server" CssClass="form-control required width250px">
                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-2">
                                            RACT Traceability :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtract" CssClass="form-control required width250px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Primary PI :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtprimarypi" CssClass="form-control required width250px"></asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                            Secondary PI :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtsecpi" CssClass="form-control required width250px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            <h4 class="box-title">
                                                Map with Risk Bank
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Category :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpRiskCategory" runat="server" CssClass="form-control width250px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpRiskCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-2">
                                            Sub-Category :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpRiskSubCategory" runat="server" CssClass="form-control width250px"
                                                AutoPostBack="true" OnSelectedIndexChanged="drpRiskSubCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Factor :&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="drpRiskFactor" runat="server" CssClass="form-control width250px">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="label col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Event Description (Project Level) : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtRiskDescription" Height="36px" Width="727px" TextMode="MultiLine"
                                                CssClass="form-control "> 
                                            </asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Event Description (Country Level) : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtRiskDescriptionC" Height="36px" Width="727px"
                                                TextMode="MultiLine" CssClass="form-control "> 
                                            </asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Event Description (Inv. Level) : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtRiskDescriptionInv" Height="36px" Width="727px"
                                                TextMode="MultiLine" CssClass="form-control "> 
                                            </asp:TextBox>
                                        </div>
                                        <div class="label col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Impacts : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:ListBox ID="lstRiskImpact" runat="server" CssClass="select" SelectionMode="Multiple"
                                                Width="727px"></asp:ListBox>
                                        </div>
                                        <div class="label col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="label col-md-2">
                                            Risk Type : &nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:ListBox ID="lstRiskType" runat="server" CssClass="select" SelectionMode="Multiple"
                                                Width="727px"></asp:ListBox>
                                        </div>
                                        <div class="label col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="txt_center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
