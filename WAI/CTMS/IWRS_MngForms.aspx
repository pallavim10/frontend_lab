<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IWRS_MngForms.aspx.cs" Inherits="CTMS.IWRS_MngForms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonFunction.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
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
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_showAuditTrail.js"></script>
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 300px;
        }
    </style>
    <script>

        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();
            $('.txtDate').each(function (index, element) {
                $(element).pikaday({
                    field: element,
                    format: 'DD-MMM-YYYY',
                    yearRange: [1910, 2050]
                });
            });

            $(function () {
                $(".Datatable").dataTable({
                    "bSort": true,
                    "ordering": false,
                    "bDestroy": true,
                    "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                    stateSave: false,
                    fixedHeader: true
                });
            });
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

        function set_FieldColor(element) {
            var fcolor = element.value;
            $('#<% =hfFieldColor.ClientID %>').attr('value', fcolor);
        }
        function set_AnsColor(element) {
            var acolor = element.value;
            $('#<% =hfAnsColor.ClientID %>').attr('value', acolor);
        }

        function Set_Condition(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var FieldName = $(element).closest('tr').find('td:eq(9)').find('span').html();
            var FORMID = $('#HDNFORMID').val();
            var test = "NIWRS_FORMS_SET_VALUES.aspx?FIELDID=" + ID + "&FieldName=" + FieldName + "&FORMID=" + FORMID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1350";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddDrpDownData(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var VARIABLENAME = $(element).closest('tr').find('td:eq(6)').find('span').html();
            var CONTROLTYPE = $(element).closest('tr').find('td:eq(7)').find('span').html();
            var FORMID = $('#HDNFORMID').val();

            var test = "IWRS_PROJECT_DRPS.aspx?ID=" + ID + "&VARIABLENAME=" + VARIABLENAME + "&FORMID=" + FORMID + "&CONTROL=" + CONTROLTYPE;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1100";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ConfigFrozen_MSG() {
            alert('Configuration has been Frozen');
            return false;
        }

    </script>
    <%--<script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="box box-success">
                <div class="box-body">
                    <asp:HiddenField runat="server" ID="hdnREVIEWSTATUS" />
                    <div class="box-header with-border">
                        <h4 class="box-title" align="left">Manage Forms
                        </h4>
                        <div class="pull-right" style="margin-right: 10px;">
                            <%--<asp:LinkButton ID="lbtnExportSpecs" runat="server" Text="Export Specs" OnClick="lbtnExportSpecs_Click"> Export Specifications&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>--%>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary" runat="server" id="divRecord" style="min-height: 300px;">
                                <div class="box-header with-border" style="float: left;">
                                    <h4 class="box-title" align="left">Records
                                        <asp:HiddenField ID="HDNFORMID" runat="server" />
                                        <asp:HiddenField ID="HDNMODULEID" runat="server" />
                                        <asp:HiddenField ID="HDMODULENAME" runat="server" />
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div>
                                            <div class="rows">
                                                <div style="width: 100%; overflow: auto;">
                                                    <div>
                                                        <asp:GridView ID="grdField" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                            Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdField_RowCommand"
                                                            OnRowDataBound="grdField_RowDataBound" OnPreRender="grdField_PreRender">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                    ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("FIELD_ID") %>'
                                                                            CommandName="EditField" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Add Option" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnlAddOption" runat="server" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                                                            ToolTip="Add Option" OnClientClick="return AddDrpDownData(this)">  <i class="glyphicon glyphicon-cog"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnSync" runat="server" Visible="false" CommandName="Sync" CommandArgument='<%# Eval("FIELD_ID") %>'
                                                                            ToolTip="Sync"><i class="fa fa-refresh" style="color:red;" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lbtnUNSYNC" runat="server" Visible="false" CommandName="UNSYNC"
                                                                            ToolTip="Synced" CommandArgument='<%# Eval("FIELD_ID") %>'><i class="fa fa-refresh" style="color:green;" aria-hidden="true"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center" HeaderText="Set Value">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnSetFieldValues" runat="server" ToolTip="Set Field Values"
                                                                            CommandArgument='<%# Eval("FIELD_ID") %>' OnClientClick="return Set_Condition(this)"><i class="fa fa-wrench" style="color:#0000FF;" aria-hidden="true"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Seq. No." ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Variable Name" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Control Type" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                                    ItemStyle-CssClass="disp-none">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="CONTROL" runat="server" Text='<%# Bind("CONTROLTYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Module Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="FIELDNAME" runat="server" Style="text-align: left" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('IWRS_PROJECT_MASTER', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this field : ", Eval("FIELDNAME")) %>' CommandName="DELETEFIELD" CommandArgument='<%# Eval("FIELD_ID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="LbtnUndo" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure want to undo this field : ", Eval("FIELDNAME")) %>' CommandName="UNDOFIELD" Visible="false" CommandArgument='<%# Eval("FIELD_ID") %>'><i class="fa fa-undo"></i></i></asp:LinkButton>
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
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-primary" id="divField" runat="server">
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div>
                                            <div class="row" style="margin-top: 7px;">
                                                <div class="col-md-12">
                                                    <div class="col-md-2" style="width: 130px;">
                                                        <label>
                                                            Enter Field Name:</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox Style="width: 552px;" ID="txtFieldName" ValidationGroup="section" runat="server"
                                                            CssClass="form-control required2"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 7px;">
                                                <div class="col-md-12">
                                                    <div class="col-md-2" style="width: 130px;">
                                                        <label>Variable Name:</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label Style="width: 250px;" ID="lblVariableName" ValidationGroup="section" Text=""
                                                            runat="server" CssClass="form-control required2"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2" style="width: 130px;">
                                                        <label>Enter Seq No:</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox Style="width: 90px;" ID="txtSeqno" MaxLength="3" ValidationGroup="section"
                                                            runat="server" CssClass="form-control numeric required2"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2" style="width: 150px;">
                                                        <label>Select Control Type:</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="drpSCControl" AutoPostBack="true" runat="server" Style="width: 165px;"
                                                            class="form-control drpControl required2" ValidationGroup="SubCheklist"
                                                            OnSelectedIndexChanged="drpSCControl_SelectedIndexChanged">
                                                            <asp:ListItem Text="-Select-" Value="0">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="CHECKBOX" Value="CHECKBOX">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="RADIOBUTTON" Value="RADIOBUTTON">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="DROPDOWN" Value="DROPDOWN">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="NUMERIC" Value="NUMERIC">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="DECIMAL" Value="DECIMAL">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="TEXTBOX" Value="TEXTBOX">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="DATE" Value="DATE">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="DATE with Input Mask" Value="DATEINPUTMASK">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="DATE without Futuristic Date" Value="DATENOFUTURE">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="TIME" Value="TIME">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="HEADER" Value="HEADER">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="TEXTBOX with Options" Value="TEXTBOX with Options">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="HTML TEXTBOX" Value="HTML TEXTBOX">
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 7px;">
                                                <div class="col-md-12">
                                                    <div class="col-md-2" style="width: 130px;">
                                                        <label>
                                                            Description:</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox Style="width: 552px;" TextMode="MultiLine" ID="txtDescrip" ValidationGroup="section"
                                                            runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-1" style="width: 411px;">
                                                        &nbsp;
                                                    </div>
                                                    <div id="DIVmaxLenght" runat="server" visible="false">
                                                        <div class="col-md-2" style="width: 150px;">
                                                            <label>
                                                                Max Length :</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox Style="width: 165px;" ID="txtMaxLength" ValidationGroup="section" runat="server" MaxLength="5"
                                                                CssClass="form-control numeric"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="divFormat" visible="false" class="row">
                                                        <div class="col-md-2" style="width: 150px;">
                                                            <label>
                                                                Decimal Format :</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtFormat" ValidationGroup="section" runat="server" CssClass="form-control required2" Style="width: 165px;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="box box-warning">
                                                        <div class="box-header with-border" style="margin-top: 3px;">
                                                            <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Display Features:</h4>
                                                            <div class="col-md-9">
                                                                <div class="col-md-5">
                                                                    <input type="color" value="<% =FieldColorValue %>" id="FieldColor" name="FieldColor"
                                                                        onchange="set_FieldColor(this);" />&nbsp;&nbsp;
                                                                        <asp:HiddenField ID="hfFieldColor" runat="server" />
                                                                    <label>Text Color</label>
                                                                </div>
                                                                <div class="col-md-7">
                                                                    <input type="color" value="<% =AnsColorValue %>" id="AnsColor" name="AnsColor" onchange="set_AnsColor(this);" />&nbsp;&nbsp;
                                                                        <asp:HiddenField ID="hfAnsColor" runat="server" />
                                                                    <label>Response Text Color</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3">
                                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkBold" />&nbsp;&nbsp;
                                                                        <label>Bold YN</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInvisible" />&nbsp;&nbsp;
                                                                    <label>Mask Field YN</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUppercase" />&nbsp;&nbsp;
                                                                         <label>UpperCase Only</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-3">
                                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRead" />&nbsp;&nbsp;
                                                                        <label>Read only YN</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUnderline" />&nbsp;&nbsp;
                                                                        <label>Underline YN</label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMultiline" />&nbsp;&nbsp;
                                                                        <label>Freetext YN</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-md-2" style="padding-left: 0px;">
                                                    <div class="box box-warning">
                                                        <div class="box-header with-border">
                                                            <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Data Significance:</h4>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-12">
                                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMandatory" />&nbsp;&nbsp;
                                                                    <label>Mandatory Information</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="box box-warning">
                                                        <div class="box-header with-border">
                                                            <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Data Linkages:</h4>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-10" style="display: inline-flex;">
                                                                    <asp:CheckBox runat="server" OnCheckedChanged="chkDefault_CheckedChanged" AutoPostBack="true"
                                                                        ToolTip="Select if 'YES'" ID="chkDefault" />&nbsp;&nbsp;
                                                                    <label>Protocol Predefine Data</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:TextBox Style="width: 150px;" ID="txtDefaultData" Visible="false" ValidationGroup="section" MaxLength="50"
                                                                        runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-5">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="box box-warning">
                                                        <div class="box-header with-border" style="margin-top: 3px;">
                                                            <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Applicable Reports:
                                                            </h4>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-5">
                                                                    <h5 style="font-weight: bold; color: darkviolet;">Blinded</h5>
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-4">
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkVisitSummary" />&nbsp;&nbsp;
                            <label>Visit Summary</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkDosingSummary" />&nbsp;&nbsp;
                            <label>Dosing Summary</label>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-7">
                                                                    <h5 style="font-weight: bold; color: darkviolet;">Unblinded</h5>
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-3">
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkDosingSummary_U" />&nbsp;&nbsp;
                            <label>Dosing Summary</label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRandomizationKitSummary" />&nbsp;&nbsp;
                            <label>Randomization & Kit Summary</label>
                                                                        </div>
                                                                        <div class="col-md-5">
                                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRandomizationTreatmentSummary" />&nbsp;&nbsp;
                            <label>Randomization & Treatment Summary</label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <br />
                                                    </div>
                                                </div>

                                                <br />
                                                <div class="text-center">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnUpdateField" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                            OnClick="btnUpdateField_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnCancelField" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
        OnClick="btnCancelField_Click" />
                                                    </div>
                                                </div>
                                            <br />

                                            <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
                                                <div id="DivAuditTrail" style="font-size: small;">
                                                </div>
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
