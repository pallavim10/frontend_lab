<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProtDev.aspx.cs" Inherits="CTMS.ProtDev" %>

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
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
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
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        $(document).on("click", ".cls-btnSave2", function () {
            var test = "0";

            $('.required2').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        $(document).on("click", ".cls-btnSave3", function () {
            var test = "0";

            $('.required3').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });

        $(document).on("click", ".cls-btnSave4", function () {
            var test = "0";

            $('.required4').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == null) {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
                else if (ctrl == "text" || ctrl == "textarea") {
                    if (value == "") {
                        $(this).addClass("brd-1px-redimp");
                        test = "1";
                    }
                    else {
                        $(this).removeClass("brd-1px-redimp");
                    }
                }
            });

            if (test == "1") {
                return false;
            }
            return true;
        });


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

        function ClickNav(ID) {

            var TabElement = $($('#' + ID).find('a'));

            $(TabElement).parent().addClass("active");
            $(TabElement).parent().siblings().removeClass("active");
            var tab = $(TabElement).attr("href");
            $(".tab-content").not(tab).css("display", "none");
            $(tab).fadeIn();

        }

        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });

            if ($(location).attr('href').indexOf('NIWRS_FORM') != -1) {

                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050],
                        maxDate: new Date()
                    });
                });

            }
            else {

                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050]
                    });
                });

            }

            $('.txtDateNoFuture').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050],
                    maxDate: new Date()
                });
            });

            $('.txtDateMask').each(function (index, element) {
                $(element).inputmask("dd/mm/yyyy", { placeholder: "dd/mm/yyyy" });
            });

            $('.txtTime').each(function (index, element) {
                $(element).inputmask(
                    "hh:mm", {
                    placeholder: "HH:MM",
                    insertMode: false,
                    showMaskOnHover: false,
                    hourFormat: "24"
                });
            });


            $('.txtuppercase').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });

            $('.txtuppercase').keydown(function (e) {

                var key = e.keyCode;
                if (key === 189 && e.shiftKey === true) {
                    return true;
                }
                else if ((key == 189) || (key == 109)) {
                    return true;
                }
                else if (e.ctrlKey || e.altKey) {
                    e.preventDefault();
                }
                else {
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }

            });
        });

        function Print() {
            var ProjectId = '<%= Session["PROJECTID"] %>'
            var PROTVOIL_ID = $('#hdPROTVOILID').val();
            var test = "ReportProtDev.aspx?PROTVOIL_ID=" + PROTVOIL_ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function POST_TO_RISK() {

            $('#<%=popup_POST_PROTOCOL_TO_ISSUE.ClientID%>').dialog({
                title: "Post To Risk",
                width: 600,
                height: "auto",
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });
            $('#<%=popup_POST_PROTOCOL_TO_ISSUE.ClientID%>').dialog("open");
            return false;
        }

        function YESPOSTRISK_Prot() {
            var InvID = $("#drp_InvID option:selected").text();
            var SUBJID = $("#drp_SUBJID option:selected").text();
            var Dept = $("#drp_DEPT").val();
            var Summary = $("#txtSummary").val();
            var Status = $("#drp_Status option:selected").text();
            var Classification = $("#drp_Priority_Ops option:selected").text();
            var Description = $("#txtDescription").val();
            var RefID = $("#hdPROTVOILID").val();
            var Source = $("#txtSource").val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/POST_PROTOCOL_TO_ISSUE_ALL",
                data: '{InvID: "' + InvID + '",SUBJID: "' + SUBJID + '",Dept: "' + Dept + '",Summary: "' + Summary + '",Status: "' + Status + '",Classification: "' + Classification + '",Description: "' + Description + '",RefID: "' + RefID + '",Source: "' + Source + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $("#popup_POST_PROTOCOL_TO_ISSUE").dialog('close');
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

        function POSTNORISK_Prot() {
            var InvID = $("#drp_InvID option:selected").text();
            var SUBJID = $("#drp_SUBJID option:selected").text();
            var Dept = $("#drp_DEPT").val();
            var Summary = $("#txtSummary").val();
            var Status = $("#drp_Status option:selected").text();
            var Classification = $("#drp_Priority_Ops option:selected").text();
            var Description = $("#txtDescription").val();
            var RefID = $("#hdPROTVOILID").val();
            var Source = $("#txtSource").val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/POST_PROTOCOL_TO_ISSUE",
                data: '{InvID: "' + InvID + '",SUBJID: "' + SUBJID + '",Dept: "' + Dept + '",Summary: "' + Summary + '",Status: "' + Status + '",Classification: "' + Classification + '",Description: "' + Description + '",RefID: "' + RefID + '",Source: "' + Source + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $("#popup_POST_PROTOCOL_TO_ISSUE").dialog('close');
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


        function ProtDevDetails(element) {
            var UserType = $('#hdnUserType').val();
            var PROTVOIL_ID = $(element).prev().attr('commandargument');
            var test = "ProtDev.aspx?PROTVOIL_ID=" + PROTVOIL_ID + "&UserType=" + UserType;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1100";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Protocol Deviation No.
                <asp:Label runat="server" ID="lblPROTVOILID"></asp:Label>
                    &nbsp;
                <asp:LinkButton ID="lbtnprint" runat="server" Text="Print" OnClientClick="return Print()"
                    CssClass="btn-sm">
      <span class="glyphicon glyphicon-print"></span>Print</asp:LinkButton>
                </h3>
            </div>
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="hdnUserType" />
            <asp:HiddenField ID="hdPROTVOILID" runat="server" />
            <div class="box-body">
                <div class="form-group">
                    <div class="row disp-none">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Project :&nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="Drp_Project" runat="server" ForeColor="Blue" class="form-control drpControl required"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Site ID :&nbsp;
                            <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drp_InvID" runat="server" ForeColor="Blue" AutoPostBack="true"
                                    class="form-control drpControl width200px required">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Subject ID: &nbsp;
                            <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drp_SUBJID" runat="server" ForeColor="Blue" AutoPostBack="true"
                                    class="form-control drpControl width200px required ">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Department :&nbsp;
                            <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drp_DEPT" runat="server" ForeColor="Blue" AutoPostBack="true"
                                    class="form-control drpControl width200px required">
                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Central Laboratory" Value="Central Laboratory"></asp:ListItem>
                                    <asp:ListItem Text="Data Management" Value="Data Management"></asp:ListItem>
                                    <asp:ListItem Text="Medical" Value="Medical"></asp:ListItem>
                                    <asp:ListItem Text="Operations" Value="Operations"></asp:ListItem>
                                    <asp:ListItem Text="Pharmacy" Value="Pharmacy"></asp:ListItem>
                                    <asp:ListItem Text="Statistics" Value="Statistics"></asp:ListItem>
                                    <asp:ListItem Text="Investigator" Value="Investigator"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Visit No.: &nbsp;
                            <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlVISITNUM" runat="server" ForeColor="Blue" class="form-control width200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Date of Identified :&nbsp;
                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtDateIdentified" ReadOnly="true" ForeColor="Blue" runat="server"
                                    class="form-control width200px"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                Status : &nbsp;
                            <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drp_Status" runat="server" ForeColor="Blue" AutoPostBack="true"
                                    class="form-control drpControl required width200px">
                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                                    <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                                    <asp:ListItem Text="New" Value="New"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Source :&nbsp;
                            <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtSource" runat="server" ForeColor="Blue" class="form-control width200px"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                Reference : &nbsp;
                            <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtReference" runat="server" ForeColor="Blue" class="form-control required width200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Date of Ocuurence :&nbsp;
                            <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtOCDate" runat="server" class="form-control txtDate width200px"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                Date of Report : &nbsp;
                            <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCloseDate" runat="server" class="form-control txtDate width200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Count :&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtPdmasterID" runat="server" Visible="false" class="form-control"></asp:TextBox>
                                <asp:TextBox ID="txtCount" runat="server" ReadOnly="true" Style="color: Red" class="form-control txt_center"></asp:TextBox>
                            </div>
                            <div class="label col-md-2">
                                Date of Reported to IRB : &nbsp;
                            <asp:Label ID="Label21" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtIRBDate" runat="server" class="form-control txtDate width200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Description : &nbsp;
                            <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtDescription" runat="server" class="form-control required" TextMode="MultiLine"
                                    Width="660px" Height="50px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Process :&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drp_Nature" ForeColor="Maroon" CssClass="form-control required width200px"
                                    AutoPostBack="true" OnSelectedIndexChanged="drp_Nature_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Category : &nbsp;
                            <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drp_PDCode1" ForeColor="Maroon" CssClass="form-control required width200px"
                                    AutoPostBack="True" OnSelectedIndexChanged="drp_PDCode1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Sub-Category :&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="drp_PDCode2" ForeColor="Maroon" CssClass="form-control required width200px"
                                    OnSelectedIndexChanged="drp_PDCode2_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2 disp-none">
                                Duplicacy : &nbsp;
                            <asp:Label ID="Label15" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3 disp-none">
                                <asp:DropDownList runat="server" ID="ddlDuplicacy" ForeColor="Maroon" CssClass="form-control width200px">
                                    <asp:ListItem Selected="True" Text="New" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Possibly Duplicate" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Duplicate" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Classification :&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lbl_Priority_Default" runat="server" ForeColor="Maroon" class="form-control width200px">
                                </asp:Label>
                            </div>
                            <div class="label col-md-2" style="display: inline-flex">
                                Classification by Medical
                            <asp:Label ID="Label16" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drp_Priority_Med" runat="server" ForeColor="Maroon" class="form-control drpControl width200px">
                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Major" Value="Major"></asp:ListItem>
                                    <asp:ListItem Text="Minor" Value="Minor"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Classification by Statistician :&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drp_Priority_Ops" runat="server" ForeColor="Maroon" class="form-control drpControl width200px">
                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Major" Value="Major"></asp:ListItem>
                                    <asp:ListItem Text="Minor" Value="Minor"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="label col-md-2">
                                Final Classification : &nbsp;
                            <asp:Label ID="Label18" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drp_Priority_Final" runat="server" ForeColor="Maroon" class="form-control drpControl width200px">
                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Major" Value="Major"></asp:ListItem>
                                    <asp:ListItem Text="Minor" Value="Minor"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row disp-none">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Summary :&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtSummary" runat="server" CssClass=" form-control" TextMode="MultiLine"
                                    Width="660px" Height="50px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="label col-md-2">
                                Final Summary : &nbsp;
                            <asp:Label ID="Label17" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtRationalise" runat="server" class="form-control" TextMode="MultiLine"
                                    Width="660px" Height="50px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div style="margin-left: 20%;">
                            <asp:Button ID="bntSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="bntSave_Click" />
                            <asp:Button ID="btnPost" Text="Post To Issue" runat="server" CssClass="btn btn-danger btn-sm cls-btnSave disp-none"
                                OnClick="btnPost_Click" />
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
        <div id="tabscontainer" class="nav-tabs-custom" runat="server">
            <ul class="nav nav-tabs">
                <li id="li1" runat="server" class="active"><a href="#tab-1" data-toggle="tab">Related
                Protocol Deviations</a></li>
                <li id="li2" runat="server"><a href="#tab-2" data-toggle="tab">Site
                <asp:Label runat="server" ID="lblSite"></asp:Label>
                    - Protocol Deviations </a></li>
                <li id="li3" runat="server"><a href="#tab-3" data-toggle="tab">Subject
                <asp:Label runat="server" ID="lblSubjectID"></asp:Label>
                    - Protocol Deviations </a></li>
                <li id="li4" runat="server"><a href="#tab-4" data-toggle="tab">Comments</a></li>
                <li id="li5" runat="server"><a href="#tab-5" data-toggle="tab">Impact</a></li>
                <li id="li6" runat="server"><a href="#tab-6" data-toggle="tab">CAPA</a></li>
                <li id="li7" runat="server"><a href="#tab-7" data-toggle="tab">Audit Trail</a></li>
            </ul>
            <div class="tab">
                <div id="tab-1" class="tab-content current">
                    <asp:GridView ID="grdRelatedPROTOCOL" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="table-bordered table-striped margin-top4 Datatable" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" OnRowDataBound="grdRelatedPROTOCOL_RowDataBound" OnPreRender="GridView_PreRender">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Deviation No." ItemStyle-CssClass="txtCenter">
                                <ItemTemplate>
                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("PROTVOIL_ID") %>' CommandArgument='<%# Eval("PROTVOIL_ID") %>'
                                        CssClass="disp-noneimp" />
                                    <asp:LinkButton ID="lnkID" runat="server" Text='<%# Bind("Refrence") %>' CommandArgument='<%# Eval("PROTVOIL_ID") %>'
                                        OnClientClick="return ProtDevDetails(this);"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duplicacy" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label runat="server" ToolTip="Duplicate" ID="lblDuplicateCheck" Visible="false">
                                        <i class="fa fa-check-square-o" style="color:Black;" ></i></asp:Label>
                                    <asp:Label runat="server" ToolTip="Possibly Duplicate" ID="lblDuplicateCheckPossible"
                                        Visible="false">
                                            <i class="fa fa-check-square-o" style="color:Blue;" ></i></asp:Label>
                                    <asp:Label runat="server" ToolTip="New" ID="lblDuplicateUnCheck" Visible="false"><i class="fa fa-square-o" style="color:Black;" ></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Added details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Update details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Updated By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="tab-2" class="tab-content current">
                    <asp:GridView ID="grdSiteRelatedProtocol" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="table-bordered table-striped margin-top4 Datatable" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" OnRowDataBound="grdRelatedPROTOCOL_RowDataBound" OnPreRender="GridView_PreRender">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Deviation No." ItemStyle-CssClass="txtCenter">
                                <ItemTemplate>
                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("PROTVOIL_ID") %>' CommandArgument='<%# Eval("PROTVOIL_ID") %>'
                                        CssClass="disp-noneimp" />
                                    <asp:LinkButton ID="lnkID" runat="server" Text='<%# Bind("Refrence") %>' CommandArgument='<%# Eval("PROTVOIL_ID") %>'
                                        OnClientClick="return ProtDevDetails(this);"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duplicacy" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label runat="server" ToolTip="Duplicate" ID="lblDuplicateCheck" Visible="false">
                                        <i class="fa fa-check-square-o" style="color:Black;" ></i></asp:Label>
                                    <asp:Label runat="server" ToolTip="Possibly Duplicate" ID="lblDuplicateCheckPossible"
                                        Visible="false">
                                            <i class="fa fa-check-square-o" style="color:Blue;" ></i></asp:Label>
                                    <asp:Label runat="server" ToolTip="New" ID="lblDuplicateUnCheck" Visible="false"><i class="fa fa-square-o" style="color:Black;" ></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Added details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Update details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Updated By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="tab-3" class="tab-content current">
                    <asp:GridView ID="grdSubjectRelatedPROTOCOL" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="table-bordered table-striped margin-top4 Datatable" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" OnRowDataBound="grdRelatedPROTOCOL_RowDataBound" OnPreRender="GridView_PreRender">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Deviation No." ItemStyle-CssClass="txtCenter">
                                <ItemTemplate>
                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("PROTVOIL_ID") %>' CommandArgument='<%# Eval("PROTVOIL_ID") %>'
                                        CssClass="disp-noneimp" />
                                    <asp:LinkButton ID="lnkID" runat="server" Text='<%# Bind("Refrence") %>' CommandArgument='<%# Eval("PROTVOIL_ID") %>'
                                        OnClientClick="return ProtDevDetails(this);"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duplicacy" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label runat="server" ToolTip="Duplicate" ID="lblDuplicateCheck" Visible="false">
                                        <i class="fa fa-check-square-o" style="color:Black;" ></i></asp:Label>
                                    <asp:Label runat="server" ToolTip="Possibly Duplicate" ID="lblDuplicateCheckPossible"
                                        Visible="false">
                                            <i class="fa fa-check-square-o" style="color:Blue;" ></i></asp:Label>
                                    <asp:Label runat="server" ToolTip="New" ID="lblDuplicateUnCheck" Visible="false"><i class="fa fa-square-o" style="color:Black;" ></i></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Added details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Update details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Updated By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="tab-4" class="tab-content current">
                    <div class="form-group" style="display: inline-flex">
                        <label class="label" style="color: Maroon;">
                            Enter Comment:
                        </label>
                        <div class="Control">
                            <asp:TextBox ID="txtComments" runat="server" class="form-control required2" TextMode="MultiLine"
                                Width="700px" Height="50px" Rows="3"></asp:TextBox>
                        </div>
                        <asp:Button ID="bntCommentAdd" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                            Text="Add" OnClick="bntCommentAdd_Click" />
                        <asp:Button ID="btnUpdatedComment" runat="server" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave2"
                            Text="Update" OnClick="btnUpdatedComment_Click" />
                    </div>
                    <asp:GridView ID="grdCmts" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="Gtable table-bordered table-striped margin-top4 Datatable" AlternatingRowStyle-CssClass="alt" OnPreRender="GridView_PreRender"
                        PagerStyle-CssClass="pgr" OnRowCommand="grdCmts_RowCommand">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" CommandName="EditComment" CommandArgument='<%# Bind("PDCOM_ID") %>'
                                        runat="server"><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PDCOM_ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="PDCOM_ID" runat="server" Text='<%# Bind("PDCOM_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comments">
                                <ItemTemplate>
                                    <asp:Label ID="Comment" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Added details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Update details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Updated By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
                <div id="tab-5" class="tab-content">
                    <div runat="server" id="divImpactAdd" class="form-group" style="display: inline-flex">
                        <label class="label" style="color: Maroon;">
                            Enter Impact:
                        </label>
                        <div class="Control">
                            <asp:TextBox ID="txtImpact" runat="server" class="form-control drpControl required3 width200px" Rows="3" TextMode="MultiLine" Width="200">
                            </asp:TextBox>
                        </div>
                        <asp:Button ID="bntImpactAdd" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave3"
                            Text="Add" OnClick="bntImpactAdd_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="bntImpactUpdate" runat="server" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave3"
                            Text="Update" OnClick="bntImpactUpdate_Click" />
                    </div>
                    <asp:GridView ID="grdImpact" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped Datatable"
                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowCommand="grdImpact_RowCommand" OnPreRender="GridView_PreRender"
                        Width="100%">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="PD_Imapct_ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="PD_Imapct_ID" runat="server" Text='<%# Bind("PD_Imapct_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deviation No.">
                                <ItemTemplate>
                                    <asp:Label ID="PROTVOIL_ID" runat="server" Text='<%# Bind("PROTVOIL_ID") %>' Width="60px"
                                        Style="text-align: center"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Impact" ItemStyle-CssClass="width250px">
                                <ItemTemplate>
                                    <asp:Label ID="lblImpact" runat="server" Text='<%# Bind("Impact") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Added details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Update details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Updated By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
                <div id="tab-6" class="tab-content">
                    <div runat="server" id="divCAPAAdd">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Enter CAPA : &nbsp;
                                <asp:Label ID="Label19" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCAPA" runat="server" class="form-control required4" TextMode="MultiLine" Rows="3"
                                        Width="700px" Height="50px"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="label col-md-2">
                                    Select Responsibility: &nbsp;
                                <asp:Label ID="Label20" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlResponsibility" runat="server" class="form-control drpControl required4 width200px"
                                        Width="200">
                                    </asp:DropDownList>
                                </div>
                                <div class="label col-md-2">
                                    Enter Resolution Date:&nbsp;
                                <asp:Label ID="Label451" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtResDate" runat="server" class="form-control required4 txtDate"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div style="margin-left: 20%;">
                                <asp:Button ID="bntCAPAAdd" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave4"
                                    Text="Add" OnClick="bntCAPAAdd_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="bntCAPAUpdate" runat="server" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave4"
                                    Text="Update" OnClick="bntCAPAUpdate_Click" />
                            </div>
                        </div>
                        <br />
                    </div>
                    <asp:GridView ID="grdCAPA" runat="server" Width="100%" AutoGenerateColumns="false"
                        ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-striped Datatable txtCenter"
                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowCommand="grdCAPA_RowCommand" OnPreRender="GridView_PreRender">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="PDCAPA_ID" Visible="false" ItemStyle-CssClass="txt_center width80px">
                                <ItemTemplate>
                                    <asp:Label ID="PDCAPA_ID" runat="server" Text='<%# Bind("PDCAPA_ID") %>' ReadOnly="true"
                                        Width="60px" Style="text-align: center"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deviation No.">
                                <ItemTemplate>
                                    <asp:Label ID="PROTVOIL_ID" runat="server" Text='<%# Bind("PROTVOIL_ID") %>' ReadOnly="true"
                                        Width="60px" Style="text-align: center"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CAPA" ItemStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label ID="CAPA" runat="server" Text='<%# Bind("CAPA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Responsibility">
                                <ItemTemplate>
                                    <asp:Label ID="Responsibility" runat="server" Text='<%# Bind("Responsibility") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Resolution Date" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="Resolution_DT" runat="server" Text='<%# Bind("Resolution_DT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Added details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Added By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="align-left">
                                <HeaderTemplate>
                                    <label>Update details</label><br />
                                    <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Updated By]</label><br />
                                    <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                    <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div>
                                        <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" CommandName="EditCAPA" CommandArgument='<%# Bind("PDCAPA_ID") %>'
                                        runat="server"><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
                <div id="tab-7" class="tab-content current">
                    <asp:GridView ID="grdAuditTrail" runat="server" AutoGenerateColumns="true"
                        CssClass="table table-bordered table-striped Datatable txt_center" OnPreRender="GridView_PreRender">
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div id="popup_POST_PROTOCOL_TO_ISSUE" runat="server" title="Post To Issue" class="disp-none">
            <div id="Div6" class="formControl">
                <asp:Label ID="Label3" runat="server" CssClass="txt_center" Text="Do you want to Post Protocol Deviation as Issue?"></asp:Label>
            </div>
            <div style="margin-top: 10px">
                <asp:Button ID="btnYESPost" runat="server" CssClass="cls-btnSave1" Style="margin-left: 10px;"
                    Text="YES" OnClientClick="YESPOSTRISK_Prot();" />
                <asp:Button ID="btnNoPost" runat="server" Style="margin-left: 10px;" Text="NO" OnClientClick="POSTNORISK_Prot();" />
            </div>
        </div>
    </form>
</body>
</html>
