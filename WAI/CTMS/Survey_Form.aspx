<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Survey_Form.aspx.cs" Inherits="CTMS.Survey_Form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
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
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <%--<script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>--%>
    <%--<link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />--%>
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
    <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>
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
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {

            $('.txtDateMask').inputmask(
            "dd/mm/yyyy", {
                placeholder: "dd/mm/yyyy"
            }
            );
        });

        $(function () {

            $('.txtTime').inputmask(
        "hh:mm", {
            placeholder: "HH:MM",
            insertMode: false,
            showMaskOnHover: false,
            hourFormat: "24"
        }
      );


        });

        function pageLoad() {
            //    $(".Datatable").dataTable();
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                stateSave: false
                //                "bPaginate": true,
                //                "bLengthChange": false,
                //                "bFilter": false,
                // "bSort": true,
                //                "bInfo": true,
                //                "bAutoWidth": false
            });

            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    // trigger: $(element).closest('div').find('.datepicker-button').get(0), // <<<<
                    // firstDay: 1,
                    //position: 'top right',
                    // minDate: new Date('2000-01-01'),
                    // maxDate: new Date('9999-12-31'),

                    // for date restriction
                    //  maxDate: new Date(),  
                    format: 'DD-MMM-YYYY',
                    //  defaultDate: new Date(''),
                    //setDefaultDate: false,
                    yearRange: [1910, 2050]
                });
            });

            $('.txtuppercase').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
            margin-right: 142px;
        }
        .style2
        {
            width: 540px;
            background-color: #E98454;
        }
        .table-bordered > tbody > tr > td
        {
            border: none;
        }
        .table-bordered > tbody > tr
        {
            border: none;
        }
        .fontbold
        {
            font-weight: bold;
        }
    </style>
    <script language="javascript" type="text/javascript">

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

        function showChild(element) {

            var MODULENAME = $("#lblModuleName").text();
            var MODULEID = getUrlVars()["MODULEID"];

            var Child_Val;
            if ($(element).context.className == "radio" || $(element).context.className == "checkbox") {
                Child_Val = $(element).context.lastChild.textContent.trim();
            }
            else {
                if ($(element).val() != null) {
                    Child_Val = $(element).val().trim();
                }
            }




            var SelfTableName = $(element).closest('table').attr('id');
            var TableName = $($(element).closest('tr').next('tr').find('table').toArray()[0]).attr('id');


            var Upper = $("#" + TableName + "").find('tr').eq(0).find('td').eq(7).find('input').val();
            var Lower = $("#" + TableName + "").find('tr').eq(2).find('td').eq(7).find('input').val();

            if ($(element).context.className == "checkbox") {
                var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
                    if (TableName != 'MainContent_grd_Data' && TableName != SelfTableName) {
                        if ($(this).find('td').toArray().length > 5) {
                            if ($.trim($(this).find('td').eq(8).text().trim()) == Child_Val) {
                                if ($($(element).context.getElementsByTagName("input")).prop('checked') == true) {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                    }

                                    if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                        $(element).closest('tr').next('tr').removeClass('disp-none');
                                    }
                                }
                                else {
                                    if ($(this).hasClass('disp-none')) {
                                    }
                                    else {
                                        $(this).addClass('disp-none');
                                    }

                                }
                            }
                        }
                    }
                })
            }
            else if ($(element).context.type == "text") {
                var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
                    if (TableName != 'MainContent_grd_Data' && TableName != SelfTableName) {
                        if ($(this).find('td').toArray().length > 5) {
                            if ($.trim($(this).find('td').eq(8).text().trim()) == "Is Not Blank") {

                                if (Child_Val != "") {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                    }

                                    if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                        $(element).closest('tr').next('tr').removeClass('disp-none');
                                    }
                                }
                                else {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                        $(this).addClass('disp-none');
                                    }
                                    else {
                                        $(this).addClass('disp-none');
                                    }
                                }

                            }
                            else if ($.trim($(this).find('td').eq(8).text().trim()) == "Compare") {

                                if (parseInt(Child_Val) > parseInt(Upper) || parseInt(Child_Val) < parseInt(Lower)) {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                    }

                                    if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                        $(element).closest('tr').next('tr').removeClass('disp-none');
                                    }
                                }
                                else {
                                    if ($(this).hasClass('disp-none')) {
                                        $(this).removeClass('disp-none');
                                        $(this).addClass('disp-none');
                                    }
                                    else {
                                        $(this).addClass('disp-none');
                                    }
                                }

                            }
                            else {
                                if ($(this).hasClass('disp-none')) {
                                    $(this).removeClass('disp-none');
                                    $(this).addClass('disp-none');
                                }
                                else {
                                    $(this).addClass('disp-none');
                                }
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }
                        }
                    }
                })
            }
            else {
                var $rowsNo = $('#' + TableName + ' tbody tr').filter(function () {
                    if (TableName != 'MainContent_grd_Data' && TableName != SelfTableName) {
                        if ($(this).find('td').toArray().length > 5) {
                            if ($.trim($(this).find('td').eq(8).text().trim()) == Child_Val) {
                                if ($(this).hasClass('disp-none')) {
                                    $(this).removeClass('disp-none');
                                }

                                if ($(element).closest('tr').next('tr').hasClass('disp-none')) {
                                    $(element).closest('tr').next('tr').removeClass('disp-none');
                                }
                            }
                            else {
                                if ($(this).hasClass('disp-none')) {
                                    $(this).removeClass('disp-none');
                                    $(this).addClass('disp-none');
                                }
                                else {
                                    $(this).addClass('disp-none');
                                }
                            }
                        }
                        else {
                            if ($(this).hasClass('disp-none')) {
                                $(this).removeClass('disp-none');
                            }
                        }
                    }
                })
            }

        }
        
    </script>
    <script language="javascript" type="text/javascript">
        function OpenModule(element) {
            var MODULE_ID = $(element).closest('tr').find('td:eq(16)').find('span').text();
            var MODULE_NAME = $(element).closest('tr').find('td:eq(17)').find('span').text();
            if (MODULE_ID != "0") {
                var test = "DM_Mappings.aspx?MODULEID=" + MODULE_ID + "&MODULENAME=" + MODULE_NAME;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        $(function callChange() {

            var a;
            var b;
            var c;
            var d;


            var DRP_FIELD = $("select[id*='DRP_FIELD']").toArray();
            for (a = 0; a < DRP_FIELD.length; ++a) {
                showChild(DRP_FIELD[a]);
            }

            var TXT_FIELD = $("input[id*='TXT_FIELD']").toArray();
            for (a = 0; a < TXT_FIELD.length; ++a) {
                showChild(TXT_FIELD[a]);
            }

            var CHK_FIELD = $("input[id*='CHK_FIELD']:checked").closest('span').toArray();
            for (c = 0; c < CHK_FIELD.length; ++c) {
                showChild(CHK_FIELD[c]);
            }

            if (CHK_FIELD.length < 1) {
                var CHK_FIELD1 = $("input[id*='CHK_FIELD']").closest('span').toArray();
                for (c = 0; c < CHK_FIELD1.length; ++c) {
                    showChild(CHK_FIELD1[c]);
                }
            }

            var RAD_FIELD = $("input[id*='RAD_FIELD']:checked").closest('span').toArray();
            for (d = 0; d < RAD_FIELD.length; ++d) {
                showChild(RAD_FIELD[d]);
            }

            if (RAD_FIELD.length < 1) {
                var RAD_FIELD1 = $("input[id*='RAD_FIELD']").toArray();
                for (d = 0; d < RAD_FIELD1.length; ++d) {
                    showChild(RAD_FIELD1[d]);
                }
            }

        });

    </script>
    <script type="text/javascript" language="javascript">

        function RadioCheck(rb) {
            //            var gv = document.getElementById($(rb).closest('table').attr('id'));
            var rbs = rb.parentNode.parentNode.parentNode.getElementsByTagName("input");
            var row = rb.parentNode.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }

        function Print() {
            var ProjectId = '<%= Session["PROJECTID"] %>'
            var MODULEID = $("#HDNMODULEID").val();
            var MODULENAME = $("#HDMODULENAME").val();
            var test = "ReportDM_Mappings.aspx?ProjectId=" + ProjectId + "&MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500px,width=1000px";
            window.open(test, '_blank', strWinProperty);
            return false;
        }
               
        
    </script>
    <script type="text/javascript" language="javascript">

        function GetDefaultData(element) {
            var dataFreezeLockStatus = '<%= Session["DATAFREEZELOCKSTATUS"] %>';
            if (dataFreezeLockStatus != '1') {
                var LabID = $(element).val(); //bindin  

                $.ajax({
                    type: "POST",
                    url: "DM_Mappings.aspx/GetDefaultData",
                    data: '{LabID: "' + LabID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
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

    
    </script>
</head>
<body style="cursor: auto;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header text-center">
            <h3 class="box-title" style="width: 100%;">
                <asp:Label runat="server" ID="lblUser" Style="font-size: larger;"></asp:Label>
                <asp:HiddenField ID="HDNMODULEID" runat="server" />
                <asp:HiddenField ID="HDMODULENAME" runat="server" />
                <asp:HiddenField ID="HDNTABLENAME" runat="server" />
                <asp:HiddenField ID="HDNUSERID" runat="server" />
            </h3>
        </div>
        <div class="box-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <asp:Label ID="lblErrorMsg" runat="server" Style="color: #CC3300; font-weight: 700;
                            font-size: 15px; margin-left: 7px"></asp:Label>
                        <asp:Label ID="lblModuleName" runat="server" Text="" Font-Size="12px" Font-Bold="true"
                            Font-Names="Arial" CssClass="list-group-item">Please Enter Following Details</asp:Label>
                        <asp:GridView ID="grd_Data" runat="server" CellPadding="3" AutoGenerateColumns="False"
                            ShowHeader="False" CssClass="table table-bordered table-striped" OnRowDataBound="grd_Data_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" onfocus="myFocus(this)"
                                            onchange="myFunction(this)" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
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
                                <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                    ControlStyle-CssClass="label" ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                            Style="text-align: LEFT" runat="server"></asp:Label>
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
                                    ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                    <ItemTemplate>
                                        <div class="col-md-12">
                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                OnClientClick="OpenModule(this);"></asp:LinkButton>
                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false"
                                                Width="200px">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" autocomplete="off" Visible="false"
                                                onchange="showChild(this);"></asp:TextBox>
                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                <ItemTemplate>
                                                    <div class="col-md-4">
                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                <ItemTemplate>
                                                    <div class="col-md-4">
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
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                <div style="float: right; font-size: 13px;">
                                                </div>
                                                <div>
                                                    <div id="_CHILD" style="display: block; position: relative;">
                                                        <asp:GridView ID="grd_Data1" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                            ShowHeader="False" CssClass="table table-bordered table-striped table-striped1"
                                                            OnRowDataBound="grd_Data1_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" onfocus="myFocus(this)"
                                                                            onchange="myFunction(this)" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
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
                                                                <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                    ControlStyle-CssClass="label" ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                            Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                                    ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                    <ItemTemplate>
                                                                        <div class="col-md-12">
                                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false"
                                                                                Width="200px">
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                <ItemTemplate>
                                                                                    <div class="col-md-4">
                                                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                <ItemTemplate>
                                                                                    <div class="col-md-4">
                                                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                            onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                            Text='<%# Bind("TEXT") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
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
                                                                                        <asp:GridView ID="grd_Data2" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                            ShowHeader="False" CssClass="table table-bordered table-striped table-striped2"
                                                                                            OnRowDataBound="grd_Data2_RowDataBound">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" onfocus="myFocus(this)"
                                                                                                            onchange="myFunction(this)" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
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
                                                                                                <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                                                    ControlStyle-CssClass="label" ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                            Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                                                                    ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                                                    <ItemTemplate>
                                                                                                        <div class="col-md-12">
                                                                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false"
                                                                                                                Width="200px">
                                                                                                            </asp:DropDownList>
                                                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                                                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                <ItemTemplate>
                                                                                                                    <div class="col-md-4">
                                                                                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                                                    </div>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:Repeater>
                                                                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                <ItemTemplate>
                                                                                                                    <div class="col-md-4">
                                                                                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                            onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                                            Text='<%# Bind("TEXT") %>' />
                                                                                                                    </div>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:Repeater>
                                                                                                        </div>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                                                                                <div style="float: right; font-size: 13px;">
                                                                                                                </div>
                                                                                                                <div>
                                                                                                                    <%--<div class="rows">
                                                                                                                                            <div class="col-md-12">--%>
                                                                                                                    <div id="_CHILD" style="display: block; position: relative;">
                                                                                                                        <asp:GridView ID="grd_Data3" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                                                                                            ShowHeader="False" CssClass="table table-bordered table-striped table-striped3"
                                                                                                                            OnRowDataBound="grd_Data3_RowDataBound">
                                                                                                                            <Columns>
                                                                                                                                <asp:TemplateField HeaderText="ContID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:TextBox ID="ContID" runat="server" ToolTip="ContID" onfocus="myFocus(this)"
                                                                                                                                            onchange="myFunction(this)" Font-Size="X-Small" font-family="Arial"></asp:TextBox>
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
                                                                                                                                <asp:TemplateField HeaderText="FIELD NAME" HeaderStyle-CssClass="align-center" ItemStyle-CssClass="align-left"
                                                                                                                                    ControlStyle-CssClass="label" ItemStyle-Width="30%" HeaderStyle-Width="30%">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblFieldName" Font-Size="Small" ToolTip='<%# Bind("Descrip") %>' Text='<%# Bind("FIELDNAME") %>'
                                                                                                                                            Style="text-align: LEFT" runat="server"></asp:Label>
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
                                                                                                                                    ItemStyle-Width="100%" HeaderStyle-Width="100%">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <div class="col-md-12">
                                                                                                                                            <asp:LinkButton runat="server" ID="LBTN_FIELD" Visible="false" Text='<%# Bind("MODULENAME") %>'
                                                                                                                                                OnClientClick="OpenModule(this);"></asp:LinkButton>
                                                                                                                                            <asp:DropDownList ID="DRP_FIELD" runat="server" onchange="showChild(this);" Visible="false"
                                                                                                                                                Width="200px">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                            <asp:TextBox ID="TXT_FIELD" runat="server" Width="200px" Visible="false" onchange="showChild(this);"></asp:TextBox>
                                                                                                                                            <asp:Repeater runat="server" ID="repeat_CHK">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:CheckBox ID="CHK_FIELD" runat="server" onchange="showChild(this);" CssClass="checkbox"
                                                                                                                                                            ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>' Text='<%# Bind("TEXT") %>' />
                                                                                                                                                    </div>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:Repeater>
                                                                                                                                            <asp:Repeater runat="server" ID="repeat_RAD">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <div class="col-md-4">
                                                                                                                                                        <asp:RadioButton ID="RAD_FIELD" runat="server" ForeColor='<%#System.Drawing.Color.FromName(Eval("color").ToString()) %>'
                                                                                                                                                            onchange="showChild(this);" onclick="return RadioCheck(this);" CssClass="radio"
                                                                                                                                                            Text='<%# Bind("TEXT") %>' />
                                                                                                                                                    </div>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:Repeater>
                                                                                                                                        </div>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblVal_Child" Text='<%# Bind("VAL_Child") %>' runat="server"></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblMODULEID" Text='<%# Bind("MODULEID") %>' runat="server"></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblMODULENAME" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
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
                        <br />
                        <div class="row">
                            <div class="col-md-8 txt_center">
                                <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                    OnClick="btnsubmit_Click" />
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
