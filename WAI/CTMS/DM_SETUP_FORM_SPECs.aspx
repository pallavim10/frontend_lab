<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DM_SETUP_FORM_SPECs.aspx.cs"
    Inherits="CTMS.DM_SETUP_FORM_SPECs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
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
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        const { title } = require("node:process");


        function DisableDiv() {
            var nodes = document.getElementById("divCrit").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            //Get Query String Value
            const params = new Proxy(new URLSearchParams(window.location.search), {
                get: (searchParams, prop) => searchParams.get(prop),
            });

            let FREEZE = params.FREEZE;

            $("#lblStatus").text("This module is " + FREEZE + ".");
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

        function OpenAddFormula() {

            //  if ($('#ChkIsDerived').val() == 'on') {

            if ($('#PopupFormula').length === 0) {
                console.error("PopupFormula does not exist at this time.");
                return false;
            }
            // $("#PopupFormula").removeClass("disp-none");
            // title = "Add Formula";
            $("#PopupFormula").dialog({
                title: title,
                width: 990,
                height: 370,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            }); //}
            return false;
        }
        function ClosePopupFormula() {
            if ($("#PopupFormula").hasClass("ui-dialog-content")) {
                $("#PopupFormula").dialog("close");
            } else {
                console.error("PopupFormula dialog is not initialized yet.");
            }
        }

    </script>
    <%--   <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });
        }
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="col-md-12">
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Manage OnChange Criterias</h3>
                            <span id="lblStatus" style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 51px;"></span>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div class="fixTableHead">
                                            <asp:GridView ID="grdStepCrits" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                OnRowCommand="grdStepCrits_RowCommand" OnPreRender="grdStepCrits_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditCrit"
                                                                runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Seq No." ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Visit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVISIT" Text='<%# Bind("VISITNUM") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Criteria">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCritName" Text='<%# Bind("CritName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Set Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsetValue" Text='<%# Bind("SETVALUE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Derived">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIsDerived" Text='<%# Bind("ISDERIVED") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Derived Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIsDerivedValue" Text='<%# Bind("ISDERIVED_VALUE") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_FORM_SPECs', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                        <HeaderTemplate>
                                                            <label>Entered By Details</label><br />
                                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Entered By]</label><br />
                                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div>
                                                                <div>
                                                                    <asp:Label ID="ENTEREDBYNAME" runat="server" Text='<%# Bind("ENTEREDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="ENTERED_CAL_DAT" runat="server" Text='<%# Bind("ENTERED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="ENTERED_CAL_TZDAT" runat="server" Text='<%# Bind("ENTERED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                        <HeaderTemplate>
                                                            <label>Last Modified Details</label><br />
                                                            <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Modified By]</label><br />
                                                            <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                            <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div>
                                                                <div>
                                                                    <asp:Label ID="UPDATEDBYNAME" runat="server" Text='<%# Bind("UPDATEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="UPDATED_CAL_DAT" runat="server" Text='<%# Bind("UPDATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="UPDATED_CAL_TZDAT" runat="server" Text='<%# Bind("UPDATED_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteCrit"
                                                                runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this criteria :  ", Eval("Criteria")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
                        <div id="DivAuditTrail" style="font-size: small;">
                        </div>
                    </div>
                    <div class="box box-primary" style="min-height: 210px;">
                        <div class="box-header" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">Add OnChange Criterias for : &nbsp;<asp:Label ID="lblList" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <br />
                        <br />
                        <asp:HiddenField ID="hdnFieldname" runat="server" />
                        <asp:HiddenField ID="hdnVariablename" runat="server" />
                        <asp:UpdatePanel ID="uplformspec" runat="server">
                            <ContentTemplate>
                                <div class="box-body" id="divCrit">
                                    <div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                        Visit :</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="control">
                                                        <asp:DropDownList ID="ddlVISITID" class="form-control width300px required" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
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
                                        <div class="row" id="divrestrict" runat="server">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                        Restrict :</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:CheckBox ID="chkRestrict" runat="server" AutoPostBack="true" OnCheckedChanged="chkRestrict_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" id="divErrormsg" runat="server">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                        Error Message:</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:TextBox ID="txtERR_MSG" runat="server" TextMode="MultiLine" MaxLength="2000" Height="70px" Width="48%"
                                                        CssClass="form-control required"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" id="divSetValue" runat="server">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                        Set Value:</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:CheckBox ID="chkSetValue" runat="server" AutoPostBack="true" OnCheckedChanged="chkSetValue_CheckedChanged" />
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row" id="divIsderived" runat="server" visible="false">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                        Is Derived:</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:CheckBox ID="ChkIsDerived" runat="server" AutoPostBack="true" OnCheckedChanged="ChkIsDerived_CheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10" style="display: flex">
                                                    <asp:DropDownList ID="ddlFormula" runat="server" CssClass="form-control width200px" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlFormula_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;<asp:LinkButton ID="lbtnFormulaAdd" runat="server" OnClientClick="return OpenAddFormula();" ToolTip="Add Formula" Visible="false">
                                                        <i id="I1" runat="server" class="fa fa-plus"></i>
                                                    </asp:LinkButton>
                                                    <cc2:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="PopupFormula" TargetControlID="lbtnFormulaAdd" BackgroundCssClass="Background">
                                                    </cc2:ModalPopupExtender>
                                                    <asp:HiddenField runat="server" ID="hdnFormulaTypeValue" />
                                                    <asp:HiddenField ID="hdnOpenFormula" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:HiddenField runat="server" ID="hfValueIsDerivedValue" />
                                                    <asp:TextBox ID="TxtIsDerivedValue" runat="server" TextMode="MultiLine" Height="70px" Width="48%"
                                                        CssClass="form-control required" Visible="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" id="SETVALUEVISIBLE" runat="server" visible="false">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                    </label>
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="row" runat="server">
                                                        <div class="col-md-3 disp-none">
                                                            <asp:DropDownList ID="ddlFields" runat="server" CssClass="form-control width200px"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:HiddenField runat="server" ID="hfValueSetValue" />
                                                            <asp:TextBox ID="txtSetValue" runat="server" CssClass="OptionValues required form-control width200px" AutoPostBack="true" OnTextChanged="txtSetValue_TextChanged"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" id="SETCRITERIADIV">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                        Set Criteria :</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTField1" runat="server" CssClass="form-control width200px"
                                                                AutoPostBack="true" OnSelectedIndexChanged="drpLISTField1_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="drpLISTCondition1" runat="server" CssClass="form-control"
                                                                Width="100%">
                                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                                                <asp:ListItem Selected="True" Text="--Select Condition--" Value="-1"></asp:ListItem>
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
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4">
                                                    &nbsp;
                                               
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:Button ID="btnsubmit" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnsubmit_Click" />
                                                    <asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnUpdate_Click" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                       
                                                    <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                        OnClick="btncancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                                <asp:Panel ID="PopupFormula" runat="server" Style="display: none; width: 100%" CssClass="PopupCRITS1">
                                    <div class="modal-body" runat="server">
                                        <div id="ModelPopup3">
                                            <div class="box box-primary">
                                                <div class="box-header with-border d-flex justify-content-between align-items-center">
                                                    <h4 class="box-title" align="left">Add Formula
                                                    </h4>
                                                    <%--<button type="button" runat="server" class="close" onclick="btnfrmCancel_Click"><i style="color: red; padding:0;" class="fa fa-times close"></i></button>--%>
                                                   <asp:Button ID="btncancelpopup" runat="server" Text="Close"
                                CssClass="btn btn-danger btn-sm" Style="height: 30px; width: 60px; font-size: 14px; float:right;margin: 2px 2px 0px 0px;" OnClick="ClosePopupFormula" />
                                                </div>

                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblFormulaType" runat="server" Text="Enter Formula Type:" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txt_formulatype" CssClass="form-control-model required2" runat="server" Style="min-width: 250px;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblFormula" runat="server" Text="Enter Formula:" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:TextBox ID="txt_formulaval" CssClass="form-control-model required2" TextMode="MultiLine" runat="server" Style="min-width: 250px;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    </br>
                                                     <div class="row">
                                                         <div class="col-md-4">
                                                             &nbsp;
                                                         </div>
                                                         <div class="col-md-8">
                                                             <asp:Button ID="btnfrmsubmit" runat="server" Text="Submit" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen btn-sm cls-btnSave2" OnClick="btnfrmSubmit_Click" />
                                                             <asp:Button ID="btnfrmupdate" runat="server" Text="Update" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen btn-sm cls-btnSave2" Visible="false" OnClick="btnfrmUpdate_Click" />
                                                             &nbsp;&nbsp;
                            <asp:Button ID="btnfrmcancel" runat="server" Text="Cancel"
                                CssClass="btn btn-danger btn-sm" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnfrmCancel_Click" />
                                                         </div>
                                                     </div>
                                                </div>
                                            </br>
                                            </div>
                                            </br>
                                             <div class="box box-primary">
                                                 <div class="box-header with-border">
                                                     <h4 class="box-title" align="left">Records
                                                     </h4>
                                                 </div>
                                                 <div class="box-body">
                                                     <div align="center">
                                                         <div class="row">
                                                             <div style="width: 91%; min-height: auto; max-height: 170px; overflow: auto;">
                                                                 <div>
                                                                     <asp:GridView ID="grdViewFormula" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable" Style="width: 95%; margin-left: 25px;" OnRowCommand="grdViewFormula_RowCommand" OnPreRender="grdViewFormula_PreRender">
                                                                         <Columns>
                                                                             <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                                                 <ItemTemplate>
                                                                                     <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                         CommandName="EditFormula" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="align-center">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="Name" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Formula" HeaderStyle-CssClass="align-center">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="Formula" runat="server" Text='<%# Bind("Formula") %>'></asp:Label>
                                                                                 </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                                                 <ItemTemplate>
                                                                                     <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                                         CommandName="DeleteFormula" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnsubmit" />
                                <asp:PostBackTrigger ControlID="btnUpdate" />
                                <asp:PostBackTrigger ControlID="lbtnFormulaAdd" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <%--   <asp:UpdatePanel ID="upnlPopup" runat="server">
    <ContentTemplate>
        <cc2:modalpopupextender id="ModalPopupExtender1" runat="server" popupcontrolid="PopupFormula" targetcontrolid="lbtnFormulaAdd"
            backgroundcssclass="Background">
        </cc2:modalpopupextender>
        <asp:Panel ID="PopupFormula" runat="server" Style="display: none;" CssClass="Popup1">
            <div class="modal-body" runat="server">
                <div id="ModelPopup3">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <asp:Label ID="lblFormuleType" runat="server" Text="Enter Formule Type" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txt_formuletype" CssClass="form-control-model" runat="server" Style="min-width: 250px;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <asp:Label ID="lblFormula" runat="server" Text="Enter Formula" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txt_formulaval" CssClass="form-control-model" runat="server" Style="min-width: 250px;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-3">
                            &nbsp;
                        </div>
                        <div class="col-md-9">
                            <asp:Button ID="Button1" runat="server" Text="Submit" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen btn-sm cls-btnSave2" OnClick="btnfrmSubmit_Click" />
                            &nbsp;
                            &nbsp;
                            <asp:Button ID="Button2" runat="server" Text="Cancel"
                                CssClass="btn btn-DarkGreen btn-sm" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnfrmCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        </ContentTemplate>
    <Triggers>
       
    </Triggers>
</asp:UpdatePanel>--%>
    </form>

</body>
</html>
