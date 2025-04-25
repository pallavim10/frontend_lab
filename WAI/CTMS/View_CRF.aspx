<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_CRF.aspx.cs" Inherits="CTMS.View_CRF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta content="text" charset="UTF-8" />
    <title>WAI</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
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
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
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
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/CKEditor/ckeditor.js"></script>
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/DataEntry_Table.css" rel="stylesheet" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TextBox_Options.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/Button_Mandatory.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/OpenPopUp.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/callChange_Review.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/showChild.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/DB/PARENT_CHILD_LINKED.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <style>
        .brd-1px-redimp
        {
            border: 1px solid !important;
            border-color: Red !important;
        }
        .disp-none
        {
            display: none;
        }
        .border3pxsolidblack
        {
            border: 3px solid black !important;
        }
        .strikeThrough
        {
            text-decoration: line-through;
        }
    </style>
    <style type="text/css">
        .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 850px;
            height: 500px;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

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
                else if (ctrl == "text" || ctrl == "textarea" || ctrl == "password") {
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


        function pageLoad() {

            if ($(location).attr('href').indexOf('NIWRS_FORM') != -1) {

                $('.txtDate').each(function (index, element) {
                    $(element).pikaday({
                        field: element,
                        format: 'DD-MMM-YYYY',
                        yearRange: [1910, 2050],
                        maxDate: new Date(),
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

        }


        function ForgotPwd() {

            $.ajax({
                type: "POST",
                url: "HomePage.aspx/ForgotPwd",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        //                        var url = data.d;
                        //                        $(location).attr('href', url);     
                        alert('Change password link send to your EmailId,Please check your email.');
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

            location.reload(true);
        }


    </script>
    <style>
        .table1 > tbody > tr > td
        {
            border: none;
        }
        .table
        {
            border: none;
        }
        .table-bordered
        {
            border-color: transparent;
        }
        .table-bordered-top
        {
            border-top: 1px solid #ced4da;
        }
    </style>
</head>
<body style="cursor: auto;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div class="box box-warning">
            <div class="box-body" style="margin-left: 2%; margin-right: 2%;">
                <div class="rows">
                    <div class="col-md-12">
                        <div class="row">
                            <h2 class="box-title bg-light-blue">
                                <asp:Label ID="lblHeader" runat="server" ForeColor="White" Text=""></asp:Label></h2>
                        </div>
                        <div class="rows">
                            <div class="col-md-12">

                                <div class="row" runat="server" id="divForm">
                                    <div class="form-group">
                                        <asp:HiddenField runat="server" ID="hfTablename" Value="ePRO_DATA_EPROIE" />
                                        <asp:HiddenField runat="server" ID="hdnSUBJECTID" />
                                        <asp:HiddenField runat="server" ID="hdnMODULEID" Value="32" />
                                        <asp:HiddenField runat="server" ID="hfDM_Tablename" Value="" />
                                        <asp:HiddenField runat="server" ID="hfDM_SYNC" />
                                        <asp:HiddenField runat="server" ID="hdnVISIT" />
                                        <asp:HiddenField runat="server" ID="hdnqualifieddata" />
                                        <asp:GridView ID="grd_Data" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                            CssClass="table table-sm table1" OnRowDataBound="grd_Data_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="disp-none"></HeaderStyle>
                                                    <ItemStyle CssClass="disp-none"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center disp-none"
                                                    ItemStyle-CssClass="align-left  disp-none">
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <asp:Label ID="lblFieldName1" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                    Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTABLENAME" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DATATYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDATATYPE" Text='<%# Bind("DATATYPE") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left">
                                                    <ItemTemplate>
                                                        <div class="form-group" style="margin-bottom: 0px;">
                                                            <asp:Label ID="lblFieldName" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblFieldNameFrench" CssClass="disp-none" Style="text-align: LEFT"
                                                                runat="server"></asp:Label>
                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                            <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" CssClass="manualInput"
                                                                onkeyup="setInitials();" onchange="showChild(this); CheckHeightValue(this); calcAge(this);"></asp:TextBox>
                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                <ItemTemplate>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                                ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                <ItemTemplate>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                Text='<%# Bind("TEXT") %>' />
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                <div style="float: right; font-size: 13px;">
                                                                </div>
                                                                <div>
                                                                    <div id="_CHILD" style="display: block; position: relative;">
                                                                        <asp:GridView ID="grd_Data1" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                                            CssClass="table table-sm table1" OnRowDataBound="grd_Data1_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="disp-none"></HeaderStyle>
                                                                                    <ItemStyle CssClass="disp-none"></ItemStyle>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center disp-none"
                                                                                    ItemStyle-CssClass="align-left  disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFieldName1" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                                            Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTABLENAME" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="DATATYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDATATYPE" Text='<%# Bind("DATATYPE") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left">
                                                                                    <ItemTemplate>
                                                                                        <div class="form-group" style="margin-bottom: 0px;">
                                                                                            <asp:Label ID="lblFieldName" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                            <br />
                                                                                            <asp:Label ID="lblFieldNameFrench" CssClass="disp-none" Style="text-align: LEFT"
                                                                                                runat="server"></asp:Label>
                                                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false">
                                                                                            </asp:DropDownList>
                                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                <ItemTemplate>
                                                                                                    <div class="col-sm-12">
                                                                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                <ItemTemplate>
                                                                                                    <div class="col-sm-12">
                                                                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                            onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                            Text='<%# Bind("TEXT") %>' />
                                                                                                    </div>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                                <div style="float: right; font-size: 13px;">
                                                                                                </div>
                                                                                                <div>
                                                                                                    <div id="_CHILD" style="display: block; position: relative;">
                                                                                                        <asp:GridView ID="grd_Data2" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                                                                            CssClass="table table-sm table1" OnRowDataBound="grd_Data2_RowDataBound">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle CssClass="disp-none"></HeaderStyle>
                                                                                                                    <ItemStyle CssClass="disp-none"></ItemStyle>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center disp-none"
                                                                                                                    ItemStyle-CssClass="align-left disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblFieldName1" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                                                                            Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblTABLENAME" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="DATATYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblDATATYPE" Text='<%# Bind("DATATYPE") %>' runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left">
                                                                                                                    <ItemTemplate>
                                                                                                                        <div class="form-group" style="margin-bottom: 0px;">
                                                                                                                            <asp:Label ID="lblFieldName" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                            <br />
                                                                                                                            <asp:Label ID="lblFieldNameFrench" CssClass="disp-none" Style="text-align: LEFT"
                                                                                                                                runat="server"></asp:Label>
                                                                                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                                OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false">
                                                                                                                            </asp:DropDownList>
                                                                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                                                                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <div class="col-sm-6">
                                                                                                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                                                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                                                                    </div>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:Repeater>
                                                                                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <div class="col-sm-6">
                                                                                                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                            onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                                                            Text='<%# Bind("TEXT") %>' />
                                                                                                                                    </div>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:Repeater>
                                                                                                                        </div>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <tr>
                                                                                                                            <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                                                                <div style="float: right; font-size: 13px;">
                                                                                                                                </div>
                                                                                                                                <div>
                                                                                                                                    <div id="_CHILD" style="display: block; position: relative;">
                                                                                                                                        <asp:GridView ID="grd_Data3" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                                                                                                            CssClass="table table-sm table1" OnRowDataBound="grd_Data3_RowDataBound">
                                                                                                                                            <Columns>
                                                                                                                                                <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                    <HeaderStyle CssClass="disp-none"></HeaderStyle>
                                                                                                                                                    <ItemStyle CssClass="disp-none"></ItemStyle>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="VARIABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblVARIABLENAME" Text='<%# Bind("VARIABLENAME") %>' runat="server"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="CONTROLTYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblCONTROLTYPE" Text='<%# Bind("CONTROLTYPE") %>' runat="server"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center disp-none"
                                                                                                                                                    ItemStyle-CssClass="align-lef dispt disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblFieldName1" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>'
                                                                                                                                                            Text='<%# Bind("FIELDNAME") %>' Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="CLASSNAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblCLASS" Text='<%# Bind("CLASS") %>' runat="server"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblTABLENAME" Text='<%# Bind("TABLENAME") %>' runat="server"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="DATATYPE" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblDATATYPE" Text='<%# Bind("DATATYPE") %>' runat="server"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="align-left" ItemStyle-CssClass="align-left"
                                                                                                                                                    ItemStyle-Width="25%">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <div class="form-group" style="margin-bottom: 0px;">
                                                                                                                                                            <asp:Label ID="lblFieldName" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                                                                                Style="text-align: LEFT" runat="server"></asp:Label>
                                                                                                                                                            <br />
                                                                                                                                                            <asp:Label ID="lblFieldNameFrench" CssClass="disp-none" Style="text-align: LEFT"
                                                                                                                                                                runat="server"></asp:Label>
                                                                                                                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                                                                OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false">
                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                                                                                                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <div class="col-sm-6">
                                                                                                                                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                                                                                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                                                                                                    </div>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:Repeater>
                                                                                                                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <div class="col-sm-6">
                                                                                                                                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                                            onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                                                                                            Text='<%# Bind("TEXT") %>' />
                                                                                                                                                                    </div>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:Repeater>
                                                                                                                                                        </div>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="REQUIREDYN" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblREQUIREDYN" Text='<%# Bind("REQUIREDYN") %>' runat="server"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                                <asp:TemplateField HeaderText="PREFIXTEXT" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <asp:Label ID="lblPREFIXTEXT" Text='<%# Bind("PREFIXTEXT") %>' runat="server"></asp:Label>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                </asp:TemplateField>
                                                                                                                                            </Columns>
                                                                                                                                        </asp:GridView>
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
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
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
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
