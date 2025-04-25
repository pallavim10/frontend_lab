<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NIWRS_WORKFLOW_CRITs.aspx.cs"
    Inherits="CTMS.NIWRS_WORKFLOW_CRITs" %>

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
   <%-- <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <script type="text/javascript" src="CommonFunctionsJs/IWRS/IWRS_Datatable.js"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_showAuditTrail.js"></script>
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

    </script>
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript">
        CKEDITOR.config.toolbar = [
            ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Outdent', 'Indent', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Styles', 'Format', 'Font', 'FontSize']
        ];

        CKEDITOR.config.height = 250;

        function CallCkedit() {

            CKEDITOR.replace("txtEmailBody");

        }

        function ShowEmailDiv() {

            var element = document.getElementById('chkSendEmail');
            if ($(element).prop('checked') == true) {
                $('#divEmailIDs').removeClass('disp-none');
                $('#divEmail').removeClass('disp-none');
            }
            else if ($(element).prop('checked') == false) {
                $('#divEmailIDs').addClass('disp-none');
                $('#divEmail').addClass('disp-none');
            }

        }

        function showDivNextVisit() {

            var element = document.getElementById('ddlVisit');
            if ($(element).val() != "0") {
                $('#divNextVisit').removeClass('disp-none');
            }
            else if ($(element).val() == "0") {
                $('#divNextVisit').addClass('disp-none');
            }

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
            <asp:HiddenField runat="server" ID="hdnREVIEWSTATUS" />
            <div class="row">
                <div class="lblError">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Manage Criterias</h3>
                </div>
                <div class="rows">
                    <asp:GridView ID="grdStepCrits" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                        OnRowCommand="grdStepCrits_RowCommand" OnPreRender="grdStepCrits_PreRender" OnRowDataBound="grdStepCrits_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                ItemStyle-CssClass="disp-none">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditCrit"
                                        runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No." ItemStyle-CssClass="txt_center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Criteria Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCritName" Text='<%# Bind("CritName") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Criteria">
                                <ItemTemplate>
                                    <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Navigate To">
                                <ItemTemplate>
                                    <asp:Label ID="lblNAVIGATE" Text='<%# Bind("NAVIGATE") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('NIWRS_WORKFLOW_STEPS_CRITs', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteCrit"
                                        runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this criteria : ", Eval("CritName")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
                <div id="DivAuditTrail" style="font-size: small;">
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
                                                Enter Criteria Name:</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtCritName" runat="server" CssClass="form-control width200px required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>
                                                Enter Sequence No.:</label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:TextBox ID="txtSEQNO" runat="server" Width="10%" CssClass="form-control numeric required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>
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
                                                    <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control required"
                                                        Width="100%">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Is Blank" Value="IS NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is
    Not Blank"
                                                            Value="IS NOT NULL"></asp:ListItem>
                                                        <asp:ListItem Text="Is Equals To" Value="="></asp:ListItem>
                                                        <asp:ListItem Text="Is Not Equals To" Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser
    Than"
                                                            Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than OR Equals To" Value="=<"></asp:ListItem>
                                                        <asp:ListItem Text="Begins With" Value="[_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Begins With" Value="![_]%"></asp:ListItem>
                                                        <asp:ListItem Text="Contains" Value="%_%"></asp:ListItem>
                                                        <asp:ListItem Text="Does Not Contains" Value="!%_%"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:HiddenField runat="server" ID="hfValue1" />
                                                    <asp:TextBox runat="server" CssClass="OptionValues form-control" ID="txtLISTValue1"
                                                        Width="100%"> </asp:TextBox>
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
                                                        <asp:ListItem Text="Is Not Equals
    To"
                                                            Value="!="></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than" Value=">"></asp:ListItem>
                                                        <asp:ListItem Text="Is Greater Than OR Equals To" Value="=>"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than" Value="<"></asp:ListItem>
                                                        <asp:ListItem Text="Is Lesser Than
    OR Equals To"
                                                            Value="=<"></asp:ListItem>
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
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="box box-group" style="background-color: lightcyan;">
                                    <div class="box-header">
                                        <h3 class="box-title">Actions</h3>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-3">
                                                <label>
                                                    Applicable Visit :</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:DropDownList runat="server" ID="ddlVisit" onchange="return showDivNextVisit();"
                                                    CssClass="form-control" Width="80%">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-3">
                                                <label>
                                                    Activity :</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="ddlPerform" runat="server" CssClass="form-control" Width="80%">
                                                    <asp:ListItem Selected="True" Text="None" Value="None"></asp:ListItem>
                                                    <asp:ListItem Text="Assign Screening Id" Value="Screening"></asp:ListItem>
                                                    <asp:ListItem Text="Pre-Randomization" Value="Pre-Randomization"></asp:ListItem>
                                                    <asp:ListItem Text="Assign Randomization Number" Value="Randomization"></asp:ListItem>
                                                    <asp:ListItem Text="Unassign Randomization Number" Value="De-Randomization"></asp:ListItem>
                                                    <asp:ListItem Text="Pre-Dosing" Value="Pre-Dosing"></asp:ListItem>
                                                    <asp:ListItem Text="Allocate Kit" Value="Dosing"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divNextVisit" class="disp-none">
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="col-md-3">
                                                    <label>
                                                        Next Visit :</label>
                                                </div>
                                                <div class="col-md-9">
                                                    <asp:DropDownList runat="server" ID="ddlNextVisit" CssClass="form-control" Width="80%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="col-md-3">
                                                <label>
                                                    Messagebox :</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="txtMSGBOX" Height="50px" Width="80%" TextMode="MultiLine"
                                                    CssClass="form-control" MaxLength="200"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="col-md-3">
                                                <label>
                                                    Event History :</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:TextBox runat="server" ID="txtEventHistory" Height="50px" Width="80%" TextMode="MultiLine"
                                                    CssClass="form-control" MaxLength="200"> </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>
                                                            Set Field Value :</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <div class="col-md-3" style="padding: 0;">
                                                            <asp:Label ID="Label1" runat="server" Text="Status" CssClass="label"></asp:Label>
                                                        </div>
                                                        <div class="col-md-5" style="padding: 0;">
                                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control width200px">
                                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Listing" Value="Listing"></asp:ListItem>
                                                                <asp:ListItem Text="Form" Value="Form"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Repeater runat="server" ID="repeatSetFields">
                                                    <ItemTemplate>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <label>
                                                                    &nbsp;</label>
                                                                <asp:HiddenField ID="hfCOLNAME" runat="server" Value='<%# Eval("COL_NAME") %>' />
                                                            </div>
                                                            <div class="col-md-9">
                                                                <div class="col-md-3" style="padding: 0;">
                                                                    <asp:Label ID="lblFIELDNAME" runat="server" Text='<%# Eval("FIELDNAME") %>' CssClass="label"></asp:Label>
                                                                </div>
                                                                <div class="col-md-5" style="padding: 0;">
                                                                    <asp:TextBox runat="server" ID="txtSetFieldVal" Text='<%# Eval("Set_VALUE") %>' CssClass="form-control width200px"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>
                                                            Navigate To :</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <div class="col-md-3" style="padding: 0;">
                                                            <asp:Label ID="Label2" runat="server" Text="Navigation Type :" CssClass="label"></asp:Label>
                                                        </div>
                                                        <div class="col-md-5" style="padding: 0;">
                                                            <asp:DropDownList runat="server" ID="ddlNavType" CssClass="form-control width200px"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlNavType_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Listing" Value="Listing"></asp:ListItem>
                                                                <asp:ListItem Text="Form" Value="Form"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <div class="col-md-3" style="padding: 0;">
                                                            <asp:Label ID="Label3" runat="server" Text="Navigation Source :" CssClass="label"></asp:Label>
                                                        </div>
                                                        <div class="col-md-5" style="padding: 0;">
                                                            <asp:DropDownList runat="server" ID="ddlNavTo" CssClass="form-control width200px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>
                                                            Send Email :</label>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <div class="col-md-10" style="padding: 0;">
                                                            <asp:CheckBox ID="chkSendEmail" runat="server" Text="   Select, if Yes." onchange="ShowEmailDiv()" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-9" style="padding: 0;">
                                                        <div class="disp-none" id="divEmailIDs">
                                                            <label>
                                                                Email IDs :</label>
                                                            <asp:GridView runat="server" ID="gvEmailds" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                Style="width: 90%; border-collapse: collapse;">
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Site ID">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSiteID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="To Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                                                runat="server" Text='<%# Bind("EMAILIDS") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                                                runat="server" Text='<%# Bind("CCEMAILIDS") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bcc Email IDs<br/>(Use ',' in case of multiple)" ItemStyle-Width="30%">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtBCCEMAILIDs" CssClass="form-control" Width="100%" TextMode="MultiLine"
                                                                                runat="server" Text='<%# Bind("BCCEMAILIDS") %>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="disp-none" id="divEmail">
                                                    <div class="rows">
                                                        <div class="row">
                                                            <label>
                                                                Email Subject :</label>
                                                            <asp:TextBox runat="server" ID="txtEmailSubject" Height="50px" Width="100%" TextMode="MultiLine"
                                                                CssClass="form-control"> 
                                                            </asp:TextBox>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <label>
                                                                Email Body :</label>
                                                            <asp:TextBox runat="server" ID="txtEmailBody" CssClass="ckeditor" Height="50%" TextMode="MultiLine"
                                                                Width="100%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
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
                </div>
            </div>
        </div>
    </form>
</body>
</html>
