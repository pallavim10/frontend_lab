<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_Risk_MitigationDetails.aspx.cs"
    Inherits="CTMS.RM_Risk_MitigationDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8">
    <title>WAI</title>
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
    <script type="text/javascript">
        function ViewRisk(element) {
            var RiskId = document.getElementById("hfdID").value;
            var TYPE = "UPDATE";

            var test = "RM_RiskView.aspx?RiskId=" + RiskId + "&TYPE=" + TYPE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
    </script>
    <script type="text/javascript">

        function DecodeUrl(url) {

            var dec = decodeURI(url);

            window.location.href = dec;
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

        $(document).on("click", ".cls-btnSave1", function () {
            var test = "0";

            $('.required1').each(function (index, element) {
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


        // js prototype
        if (typeof (Number.prototype.isBetween) === "undefined") {
            Number.prototype.isBetween = function (min, max, notBoundaries) {
                var between = false;
                if (notBoundaries) {
                    if ((this < max) && (this > min)) between = true;
                } else {
                    if ((this <= max) && (this >= min)) between = true;
                }
                return between;
            }
        }
        function CalculateRPN() {
            var Severity = $("#<%=drpSeverity.ClientID%>").val();
            var Probability = $("#<%=drpProbability.ClientID%>").val();
            var Detectability = $("#<%=drpDetectability.ClientID%>").val();
            var RPN = (Severity * Probability * Detectability);
            var PDMULTIPLY = (Probability * Detectability);
            $("#<%=txtRPN.ClientID%>").val(RPN);
            var RiskCategory = $("#<%=txtRiskCategory.ClientID%>");
            RiskCategory.removeClass();
            if (Severity == '1') {
                if (PDMULTIPLY.isBetween(1, 15)) {
                    RiskCategory.val('Low');
                    RiskCategory.addClass("form-control txtCenter bkColorGreen");
                }
                if (PDMULTIPLY.isBetween(15, 25)) {
                    RiskCategory.val('Moderate');
                    RiskCategory.addClass("form-control txtCenter bkColorYellow");
                }
            }
            if (Severity == '2') {
                if (PDMULTIPLY.isBetween(1, 5)) {
                    RiskCategory.val('Low');
                    RiskCategory.addClass("bkColorGreen form-control txtCenter");
                }
                if (PDMULTIPLY.isBetween(5, 15)) {
                    RiskCategory.val('Moderate');
                    RiskCategory.addClass("bkColorYellow form-control txtCenter");

                }
                if (PDMULTIPLY.isBetween(15, 25)) {
                    RiskCategory.val('Medium');
                    RiskCategory.addClass("bkColororange form-control txtCenter");
                }
            }
            if (Severity == '3') {
                if (PDMULTIPLY.isBetween(1, 5)) {
                    RiskCategory.val('Low');
                    RiskCategory.addClass("bkColorGreen form-control txtCenter");
                }
                if (PDMULTIPLY.isBetween(5, 10)) {
                    RiskCategory.val('Moderate');
                    RiskCategory.addClass("bkColorYellow form-control txtCenter");

                }
                if (PDMULTIPLY.isBetween(10, 15)) {
                    RiskCategory.val('Medium');
                    RiskCategory.addClass("bkColororange form-control txtCenter");

                }
                if (PDMULTIPLY.isBetween(15, 25)) {
                    RiskCategory.val('Severe');
                    RiskCategory.addClass("bkColorRed form-control txtCenter");
                }
            }
            if (Severity == '4') {
                if (PDMULTIPLY.isBetween(1, 5)) {
                    RiskCategory.val('Moderate');
                    RiskCategory.addClass("bkColorYellow form-control txtCenter");

                }
                if (PDMULTIPLY.isBetween(5, 10)) {
                    RiskCategory.val('Medium');
                    RiskCategory.addClass("bkColororange form-control txtCenter");

                }
                if (PDMULTIPLY.isBetween(10, 25)) {
                    RiskCategory.val('Severe');
                    RiskCategory.addClass("bkColorRed form-control txtCenter");
                }
            }
            if (Severity == '5') {
                if (PDMULTIPLY.isBetween(1, 5)) {
                    RiskCategory.val('Moderate');
                    RiskCategory.addClass("bkColorYellow form-control txtCenter");

                }
                if (PDMULTIPLY.isBetween(5, 10)) {
                    RiskCategory.val('Medium');
                    RiskCategory.addClass("bkColororange form-control txtCenter");

                }
                if (PDMULTIPLY.isBetween(10, 25)) {
                    RiskCategory.val('Severe');
                    RiskCategory.addClass("bkColorRed form-control txtCenter");
                }
            }
        }

        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });
        function pageLoad() {
            CalculateRPN();
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                    // firstDay: 1,
                    //position: 'top right',
                    // minDate: new Date('2000-01-01'),
                    // maxDate: new Date('9999-12-31'),
                    format: 'DD-MMM-YYYY',
                    //  defaultDate: new Date(''),
                    //setDefaultDate: false,
                    yearRange: [1910, 2050]
                });
            });
        }
    </script>
    <script type="text/javascript">
        function OpenPopup() {

            $('#<%=popup_Risk_UpdateData.ClientID%>').dialog({
                title: "Save Data",
                width: 700,
                height: "auto",
                modal: true
            });
            $('#<%=popup_Risk_UpdateData.ClientID%>').dialog("open");
            return false;
        }

        function ClosePopup() {
            $("#popup_Risk_UpdateData").dialog('close');
            window.location = window.location;
            return false;
        }

        function SaveData(element) {

            var P = $('#drpProbability').val().trim();
            var S = $('#drpSeverity').val().trim();
            var D = $('#drpDetectability').val().trim();
            var RPN = $('#txtRPN').val().trim();
            var RiskCat = $('#txtRiskCategory').val().trim();
            var Notes = $('#txtNotes').val();
            var Up = $('#txtUp').val().trim();
            var Down = $('#txtDown').val().trim();
            var Comment = $('#txt_Comments').val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/Risk_saveData",
                data: '{P:"' + $('#drpProbability').val().trim() + '",  S: "' + $('#drpSeverity').val().trim() + '", D: "' + $('#drpDetectability').val().trim() + '" ,RPN: "' + $('#txtRPN').val().trim() + '",RiskCat: "' + $('#txtRiskCategory').val().trim() + '",Notes: "' + $('#txtNotes').val() + '",Up: "' + $('#txtUp').val().trim() + '",Down: "' + $('#txtDown').val().trim() + '",Comment: "' + $('#txt_Comments').val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $("#popup_Risk_UpdateData").dialog('close');
                        window.location = window.location;
                        alert("Record Updated Successfully.");
                        window.close();
                    }
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        alert("Contact administrator not suceesfully updated")
                    }
                }
            });
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
                Update Risk Bucket Details</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <asp:HiddenField ID="hf_P" runat="server" Value="" />
        <asp:HiddenField ID="hf_S" runat="server" Value="" />
        <asp:HiddenField ID="hf_D" runat="server" Value="" />
        <div id="popup_Risk_UpdateData" runat="server" title="Reason For Change" class="disp-none">
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label3" runat="server" CssClass="wrapperLable" Text="Reason"></asp:Label>
                <asp:DropDownList ID="drp_Reason" CssClass="width245px form-control required1" runat="server">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="Other">Other</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="formControl" style="display: inline-flex">
                <asp:Label ID="Label9" runat="server" CssClass="wrapperLable" Text="Comments"></asp:Label>
                <asp:TextBox ID="txt_Comments" CssClass="width245px form-control required1" TextMode="MultiLine"
                    runat="server"></asp:TextBox>
            </div>
            <div style="margin-top: 10px">
                <asp:Button ID="btn_Update" runat="server" Style="margin-left: 107px;" CssClass="btn btn-primary btn-sm cls-btnSave1"
                    Text="Save" OnClientClick="SaveData(this);" />
                <asp:Button ID="btn_Cancel" runat="server" Style="margin-left: 62px" CssClass="btn btn-danger btn-sm"
                    Text="Cancel" OnClientClick="ClosePopup();" />
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="box-body">
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Risk ID</label>
                <div class="col-lg-3">
                    <asp:LinkButton runat="server" ID="lblRiskIds" OnClientClick="return ViewRisk(this);"
                        Width="100px" CssClass="txtCenter" />
                    <asp:HiddenField runat="server" ID="hfdID" />
                </div>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="box-body">
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Category</label>
                <div class="col-lg-3">
                    <asp:Label runat="server" ID="lbl_Category" Width="300px" CssClass="form-control" />
                </div>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="box-body">
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Sub Category</label>
                <div class="col-lg-3">
                    <asp:Label runat="server" ID="lbl_SubCategory" Width="300px" CssClass="form-control" />
                </div>
                <label class="col-lg-3 width100px label">
                    Up Trigger</label>
                <div class="col-lg-3">
                    <asp:TextBox runat="server" ID="txtUp" Width="50px" CssClass="form-control required txtCenter" />
                </div>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="box-body">
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Factor</label>
                <div class="col-lg-3">
                    <asp:Label runat="server" ID="lbl_Factor" Width="300px" CssClass="form-control" />
                </div>
                <label class="col-lg-3 width100px label">
                    Down Trigger</label>
                <div class="col-lg-3">
                    <asp:TextBox runat="server" ID="txtDown" Width="50px" CssClass="form-control required txtCenter" />
                </div>
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <div class="box-body">
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Probability</label>
                <div class="col-lg-3">
                    <asp:DropDownList runat="server" ID="drpProbability" onchange="CalculateRPN();" CssClass="form-control txtCenter required"
                        Width="60px">
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="label">
                    Severity
                </div>
                <div class="col-lg-3 ">
                    <asp:DropDownList runat="server" ID="drpSeverity" onchange="CalculateRPN();" CssClass="form-control txtCenter required"
                        Width="60px">
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="label txtCenter">
                    Detectability</div>
                <div class="col-lg-3 ">
                    <asp:DropDownList runat="server" ID="drpDetectability" onchange="CalculateRPN();"
                        CssClass="form-control txtCenter required" Width="60px">
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="label txtCenter">
                    RPN
                </div>
                <div class="col-lg-3 ">
                    <asp:TextBox runat="server" ID="txtRPN" CssClass="form-control txtCenter" Style="color: Red"
                        Width="50px"></asp:TextBox>
                </div>
                <div class="label txtCenter">
                    Risk Category
                </div>
                <div class="col-lg-3 ">
                    <asp:TextBox runat="server" ID="txtRiskCategory" Width="100px" Style="color: blue"
                        CssClass="form-control txtCenter" />
                </div>
            </div>
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Notes</label>
                <div class="col-lg-8">
                    <asp:TextBox runat="server" ID="txtNotes" Width="670px" Height="100px" TextMode="MultiLine"
                        CssClass="form-control required"> 
                    </asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Button ID="bntSave" runat="server" Text="Save" Style="margin-left: 10px" CssClass="btn btn-primary btn-sm cls-btnSave"
                    OnClick="bntSave_Click" />
            </h3>
        </div>
    </div>
    </form>
</body>
</html>
