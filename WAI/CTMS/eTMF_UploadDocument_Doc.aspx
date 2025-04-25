<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eTMF_UploadDocument_Doc.aspx.cs"
    Inherits="CTMS.eTMF_UploadDocument_Doc" %>

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
    <script src="js/plugins/moment/moment.min.js" type="text/javascript"></script>
    <script src="js/plugins/moment/datetime-moment.js" type="text/javascript"></script>
    <link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
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
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_OpenDoc.js"></script>

    <style type="text/css">
        .select2-selection select2-selection--single {
            height: 20px;
        }
    </style>
    <script>
        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();

        }



        $(function () {

            $('.select2').select2()
        });


        $(document).on("click", ".cls-btnSave", function () {
            var test = "0";

            $('.required').each(function (index, element) {
                var value = $(this).val();
                var ctrl = $(this).prop('type');

                if (ctrl == "select-one") {
                    if (value == "0" || value == "--Select--" || value == null) {
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
    </script>
    <style>
        .label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: -2px;
            font-weight: bold;
            font-size: 13px;
            margin-left: 0px;
        }
    </style>
    <script>

        $(document).ready(function () {

            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });

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

        function DOC_TRACKING(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_DOCUMENT_TRACKING.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=600";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function VERSION_HISTORY(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_VERSION_HISTORY.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ConfirmMsg() {
            var newLine = "\r\n"

            var error_msg = "This File is already exists";

            error_msg += newLine;
            error_msg += newLine;

            error_msg += "Note : Press OK to Proceed.";

            if (confirm(error_msg)) {

                $("#btnUploadAgainDoc").click();

                return true;

            } else {

                return false;
            }

        }

        function SetEmailYes(element) {

            $('#lbtnSendEmailYes').addClass("disp-none");
            $('#lbtnSendEmailNO').removeClass("disp-none");
            $('#divEmail').removeClass("disp-none");

            $('#txtToEmailIds').val("");
            $('#txtCcEmailIds').val("");

            return false;
        }

        function SetEmailNo(element) {

            $('#lbtnSendEmailYes').removeClass("disp-none");
            $('#lbtnSendEmailNO').addClass("disp-none");
            $('#divEmail').addClass("disp-none");

            $('#txtToEmailIds').val("");
            $('#txtCcEmailIds').val("");
            return false;
        }

        function ShowReminderDiv(element) {

            if ($('#divreminderdat').hasClass("disp-none")) {

                $('#divreminderdat').removeClass("disp-none");

            }
            else {

                $('#divreminderdat').addClass("disp-none");

            }

            $('#MainContent_txtReminderDat').val("");

            return false;
        }

        function bindSpecs() {
            var avaTag = $('#hfSPECS').val().split(',');

            $('#txtSPEC').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        }

        $(function () {
            var avaTag = $('#hfSPECS').val().split(',');

            $('#txtSPEC').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        });

        function bindIndividual() {
            var avaTag = $('#hfIndividual').val().split(',');

            $('#txtIndividual').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        }

        $(function () {
            var avaTag = $('#hfIndividual').val().split(',');

            $('#txtIndividual').autocomplete({
                source: avaTag, minLength: 0
            }).on('focus', function () { $(this).keydown(); });
        });

    </script>

</head>
<body>
    <form id="form1" method="post" class="content" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Upload Document</h3>
            </div>
            <div class="row">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField runat="server" ID="hfVerDATE" />
                    <asp:HiddenField runat="server" ID="hfVerSPEC" />
                    <asp:HiddenField runat="server" ID="hfSPECS" />
                    <asp:HiddenField runat="server" ID="hfVerTYPE" />
                    <asp:HiddenField runat="server" ID="hfIndividual" />
                    <asp:HiddenField runat="server" ID="hfOwnerEmailId" />
                    <asp:HiddenField runat="server" ID="hfDocTypeId" />
                    <asp:HiddenField runat="server" ID="hfZoneId" />
                    <asp:HiddenField runat="server" ID="hfSectionId" />
                    <asp:HiddenField runat="server" ID="hfArtifactId" />
                </div>
            </div>
            <div class="row" id="div1" runat="server" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div id="div3" runat="server">
                        <div class="label col-md-2">
                            Document Name :
                        </div>
                        <div class="col-md-8">
                            <asp:Label runat="server" ID="lblDocumentName" CssClass="form-control" Style="width: auto; min-width: 300px;">
                            </asp:Label>
                            <asp:HiddenField runat="server" ID="hfDocumentID" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-horizontal" id="divUploaded" visible="false" runat="server">
                <br />
                <div class="col-md-12">
                    <div id="div6" runat="server">
                        <div class="label col-md-2">
                            &nbsp;
                        </div>
                        <div class="col-md-8">
                            <a href="#gvFiles">
                                <asp:Label runat="server" ID="lblUploaded"></asp:Label>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="divInstruction" runat="server" style="padding-top: 15px;">
                <br />
                <div class="col-md-12">
                    <div id="div9" runat="server">
                        <div class="label col-md-2">
                            Instructions :
                        </div>
                        <div class="col-md-8">
                            <asp:Label runat="server" ID="lblInstruction" TextMode="MultiLine" Width="721px"
                                Font-Bold="true" Style="height: auto; max-height: 100px; color: Blue;">
                            </asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div id="divCountry" runat="server" visible="false">
                        <div class="label col-md-2">
                            Select Country : &nbsp;
                        <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="drpCountry" AutoPostBack="true" CssClass="form-control width250px"
                                OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="divINVID" runat="server" visible="false">
                        <div class="label col-md-2">
                            Site ID : &nbsp;
                        <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control width250px"
                                AutoPostBack="true" OnSelectedIndexChanged="drpSites_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div id="divIndividual" runat="server" visible="false">
                        <div class="label col-md-2">
                            Individual : &nbsp;
                        <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtIndividual" CssClass="form-control required width250px"> </asp:TextBox>
                        </div>
                    </div>
                    <div id="divSubject" runat="server" visible="false">
                        <div class="label col-md-2">
                            Subject : &nbsp;
                        <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtSubject" CssClass="form-control required width250px">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div id="divSPEC" runat="server" visible="false">
                        <div class="label col-md-2">
                            <asp:Label runat="server" ID="lblSPECtitle"></asp:Label>&nbsp; : &nbsp;
                        <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtSPEC" CssClass="form-control required width250px">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="div5" runat="server" style="padding-top: 30px;">
                <div class="col-md-12">
                    <div id="divstatus" runat="server">
                        <div class="label col-md-2">
                            Select Status : &nbsp;
                        <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="drpAction" AutoPostBack="true" CssClass="form-control required width250px"
                                OnSelectedIndexChanged="drpAction_SelectedIndexChanged">
                                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                <%--<asp:ListItem Text="Draft" Value="Draft"></asp:ListItem>--%>
                                <asp:ListItem Text="Current" Value="Current"></asp:ListItem>
                                <asp:ListItem Text="Superseded" Value="Superseded"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divDefaultView" runat="server">
                <div class="row" id="div7" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div id="divStatus2" visible="false" runat="server">
                            <div class="label col-md-2">
                                Select Sub-Status :&nbsp;
                            <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddlFinalStatus" CssClass="form-control width250px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlFinalStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                    <asp:ListItem Text="Collaborate" Value="Collaborate"></asp:ListItem>
                                    <asp:ListItem Text="Review" Value="Review"></asp:ListItem>
                                    <asp:ListItem Text="QA Review" Value="QA Review"></asp:ListItem>
                                    <asp:ListItem Text="Internal Approval" Value="Internal Approval"></asp:ListItem>
                                    <asp:ListItem Text="Sponsor Approval" Value="Sponsor Approval"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divStatus3" visible="false" runat="server">
                            <div class="label col-md-2">
                                Select Action : &nbsp;
                            <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddlStatus3" CssClass="form-control width250px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlStatus3_SelectedIndexChanged">
                                    <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Current" Value="Current"></asp:ListItem>
                                    <asp:ListItem Text="Replace" Value="Replace"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div id="divUsers" visible="false" runat="server">
                            <div class="label col-md-2">
                                Select Users : &nbsp;
                            <asp:Label ID="Label12" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:GridView ID="grd_Users" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped table-striped1 width250px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="User_ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="User_ID" Text='<%# Eval("User_ID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMAILID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="EMAILID" Text='<%# Eval("EMAILID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="User_Name" Text='<%# Eval("User_Name") %>' ToolTip='<%# Eval("User_Name") %>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div id="divDeadline" visible="false" runat="server">
                            <div class="label col-md-2">
                                Enter Deadline Date (if Applicable) : &nbsp;
                            <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtDeadline" CssClass="form-control txtDate width250px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="divFileReplace" visible="false" runat="server" style="padding-top: 15px;">
                    <div class="col-md-6">
                        <div class="label col-md-2">
                            Select File : &nbsp;
                        <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:GridView ID="gvFilesReplace" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped table-striped1">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                        <ItemTemplate>
                                            <asp:Label ID="ID" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="UploadFileName" Text='<%# Eval("UploadFileName") %>' ToolTip='<%# Eval("UploadFileName") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Version No.">
                                        <ItemTemplate>
                                            <asp:Label ID="DOC_VERSIONNO" Text='<%# Eval("DOC_VERSIONNO") %>' ToolTip='<%# Eval("DOC_VERSIONNO") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Document Date">
                                        <ItemTemplate>
                                            <asp:Label ID="DOC_DATETIME" Text='<%# Eval("DOC_DATETIME") %>' ToolTip='<%# Eval("DOC_DATETIME") %>'
                                                runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Note">
                                        <ItemTemplate>
                                            <asp:Label ID="NOTE" Text='<%# Eval("NOTE") %>' ToolTip='<%# Eval("NOTE") %>' runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row" id="div8" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Document Version Number: &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDovVersionNo" CssClass="form-control width250px"></asp:TextBox>
                        </div>
                        <div class="label col-md-2">
                            <asp:Label runat="server" ID="lblDateTitle" Text="Document Date"></asp:Label>
                            &nbsp; : &nbsp;
                        <asp:Label ID="lblRequiredDocDate" runat="server" Font-Size="Small" ForeColor="#FF3300"
                            Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDocDateTime" CssClass="form-control txtDateNoFuture width250px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" id="div13" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Note : &nbsp;
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" MaxLength="200" CssClass="form-control"
                                Height="50" Width="721"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" id="div15" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Received Date : &nbsp;
                                <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtReceiptdate" CssClass="form-control txtDateNoFuture width250px required"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" id="div11" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Enter Expiry Date (if Applicable) : &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtExpiryDate" CssClass="form-control txtDate width250px"
                                onchange="return ShowReminderDiv(this);"></asp:TextBox>
                        </div>
                        <asp:HiddenField ID="hdnEmailEnable" runat="server" />
                    </div>
                </div>
                <div class="row" id="div2" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select File : &nbsp;
                        <asp:Label ID="Label16" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="width250px" Font-Size="X-Small" />
                        </div>
                        <div class="label col-md-2" style="display: none">
                            Select File : &nbsp;
                        <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3" style="display: none">
                            <asp:FileUpload ID="FileUpload2" runat="server" CssClass="width250px" Font-Size="X-Small" />
                        </div>
                    </div>
                </div>
                <div class="row" id="div4" visible="false" runat="server" style="padding-top: 15px; display: none;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Send Email : &nbsp;
                                <asp:Label ID="Label17" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton ID="lbtnSendEmailYes" OnClientClick="return SetEmailYes(this);" runat="server">
                                    <i title="Mark as Yes" class="icon-check-empty" style="color: #333333;" aria-hidden="true"></i>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtnSendEmailNO" OnClientClick="return SetEmailNo(this);" runat="server"
                                CssClass="disp-none">
                                    <i title="Mark as No" class="icon-check" style="color: #333333;" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row disp-none" id="divEmail" runat="server" style="padding-top: 15px;">
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            To Email IDs (Use ',' in case of multiple) : &nbsp;
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtToEmailIds" TextMode="MultiLine" CssClass="form-control width250px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToEmailIds"
                                ErrorMessage="Please enter valid email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="label col-md-2">
                            Cc Email IDs (Use ',' in case of multiple) : &nbsp;
                                <asp:Label ID="Label18" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>

                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtCcEmailIds" TextMode="MultiLine" CssClass="form-control width250px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCcEmailIds"
                                ErrorMessage="Please enter valid email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <br />
                <div class="form-horizontal">
                    <div class="box-body">
                        <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                            <label class="col-lg-6 width150px label">
                                &nbsp;</label>
                            <div class="col-lg-3">
                                &nbsp;
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="btnUpload_Click" />
                                <asp:Button ID="btnUploadAgainDoc" runat="server" CssClass="disp-none" OnClick="btnUploadAgainDoc_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
        <div class="box box-warning" runat="server" id="divUpoadedDocs">
            <div class="box-header">
                <h3 class="box-title">Uploaded Document</h3>
            </div>
            <asp:GridView ID="gvFilesUploaded" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                EmptyDataText="No Record Available." CssClass="table table-bordered table-striped layerFiles Datatable"
                OnPreRender="grd_data_PreRender" OnRowDataBound="gvFilesUploaded_RowDataBound"
                OnRowCommand="gvFilesUploaded_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                        ItemStyle-CssClass="disp-none" HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                        ItemStyle-CssClass="disp-none" HeaderText="SysFileName">
                        <ItemTemplate>
                            <asp:Label ID="SysFileName" runat="server" Text='<%# Bind("SysFileName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Country">
                        <ItemTemplate>
                            <asp:Label ID="Country" Width="100%" ToolTip='<%# Bind("Country") %>' CssClass="label"
                                runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SIte Id">
                        <ItemTemplate>
                            <asp:Label ID="SiteID" Width="100%" ToolTip='<%# Bind("SiteID") %>' CssClass="label"
                                runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Spec.">
                        <ItemTemplate>
                            <asp:Label ID="Spec" Width="100%" ToolTip='<%# Bind("SPEC_CONCAT") %>' CssClass="label"
                                runat="server" Text='<%# Bind("SPEC_CONCAT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                        ItemStyle-CssClass="disp-none">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" Width="100%" ToolTip='<%# Bind("AutoNomenclature") %>' CssClass="label"
                                runat="server" Text='<%# Bind("AutoNomenclature") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                CssClass="label" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                            <asp:LinkButton ID="lbtn_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                runat="server" CssClass="label" OnClientClick="return OpenDoc(this);" Text='<%# Bind("UploadFileName") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Type" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="lbtnFileType" runat="server" Font-Size="Larger"><i id="ICONCLASS"
                                runat="server" class="fas fa-file-text"></i></asp:Label>
                            <asp:Label ID="lbl_FileSize" Width="100%" CssClass="label" Font-Size="X-Small" runat="server" Text='<%# Bind("CAL_Size") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="System Version">
                        <ItemTemplate>
                            <asp:Label ID="SysVERSION" Width="100%" ToolTip='<%# Bind("SysVERSION") %>' CssClass="label"
                                runat="server" Text='<%# Bind("SysVERSION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lbtnStatus" CssClass="label" Text='<%# Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="DocVERSION" Width="100%" ToolTip='<%# Bind("DocVERSION") %>'
                                CssClass="label" runat="server" Text='<%# Bind("DocVERSION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Document Date" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="DocDATE" Width="100%" ToolTip='<%# Bind("DocDATE") %>' CssClass="label"
                                runat="server" Text='<%# Bind("DocDATE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Note" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="NOTE" Width="100%" ToolTip='<%# Bind("NOTE") %>' CssClass="label"
                                runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Received Date" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="ReceivedDate" Width="100%" ToolTip='<%# Bind("RECEIPTDAT") %>' CssClass="label"
                                runat="server" Text='<%# Bind("RECEIPTDAT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <label>Uploading Details</label><br />
                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Uploaded By]</label><br />
                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div>
                                <div>
                                    <asp:Label ID="UPLOADBYNAME" runat="server" Text='<%# Bind("UPLOADBYNAME") %>' ForeColor="Blue"></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="UPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="UPLOAD_CAL_TZDAT" runat="server" Text='<%# Eval("UPLOAD_CAL_TZDAT") +" "+ Eval("UPLOAD_TZVAL") %>' ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDocumentHistory" runat="server" ToolTip="Document History"
                                OnClientClick="return DOCUMENT_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                                <i id="iconDochistory" runat="server" class="fa fa-history" style="color: #333333;"
                                    aria-hidden="true"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnversionHistory" runat="server" ToolTip="Version History"
                                OnClientClick="return VERSION_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                                <i id="iconhistory" runat="server" class="fa fa-files-o" style="color: #333333;"
                                    aria-hidden="true"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDownloadDoc" runat="server" ToolTip="Download" CommandName="Download" CssClass="btn"
                                CommandArgument='<%# Eval("ID") %>'><i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:Label ID="lblQC" runat="server" ToolTip="QC Document" CommandArgument='<%# Eval("ID") %>'
                                Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconQC" runat="server"
                                    class="fa fa-check"></i></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
