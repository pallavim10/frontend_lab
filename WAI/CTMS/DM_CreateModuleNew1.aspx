<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_CreateModuleNew1.aspx.cs" Inherits="CTMS.DM_CreateModuleNew1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script src="js/CKEditor/ckeditor.js" type="text/javascript"></script>
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup1 {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            min-width: 500px;
            max-width: 650px;
        }

        h5 {
            background-color: #007bff;
            padding: 0.4em 1em;
            margin-top: 0px;
            font-weight: bold;
            color: white;
        }

        .modal-body {
            position: relative;
            padding: 0px;
        }
    </style>
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

        function REVIEW_HISTORY(element) {

            var MODULEID = "";

            if ($(element).closest('tr').find('td:eq(2)').find('span').text() == "") {
                MODULEID = '<%=Session["ID"] %>';
            }
            else {
                MODULEID = $(element).closest('tr').find('td:eq(2)').find('span').text();
            }

            $.ajax({
                type: "POST",
                url: "DM_CreateModuleNew1.aspx/REVIEW_HISTORY",
                data: '{MODULEID: "' + MODULEID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivReviewLogs').html(data.d)
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

            $("#popup_ReviewLogs").dialog({
                title: "Review Logs",
                width: 1100,
                height: 450,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });

            return false;
        }
    </script>
    <script runat="server"> 
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            // the following line is important 
            MasterPage master = this.Master;
        }

    </script>
    <script src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/jscript">

        function SetModuleCriteria(element) {

            var ID = $(element).closest('tr').find('td:eq(2)').find('span').html();
            var MODULENAME = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var FREEZE = $(element).closest('tr').find('td:eq(6)').find('span').html();

            var test = "DM_SET_MODULE_CRITERIA.aspx?MODULEID=" + ID + "&MODULENAME=" + MODULENAME + "&FREEZE=" + FREEZE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=600,width=1200";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;

        }

        function DisableDiv(Status) {
            var nodes = document.getElementById("divModule").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            $('#MainContent_btnCancelModule').removeAttr("disabled", '');
            $("#lblStatus").text(Status + ".");
        }

        function validateCheckButton() {
            var inputElems = $(".Systems input[type=checkbox]").toArray();

            var count = 0;

            for (var i = 0; i < inputElems.length; i++) {

                if (inputElems[i].type === "checkbox" && inputElems[i].checked === true) {
                    count++;
                }
            }

            if (count < 1) {
                alert("Select at least one system");
                event.preventDefault();
            }
        }

        var TABLENAME;
        function showAuditTrail(TABLENAME, element) {

            var ID = $(element).closest('tr').find('td').eq(2).text().trim();
            var TABLE_NAME = TABLENAME;

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/DB_showAuditTrail",
                data: '{TABLENAME: "' + TABLE_NAME + '",ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                    else {
                        $('#DivAuditTrail').html(data.d)
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

            $("#popup_AuditTrail").dialog({
                title: "Audit Trail",
                width: 900,
                height: 450,
                modal: true,
                buttons: {
                    "Close": function () { $(this).dialog("close"); }
                }
            });
            return false;


        }

    </script>
 <script type="text/javascript">
        function toUpperCaseTextBox(textbox) {
            textbox.value = textbox.value.toUpperCase();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box-body">
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="hfOLDMODULE" />
            </div>
            <div class="box box-info">
                <div class="box-header">
                    <h4 class="box-title">Add Module
                    </h4>
                    <span id="lblStatus" style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 54px;"></span>
                    <asp:HiddenField ID="hdnModuleStatus" runat="server" />
                    <div class="pull-right" style="margin-right: 10px; padding-top: 3px; display: inline-flex;">

                        <asp:Button ID="btnSubmitModule" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                            OnClick="btnSubmitModule_Click" OnClientClick="return validateCheckButton();" TabIndex="23" />&nbsp;&nbsp;
                                        <asp:Button ID="btnUpdateModule" Text="Update" OnClientClick="return validateCheckButton();" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                            OnClick="btnUpdateModule_Click" TabIndex="24" />&nbsp;&nbsp;
                                        <asp:Button ID="btnCancelModule" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                            OnClick="btnCancelModule_Click" TabIndex="25" />&nbsp;&nbsp;


                        <asp:Button ID="btnReviewLogs" Text="Review Logs" runat="server" CssClass="btn btn-DARKORANGE btn-sm" Visible="false"
                            OnClientClick="return REVIEW_HISTORY(this);" TabIndex="26" />&nbsp;&nbsp;&nbsp;
                         <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional">
                             <ContentTemplate>
                                 <asp:Button ID="btnOpenForEdit" Text="Open For Edit" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                                     OnClick="btnOpenForEdit_Click" TabIndex="27" />
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="btnOpenForEdit" EventName="Click" />
                             </Triggers>
                         </asp:UpdatePanel>
                    </div>
                </div>
                <div class="box-body" id="divModule">
                    <div class="form-group">
                        <div class="row margin10px">
                            <div class="col-md-12">
                                <!-- Horizontal Form -->
                                <div class="box box-danger">
                                    <!-- form start -->
                                    <div class="box-body">
                                        <div class="row margin-top6">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Module Name:</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtModuleName" ValidationGroup="section" runat="server"
                                                            CssClass="form-control width300px required1" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Sequence No:</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtModuleSeqNo" ValidationGroup="section" runat="server"
                                                            CssClass="form-control numeric required1 text-center" MaxLength="5" TabIndex="2"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row margin-top6">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Domain Name:</label>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtDomain" ValidationGroup="section" runat="server" MaxLength="5" oninput="toUpperCaseTextBox(this);"
                                                            CssClass="form-control required1 text-center" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>
                        </div>
                        <div class="row margin10px">
                            <div class="col-md-12">
                                <!-- Horizontal Form -->
                                <div class="box box-warning">
                                    <div class="box-header with-border">
                                        <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; text-decoration: underline;">Select System:</h4>
                                    </div>
                                    <!-- form start -->
                                    <div class="box-body">
                                        <div class="row margin-left20">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSafety" CssClass="Systems" AutoPostBack="true" OnCheckedChanged="chkAllSystem_CheckedChanged" TabIndex="4" />&nbsp;
                                         <label>Pharmacovigilance</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkDM" CssClass="Systems" AutoPostBack="true" OnCheckedChanged="chkAllSystem_CheckedChanged" TabIndex="5" />&nbsp;&nbsp;
                                         <label>Data Management</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkESource" CssClass="Systems" AutoPostBack="true" OnCheckedChanged="chkAllSystem_CheckedChanged" TabIndex="6" />&nbsp;&nbsp;
                                          <label>eSource</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkIWRS" CssClass="Systems" TabIndex="7" />&nbsp;&nbsp;
                                          <label>IWRS</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row margin-left20">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkePRO" CssClass="Systems" TabIndex="8" />&nbsp;&nbsp;
                                                    <label>Solicited Response</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkPD" CssClass="Systems" TabIndex="9" />&nbsp;&nbsp;
                                                    <label>Protocol Deviation</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" CssClass="Systems" ID="chkExternal_Independent" TabIndex="10" />&nbsp;&nbsp;
                                         <label>External/Independent</label>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" runat="server" id="CTMS" visible="false">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" CssClass="Systems" ID="chkCTMS" TabIndex="11" />&nbsp;&nbsp;
                                          <label>CTMS</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <!-- /.box -->
                            </div>
                        </div>
                        <div class="row margin10px">
                            <div class="col-md-6" runat="server" id="divBlindedUnbl" visible="false">
                                <!-- Horizontal Form -->
                                <div class="box box-success">
                                    <div class="box-header with-border">
                                        <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; text-decoration: underline;">Blinded/Unblinded:</h4>
                                    </div>
                                    <!-- form start -->
                                    <div class="box-body">

                                        <div class="row margin-left20">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkblinded" TabIndex="12" />&nbsp;&nbsp;
                                         <label>Blinded</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUnblinded" TabIndex="13" />&nbsp;&nbsp;
                                    <label>Unblinded</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>
                            <div class="col-md-6" runat="server" id="divINVMedOpinion" visible="false">
                                <!-- Horizontal Form -->
                                <div class="box box-success">
                                    <div class="box-header with-border">
                                        <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; text-decoration: underline;"></h4>
                                    </div>

                                    <!-- form start -->
                                    <div class="box-body">

                                        <div class="row margin-left20">
                                            <div class="col-sm-6" id="divmedOpinion" runat="server" visible="false">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMEDOP" TabIndex="14" />&nbsp;&nbsp;
                                         <label>Med. Opinion</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" id="divINVeCRF" runat="server" visible="false">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="Check_eCRF" TabIndex="15" />&nbsp;&nbsp;
                                    <label>eCRF INV Sign Off</label>
                                                </div>
                                            </div>
                                        </div>



                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <!-- /.box -->

                            </div>
                        </div>


                        <div class="row margin10px">



                            <div class="col-md-6" runat="server" id="divhelpInstruction" visible="false">
                                <!-- Horizontal Form -->
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; text-decoration: underline;">Help (Instructions):</h4>
                                    </div>

                                    <!-- form start -->
                                    <div class="box-body">
                                        <div class="row margin-left20">
                                            <div class="col-sm-6" id="divhelpCrf" runat="server" visible="false">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkHelpData" AutoPostBack="true"
                                                        OnCheckedChanged="chkHelpData_CheckedChanged" TabIndex="16" />&nbsp;&nbsp;
                                                    <label>CRF</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" id="divhelpSefty" runat="server" visible="false">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSafetyHelpData" AutoPostBack="true"
                                                        OnCheckedChanged="chkSafetyHelpData_CheckedChanged" TabIndex="17" />
                                                    <label>Pharmacovigilance</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <!-- /.box -->

                            </div>


                            <div class="col-md-6" runat="server" id="divMultiple" visible="false">
                                <!-- Horizontal Form -->
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; text-decoration: underline;">Multiple Entry:</h4>
                                    </div>

                                    <!-- form start -->
                                    <div class="box-body">

                                        <div class="row margin-left20">
                                            <div class="col-sm-6" id="divmultiCRF" runat="server" visible="false">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMultiple" AutoPostBack="true" OnCheckedChanged="chkMultiple_CheckedChanged" TabIndex="18" />&nbsp;&nbsp;
                                    <label>CRF</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" id="divmultiSefty" runat="server" visible="false">
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMultipleSAE" AutoPostBack="true" OnCheckedChanged="chkMultipleSAE_CheckedChanged" TabIndex="19" />&nbsp;&nbsp;
                                    <label>Pharmacovigilance</label>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row margin-left15" runat="server" id="lbllimit" visible="false">

                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Limit(If Required):</label>

                                                    <div class="col-sm-4">
                                                        <asp:TextBox Style="width: 50px;" ID="txtLimit" ValidationGroup="section" runat="server"
                                                            CssClass="form-control numeric text-center" TabIndex="20"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>



                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <!-- /.box -->

                            </div>
                        </div>



                        <div id="divHelpData" runat="server" visible="false" style="margin-top: 7px; margin-left: 19px; margin-right: 21px;">
                            <div class="box box-info" style="min-height: 300px;">
                                <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                    <h4 class="box-title" align="left">Add CRF Help (Instructions)
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="txtData" name="message" CssClass="ckeditor required1" Height="100%" TextMode="MultiLine"
                                                Width="100%" ValidateRequestMode="Disabled" TabIndex="21"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divSAEHelpData" runat="server" visible="false" style="margin-top: 7px; margin-left: 19px; margin-right: 21px;">
                            <div class="box box-info" style="min-height: 300px;">
                                <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                                    <h4 class="box-title" align="left">Add Pharmacovigilance Help (Instructions)
                                    </h4>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:TextBox runat="server" ID="txtSAEHelpData" CssClass="ckeditor required1" Height="100%"
                                                TextMode="MultiLine" Width="100%" TabIndex="22"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <br />
                    </div>
                </div>
            </div>
            <div class="box box-primary" style="margin-top: 7px;">
                <div class="box-header with-border">
                    <h4 class="box-title" align="left">Records
                    </h4>
                    <div class="pull-right" style="padding-top: 4px; margin-right: 10px;">
                        <asp:LinkButton runat="server" ID="btnUploadInstruction" Text="Upload Help (Instructions)" CssClass="btn btn-primary btn-sm" ForeColor="White" Font-Bold="true" PostBackUrl="DB_UPLOAD_HELP_INSTRUCTION.aspx" TabIndex="28"> Upload Instruction&nbsp;<span class="glyphicon glyphicon-upload 2x"></span></asp:LinkButton>

                        <asp:LinkButton runat="server" ID="lbtnExportCrfIntruc" OnClick="lbtnExportCrfIntruc_Click"
                            Text="Export Instructions" CssClass="btn btn-success btn-sm" ForeColor="White" Font-Bold="true" TabIndex="29"> Export Instructions&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>

                        <asp:LinkButton runat="server" ID="btnExportExcel" OnClick="btnExportExcel_Click" Visible="false" Text="Export Module" CssClass="btn btn-info btn-sm" ForeColor="White" Font-Bold="true" TabIndex="30"> Export Module&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>

                    </div>
                </div>

                <div class="box-body">
                    <div align="left">
                        <div>
                            <div align="left" style="margin-left: 5px;">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-bottom: 10px;">
                                            <div class="col-md-2" style="width: 130px;">
                                                <label>Select System :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList runat="server" ID="drpSystem" Style="width: 250px;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpSystem_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="rows">
                                    <div class="fixTableHead">
                                        <asp:GridView ID="grdModule" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                            Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdModule_RowCommand"
                                            OnRowDataBound="grdModule_RowDataBound" OnPreRender="grdModule_PreRender">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                            CommandName="EditModule" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Set Criteria" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnSetCriteria" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            ToolTip="Set Criteria" OnClientClick="return SetModuleCriteria(this)">  <i class="fa fa-cog"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                    ItemStyle-CssClass="disp-none">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="DM" Value='<%# Eval("DM") %>' />
                                                        <asp:HiddenField runat="server" ID="eSource" Value='<%# Eval("eSource") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_MODULENAME', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                            CommandName="DeleteModule" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this module : ", Eval("MODULENAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Review Logs" ItemStyle-CssClass="txt_center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnReviewHistory" runat="server" ToolTip="Review Logs" OnClientClick="return REVIEW_HISTORY(this);">
                                        <i class="fa fa-book fa-lg" style="font-size:large;" aria-hidden="true"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Freeze Status" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FREEZE" runat="server" Text='<%# Bind("FREEZE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sequence No" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Module Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Domain Name" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Domain" runat="server" Text='<%# Bind("DOMAIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Multiple Entry CRF" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MULTIPLEYN" runat="server" CommandArgument='<%# Eval("MULTIPLEYN") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconMULTIPLEYN" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Multiple Entry Pharmacovigilance" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MULTIPLEYNSAE" runat="server" CommandArgument='<%# Eval("MULTIPLEYN_SAE") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconMULTIPLEYNSAE" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pharmacovigilance" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPharmacovigilance" runat="server" CommandArgument='<%# Eval("Safety") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconPharmacovigilance" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Data Management" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDataManagement" runat="server" CommandArgument='<%# Eval("DM") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconDataManagement" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="eSource" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbleSource" runat="server" CommandArgument='<%# Eval("eSource") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconeSource" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IWRS" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIWRS" runat="server" CommandArgument='<%# Eval("IWRS") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconIWRS" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Solicited Response" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblePRO" runat="server" CommandArgument='<%# Eval("ePRO") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconePRO" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Protocol Deviation" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPD" runat="server" CommandArgument='<%# Eval("PD") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconPD" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="External/Independent" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEXT" runat="server" CommandArgument='<%# Eval("EXT") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconEXT" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Blinded" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBlinded" runat="server" CommandArgument='<%# Eval("BLINDED") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconBlinded" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unblinded" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnblinded" runat="server" CommandArgument='<%# Eval("UNBLINDED") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconUnblinded" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Med. Opinion" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMedop" runat="server" CommandArgument='<%# Eval("MEDOP") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconMedop" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="INV Sign Off Required" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbleCRF_SignOff" runat="server" CommandArgument='<%# Eval("eCRF_SignOff") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconeCRF_SignOff" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Help (Instructions) CRF" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHelp_CRF" runat="server" CommandArgument='<%# Eval("Help_CRF") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconHelp_CRF" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Help (Instructions) Pharmacovigilance" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHelp_Pharmacovigilance" runat="server" CommandArgument='<%# Eval("Help_Pharmacovigilance") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconHelp_Pharmacovigilance" runat="server" class="fa fa-check"></i></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Limit" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLIMIT" runat="server" CommandArgument='<%# Eval("LIMIT") %>' Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconLIMIT" runat="server" class="fa fa-check"></i></asp:Label>
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

        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="Panel1" TargetControlID="Button1"
            BackgroundCssClass="Background">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" Style="display: none;" CssClass="Popup1">
            <asp:Button runat="server" ID="Button1" Style="display: none" />
            <asp:UpdatePanel ID="updPnlIDDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <h5 class="heading">Open For Edit Reason</h5>
                    <div class="modal-body" runat="server" style="padding: 10px;">
                        <div id="ModelPopup3">
                            <div class="row">
                                <div style="width: 100%; height: auto; overflow: auto;">
                                    <div>
                                        <label id="lblEditForEditHeader" runat="server" style="color: blue; margin-left: 3%;"></label>
                                    </div>
                                    <asp:ListView ID="lstOpenForEditSystems" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <ul>
                                                        <li>
                                                            <asp:Label runat="server" ID="lblOpenForEditSystemName" Text='<%# Bind("SystemName") %>' ForeColor="Maroon"></asp:Label>
                                                        </li>
                                                    </ul>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:TextBox runat="server" ID="txtOpenForEditReason" placeholder="Please enter comment to open for edit module for above systems...."
                                            TextMode="MultiLine" CssClass="form-control required5" Style="width: 575px; height: 131px;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitEditForEdit" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;"
                                        CssClass="btn btn-DarkGreen btn-sm cls-btnSave5" Text="Submit" OnClick="btnSubmitEditForEdit_Click" />
                                    &nbsp;
                            &nbsp;
                                <asp:Button ID="btnUpdateEdit" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;"
                                    CssClass="btn btn-DarkGreen btn-sm cls-btnSave5" Text="Submit" Visible="false" OnClick="btnSubmitEditForEdit_Click" />
                                    <asp:Button ID="btnCancelEditForEdit" runat="server" Text="Cancel"
                                        CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelEditForEdit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
