<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NIWRS_SETUP_Multiple_Dependency.aspx.cs" Inherits="CTMS.NIWRS_SETUP_Multiple_Dependency" %>

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
</head>
<body>
    <form id="form1" runat="server" style="background-color: white">
        <br />
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">MANAGE MULTIPLE DEPENDENCY</h3>
            </div>
            <div class="box box-primary">
                <div class="rows">
                    <asp:HiddenField ID="hdnmoduleids" runat="server" />
                    <asp:HiddenField runat="server" ID="hdnREVIEWSTATUS" />
                    <asp:GridView ID="grdMultDepemdency" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowCommand="grdMultDepemdency_RowCommand" OnRowDataBound="grdMultDepemdency_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="LabelID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Eval("ID") %>' CommandName="EditMUltDENDCY"
                                        runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SeqNo" ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select Dependency">
                                <ItemTemplate>
                                    <asp:Label ID="lblMODULE" Text='<%# Bind("MODULENAME") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dependency Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblNAME" Text='<%# Bind("NAME") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Window">
                                <ItemTemplate>
                                    <asp:Label ID="lblWINDOW" Text='<%# Bind("WINDOW") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Early Window">
                                <ItemTemplate>
                                    <asp:Label ID="lblEARLY" Text='<%# Bind("EARLY") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Late Window">
                                <ItemTemplate>
                                    <asp:Label ID="lblLATE" Text='<%# Bind("LATE") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Criteria">
                                <ItemTemplate>
                                    <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_VISIT_DEPEND', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this dependency : ", Eval("NAME")) %>' CommandArgument='<%# Eval("ID") %>' CommandName="DeleteMUltDENDCY"
                                        runat="server" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>&nbsp;&nbsp;
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <div class="box box-primary">
                    <div class="box-header">
                        <h4 class="box-title" style="width: 166px; height: 19px">Set Dependency </h4>
                    </div>
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="divDepName" runat="server">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label id="lblDenName" runat="server">
                                    Dependency Name :</label>
                            </div>
                            <div class="auto-style1">
                                <asp:TextBox ID="TxtDepName" runat="server" CssClass="form-control required width200px"></asp:TextBox>
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
                                <asp:TextBox ID="txtSEQNO" runat="server" CssClass="form-control required width100px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Select Dependency :</label>
                            </div>
                            <div class="col-md-7" style="display: inline-flex;">
                                <asp:DropDownList runat="server" ID="ddlVisModule" CssClass="form-control width200px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlVisModule_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;
                                  <asp:DropDownList runat="server" Visible="false" ID="ddlVisField" CssClass="form-control required width200px">
                                  </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Enter Window Period :</label>
                            </div>
                            <div class="col-md-7" style="display: inline-flex;" runat="server" id="divtxtWindow">
                                <asp:TextBox Style="width: 100px;" MaxLength="5" ID="txtWindow" runat="server" CssClass="form-control numeric required"></asp:TextBox>&nbsp;&nbsp;&nbsp;Days
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Enter Early Window Period :</label>
                            </div>
                            <div class="col-md-7" style="display: inline-flex;" runat="server" id="divtxtearlyWindow">
                                <asp:TextBox Style="width: 100px;" MaxLength="5" ID="txtEarly" runat="server" CssClass="form-control numeric required"></asp:TextBox>&nbsp;&nbsp;&nbsp;Days
                            </div>

                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>
                                    Enter Late Window Period :</label>
                            </div>
                            <div class="col-md-7" style="display: inline-flex;" id="divtxtlateWindow" runat="server">
                                <asp:TextBox Style="width: 100px;" MaxLength="5" ID="txtLate" runat="server" CssClass="form-control numeric required"></asp:TextBox>&nbsp;&nbsp;&nbsp;Days
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
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnsubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                               <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnupdate_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                               <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" OnClick="btncancel_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            
                                        </div>
                                        <br />
                                        <br />
                                    </div>
                                    <br />
                                </div>
                                <br />
                                <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
                                    <div id="DivAuditTrail" style="font-size: small;">
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
