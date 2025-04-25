<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_CreateModuleNew.aspx.cs" Inherits="CTMS.DM_CreateModuleNew" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndexPost.js"></script>
    <script type="text/jscript">
        function DisableDiv(Status) {
            var nodes = document.getElementById("MainContent_divshowsystem").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            var nodes = document.getElementById("MainContent_drpModule").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = false;
            }

            $('#MainContent_drpModule').removeAttr("disabled", '');
            $('#MainContent_btnCancelField').removeAttr("disabled", '');
            $("#lblStatus").text(Status + ".");

            var nodes = document.getElementById("MainContent_DivRecords").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            var nodes = document.getElementById("MainContent_DivMasterData").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            var nodes = document.getElementById("MainContent_DivManageMap").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }
        }

        function countCheckboxes() {
            var inputElems = $("input[type=checkbox][id*=Chk_VISIT]").toArray()

            var count = 0;

            for (var i = 0; i < inputElems.length; i++) {

                if (inputElems[i].type === "checkbox" && inputElems[i].checked === true) {
                    count++;
                }
            }

            if (count < 1) {
                alert("Select at least one visit");
                event.preventDefault();
            }
        }

        function AddDrpDownData(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var FIELDNAME = $(element).closest('tr').find('td:eq(7)').find('span').html();
            var VARIABLENAME = $(element).closest('tr').find('td:eq(4)').find('span').html();
            var CONTROL_TYPE = $(element).closest('tr').find('td:eq(5)').find('span').html();
            var FREEZE = $('#MainContent_hdnModuleStatus').val();
            var PGL_TYPE = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var MODULEID = $("#MainContent_drpModule option:selected").val();
            var SYSTEM = $("#MainContent_drpSystem option:selected").val();

            var test = "DM_AddDrpDownData.aspx?ID=" + ID + "&VARIABLENAME=" + VARIABLENAME + "&CONTROL=" + CONTROL_TYPE + "&FREEZE=" + FREEZE + "&PGL_TYPE=" + PGL_TYPE + "&MODULEID=" + MODULEID + "&SYSTEM=" + SYSTEM;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=1200";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddDrpDownData_LINKED(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var FIELDNAME = $(element).closest('tr').find('td:eq(7)').find('span').html();
            var VARIABLENAME = $(element).closest('tr').find('td:eq(4)').find('span').html();
            var CONTROL_TYPE = $(element).closest('tr').find('td:eq(5)').find('span').html();
            var FREEZE = $('#MainContent_hdnModuleStatus').val();
            var PGL_TYPE = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var MODULEID = $("#MainContent_drpModule option:selected").val();
            var SYSTEM = $("#MainContent_drpSystem option:selected").val();

            var test = "DM_AddDrpDownData_LINKED.aspx?ID=" + ID + "&VARIABLENAME=" + VARIABLENAME + "&CONTROL=" + CONTROL_TYPE + "&FREEZE=" + FREEZE + "&PGL_TYPE=" + PGL_TYPE + "&MODULEID=" + MODULEID + "&SYSTEM=" + SYSTEM;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=1200";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function View_Map() {


            var MODULEID = $("#MainContent_drpModule option:selected").val();
            var MODULENAME = $("#<%= drpModule.ClientID %> option:selected").text();
            if (MODULEID != "0") {
                var test = "DM_Mappings.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&VIEWMAPING=1" + "&SYSTEM=" + $("#MainContent_drpSystem option:selected").val();;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

        function set_FieldColor(element) {
            var fcolor = element.value;
            $('#<% =hfFieldColor.ClientID %>').attr('value', fcolor);
        }

        function set_AnsColor(element) {
            var acolor = element.value;
            $('#<% =hfAnsColor.ClientID %>').attr('value', acolor);
        }

        function Set_FormSpec(element) {
            var FIELDID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var FIELDNAME = $(element).closest('tr').find('td:eq(7)').find('span').html();
            var VARIABLENAME = $(element).closest('tr').find('td:eq(4)').find('span').html();
            var MODULEID = $("#MainContent_drpModule option:selected").val();
            var FREEZE = $('#MainContent_hdnModuleStatus').val();

            var test = "DM_SETUP_FORM_SPECs.aspx?FIELDID=" + FIELDID + "&VARIABLENAME=" + VARIABLENAME + "&MODULEID=" + MODULEID + "&FREEZE=" + FREEZE;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1350";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Set_OnSubmitCRITs(element) {
            var MODULEID = $("#MainContent_drpModule option:selected").val();
            var MODULENAME = $("#MainContent_drpModule option:selected").text();
            var SYSTEM = $("#MainContent_drpSystem option:selected").text();
            var FREEZE = $('#MainContent_hdnModuleStatus').val();

            var test = "DM_SetOnsubmitCrits.aspx?MODULEID=" + MODULEID + "&MODULENAME=" + MODULENAME + "&FREEZE=" + FREEZE + "&SYSTEM=" + SYSTEM;
            window.open(test, '_blank');
            return false;

        }

        function Set_LabDefault(element) {

            var ID = $("#MainContent_drpModule option:selected").val();
            var MODULENAME = $("#<%= drpModule.ClientID %> option:selected").text();
            var FREEZE = $('#MainContent_hdnModuleStatus').val();

            var test = "DM_SetLabDefault.aspx?ID=" + ID + "&MODULENAME=" + MODULENAME + "&FREEZE=" + FREEZE;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=1350";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function Set_CodeMapping(element) {

            var ID = $("#MainContent_drpModule option:selected").val();
            var FREEZE = $('#MainContent_hdnModuleStatus').val();

            var test = "DM_CodeMapping.aspx?ID=" + ID + "&FREEZE=" + FREEZE;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=550,width=1350";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;

        }

        function REVIEW_HISTORY(element) {

            var MODULEID = $("#MainContent_drpModule option:selected").val();

            $.ajax({
                type: "POST",
                url: "DM_CreateModuleNew.aspx/REVIEW_HISTORY",
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

    <script type="text/javascript">

        function CheckStatusLogsBeforeDelete(id, fieldName, ENTEREDDAT, buttonClientID) {

            // Check the selected value of drpSystem
            var systemValue = document.getElementById('<%= drpSystem.ClientID %>').value;

            if (systemValue === "Data Management" || systemValue === "eSource") {
                // Proceed with AJAX check if the system is Data Management or eSource
                $.ajax({
                    type: "POST",
                    url: "DM_CreateModuleNew.aspx/CheckStatusLogs",
                    data: JSON.stringify({ ENTEREDDAT: ENTEREDDAT }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        if (response.d) {
                            // Data exists - show the delete popup
                            ShowDeletePopup(id, fieldName);
                        } else {
                            // No data - ask for confirmation before deletion
                            if (confirm('Are you sure you want to permanently delete this item?')) {
                                // Trigger GridView RowCommand with CommandName "DeleteModule"
                                __doPostBack('<%= grdField.UniqueID %>', 'DeleteField$' + id);
                            }
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error('Error: ' + error);
                    }
                });

            } else {
                // For other systems, show direct confirmation
                if (confirm('Are you sure you want to permanently delete this item?')) {
                    __doPostBack('<%= grdField.UniqueID %>', 'DeleteField$' + id);
                }
            }
            return false; // Prevent postback until AJAX completes
        }

        function ShowDeletePopup(id, fieldName) {
            // Set the hidden field values with the ID and Variable Name
            document.getElementById('<%= hdnDeleteFieldID.ClientID %>').value = id;
            document.getElementById('<%= hdnVariableName.ClientID %>').value = fieldName;

            // Trigger the hidden button to show the modal popup
            document.getElementById('<%= btnShowDeletePopup.ClientID %>').click();
            return false; // Prevent postback
        }

        function ConfirmDeleteAction() {
            var permanentDelete = document.getElementById('<%= rdoPermanentDelete.ClientID %>').checked;
            var prospectiveDelete = document.getElementById('<%= rdoProspectiveDelete.ClientID %>').checked;

            if (permanentDelete) {
                var isConfirmed = confirm('Are you sure you want to permanently delete this item?');
                if (isConfirmed) {
                    // Trigger the postback for permanent delete
                    __doPostBack('<%= btnConfirmDelete.UniqueID %>', '');
                    // Hide modal after confirmation
                    document.getElementById('<%= pnlDeleteConfirmation.ClientID %>').style.display = 'none';
                }
                return false; // Prevent default postback
            } else if (prospectiveDelete) {
                // Trigger the postback for prospective delete
                __doPostBack('<%= btnConfirmDelete.UniqueID %>', '');
                // Hide modal after confirmation
                document.getElementById('<%= pnlDeleteConfirmation.ClientID %>').style.display = 'none';
                return false;
            } else {
                alert('Please select an option!');
                return false; // Prevent postback if no option is selected
            }
        }

        function CancelDelete() {
            // Close the confirmation modal without doing anything
            document.getElementById('<%= pnlDeleteConfirmation.ClientID %>').style.display = 'none';
        }

    </script>

    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {

            if ($('#MainContent_hfFieldColor').val() != "") {
                $('#FieldColor').attr('value', $('#MainContent_hfFieldColor').val());
            }

            if ($('#MainContent_hfAnsColor').val() != "") {
                $('#AnsColor').attr('value', $('#MainContent_hfAnsColor').val());
            }

            $('.select').select2();

            $(".numeric").on("keypress keyup blur", function (event) {
                $(this).val($(this).val().replace(/[^\d].+/, ""));
                if ((event.which < 48 || event.which > 57)) {
                    event.preventDefault();
                }
            });

            //only for numeric value
            $('.numeric').keypress(function (event) {

                if (event.keyCode == 8 || event.keyCode == 9 || event.charCode == 48 || event.charCode == 49 || event.charCode == 50 || event.charCode == 51
                    || event.charCode == 52 || event.charCode == 52 || event.charCode == 53 || event.charCode == 54 || event.charCode == 55 || event.charCode == 56 || event.charCode == 57) {
                    // let it happen, don't do anything
                    return true;
                }
                else {
                    event.preventDefault();
                }
            });

            $(".Datatable").dataTable({
                "bSort": false,
                "ordering": false,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true,
                fixedHeader: true
            });

            $('#MainContent_drpModule').change(function () {
                $('#MainContent_DivRecords').focus();
            });
        }

        function countMasterCheckboxes() {
            var inputElems = $("input[type=checkbox][id*=Chk_Sel_Fun]").toArray()

            var count = 0;

            for (var i = 0; i < inputElems.length; i++) {

                if (inputElems[i].type === "checkbox" && inputElems[i].checked === true) {
                    count++;
                }
            }

            if (count < 1) {
                alert("Select at least one master data field.");
                event.preventDefault();
            }
        }
    </script>





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

        /*    .modal-body {
            position: relative;
            padding: 0px;
        }


        .modalPopup {
            padding: 20px;
            background-color: white;
            border: 1px solid #ccc;
            text-align: center;
        }

        .modalBackground {
            background-color: black;
            opacity: 0.7;
        }*/
    </style>

    <style>
        /* Overlay style (blurred background) */
        .modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent black */
            backdrop-filter: blur(5px); /* Apply blur effect */
            z-index: 1; /* Behind the modal */
        }

        /* Modal container styles */
        .modalPopup {
            padding: 20px;
            background-color: white;
            border: 1px solid #ccc;
            border-radius: 8px;
            text-align: center;
            width: 450px;
            max-width: 100%;
            margin: 0 auto;
            position: relative;
            z-index: 2;
        }

        .modal-title {
            font-size: 22px;
            margin-bottom: 15px;
            margin-top: 0;
        }

        .radio-options {
            display: flex;
            justify-content: space-evenly;
            align-items: center;
            margin-bottom: 20px;
        }

        .radio-item {
            display: flex;
            align-items: center;
            gap: 8px;
            font-size: 16px;
        }

        .button-group {
            display: flex;
            justify-content: center;
            gap: 25px;
            margin-top: 10px;
        }

        .txtVariableName {
            text-transform: uppercase;
        }
    </style>
    <script type="text/javascript">
        function toUpperCaseTextBox(textbox) {
            textbox.value = textbox.value.toUpperCase();
        }
    </script>
    <%--  <script  type="text/javascript" src="js/MaxLength.min.js"></script>
    <script  type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        <asp:HiddenField runat="server" ID="hfOLDVARIABLENAME" />
                        <asp:HiddenField ID="hdnModuleStatus" runat="server" />
                    </div>
                    <div class="box box-primary" id="divField" runat="server">
                        <div class="box-header with-border">
                            <h4 class="box-title" align="left">Add Field</h4>
                            <span id="lblStatus" style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 89px;"></span>
                            <div class="pull-right" style="margin-right: 10px; padding-top: 3px; display: inline-flex;">
                                <asp:Button ID="btnReviewLogs" Text="Review Logs" runat="server" CssClass="btn btn-DARKORANGE btn-sm" Visible="false"
                                    OnClientClick="return REVIEW_HISTORY(this);" />&nbsp;&nbsp;&nbsp;
                                 
                                <asp:UpdatePanel runat="server" ID="upnlBtn" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:Button ID="btnSendToReview" Text="Send For Review" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                                            OnClick="btnSendToReview_Click" />
                                        <asp:Button ID="btnOpenForEdit" Text="Open For Edit" runat="server" CssClass="btn btn-success btn-sm" Visible="false"
                                            OnClick="btnOpenForEdit_Click" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSendToReview" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnOpenForEdit" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                &nbsp;&nbsp;
                               
                                <asp:LinkButton runat="server" ID="lbtnExportCrfIntruc" OnClick="lbtnExportCrfIntruc_Click"
                                    Text="Export Instructions" Visible="false" CssClass="btn btn-warning btn-sm" ForeColor="White" Font-Bold="true"> Export Instructions&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                &nbsp;&nbsp;
                               
                                <asp:LinkButton ID="lbtnExportSpecs" runat="server" Text="Export Specs" OnClick="lbtnExportSpecs_Click" CssClass="btn btn-info btn-sm" ForeColor="white" Font-Bold="true"> Export Specs&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                            </div>
                            <div class="pull-right" style="padding-top: 4px; margin-right: 10px;">

                                <asp:Button ID="btnSubmitField" Text="Submit" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                    OnClick="btnSubmitField_Click" />&nbsp;
                                               
                                <asp:Button ID="btnUpdateField" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                    OnClick="btnUpdateField_Click" />&nbsp;&nbsp;
                                            
                                <asp:Button ID="btnCancelField" Text="Cancel" Visible="false" runat="server" CssClass="btn btn-danger btn-sm"
                                    OnClick="btnCancelField_Click" />
                            </div>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 7px;">

                                <div class="row margin10px">
                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label class="col-sm-4 control-label" style="padding-right: 0;">Select System:</label>
                                            <div class="col-sm-8 text-left" style="padding-left: 0;">
                                                <asp:DropDownList runat="server" ID="drpSystem" Style="width: 150%;" CssClass="form-control required1" AutoPostBack="true"
                                                    OnSelectedIndexChanged="drpSystem_SelectedIndexChanged" TabIndex="1">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div id="divshowsystem" runat="server" visible="false">

                                    <div class="row" style="margin-left: 1px; margin-right: 10px;">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-4 control-label" style="padding-right: 0;">Select Module:</label>
                                                <div class="col-sm-8 text-left" style="padding-left: 0;">
                                                    <asp:DropDownList Style="width: 150%" ID="drpModule" runat="server" class="form-control select drpControl required2 "
                                                        AutoPostBack="True" OnSelectedIndexChanged="drpModule_SelectedIndexChanged" TabIndex="2">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-1">
                                            <div class="form-group row">
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-5 control-label" style="padding-right: 20px; text-align: right">Enter Field Name:</label>
                                                <div class="col-sm-7 text-left" style="padding-left: 0;">
                                                    <asp:TextBox ID="txtFieldName" Style="width: 240%" ValidationGroup="section" runat="server" CssClass="form-control required2" MaxLength="10000" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-left: 1px; margin-right: 10px;">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-4 control-label" style="padding-right: 0;">Variable Name:</label>
                                                <div class="col-sm-8 text-left" style="padding-left: 0;">
                                                    <asp:TextBox ID="txtVariableName" ValidationGroup="section" Width="100%" OnTextChanged="txtVariableName_TextChanged" AutoPostBack="true" MaxLength="50"
                                                        runat="server" CssClass="form-control required2" oninput="toUpperCaseTextBox(this);" TabIndex="4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-4 control-label" style="padding-right: 0;">Enter Seq No:</label>
                                                <div class="col-sm-8 text-left" style="padding-left: 0;">
                                                    <asp:TextBox ID="txtSeqno" MaxLength="3" ValidationGroup="section" Width="100%"
                                                        runat="server" CssClass="form-control numeric required2 text-center" TabIndex="5"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4" id="divControl" runat="server" visible="true">
                                            <div class="form-group row">
                                                <label class="col-sm-5 control-label pull-left" style="text-align: right">Select Control Type:</label>
                                                <div class="col-sm-7 text-left" style="padding-left: 0;">
                                                    <asp:HiddenField ID="hdnOldControlType" runat="server" />
                                                    <asp:DropDownList ID="drpSCControl" AutoPostBack="true" runat="server" Width="100%"
                                                        class="form-control drpControl required2" ValidationGroup="SubCheklist" Visible="true"
                                                        OnSelectedIndexChanged="drpSCControl_SelectedIndexChanged" TabIndex="6">
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
                                                        <asp:ListItem Text="Child Module" Value="ChildModule">
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="display: flex; align-items: center; flex-wrap: wrap;" runat="server" id="DivDesc" visible="true">
                                        <div class="col-md-12" style="display: flex; align-items: center; width: 100%; flex-wrap: wrap;">

                                            <!-- Label for Description -->
                                            <div class="col-md-2" style="flex: 1; max-width: 110px; min-width: 100px;">
                                                <label>
                                                    Description:</label>
                                            </div>

                                            <!-- TextBox for Description -->
                                            <div class="col-md-6" style="flex: 1; max-width: 500px; min-width: 200px;">

                                                <asp:TextBox Height="50" Style="width: 100%;" TextMode="MultiLine" ID="txtDescrip" MaxLength="2000"
                                                    runat="server" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                            </div>

                                            <!-- Spacer -->
                                            <%--        <div class="col-md-1" style="width: 333px;">
            &nbsp;
        </div>--%>
                                            <!-- Decimal Format Section -->
                                            <div runat="server" id="divFormat" visible="false" style="flex: 1;">
                                                <div class="col-md-2" style="width: 150px;">
                                                    <label>Decimal Format:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtFormat" ValidationGroup="section" runat="server" CssClass="form-control required2" TabIndex="8"></asp:TextBox>
                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Decimal Only" ForeColor="Red" ControlToValidate="txtFormat"
                                                        ValidationExpression="^9{1,9}\.9{1,9}$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                            <!-- Max Length Section -->
                                            <div id="DIVmaxLenght" runat="server" visible="false" style="flex: 1;">
                                                <div class="col-md-2" style="width: 150px;">
                                                    <label>Max Length :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtMaxLength" ValidationGroup="section" runat="server" MaxLength="5" CssClass="form-control numeric" TabIndex="9"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div id="DIVChildModule" runat="server" visible="false" style="flex: 1;">
                                                <div class="col-md-2" style="width: 150px;">
                                                    <label>Select Link Module :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlChildModule" runat="server" class="form-control drpControl required2" TabIndex="10">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row margin10px">
                                        <div class="col-md-6" id="DivDisplay" runat="server" visible="false">
                                            <div class="box box-warning">
                                                <div class="box-header with-border" style="margin-top: 3px;">
                                                    <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Display Features:</h4>
                                                    <div class="col-md-9">
                                                        <div class="col-md-5">
                                                            <input type="color" value="<% =FieldColorValue %>" id="FieldColor" name="FieldColor"
                                                                onchange="set_FieldColor(this);" tabindex="11" />&nbsp;&nbsp;
                                                                       
                                                            <asp:HiddenField ID="hfFieldColor" runat="server" />
                                                            <label>Text Color</label>
                                                        </div>
                                                        <div class="col-md-7">
                                                            <input type="color" value="<% =AnsColorValue %>" id="AnsColor" name="AnsColor" onchange="set_AnsColor(this);" tabindex="12" />&nbsp;&nbsp;
                                                                       
                                                            <asp:HiddenField ID="hfAnsColor" runat="server" />
                                                            <label>Response Text Color</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-3" id="divBOLD" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkBold" TabIndex="13" />&nbsp;&nbsp;
                                                                       
                                                            <label>Bold</label>
                                                        </div>
                                                        <div class="col-md-4" id="divMaskField" runat="server">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInvisible" TabIndex="14" />&nbsp;&nbsp;
                                                                   
                                                            <label>Mask Field</label>
                                                        </div>
                                                        <div class="col-md-4" id="divUpperCase" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUppercase" TabIndex="15" />&nbsp;&nbsp;
                                                                        
                                                            <label>UpperCase Only</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-3" id="DivReadonly" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRead" TabIndex="16" AutoPostBack="true" OnCheckedChanged="chkRead_CheckedChanged" />&nbsp;&nbsp;
                                                                       
                                                            <label>Read only</label>
                                                        </div>
                                                        <div class="col-md-4" id="DivUnderline" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUnderline" TabIndex="17" />&nbsp;&nbsp;
                                                                       
                                                            <label>Underline</label>
                                                        </div>
                                                        <div class="col-md-4" id="DivFreetext" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMultiline" TabIndex="18" />&nbsp;&nbsp;
                                                                       
                                                            <label>Freetext</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding-left: 0px;" id="DivSignificant" runat="server" visible="false">
                                            <div class="box box-warning">
                                                <div class="box-header with-border">
                                                    <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Data Significance:</h4>
                                                    <asp:Label ID="lblSignificant" Text="Not Applicable" runat="server" Style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 10px;"></asp:Label>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-1" style="width: 192px" id="DivRequiredInfo" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRequired" TabIndex="19" />&nbsp;&nbsp;
                                                                   
                                                            <label>Required Information</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 181px" id="DivMandatoryInfo" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMandatory" TabIndex="20" />&nbsp;&nbsp;
                                                                   
                                                            <label>Mandatory Information</label>
                                                        </div>
                                                        <%-- style="width: 166px" --%>
                                                        <div class="col-md-1" style="width: 192px" id="divSDV" runat="server" visible="true">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkSDV" TabIndex="21" AutoPostBack="true" OnCheckedChanged="chkSDV_CheckedChanged" />&nbsp;&nbsp;
                                                                   
                                                            <label>SDV/SDR</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 207px" id="divMedAuthRes" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMEDOP" TabIndex="22" />&nbsp;&nbsp;
                                                                   
                                                            <label>Medical Authority Response</label>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-1" style="width: 192px" id="divCriticalDP" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Critical Data-Point" ID="chkCriticDP" TabIndex="23" />&nbsp;&nbsp;
                                                                   
                                                            <label>Critical Data Point</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 220px" id="divDuplicate" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkduplicate" TabIndex="24" />&nbsp;&nbsp;                                                                   
                                                            <label>Duplicates Check Information</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-1" style="width: 192px" id="divPGL_Type" runat="server" visible="false">
                                                            <asp:DropDownList ID="drp_PGL_Type" runat="server" class="form-control drpControl required2">
                                                                <asp:ListItem Text="--Select PGL Type--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Retrospective" Value="Retrospective"></asp:ListItem>
                                                                <asp:ListItem Text="Prospective" Value="Prospective"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row margin10px">
                                        <div class="col-md-6" id="DivLinkaged" runat="server" visible="false">
                                            <div class="box box-warning">
                                                <div class="box-header with-border">
                                                    <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Data Linkages:</h4>
                                                    <asp:Label ID="lblDataLinkages" runat="server" Text="Not Applicable" Visible="false" Style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 10px;"></asp:Label>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-3" style="width: 162px" id="DivLinkedParent" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkParentLinked" AutoPostBack="true" OnCheckedChanged="chkParentLinked_CheckedChanged" TabIndex="25" />&nbsp;
                                                                   
                                                            <label>Linked Field(Parent)</label>
                                                        </div>
                                                        <div class="col-md-3" style="width: 169px" id="divLickedChild" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkChildLinked" AutoPostBack="true" OnCheckedChanged="chkChildLinked_CheckedChanged" TabIndex="26" />&nbsp;
                                                                   
                                                            <label>Linked Field(Child)</label>
                                                        </div>
                                                        <div class="col-md-3" style="width: 185px" id="divReferances" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkLab" TabIndex="27" />&nbsp;&nbsp;
                                                                   
                                                            <label>Lab Reference Range</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-1" id="divParent" style="width: 162px" runat="server">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAutoCode" OnCheckedChanged="chkAutoCode_CheckedChanged" AutoPostBack="true" TabIndex="28" />&nbsp;&nbsp;
                                                                   
                                                            <label>AutoCode</label>&nbsp;
                                                                    
                                                            <asp:DropDownList runat="server" ID="drpAutoCode" Width="75%" CssClass="form-control required2" Visible="false" TabIndex="29">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="MedDRA" Value="MedDRA"></asp:ListItem>
                                                                <asp:ListItem Text="WHODD" Value="WHODD"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-1" style="width: 169px" id="divLinkedDataFlowfield" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkREF" TabIndex="30" />&nbsp;
                                                                   
                                                            <label>Linked Dataflow Field</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 188px" id="DivProtocalPredefineData" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" OnCheckedChanged="chkDefault_CheckedChanged" AutoPostBack="true"
                                                                ToolTip="Select if 'YES'" ID="chkDefault" TabIndex="31" />&nbsp;
                                                                   
                                                            <label>Protocol Predefined Data</label>
                                                            <asp:TextBox Style="width: 150px;" ID="txtDefaultData" Visible="false" ValidationGroup="section" MaxLength="50"
                                                                runat="server" CssClass="form-control" TabIndex="32"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="padding-left: 0px;" id="DivEntry" runat="server" visible="false">
                                            <div class="box box-warning">
                                                <div class="box-header with-border">
                                                    <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Multiple Data Entry:</h4>
                                                    <asp:Label ID="lblDataEntry" runat="server" Text="Not Applicable" Visible="false" Style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 10px;"></asp:Label>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-1" style="width: 225px" id="divSequentialYN" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAutoNum" TabIndex="33" />&nbsp;&nbsp;
                                                                   
                                                            <label>Sequential Auto-Numbering</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 138px" id="divNonRepetative" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chknonRepetative" TabIndex="34" />&nbsp;&nbsp;
                                                                   
                                                            <label>
                                                                Non-Repetitive</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2" style="display: inline-flex; width: 225px" id="divInList" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInList" OnCheckedChanged="chkInList_CheckedChanged"
                                                                AutoPostBack="true" TabIndex="35" />&nbsp;&nbsp;
                                                                   
                                                            <label>
                                                                In List Data</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   
                                                            <div runat="server" id="divInlistEditable" visible="false">
                                                                <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInlistEditable" TabIndex="36" />&nbsp;&nbsp;
                                                                       
                                                                <label>
                                                                    Editable</label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2" style="display: inline-flex; width: 257px" id="divPrefix" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkPrefix" AutoPostBack="true"
                                                                OnCheckedChanged="chkPrefix_CheckedChanged" TabIndex="37" />&nbsp;&nbsp;
                                                                       
                                                            <label>
                                                                Prefix</label>&nbsp;&nbsp;&nbsp;
                                                                       
                                                            <asp:TextBox Style="width: 150px;" ID="txtPrefix" Visible="false" ValidationGroup="section"
                                                                runat="server" CssClass="form-control required2" TabIndex="38"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row margin10px" style="margin-top: 7px;">
                                        <div class="col-md-12">
                                            <div class="col-md-5">
                                                &nbsp;
                                           
                                            </div>
                                            <div class="col-md-7">
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box box-primary" id="DivRecords" runat="server" visible="false">
                        <div class="box-header with-border">
                            <h4 class="box-title" align="left">Records
                            </h4>
                            <div class="pull-right" style="margin-right: 10px;">
                                <asp:LinkButton ID="lnkCodeMapping" runat="server" Text="Code Mapping" Font-Bold="true" Visible="false" CssClass="btn btn-success btn-sm" Style="margin-top: 3px" ForeColor="White" OnClientClick="return Set_CodeMapping(this);">
                                           Code Mapping 
                                </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                               
                                <asp:LinkButton ID="lbtn_set_labDefault" runat="server" Text="Set Lab Reference Range" Visible="false" Font-Bold="true" CssClass="btn btn-warning btn-sm" Style="margin-top: 3px" ForeColor="White" OnClientClick="return Set_LabDefault(this);">
                                         Set Lab Reference Range
                                </asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                               
                                <asp:LinkButton ID="lbtnsetOnsubmitCrits" runat="server" CssClass="btn btn-DARKORANGE btn-sm" Style="margin-top: 3px" ForeColor="White" Text="Set OnSubmit/OnLoad Criteria"
                                    Font-Bold="true" Visible="false" OnClientClick="return Set_OnSubmitCRITs(this);">
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div class="fixTableHead">
                                            <asp:GridView ID="grdField" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                OnRowCommand="grdField_RowCommand"
                                                OnRowDataBound="grdField_RowDataBound" OnPreRender="grdField_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="EditField" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Add Option">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnlAddOption" runat="server" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                                                ToolTip="Add Option" OnClientClick="return AddDrpDownData(this)">  <i class="fa fa-cog"  ></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnlAddOption_LINKED" runat="server" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                                                ToolTip="Add Options" OnClientClick="return AddDrpDownData_LINKED(this)">  <i class="fa fa-cog"  ></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Add OnChange Criteria">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnSpecs" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                OnClientClick="return Set_FormSpec(this)" ToolTip="Add OnChange Criteria"><i class="fa fa-asterisk"></i></asp:LinkButton>&nbsp;&nbsp;
                                                       
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Variable Name" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Control Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CONTROL" runat="server" Text='<%# Bind("CONTROLS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Seq No" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="FIELDNAME" runat="server" Style="text-align: left" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PGL Type" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PGL_TYPE" runat="server" Style="text-align: left" Text='<%# Bind("PGL_TYPE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_MODULE_FIELD', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
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
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <%--   <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="DeleteField" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this  field : ", Eval("FIELDNAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>--%>

                                                            <%--<asp:LinkButton ID="lbtndeleteSection"  runat="server"  CommandArgument='<%# Bind("ID") %>'   CommandName="DeleteField" OnClientClick='<%# string.Format("return CheckStatusLogsBeforeDelete(\"{0}\", \"{1}\");", Eval("ID"), Eval("VARIABLENAME")) %>' ToolTip="Delete">
                                                 <i class="fa fa-trash-o"></i></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteField" OnClientClick='<%# string.Format("return CheckStatusLogsBeforeDelete(\"{0}\", \"{1}\", \"{2}\", \"{3}\");", Eval("ID"), Eval("VARIABLENAME"),Eval("ENTEREDDAT"), this.ClientID) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                    <div class="box box-primary" id="DivMasterData" runat="server" visible="false">
                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">Master Data
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>
                                                Select Module:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList Style="width: 250px;" ID="drpModuleMaster" runat="server" class="form-control drpControl select"
                                                AutoPostBack="True" OnSelectedIndexChanged="drpModuleMaster_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="fixTableHead">
                                    <asp:GridView ID="gvChecklists" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="gvChecklists_RowDataBound"
                                        CssClass="table table-bordered table-striped Datatable" OnPreRender="GridView1_PreRender" Width="98%">
                                        <Columns>
                                            <asp:TemplateField Visible="false" HeaderText="ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Seq No">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtSeqNO" runat="server" CssClass="txt_center" Text='<%# Bind("FIELD_SEQNO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Variable Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Field Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtFIELDNAME" runat="server" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                    <itemstyle horizontalalign="Center" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Description" runat="server" Text='<%# Bind("Descrip") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="txtCenter">
                                                <HeaderTemplate>
                                                    <asp:Button ID="Btn_Add_Fun" runat="server" Text="Add" CssClass="btn btn-primary btn-sm cls-btnSave3 "
                                                        OnClick="Btn_Add_Fun_Click" OnClientClick="return countMasterCheckboxes();" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                    <asp:Label ID="lblStatus" runat="server" Text=" " ForeColor="Blue"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="DivManageMap" runat="server" visible="false">
                        <div class="col-md-6">
                            <div class="box box-primary" style="min-height: 264px;">
                                <div class="box-header with-border" style="float: left; top: 0px; left: 0px; width: 100%;">
                                    <h4 class="box-title" align="left">Manage Mappings
                                    </h4>

                                    <div class="pull-right" style="margin-right: 5%; margin-top: 1%;">
                                        <asp:LinkButton runat="server" ID="lnkClearMapping" Text="Clear Mappings" Visible="false" OnClick="lnkClearMapping_Click"
                                            CssClass="btn btn-danger btn-sm cls-btnSave3" Style="color: white;"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px; min-height: 296px;">
                                        <div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            Select Field:</label>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlFIELD" runat="server" class="form-control drpControl required1 "
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlFIELD_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            Select Answer Trigger:</label>
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlAnsTrigger" runat="server" class="form-control drpControl required1 "
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlAnsTrigger_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            Select Child Field :</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo" runat="server" class="form-control drpControl required1">
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;<asp:LinkButton ID="lbtnAddMoreChild" runat="server" OnClick="lbtnAddMoreChild_Click" ToolTip="Add Child Field">
                                                            <i id="I1" runat="server" class="fa fa-plus"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild2" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo2" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild3" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo3" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild4" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo4" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild5" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo5" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild6" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo6" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild7" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo7" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild8" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo8" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild9" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo9" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild10" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo10" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild11" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo11" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild12" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo12" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild13" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo13" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild14" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo14" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild15" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo15" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild16" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo16" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild17" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo17" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild18" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo18" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild19" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo19" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild20" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo20" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild21" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo21" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild22" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo22" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild23" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo23" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild24" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo24" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild25" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo25" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild26" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo26" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild27" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo27" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild28" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo28" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild29" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo29" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" runat="server" id="divChild30" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNo30" Visible="false" runat="server"
                                                            class="form-control drpControl">
                                                        </asp:DropDownList>
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
                                                        <asp:Button ID="btnSubmit_CHILD" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                            OnClick="btnSubmit_CHILD_Click" />
                                                        <asp:Button ID="btnCancel_CHILD" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                                            OnClick="btnCancel_CHILD_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <%--<div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            Select Child Module :</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildModule" runat="server" OnSelectedIndexChanged ="ddlModule_SelectedIndexChanged" class="form-control drpControl "  AutoPostBack="true">
                                                        </asp:DropDownList>
                                                         
                                                        &nbsp;&nbsp;<asp:LinkButton ID="lbtnAddMoreChildModule" runat="server" OnClick="lbtnAddMoreChildModule_Click" ToolTip="Add Child Module">
                                                           
                                                            <i id="I2" runat="server" class="fa fa-plus"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <%--<div class="row" runat="server" id="divChild1" visible="false"> 
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <label>
                                                            &nbsp;</label>
                                                    </div>
                                                    <div class="col-md-7" style="display: inline-flex;">
                                                        <asp:DropDownList Style="width: 250px;" ID="ddlChildNum1" Visible="false" runat="server" class="form-control drpControl" OnSelectedIndexChanged="ddlChlModule_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <br />
                                            <%-- <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-md-7">
                                                        <asp:Button ID="btnSubmitModule_CHILD" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1"
                                                            OnClick="btnSubmitModule_CHILD_Click" />
                                                        <asp:Button ID="btnCancelModule_CHILD" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                            OnClick="btnCancelModule_CHILD_Click" />
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="box box-primary" style="min-height: 264px;">
                                <div class="box-header with-border" style="float: left; top: 0px; left: 0px; width: 100%;">
                                    <h4 class="box-title" align="left">Mappings of Fields
                                    </h4>
                                    <div class="pull-right" style="margin-right: 5%; margin-top: 1%;">
                                        <asp:LinkButton runat="server" ID="lbtnViewMap" Text="View Mappings" Visible="false" OnClientClick="return View_Map();"
                                            CssClass="btn btn-danger btn-sm cls-btnSave3" Style="color: white;"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div align="left" style="margin-left: 5px">
                                        <div>
                                            <div class="rows">
                                                <div style="width: 100%; height: 264px; overflow: auto;">
                                                    <div>
                                                        <asp:TreeView runat="server" ID="treeFieldMap" ShowLines="true">
                                                            <NodeStyle ForeColor="#333333" Font-Bold="true" />
                                                        </asp:TreeView>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitField" />
            <asp:PostBackTrigger ControlID="btnUpdateField" />
            <asp:AsyncPostBackTrigger ControlID="treeFieldMap" />
        </Triggers>
    </asp:UpdatePanel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="VisitCheck" TargetControlID="Button_Popup" BackgroundCssClass="Background"></cc1:ModalPopupExtender>
    <asp:Panel ID="VisitCheck" runat="server" Style="display: none;" CssClass="Popup1">
        <asp:Button runat="server" ID="Button_Popup" Style="display: none" />
        <h5 class="heading">Check Visit</h5>
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="modulelbl" runat="server" Style="margin-left: 10px;" ForeColor="Black" Font-Bold="true" Text=" Module Name:"></asp:Label>
            </div>
            <div class="col-md-9">
                <asp:Label ID="LlbModulevisit" runat="server" Style="margin-left: 10px;" ForeColor="Blue" Font-Bold="true" Text=""></asp:Label>
            </div>
        </div>
        <div class="row" style="margin-top: 7px;">
            <div class="col-md-3">
                <asp:Label ID="visitlbl" runat="server" Style="margin-left: 10px;" ForeColor="Black" Font-Bold="true" Text=" Field Name:"></asp:Label>
            </div>
            <div class="col-md-9">
                <asp:Label ID="LlbFieldevisit" runat="server" CssClass="form-control-model" Style="margin-right: 50px; overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>

            </div>
        </div>
        <div class="row" style="margin-top: 7px;">
            <div class="col-md-12">
                <asp:Label ID="Label7" runat="server" Style="margin-left: 10px; margin-right: 10px;" Font-Bold="true" Text=""></asp:Label>
            </div>
        </div>
        <div class="row" style="margin-top: 7px;">
            <div class="col-md-12">
                <asp:Label ID="Label6" runat="server" Style="margin-left: 10px; margin-right: 10px;" Font-Bold="true" Text="Do you want to add this field at the visits level? If so, please select 'Below Visits'."></asp:Label>
            </div>
        </div>
        <br />
        <div class="row">
            <div style="width: 95%; min-height: auto; max-height: 170px; overflow: auto;">
                <asp:GridView ID="grdvisitSubmit" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" Style="width: 90%; margin-left: 25px;">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="Chk_VISIT" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                            <ItemTemplate>
                                <asp:Label ID="VISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Visit">
                            <ItemTemplate>
                                <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <div class="row" style="margin-bottom: 10px;">
            <div class="col-md-3">
                &nbsp;
           
            </div>
            <div class="col-md-9">
                <asp:Button ID="btnSubmitModuleField" runat="server" CssClass="btn btn-DarkGreen" Height="32px" Font-Size="Larger" Width="60px" Text="Yes" OnClick="btnSubmitModuleField_Click" OnClientClick="return countCheckboxes();" />&nbsp;&nbsp;
               
                <asp:Button ID="btnCancelModuleField" runat="server" Text="Cancel" CssClass="btn btn-danger" Height="32px" Font-Size="Larger" Width="60px" OnClick="btnCancelModuleField_Click" />
            </div>
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="VisitCheckON" TargetControlID="Button_Popup1" BackgroundCssClass="Background"></cc1:ModalPopupExtender>
    <asp:Panel ID="VisitCheckON" runat="server" Style="display: none;" CssClass="Popup1">
        <div class="" runat="server" style="width: 100%;">
            <div id="ModelPopup1">
                <asp:Button runat="server" ID="Button_Popup1" Style="display: none" />
                <h5 class="heading">Check Visit ON</h5>
                <div class="row">
                    <div class="col-md-3" style="width: 121px;">
                        <asp:Label ID="Label3" runat="server" Style="margin-left: 10px;" ForeColor="Black" Font-Bold="true" Text=" Module Name:"></asp:Label>
                    </div>
                    <div class="col-md-9">
                        <asp:Label ID="Label3Module" runat="server" Style="margin-left: 10px;" ForeColor="Blue" Font-Bold="true" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row" style="margin-top: 7px;">
                    <div class="col-md-3">
                        <asp:Label ID="Label4" runat="server" Style="margin-left: 10px;" ForeColor="Black" Font-Bold="true" Text=" Field Name:"></asp:Label>
                    </div>
                    <div class="col-md-9">

                        <asp:Label ID="Label4Field" runat="server" ForeColor="Green" Font-Bold="true" Text="" class="form-control-model" Style="color: Green; font-weight: bold; margin-right: 50px; overflow-y: auto; max-height: 100px; min-height: 21px; min-width: 250px;"></asp:Label>
                    </div>
                </div>


                <div class="row" style="margin-top: 7px;">
                    <div class="col-md-12">
                        <asp:Label ID="Label1" runat="server" Style="margin-left: 10px; margin-right: 10px;" Font-Bold="true">This Field is available in below visit.</asp:Label>
                    </div>
                </div>
                <div class="row" style="margin-top: 7px;">
                    <div class="col-md-12">
                        <asp:Label ID="Label2" runat="server" Style="margin-left: 10px; margin-right: 10px;" Font-Bold="true" Text="These changes will apply to all listed visits below where the module is present."></asp:Label>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div style="width: 95%; min-height: auto; max-height: 170px; overflow: auto;">
                        <asp:GridView ID="grdVisitUpdate" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" Style="width: 90%; margin-left: 25px;">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="VISITNUM" runat="server" Text='<%# Bind("VISITNUM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visit">
                                    <ItemTemplate>
                                        <asp:Label ID="VISIT" runat="server" Text='<%# Bind("VISIT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <div class="row" style="margin-bottom: 10px;">
                    <div class="col-md-4">
                        &nbsp;
                   
                    </div>
                    <div class="col-md-7">
                        <asp:Button ID="btnUpdateVisitField" runat="server" CssClass="btn btn-DarkGreen" Font-Size="Larger" Height="32px" Width="60px" Text="Yes" OnClick="btnUpdateVisitField_Click" />&nbsp;&nbsp;
                       
                        <asp:Button ID="btnCancelVisitField" runat="server" Text="Cancel" Height="32px" Font-Size="Larger" Width="60px" CssClass="btn btn-danger" OnClick="btnCancelVisitField_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnlReview" TargetControlID="lnkReason"
        BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlReview" runat="server" Style="display: none;" CssClass="Popup1">
        <asp:Button runat="server" ID="lnkReason" Style="display: none" />
        <asp:UpdatePanel ID="updpnlSendforreview" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <h5 class="heading">Send For Review Reason</h5>
                <div class="modal-body" runat="server" style="padding: 10px;">
                    <div id="ModelPopup2">
                        <div class="row">
                            <div style="width: 100%; height: auto; overflow: auto;">
                                <div>
                                    <label id="lblReviewHeader" runat="server" style="color: blue; margin-left: 3%;"></label>
                                </div>
                                <asp:ListView ID="lstSystems" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <ul>
                                                    <li>
                                                        <asp:Label runat="server" ID="lblSystemName" Text='<%# Bind("SystemName") %>' ForeColor="Maroon"></asp:Label>
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
                                    <asp:TextBox runat="server" ID="txtReason" MaxLength="500" placeholder="Please enter comment to Send module to review for above systems...."
                                        TextMode="MultiLine" CssClass="form-control required4" Style="width: 575px; height: 131px;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                &nbsp;
                           
                            </div>
                            <div class="col-md-9">
                                <asp:Button ID="btnSubmitReview" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;"
                                    CssClass="btn btn-DarkGreen btn-sm cls-btnSave4" Text="Submit" OnClick="btnSubmitReview_Click" />
                                &nbsp;
                            &nbsp;
                           
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
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
                                        TextMode="MultiLine" MaxLength="500" CssClass="form-control required5" Style="width: 575px; height: 131px;"></asp:TextBox>
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
                           
                                <asp:Button ID="btnCancelEditForEdit" runat="server" Text="Cancel"
                                    CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelEditForEdit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lbtnExportSpecs" />
                <asp:PostBackTrigger ControlID="lbtnExportCrfIntruc" />
                <%--<asp:AsyncPostBackTrigger ControlID="lbtnAddMoreChildModule" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>

    <asp:UpdatePanel ID="upModalPopup" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- Hidden button to trigger ModalPopupExtender -->
            <asp:Button ID="btnShowDeletePopup" runat="server" Style="display: none;" />

            <!-- Delete confirmation modal -->
            <asp:Panel ID="pnlDeleteConfirmation" runat="server" CssClass="modalPopup" Style="display: none;">
                <h3 class="modal-title">Confirm Action</h3>

                <!-- Radio buttons for delete type -->
                <div class="radio-options">
                    <asp:RadioButton ID="rdoPermanentDelete" runat="server" GroupName="DeleteType" Text="Permanent Delete" CssClass="radio-item" />
                    <asp:RadioButton ID="rdoProspectiveDelete" runat="server" GroupName="DeleteType" Text="Mark As Delete" CssClass="radio-item" />
                </div>

                <!-- Hidden field to store ID -->
                <asp:HiddenField ID="hdnDeleteFieldID" runat="server" />
                <asp:HiddenField ID="hdnVariableName" runat="server" />

                <!-- Submit and cancel buttons -->
                <div class="button-group">
                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Submit" CssClass="btn btn-success btn-sm" OnClientClick="return ConfirmDeleteAction();" OnClick="btnConfirmDelete_Click" />
                    <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" CssClass="btn btn-danger btn-sm" OnClick="btnCancelDelete_Click" />
                </div>

            </asp:Panel>

            <!-- ModalPopupExtender -->
            <ajaxToolkit:ModalPopupExtender
                ID="mpeDeleteConfirmation"
                runat="server"
                TargetControlID="btnShowDeletePopup"
                PopupControlID="pnlDeleteConfirmation"
                BackgroundCssClass="Background">
            </ajaxToolkit:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
