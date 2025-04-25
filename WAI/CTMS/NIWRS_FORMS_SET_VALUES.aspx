<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NIWRS_FORMS_SET_VALUES.aspx.cs"
    Inherits="CTMS.NIWRS_FORMS_SET_VALUES" %>

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
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 300px;
        }

        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 83.33333333333334%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
    <script>

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

        function bindOptionValues() {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'txtLISTValue1') {
                    avaTag = $('#hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue2') {
                    avaTag = $('#hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue3') {
                    avaTag = $('#hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue4') {
                    avaTag = $('#hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue5') {
                    avaTag = $('#hfValue5').val().split(',');
                }

                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        }

        $(function () {
            var colorFields = $(".OptionValues").toArray();
            for (a = 0; a < colorFields.length; ++a) {

                var avaTag = "";
                if ($(colorFields[a]).attr('id') == 'txtLISTValue1') {
                    avaTag = $('#hfValue1').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue2') {
                    avaTag = $('#hfValue2').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue3') {
                    avaTag = $('#hfValue3').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue4') {
                    avaTag = $('#hfValue4').val().split(',');
                }
                else if ($(colorFields[a]).attr('id') == 'txtLISTValue5') {
                    avaTag = $('#hfValue5').val().split(',');
                }

                $(colorFields[a]).autocomplete({
                    source: avaTag, minLength: 0
                }).on('focus', function () { $(this).keydown(); });
            }
        });

        function ConfigFrozen_MSG() {
            alert('Configuration has been Frozen');
            return false;
        }

    </script>
    <%-- <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_showAuditTrail.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 5px">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:HiddenField runat="server" ID="hdnREVIEWSTATUS" />
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <asp:HiddenField ID="hdnFieldvalue" runat="server" />
            <asp:HiddenField ID="hdnFieldName" runat="server" />
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Manage Forms Set Values</h3>
                </div>
                <div class="rows">
                    <asp:HiddenField ID="hdnmoduleids" runat="server" />
                    <asp:GridView ID="grdFormsSetValue" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                        OnRowCommand="grdFormsSetValue_RowCommand" OnRowDataBound="grdFormsSetValue_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="LabelID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Eval("ID") %>' CommandName="EditCrit"
                                        runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="METHOD" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblMETHOD" Text='<%# Bind("METHOD") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="txt_center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FORMULA" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblFORMULA" Text='<%# Bind("FORMULA") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="txt_center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No." ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="txt_center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Criteria">
                                <ItemTemplate>
                                    <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblCritName" Text='<%# Bind("Value") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_FORMS_SET_VALUE', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Criteria/Formula : ", Eval("Criteria")) %>' CommandArgument='<%# Eval("ID") %>' CommandName="DeleteCrit"
                                        runat="server" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary" style="min-height: 210px;">
                        <div class="box-header" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">Add Additional Criterias for : &nbsp;<asp:Label ID="lblList" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <br />
                        <br />
                        <div class="box-body">
                            <div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>
                                                Field Name:</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:Label ID="lblFieldName" runat="server" CssClass="label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <br />

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>
                                                Calculation Method :</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:DropDownList runat="server" ID="Ddcalculation" CssClass="form-control required width200px" AutoPostBack="true" OnSelectedIndexChanged="Ddcalculation_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Text="---Select---" Value="Select"></asp:ListItem>
                                                <asp:ListItem Value="By Formula">By Formula</asp:ListItem>
                                                <asp:ListItem Value="By Criteria">By Criteria</asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row" id="divsequence" runat="server">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label id="lblsequence" runat="server">
                                                Enter Sequence No.:</label>
                                        </div>
                                        <div class="auto-style1">
                                            <asp:TextBox ID="txtSEQNO" runat="server" Width="10%" CssClass="form-control numeric required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row" id="divalues" runat="server">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label id="lbltxtvalue" runat="server">
                                                Enter Values:</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtvalues" runat="server" CssClass="form-control width200px required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label id="lblFormula" runat="server">
                                                Enter Formula:</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="TxtFormula" runat="server" Height="207px" SelectionMode="Multiple" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-md-5" id="showformula" runat="server" visible="false">
                                            <table border="1" runat="server" id="grd_formula" style="width: 30%; border-collapse: collapse;" class="table table-bordered table-striped">
                                                <tr>
                                                    <td style="font-weight: bold; font-size: 15px; text-align: center; vertical-align: middle;">Sample Formula </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; vertical-align: middle;">
                                                        <asp:Button ID="btnYear" runat="server" Text="Year" style="width: 100%;" CssClass="btn btn-primary" OnClick="btnYear_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; vertical-align: middle;">
                                                        <asp:Button ID="btnMonths" runat="server" Text="Months" style="width: 100%;" CssClass="btn btn-primary" OnClick="btnMonths_Click"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; vertical-align: middle;">
                                                        <asp:Button ID="btnDays" runat="server" Text="Days" style="width: 100%;" CssClass="btn btn-primary" OnClick="btnDays_Click"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; vertical-align: middle;">
                                                        <asp:Button ID="btnWeeks" runat="server" Text="Weeks" style="width: 100%;" CssClass="btn btn-primary" OnClick="btnWeeks_Click"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; vertical-align: middle;">
                                                        <asp:Button ID="btnYMD" runat="server" Text="Years, Months and Days" style="width: 100%;" CssClass="btn btn-primary" OnClick="btnYMD_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; vertical-align: middle;">
                                                        <asp:Button ID="btnMD" runat="server" Text="Months and Days" style="width: 100%;" CssClass="btn btn-primary" OnClick="btnMD_Click"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <br />

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label id="lblsetcriteria" runat="server">
                                                Set Criteria :</label>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTField1" runat="server" CssClass="form-control required width200px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="drpLISTField1_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control required" Width="100%">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:HiddenField runat="server" ID="hfValue1" />
                                                    <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue1" Width="100%"> </asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTAndOr1" runat="server" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                        <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTField2" runat="server" CssClass="form-control width200px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="drpLISTField2_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTCondition2" runat="server" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:HiddenField runat="server" ID="hfValue2" />
                                                    <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue2"
                                                        Width="100%"> </asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTAndOr2" runat="server" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                        <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTField3" runat="server" CssClass="form-control width200px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="drpLISTField3_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTCondition3" runat="server" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:HiddenField runat="server" ID="hfValue3" />
                                                    <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue3"
                                                        Width="100%"> </asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTAndOr3" runat="server" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                        <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTField4" runat="server" CssClass="form-control width200px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="drpLISTField4_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTCondition4" runat="server" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:HiddenField runat="server" ID="hfValue4" />
                                                    <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue4"
                                                        Width="100%"> </asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTAndOr4" runat="server" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Selected="True" Value="0" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="AND" Text="AND"></asp:ListItem>
                                                        <asp:ListItem Value="OR" Text="OR"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTField5" runat="server" CssClass="form-control width200px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="drpLISTField5_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="drpLISTCondition5" runat="server" CssClass="form-control" Width="100%">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Blank" Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:HiddenField runat="server" ID="hfValue5" />
                                                    <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue5"
                                                        Width="100%"> </asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    &nbsp;
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-5">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                            OnClick="btnsubmit_Click" />
                                                        <asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                            OnClick="btnUpdate_Click" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btncancel_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
            <div id="DivAuditTrail" style="font-size: small;">
            </div>
        </div>
    </form>
</body>
</html>
