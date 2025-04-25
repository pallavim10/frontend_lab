<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DM_SetOnsubmitCrits.aspx.cs"
    Inherits="CTMS.DM_SetOnsubmitCrits" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>WAI</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
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
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <script src="CommonFunctionsJs/ControlJS.js"></script>
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/CKEDITOR_Limited.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js"></script>
    <script src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <script src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">

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

            var nodes = document.getElementById("divVariableDeclaration").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            var nodes = document.getElementById("divDefineCondition").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }
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

    </script>
  <%--  <script type="text/javascript" src="js/MaxLength.min.js"></script>
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
                        <asp:HiddenField runat="server" ID="hfOLDVARIABLENAME" />
                    </div>
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Manage Criterias</h3>
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
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditCrit"
                                                                runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Visit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="VISIT" Text='<%# Bind("VISIT") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sequence No." ItemStyle-CssClass="txt_center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSEQNO" Text='<%# Bind("SEQNO") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ACTIONS" Text='<%# Bind("ACTIONS") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Criteria">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCrit" Text='<%# Bind("Criteria") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Error Message">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCritName" Text='<%# Bind("CritName") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_Onsubmit_CRITs', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
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
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteCrit" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this criteria :  ", Eval("Criteria")) %>'
                                                                runat="server" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    <div id="popup_AuditTrail" title="Audit Trail" class="disp-none">
                        <div id="DivAuditTrail" style="font-size: small;">
                        </div>
                    </div>
                    <asp:UpdatePanel ID="uplOnsubmit" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="box box-primary" style="min-height: 210px;">
                                <div class="box-header" style="float: left; top: 0px; left: 0px;">
                                    <h4 class="box-title" align="left">Add OnSubmit/OnLoad Criterias for : &nbsp;<asp:Label ID="lblList" runat="server"></asp:Label>
                                    </h4>
                                </div>
                                <br />
                                <br />
                                <asp:HiddenField ID="hdnFieldname" runat="server" />
                                <asp:HiddenField ID="hdnVariablename" runat="server" />
                                <asp:HiddenField ID="hdnCRIT_ID" runat="server" />
                                <asp:HiddenField ID="hdnVariableID" runat="server" />
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
                                                        Enter Sequence No. :</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:TextBox ID="txtSEQNO" runat="server" Width="10%" CssClass="form-control numeric required text-center"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                        Select Action :</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:DropDownList ID="ddlSelectAction" runat="server" AutoPostBack="true" CssClass="form-control width300px required" OnSelectedIndexChanged="ddlSelectAction_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Show message with Restriction OnSubmit" Value="Show message with Restriction OnSubmit"></asp:ListItem>
                                                        <asp:ListItem Text="Show message without Restriction OnSubmit" Value="Show message without Restriction OnSubmit"></asp:ListItem>
                                                        <asp:ListItem Text="Send Emails OnSubmit" Value="Send Emails OnSubmit"></asp:ListItem>
                                                        <asp:ListItem Text="Reference Note OnLoad" Value="Reference Note OnLoad"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" runat="server" visible="false">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                        Allowable :</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:CheckBox ID="chkAllowable" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row" runat="server" id="divErrMesg" visible="false">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>
                                                        Error Message/Reference Note :</label>
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:TextBox ID="txtERR_MSG" runat="server" TextMode="MultiLine" MaxLength="500" Height="70px" Width="48%"
                                                        CssClass="form-control required"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div id="divEmail" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-6">
                                                        <label>
                                                            Email IDs :</label>
                                                        <asp:GridView runat="server" ID="gvEmailds" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                            Style="width: 99%; border-collapse: collapse;">
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
                                                    <div class="col-md-6">
                                                        <div class="rows">
                                                            <div class="row">
                                                                <label>
                                                                    Email Subject :</label>
                                                                <asp:TextBox runat="server" ID="txtEmailSubject" Height="50px" Width="99%" TextMode="MultiLine"
                                                                    CssClass="form-control"> 
                                                                </asp:TextBox>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <label>
                                                                    Email Body :</label>
                                                                <asp:TextBox runat="server" ID="txtEmailBody" CssClass="ckeditor" Height="50%" TextMode="MultiLine"
                                                                    Width="99%"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="pull-right" style="margin-right: 15px;">
                                                    <div class="pull-right">

                                                        <asp:Button ID="lbtnsubmitDefine" runat="server" Text="Submit & Define Variables"
                                                            Style="color: white;" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnsubmitDefine_Click" />
                                                        <asp:Button ID="lbtnUpdateDefine" Text="Update & Define Variables" runat="server"
                                                            Style="color: white;" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                            OnClick="lbtnUpdateDefine_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="box box-group" id="divVariableDeclaration" visible="false" runat="server">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="box box-primary" style="min-height: 264px;">
                                                    <div class="box-header">
                                                        <h3 class="box-title">Define Variables</h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div align="left" style="margin-left: 0px">
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="Label5" class="label" runat="server" Text="Column Name :"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <div class="control">
                                                                            <asp:TextBox runat="server" ID="txtRuleVariableName" Width="200px" CssClass="form-control required1"> 
                                                                            </asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="Label17" class="label" runat="server" Text="Sequence No. :"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <div class="control">
                                                                            <asp:TextBox runat="server" ID="txtVariableSEQNO" Width="100px" CssClass="form-control numeric required1"> 
                                                                            </asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="lblDerived" class="label" runat="server" Text="Is Derived? :"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2 width180px">
                                                                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkDerived" AutoPostBack="true"
                                                                            OnCheckedChanged="chkDerived_CheckedChanged" />&nbsp;&nbsp;
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row" id="divDerivedTrue" runat="server" visible="false">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-4">
                                                                        <asp:Label ID="lblEnterFormula" class="label" runat="server" Text="Enter Formula :"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox runat="server" ID="txtFormula" TextMode="MultiLine" Width="300px" Height="200px"
                                                                            CssClass="form-control required1"> 
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="divDerivedFalse" runat="server">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-4">
                                                                            <asp:Label ID="Label8" class="label" runat="server" Text="Visit :"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="control">
                                                                                <asp:DropDownList ID="ddlVisit1" class="form-control width300px required1" runat="server">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-4">
                                                                            <asp:Label ID="Label9" class="label" runat="server" Text="Module :"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="control">
                                                                                <asp:DropDownList ID="ddlModule1" class="form-control width300px required1" runat="server"
                                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlModule1_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-4">
                                                                            <asp:Label ID="Label10" class="label" runat="server" Text="Field :"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="control">
                                                                                <asp:DropDownList ID="ddlField1" class="form-control width300px required1" runat="server">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-4">
                                                                            <asp:Label ID="Label16" class="label" runat="server" Text="Enter Condition (if any) :"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox runat="server" ID="txtVariableCondition" TextMode="MultiLine" Width="300px"
                                                                                Height="50px" CssClass="form-control"> 
                                                                            </asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-5">
                                                                    </div>
                                                                    <div class="col-md-7">
                                                                        <asp:LinkButton ID="lbtnSubmitVariableDec" Text="Submit" runat="server" Style="color: white;"
                                                                            CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="lbtnSubmitVariableDec_Click" />
                                                                        <asp:LinkButton ID="lbtnUpdateVariableDec" Text="Update" runat="server" Style="color: white;"
                                                                            Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="lbtnUpdateVariableDec_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:LinkButton ID="lbtnCancelVariableDec" Text="Cancel" runat="server" Style="color: white;"
                                                                    CssClass="btn btn-danger btn-sm" OnClick="lbtnCancelVariableDec_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="box box-primary" style="min-height: 300px;">
                                                    <div class="box-header with-border">
                                                        <h4 class="box-title" align="left">Records
                                                        </h4>
                                                    </div>
                                                    <div class="box-body">
                                                        <div align="left" style="margin-left: 0px">
                                                            <div class="rows">
                                                                <div class="fixTableHead">
                                                                    <asp:GridView ID="gvVariableDeclare" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                                        OnRowCommand="gvVariableDeclare_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none"
                                                                                HeaderText="ID" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="10" ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnEdit" CommandArgument='<%# Bind("ID") %>' CommandName="EditRule"
                                                                                        runat="server" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sequence No." HeaderStyle-Width="10" ItemStyle-Width="10"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Column Name" HeaderStyle-Width="20" ItemStyle-Width="20"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVariableName" runat="server" Text='<%# Bind("VariableName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Derived" HeaderStyle-Width="20" ItemStyle-Width="20"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDerived" Text='<%# Bind("Derived") %>' runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Audit Trail" HeaderStyle-Width="10" ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_Onsubmit_CRITs_Variables', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10" ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteRule"
                                                                                        OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this  field : ", Eval("VariableName")) %>'
                                                                                        runat="server" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="pull-right" style="margin-right: 15px;">
                                                        <asp:LinkButton ID="lbtnNextSetConditions" Text="Set Conditions" runat="server" Visible="false"
                                                            Style="color: white;" CssClass="btn btn-primary btn-sm" OnClick="lbtnNextSetConditions_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="box box-primary" id="divDefineCondition" runat="server" visible="false">
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
                                                <div class="col-md-6">
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnsubmit" Text="Submit" runat="server" Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave"
                                                        OnClick="btnsubmit_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlModule1" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="gvVariableDeclare" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="lbtnSubmitVariableDec" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="lbtnUpdateVariableDec" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="drpLISTField1" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="drpLISTField2" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="drpLISTField3" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="drpLISTField4" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="drpLISTField5" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ddlSelectAction" />
                            <asp:PostBackTrigger ControlID="lbtnsubmitDefine" />
                            <asp:PostBackTrigger ControlID="lbtnUpdateDefine" />
                            <asp:PostBackTrigger ControlID="btnsubmit" />
                            <asp:PostBackTrigger ControlID="lbtnNextSetConditions" />
                            <asp:PostBackTrigger ControlID="chkDerived" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
