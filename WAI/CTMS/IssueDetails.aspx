<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IssueDetails.aspx.cs" Inherits="CTMS.IssueDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
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

        function Validatedrp() {
            var pdcode1 = document.getElementById('<%=drp_PDCode1.ClientID%>');
            var pdcode2 = document.getElementById('<%=drp_PDCode2.ClientID%>');
            var nature = $("#<%=drp_Nature.ClientID%>").val();
            var pd1val = $("#<%=drp_PDCode1.ClientID%>").val();
            var pd2val = $("#<%=drp_PDCode1.ClientID%>").val();

            if (nature == "Protocol Deviation" || nature == "Process Deviation") {

                if (pd1val == "0") {
                    $(pdcode1).addClass("brd-1px-redimp");
                    test = "1";
                }
                else {
                    $(pdcode1).removeClass("brd-1px-redimp");
                    test = "0";
                }

                if (pd2val == "0") {
                    $(pdcode2).addClass("brd-1px-redimp");
                    test = "1";
                }
                else {
                    $(pdcode2).removeClass("brd-1px-redimp");
                    test = "0";
                }
            }
            else {
                $(pdcode1).removeClass("brd-1px-redimp");
                $(pdcode2).removeClass("brd-1px-redimp");
                test = "0"
            }


            if (test == "1") {
                return false;
            }
            return true;
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



        $(document).on("click", ".cls-addrequired", function () {
            var test = "0";

            $('.subcause').each(function (index, element) {
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

        function pageLoad() {
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

        function Issuedetails(element) {
            var IssueID = $(element).prev().attr('commandargument');
            var test = "IssueDetails.aspx?IssueID=" + IssueID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=950";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function POST_TO_RISK() {

            $('#<%=popup_POST_TO_RISK.ClientID%>').dialog({
                title: "Post To Risk",
                width: 600,
                height: "auto",
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });
            $('#<%=popup_POST_TO_RISK.ClientID%>').dialog("open");
            return false;
        }

        function YESPOSTRISK(element) {
            var CategoryId = $("#drp_PDCode1 option:selected").text();
            var SubcategoryId = $("#drp_PDCode2 option:selected").text();
            var TopicId = $("#drp_Factor option:selected").text();
            var Risk_Description = $("#txtDesc").val();
            var RiskNature = $("#drp_Nature").val();
            var Risk_Count = $("#txtRiskCount").val();
            var Risk_Manager = '<%= Session["User_ID"] %>';
            var Dept = $("#txtDept").val();
            var Source = $("#txtSource").val();
            var Reference = $("#txtReference").val();
            var RStatus = $("#drp_Status").val();
            var ROwner = $("#Drp_Assginedto").val();
            var IssueID = $("#hdnISSUEID").val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/POST_TO_RISK",
                data: '{CategoryId: "' + CategoryId + '",SubcategoryId: "' + SubcategoryId + '",TopicId: "' + TopicId + '",Risk_Description: "' + Risk_Description + '",RiskNature: "' + RiskNature + '",Risk_Count: "' + Risk_Count + '",Risk_Manager: "' + Risk_Manager + '",Dept: "' + Dept + '",IssueID: "' + IssueID + '",Source: "' + Source + '",Reference: "' + Reference + '",RStatus: "' + RStatus + '",ROwner: "' + ROwner + '",PROTOCOLID: "' + '' + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $("#popup_POST_TO_RISK").dialog('close');
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

        function POSTNORISK(element) {
            var CategoryId = $("#drp_PDCode1 option:selected").text();
            var SubcategoryId = $("#drp_PDCode2 option:selected").text();
            var TopicId = $("#drp_Factor option:selected").text();
            var Risk_Description = $("#txtDesc").val();
            var RiskNature = $("#drp_Nature").val();
            var Risk_Count = $("#txtRiskCount").val();
            var Risk_Manager = '<%= Session["User_ID"] %>';
            var Dept = $("#txtDept").val();
            var Source = $("#txtSource").val();
            var Reference = $("#txtReference").val();
            var RStatus = $("#drp_Status").val();
            var ROwner = $("#Drp_Assginedto").val();
            var IssueID = $("#hdnISSUEID").val();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/INSERT_RISK",
                data: '{CategoryId: "' + CategoryId + '",SubcategoryId: "' + SubcategoryId + '",TopicId: "' + TopicId + '",Risk_Description: "' + Risk_Description + '",RiskNature: "' + RiskNature + '",Risk_Count: "' + Risk_Count + '",Risk_Manager: "' + Risk_Manager + '",Dept: "' + Dept + '",IssueID: "' + IssueID + '",Source: "' + Source + '",Reference: "' + Reference + '",RStatus: "' + RStatus + '",ROwner: "' + ROwner + '",PROTOCOLID: "' + '' + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $("#popup_POST_TO_RISK").dialog('close');
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
    <style>
        .txt_center
        {
            text-align: center;
        }
        .bold
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                ISSUE DETAILS</h3>
            <asp:HiddenField ID="hdnISSUEID" runat="server" />
        </div>
        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server" Style="color: Red"></asp:Label>
        </div>
        <br />
        <asp:HiddenField ID="hdnID" runat="server" />
        <table class="margin-left10" style="margin-top: -18px; padding: 50px">
            <tr>
                <td class="label">
                    Summary
                </td>
                <td colspan="5">
                    <asp:TextBox runat="server" ID="txtSummary" Width="138%" CssClass="form-control required"
                        Height="40px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label txt_center margin-top6">
                    Site ID
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSiteId" CssClass="form-control  txt_center margin-top4"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td class="label txt_center margin-top6">
                    Subject ID
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSubjId" CssClass="form-control txt_center margin-top4"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td class="label txt_center margin-top6">
                    Project
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtProj" CssClass="form-control txt_center required margin-top4"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td class="label txt_center margin-top6">
                    Department
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDept" CssClass="form-control required margin-top4"
                        ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label txt_center margin-top6">
                    Nature
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drp_Nature" CssClass="form-control required margin-top4">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="Requiredfieldvalidator3"
                                ControlToValidate="drp_Nature" Font-Bold="true" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label txt_center margin-top6">
                    Category
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drp_PDCode1" CssClass="form-control margin-top4"
                                AutoPostBack="True" OnSelectedIndexChanged="drp_PDCode1_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ForeColor="Red" runat="server" ID="rfvdrp_pdcode1" ControlToValidate="drp_PDCode1"
                                Font-Bold="true" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label txt_center margin-top6">
                    Sub Category
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drp_PDCode2" CssClass="form-control margin-top4"
                                AutoPostBack="True" OnSelectedIndexChanged="drp_PDCode2_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="Requiredfieldvalidator1"
                                ControlToValidate="drp_PDCode2" Font-Bold="true" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label txt_center margin-top6">
                    Factor
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drp_Factor" CssClass="form-control margin-top4"
                                AutoPostBack="True" OnSelectedIndexChanged="drp_Factor_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ForeColor="Red" ID="Requiredfieldvalidator2"
                                ControlToValidate="drp_Factor" Font-Bold="true" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <%--<td class="label txt_center margin-top6">
                    Probability
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drpProbability" onchange="CalculateRPN();" Style="color: Blue"
                                CssClass="form-control txtCenter required  margin-top4">
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label txt_center margin-top6">
                    Severity
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drpSeverity" onchange="CalculateRPN();" Style="color: Blue"
                                CssClass="form-control txtCenter required  margin-top4">
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label txt_center margin-top6">
                    Detectability
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="drpDetectability" onchange="CalculateRPN();"
                                CssClass="form-control txtCenter required  margin-top4" Style="color: Blue">
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>--%>
                <td class="label txt_center margin-top6">
                    Source
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSource" CssClass="form-control required margin-top4"></asp:TextBox>
                </td>
                <td class="label txt_center">
                    Reference
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtReference" CssClass="form-control margin-top4"></asp:TextBox>
                </td>
                <td class="label txt_center margin-top6">
                    Classification
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drp_Priority" CssClass="form-control required margin-top4">
                    </asp:DropDownList>
                </td>
                <td class="label txt_center margin-top6">
                    Issue Count
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:TextBox runat="server" ID="txtRiskCount" CssClass="form-control txtCenter  margin-top4"
                                Text="0" Style="color: Red"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtRiskID" CssClass="form-control txtCenter  margin-top4"
                                Visible="false" Text="0" Style="color: Red"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="label txt_center margin-top6">
                    Assigned To
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList runat="server" ID="Drp_Assginedto" CssClass="form-control margin-top4"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="label txt_center margin-top6">
                    Resolution
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtResolution" CssClass="form-control txtDate txt_center margin-top4"></asp:TextBox>
                </td>
                <td class="label txt_center margin-top6">
                    Date Closed
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDateClose" CssClass="form-control txtDate txt_center margin-top4"></asp:TextBox>
                </td>
                <td class="label txt_center margin-top6">
                    Due Date
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDueDate" CssClass="form-control  txtDate txt_center required margin-top4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label txt_center margin-top6">
                    Opened Date
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtOpenDat" CssClass="form-control txt_center margin-top4"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td class="label txt_center margin-top6">
                    Opened By
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtOpenBy" CssClass="form-control txt_center margin-top4"
                        ReadOnly="true"></asp:TextBox>
                </td>
                <td class="label txt_center margin-top6">
                    Status
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drp_Status" CssClass="form-control margin-top4">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label txt_center margin-top6">
                    Description
                </td>
                <td colspan="5">
                    <asp:TextBox runat="server" ID="txtDesc" Width="138%" CssClass="form-control margin-top4"
                        Height="40px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    <asp:Button ID="btnPost" Text="Post Risk" runat="server" CssClass="btn btn-danger btn-sm "
                        Style="margin-right: 730px" OnClick="btnPost_Click" />
                    <asp:Button ID="btnSave" Text="Save & Close" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                        OnClick="btnSave_Click" />
                </h3>
            </div>
        </div>
    </div>
    <div class="col-md-8">
        <div id="tabscontainer" class="nav-tabs-custom" runat="server">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab-1" data-toggle="tab">Related Issues</a></li>
                <li><a href="#tab-3" data-toggle="tab">Root Cause</a></li>
                <li><a href="#tab-4" data-toggle="tab">Actionable</a></li>
                <li><a href="#tab-5" data-toggle="tab">Impact</a></li>
                <li><a href="#tab-6" data-toggle="tab">Attachments</a></li>
                <li><a href="#tab-2" data-toggle="tab">Comments</a></li>
            </ul>
            <div class="tab">
                <div id="tab-1" class="tab-content current">
                    <asp:GridView ID="grdRelatedIssues" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="ID" ItemStyle-CssClass="txt_center width20px">
                                <ItemTemplate>
                                    <asp:Label ID="ID" runat="server" Text='<%# Bind("ISSUES_ID") %>' CommandArgument='<%# Eval("ISSUES_ID") %>'
                                        CssClass="disp-noneimp" />
                                    <asp:LinkButton ID="lnkID" runat="server" Text='<%# Bind("ISSUES_ID") %>' CommandArgument='<%# Eval("ISSUES_ID") %>'
                                        OnClientClick="return Issuedetails(this);"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="txt_center width20px" HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Summary" ItemStyle-CssClass="width20px">
                                <ItemTemplate>
                                    <asp:Label ID="Summary" runat="server" Text='<%# Bind("Summary") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="tab-2" class="tab-content">
                    <asp:GridView ID="grdCmts" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr" OnRowDataBound="grdCmts_RowDataBound">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Comments" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Comment" runat="server" Text='<%# Bind("Comment") %>' CssClass="form-control"
                                        TextMode="MultiLine" Width="100%" Height="40px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EnteredDate" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="DTENTERED" Text='<%# Bind("DTENTERED") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EnteredBy" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="ENTEREDBY" Text='<%# Bind("ENTEREDBY") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UPDATE_FLAG_cmt" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="UPDATE_FLAG_cmt" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_cmt") %>'
                                        Width="22px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="30px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Button ID="bntCommentAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                        OnClick="bntCommentAdd_Click" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
                <div id="tab-3" class="tab-content">
                    <table>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label1" runat="server"> Root Cause</asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpRootCause" runat="server" Width="200px" CssClass="form-control subcause"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpRootCause_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="label">
                                <asp:Label ID="Label2" runat="server"> Sub Cause</asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="drpSubRootCause" runat="server" Width="200px" CssClass="form-control subcause">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="Add" runat="server" CssClass="btn btn-primary btn-sm   cls-addrequired"
                                    Text="Add" OnClick="bntRootCauseAdd_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="grdRootCause" runat="server" AutoGenerateColumns="false" CssClass="Gtable table-bordered table-striped margin-top4"
                        AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Issue ID" ItemStyle-CssClass="txt_center width60px">
                                <ItemTemplate>
                                    <asp:TextBox ID="ISSUES_ID" runat="server" Text='<%# Bind("ISSUES_ID") %>' ReadOnly="true"
                                        Width="50px" Style="text-align: center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Root Cause" ItemStyle-CssClass="txt_center width250px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="RootCause" Text='<%# Bind("RootCause") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub Root Cause" ItemStyle-CssClass="txt_center width250px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="Sub_Cause" Text='<%# Bind("Sub_Cause") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="tab-4" class="tab-content">
                    <asp:GridView ID="grdActionable" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Actionable" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="Actionable" runat="server" Text='<%# Bind("Actionable") %>' CssClass="form-control" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ActionBy" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="ActionBy" runat="server" Text='<%# Bind("ActionBy") %>' CssClass="form-control" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DueDate" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="DueDate" runat="server" Text='<%# Bind("DueDate") %>' CssClass="form-control txtDate" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DateCompleted" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="DateCompleted" runat="server" Text='<%# Bind("DateCompleted") %>'
                                        CssClass="form-control txtDate" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UPDATE_FLAG_Action" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="UPDATE_FLAG_Action" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_Action") %>'
                                        Width="22px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="30px" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:Button ID="btnAction" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                        OnClick="bntActionAdd_Click" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
                <div id="tab-5" class="tab-content">
                    <asp:GridView ID="grdImpact" runat="server" AutoGenerateColumns="false" CssClass="Gtable table-bordered table-striped margin-top4"
                        AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdImpact_RowDataBound" PagerStyle-CssClass="pgr">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Issue ID" ItemStyle-CssClass="txt_center width80px">
                                <ItemTemplate>
                                    <asp:TextBox ID="ISSUES_ID" runat="server" Text='<%# Bind("ISSUES_ID") %>' ReadOnly="true"
                                        Width="60px" Style="text-align: center"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Impact" ItemStyle-CssClass="txt_center width250px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="Impact" runat="server" Width="250px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UPDATE_FLAG_Impact" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="UPDATE_FLAG_Impact" runat="server" Font-Size="X-Small" Text='<%# Bind("UPDATE_FLAG_Impact") %>'
                                        Width="22px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="width30px" ItemStyle-CssClass="txt_center">
                                <HeaderTemplate>
                                    <asp:Button ID="Add" runat="server" CssClass="btn btn-primary btn-sm" Text="Add"
                                        OnClick="bntImpactAdd_Click" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
                <div id="tab-6" class="tab-content">
                    <asp:GridView ID="grdAttachment" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="Gtable table-bordered table-striped margin-top4" AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr">
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Attachments" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Name" runat="server" Text='<%# Bind("Name") %> ' CommandArgument='<%# Eval("ISSUES_ID") %> '
                                        OnClick="DownloadFile"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UPDATE_FLAG_Attach" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:TextBox ID="UPDATE_FLAG_cmt" runat="server" Font-Size="X-Small" Width="22px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div id="popup_POST_TO_RISK" runat="server" title="Post To Risk" class="disp-none">
        <div id="Div6" class="formControl">
            <asp:Label ID="Label3" runat="server" CssClass="txt_center" Text="Do you want to Post Related Issues as Risk Events?"></asp:Label>
        </div>
        <div style="margin-top: 10px">
            <asp:Button ID="btnYESPost" runat="server" CssClass="cls-btnSave1" Style="margin-left: 10px;"
                Text="YES" OnClientClick="YESPOSTRISK(this);" />
            <asp:Button ID="btnNoPost" runat="server" Style="margin-left: 10px;" Text="NO" OnClientClick="POSTNORISK();" />
        </div>
    </div>
    </form>
</body>
</html>
