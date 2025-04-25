<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NIWRS_SETUP_FORM_SPECs.aspx.cs"
    Inherits="CTMS.NIWRS_SETUP_FORM_SPECs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script>

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

     
        $(function () {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {
                var avaTag = $(colorFields[a]).closest('tr').find('td span:eq(2)').text().split(",");
                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        });

    </script>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            //CalculateRPN();
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

        function Set_Condition(element) {
            var ID = $(element).closest('tr').find('td:eq(3)').find('span').html();
            var FieldName = $(element).closest('tr').find('td:eq(4)').find('span').html();
            var FORMID = $('#hdnFormID').val();
            var test = "NIWRS_FORMS_SET_VALUES.aspx?FIELDID=" + ID + "&FieldName=" + FieldName + "&FORMID=" + FORMID;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1350";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ConfigFrozen_MSG() {
            alert('Configuration has been Frozen');
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 5px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="row">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hdnREVIEWSTATUS" />
        <asp:HiddenField ID="hdnFormID" runat="server" />
        <div class="box box-primary" style="min-height: 210px;">
            <div class="box-header" style="float: left; top: 0px; left: 0px;">
                <h4 class="box-title" align="left">
                    Define Specifications for : &nbsp;<asp:Label ID="lblForm" runat="server"></asp:Label>
                </h4>
            </div>
            <br />
            <div class="row">
                <div>
                    <asp:GridView ID="grdFormSpecs" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                        Style="width: 97%; border-collapse: collapse; margin-left: 20px;" OnRowDataBound="grdFormSpecs_RowDataBound"
                        OnRowCommand="grdFormSpecs_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center" HeaderText="Remove">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this field : ", Eval("FIELDNAME")) %>'  CommandName="DELETEFIELD" CommandArgument='<%# Eval("FIELDID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    <asp:LinkButton ID="LbtnUndo" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure want to undo this field : ", Eval("FIELDNAME")) %>'  CommandName="UNDOFIELD" Visible="false" CommandArgument='<%# Eval("FIELDID") %>'><i class="fa fa-undo"></i></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center" HeaderText="Sync">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSync" runat="server" Visible="false" CommandName="Sync" CommandArgument='<%# Eval("FIELDID") %>'
                                        ToolTip="Sync"><i class="fa fa-refresh" style="color:#333333;" aria-hidden="true"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnUNSYNC" runat="server" Visible="false" CommandName="UNSYNC"
                                        ToolTip="Synced" CommandArgument='<%# Eval("FIELDID") %>'><i class="fa fa-refresh" style="color:#0000FF;" aria-hidden="true"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center" HeaderText="Set Value">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSetFieldValues" runat="server" ToolTip="Set Field Values"
                                        CommandArgument='<%# Eval("FIELDID") %>' OnClientClick="return Set_Condition(this)"><i class="fa fa-wrench" style="color:#0000FF;" aria-hidden="true"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FIELDID" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    
                                    <asp:Label ID="lblFIELDID" Text='<%# Eval("FIELDID") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Field Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="lblOptions" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br />
        </div>
    </form>
</body>
</html>
