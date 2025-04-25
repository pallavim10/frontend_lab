<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_CreateMaster.aspx.cs" Inherits="CTMS.DM_CreateMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <link href="CommonStyles/ButtonColor.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
     <script type="text/javascript" src="CommonFunctionsJs/TabIndexPost.js"></script>

    <script type="text/jscript">
        function AddDrpDownData(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var VARIABLENAME = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var CONTROL_TYPE = $(element).closest('tr').find('td:eq(9)').find('span').html();

            var test = "DB_AddDrpDownData_Master.aspx?ID=" + ID + "&VARIABLENAME=" + VARIABLENAME + "&CONTROL=" + CONTROL_TYPE;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1100";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function AddDrpDownData_LINKED(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var VARIABLENAME = $(element).closest('tr').find('td:eq(8)').find('span').html();
            var CONTROL_TYPE = $(element).closest('tr').find('td:eq(9)').find('span').html();

            var test = "DB_AddDrpDownData_Linked_Master.aspx?ID=" + ID + "&VARIABLENAME=" + VARIABLENAME + "&CONTROL=" + CONTROL_TYPE;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1000";
            popupWindow = window.open(test, '_blank', strWinProperty);
            return false;
        }

        function set_FieldColor(element) {
            var fcolor = element.value;
            $('#<% =hfFieldColor.ClientID %>').attr('value', fcolor);
        }

        function set_AnsColor(element) {
            var acolor = element.value;
            $('#<% =hfAnsColor.ClientID %>').attr('value', acolor);
        }

        function bindOptionValues() {

            var txtFields = $(".OptionValues").toArray();
            for (a = 0; a < txtFields.length; ++a) {
                var items = "";
                items = $('#MainContent_hfValue1').val().split('¸');
                $(txtFields[a]).autocomplete({
                    source: items, minLength: 0,
                    change: function (event, ui) {
                        $("#MainContent_txtModuleName").change();
                    }
                }).on('focus', function () {
                    $(this).keydown();
                });
            }
        }

        $(function () {

            if ($('#MainContent_hfFieldColor').val() != "") {
                $('#FieldColor').attr('value', $('#MainContent_hfFieldColor').val());
            }

            if ($('#MainContent_hfAnsColor').val() != "") {
                $('#AnsColor').attr('value', $('#MainContent_hfAnsColor').val());
            }

            var txtFields = $(".OptionValues").toArray();
            for (a = 0; a < txtFields.length; ++a) {
                var items = "";
                items = $('#MainContent_hfValue1').val().split('¸');
                $(txtFields[a]).autocomplete({
                    source: items, minLength: 0,
                    change: function (event, ui) {
                        $("#MainContent_txtModuleName").change();
                    }
                }).on('focus', function () { $(this).keydown(); });
            }

        });

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
    <script type="text/javascript">
        //$(document).ready(function () {
        //    var elements = $(":input:not(:hidden)");
        //    var maxTabIndex = Math.min(40, elements.length); // Adjust max based on actual elements

        //    elements.each(function (i) {
        //        var newTabIndex = (i % maxTabIndex) + 1;
        //        $(this).attr("tabindex", newTabIndex);
        //    });

        //    $(":input[tabindex='1']").focus(); // Ensure first field is focused
        //});
        //$(document).ready(function () {
        //    var lastTabIndex = sessionStorage.getItem("lastTabIndex");
        //    console.log("lasttabindex" + lastTabIndex);
        //    if (lastTabIndex && !isNaN(lastTabIndex)) {
        //        var nextInput = $(":input[tabindex='" + (parseInt(lastTabIndex) + 1) + "']");
        //        if (nextInput.length) {
        //            nextInput.focus();
        //        } else {
        //            $(":input[tabindex='1']").focus(); // Default to first field
        //        }
        //    }
        //    else {
        //        // If no session value, start with the first input field
        //        $(":input[tabindex='1']").focus();
        //    }

        //    // Store the last focused control's tabindex before postback
        //    $(":input:not(:hidden)").on("focus", function () {
        //        sessionStorage.setItem("lastTabIndex", $(this).attr("tabindex"));
        //    });
        //});
</script>

    <style type="text/css">
         .txtVariableName {
            text-transform: uppercase;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box-body">
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h4 class="box-title" align="left">Add Master Fields
                    </h4>
                    <div class="pull-right" style="margin-right: 10px; padding-top: 3px; display: inline-flex;">
                        <asp:Button ID="btnSubmitField" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" TabIndex="39"
                            OnClick="btnSubmitField_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnUpdateField" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" TabIndex="40"
                            OnClick="btnUpdateField_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancelField" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                            OnClick="btnCancelField_Click" TabIndex="41" />
                    </div>
                </div>
                <div align="left" style="margin-left: 7px;">

                    <div class="row margin-left10 margin-top4">
                        <div class="col-sm-4">
                            <div class="form-group row">
                                <label class="col-sm-4 control-label">Module Name:</label>
                                <div class="col-sm-8 text-left">
                                    <asp:HiddenField runat="server" ID="hfValue1" />
                                    <asp:HiddenField runat="server" ID="hdnoldModulename" />
                                    <asp:TextBox ID="txtModuleName" Style="width: 250%;" ValidationGroup="section" runat="server"
                                        CssClass="form-control required OptionValues" AutoPostBack="true" OnTextChanged="txtModuleName_TextChanged" TabIndex="1"></asp:TextBox>

                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row margin-left10">
                        <div class="col-sm-4">
                            <div class="form-group row">
                                <label class="col-sm-4 control-label">Field Name:</label>
                                <div class="col-sm-8 text-left">
                                    <asp:TextBox Style="width: 250%;" ID="txtFieldName" ValidationGroup="section" runat="server" TabIndex="2"  MaxLength="10000"
                                        CssClass="form-control required" ></asp:TextBox>
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="row margin-left10">

                        <div class="col-sm-4">
                            <div class="form-group row">
                                <label class="col-sm-4 control-label ">Module Seq No:</label>
                                <div class="col-sm-8 text-left">
                                    <asp:HiddenField runat="server" ID="hdnOldModuleSeqNo" />
                                    <asp:TextBox ID="txtModuleSeqNo" MaxLength="3" ValidationGroup="section"
                                        runat="server" CssClass="form-control numeric required text-center" TabIndex="3"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group row">
                                <label class="col-sm-4 control-label">Domain Name:</label>
                                <div class="col-sm-8 text-left">
                                    <asp:HiddenField runat="server" ID="hdnOldDomain" />
                                    <asp:TextBox ID="txtDomain" ValidationGroup="section" runat="server" MaxLength="4"
                                        CssClass="form-control required text-center" TabIndex="4"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group row">
                                <label class="col-sm-4 control-label">Field Seq No:</label>
                                <div class="col-sm-8 text-left">
                                    <asp:TextBox ID="txtFieldSeqno" MaxLength="3" ValidationGroup="section"
                                        runat="server" CssClass="form-control numeric required text-center" TabIndex="5" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row margin-left10">
                        <div class="col-sm-4">
                            <div class="form-group row">
                                <label class="col-sm-4 control-label">Variable Name:</label>
                                <div class="col-sm-8 text-left">
                                    <asp:TextBox Style="width: 110%;" ID="txtVariableName" ValidationGroup="section" OnTextChanged="txtVariableName_TextChanged" TabIndex="6" MaxLength="50"
                                        runat="server" CssClass="form-control required txtVariableName" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4" id="DivControlType">
                            <div class="form-group row">
                                <label class="col-sm-4 control-label ">Control Type:</label>
                                <div class="col-sm-8 text-left">
                                    <asp:HiddenField ID="hdnOldControlType" runat="server" />
                                    <asp:DropDownList ID="drpSCControl" AutoPostBack="true" runat="server" Style="width: 110%;" TabIndex="7"
                                        class="form-control drpControl required" ValidationGroup="SubCheklist"
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
                                        <asp:ListItem Text="Child Module" Value="ChildModule">                               
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row" style="display: flex; align-items: center; flex-wrap: wrap;">
                        <div class="col-md-12" style="display: flex; width: 100%; flex-wrap: wrap;">

                            <!-- Label for Description -->
                            <div class="col-md-2" style="flex: 1; max-width: 130px; min-width: 120px;">
                                <label style="margin-left: 7px;">Description:</label>
                            </div>

                            <!-- TextBox for Description -->
                            <div class="col-md-6" style="flex: 1; max-width: 500px; min-width: 200px; padding-right: 0px;">
                                <asp:TextBox Style="width: 100%;" TextMode="MultiLine" ID="txtDescrip" MaxLength="2000" ValidationGroup="section" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                            </div>

                            <!-- Spacer -->
                            <%--        <div class="col-md-1" style="width: 333px;">
            &nbsp;
        </div>--%>
                            <!-- Decimal Format Section -->
                            <div runat="server" id="divFormat" visible="FALSE" style="flex: 1;">
                                <div class="col-md-2" style="width: 150px;">
                                    <label>Decimal Format:</label>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtFormat" ValidationGroup="section" runat="server" CssClass="form-control required" TabIndex="9"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Decimal Only" ForeColor="Red" ControlToValidate="txtFormat" ValidationExpression="^9{1,9}\.9{1,9}$"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <!-- Max Length Section -->
                            <div runat="server" id="DIVmaxLenght" visible="FALSE" style="flex: 1;">
                                <div class="col-md-2" style="width: 150px;">
                                    <label>Max Length :</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtMaxLength" ValidationGroup="section" runat="server" MaxLength="5" CssClass="form-control numeric text-center" TabIndex="10"></asp:TextBox>
                                </div>
                            </div>
                            <!-- Linked Module Section -->
                            <div runat="server" id="DIVLinkedModule" visible="FALSE" style="flex: 1;">
                                <div class="col-md-2" style="width: 150px;">
                                    <label>Select Link Module :</label>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlChildModule" runat="server" class="form-control drpControl" TabIndex="11"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row margin10px">
                        <div class="col-md-6" id="divfeature" runat="server" visible="false">
                            <div class="box box-warning">
                                <div class="box-header with-border" style="margin-top: 3px;">
                                    <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Display Features:</h4>
                                    <div class="col-md-9">
                                        <div class="col-md-5">
                                            <input type="color" value="<% =FieldColorValue %>" id="FieldColor" name="FieldColor"
                                                onchange="set_FieldColor(this);" tabindex="12" />&nbsp;&nbsp;
                                                                        <asp:HiddenField ID="hfFieldColor" runat="server" />
                                            <label>Text Color</label>
                                        </div>
                                        <div class="col-md-7">
                                            <input type="color" value="<% =AnsColorValue %>" id="AnsColor" name="AnsColor" onchange="set_AnsColor(this);" tabindex="13" />&nbsp;&nbsp;
                                                                        <asp:HiddenField ID="hfAnsColor" runat="server" />
                                            <label>Response Text Color</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3" id="divBOLD" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkBold" TabIndex="14" />&nbsp;&nbsp;
                                                                        <label>Bold</label>
                                        </div>
                                        <div class="col-md-4" id="divMaskField" runat="server">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInvisible" TabIndex="15" />&nbsp;&nbsp;
                                                                    <label>Mask Field</label>
                                        </div>
                                        <div class="col-md-4" id="divUpperCase" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUppercase" TabIndex="16" />&nbsp;&nbsp;
                                                                         <label>UpperCase Only</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3" id="DivReadonly" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRead" TabIndex="17" />&nbsp;&nbsp;
                                                                        <label>Read only</label>
                                        </div>
                                        <div class="col-md-4" id="DivUnderline" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUnderline" TabIndex="18" />&nbsp;&nbsp;
                                                                        <label>Underline</label>
                                        </div>
                                        <div class="col-md-4" id="DivFreetext" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMultiline" TabIndex="19" />&nbsp;&nbsp;
                                                                        <label>Freetext</label>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>

                        <div class="col-md-6" style="padding-left: 0px;" id="divSignificant" runat="server" visible="false">
                            <div class="box box-warning">
                                <div class="box-header with-border">
                                    <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Data Significance: </h4>
                                    <asp:Label ID="lblSignificant" Text="Not Applicable" runat="server" Style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 10px;"></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-1" style="width: 190px" id="DivRequiredInfo" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRequired"  TabIndex="20"/>&nbsp;&nbsp;
                                                                    <label>Required Information</label>
                                        </div>
                                        <div class="col-md-1" style="width: 181px" id="DivMandatoryInfo" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMandatory" TabIndex="21" />&nbsp;&nbsp;
                                                                    <label>Mandatory Information</label>
                                        </div>
                                        <div class="col-md-1" style="width: 166px" id="DivCriticalDP" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Critical Data-Point" ID="chkCriticDP" TabIndex="22" />&nbsp;&nbsp;
                                                                    <label>Critical Data Point</label>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-1" style="width: 207px" id="DivMedicalAuthRes" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMEDOP" TabIndex="23" />&nbsp;&nbsp;
                                                                    <label>Medical Authority Response</label>
                                        </div>
                                        <div style="width: 393px" id="DivDuplicatesChkInfo" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkduplicate" TabIndex="24" />&nbsp;&nbsp;
                                                                    <label>Duplicates Check Information</label>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="row margin10px">
                        <div class="col-md-6" id="divDataLinkages" runat="server" visible="false">
                            <div class="box box-warning">
                                <div class="box-header with-border">
                                    <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Data Linkages:</h4>
                                    <asp:Label ID="lblDataLinkages" runat="server" Text="Not Applicable" Visible="false" Style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 10px;"></asp:Label>

                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3" style="width: 169px" id="DivLinkedFieldParent" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkParentLinked" AutoPostBack="true" OnCheckedChanged="chkParentLinked_CheckedChanged" TabIndex="25" />&nbsp;&nbsp;
                                                                    <label>Linked Field (Parent)</label>
                                        </div>
                                        <div class="col-md-3" style="width: 169px" id="DivLinkedFieldChild" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkChildLinked" AutoPostBack="true" OnCheckedChanged="chkChildLinked_CheckedChanged" TabIndex="26" />&nbsp;&nbsp;
                                                                    <label>Linked Field (Child)</label>
                                        </div>
                                        <div class="col-md-3" style="width: 185px" id="DivLabReferance" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkLab" TabIndex="27" />&nbsp;&nbsp;
                                                                    <label>Lab Reference Range</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-1" id="divParent" style="width: 135px" runat="server">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAutoCode" OnCheckedChanged="chkAutoCode_CheckedChanged" AutoPostBack="true" TabIndex="28" />&nbsp;&nbsp;
                                                                    <label>AutoCode</label>&nbsp;&nbsp;&nbsp;
                                                                     <asp:DropDownList runat="server" ID="drpAutoCode" Width="75%" CssClass="form-control required" Visible="false" TabIndex="29">
                                                                         <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                         <asp:ListItem Text="MedDRA" Value="MedDRA"></asp:ListItem>
                                                                         <asp:ListItem Text="WHODD" Value="WHODD"></asp:ListItem>
                                                                     </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1" style="width: 179px" id="DivLinkedDataflowField" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkREF" TabIndex="30" />&nbsp;
                                            <label>Linked Dataflow Field</label>
                                        </div>
                                        <div class="col-md-1" style="width: 192px" id="DivProtocalPredefineData" runat="server" visible="false">
                                            <asp:CheckBox runat="server" OnCheckedChanged="chkDefault_CheckedChanged" AutoPostBack="true" TabIndex="31"
                                                ToolTip="Select if 'YES'" ID="chkDefault" />&nbsp;&nbsp;
                                                                    <label>Protocol Predefined Data</label>
                                            <asp:TextBox Style="width: 150px;" ID="txtDefaultData" Visible="false" ValidationGroup="section" MaxLength="50" TabIndex="32"
                                                runat="server" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="col-md-6" style="padding-left: 0px;" id="DivDataEntry" runat="server" visible="false">
                            <div class="box box-warning">
                                <div class="box-header with-border">
                                    <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Multiple Data Entry:</h4>
                                    <asp:Label ID="lblDataEntry" runat="server" Text="Not Applicable" Visible="false" Style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 10px;"></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-1" style="width: 225px" id="DivSeqAutoNumbering" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAutoNum" TabIndex="33" />&nbsp;&nbsp;
                                                                    <label>Sequential Auto-Numbering</label>
                                        </div>
                                        <div class="col-md-1" style="width: 138px" id="DivNonRepetative" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chknonRepetative" TabIndex="34" />&nbsp;&nbsp;
                                                                    <label>Non-Repetitive</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="display: inline-flex; width: 225px" id="DivInListData" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInList" OnCheckedChanged="chkInList_CheckedChanged" TabIndex="35"
                                                AutoPostBack="true" />&nbsp;&nbsp;
                                                                    <label>
                                                                        In List Data</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <div runat="server" id="divInlistEditable" visible="false">
                                                                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInlistEditable" TabIndex="36" />&nbsp;&nbsp;
                                                                        <label>
                                                                            Editable</label>
                                                                    </div>
                                        </div>
                                        <div class="col-md-2" style="display: inline-flex; width: 257px" id="DivPrefix" runat="server" visible="false">
                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkPrefix" AutoPostBack="true"
                                                OnCheckedChanged="chkPrefix_CheckedChanged" TabIndex="36" />&nbsp;&nbsp;
                                                                        <label>Prefix</label>&nbsp;&nbsp;&nbsp;
                                                                        <asp:TextBox Style="width: 150px;" ID="txtPrefix" Visible="false" ValidationGroup="section" TabIndex="38"
                                                                            runat="server" CssClass="form-control required"></asp:TextBox>
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
        <div class="box box-primary">
            <div class="box-header with-border">
                <h4 class="box-title" align="left">Records
                </h4>
                <div class="pull-right" style="padding-top: 4px; margin-right: 10px;">
                    <asp:LinkButton runat="server" ID="btnExportExcel" OnClick="btnExportExcel_Click" Visible="false" Text="Export Library" CssClass="btn btn-info btn-sm" ForeColor="White" Font-Bold="true" TabIndex="42"> Export Library&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                </div>
            </div>
            <div class="box-body">
                <div align="left">
                    <div>
                        <div class="rows">
                            <div class="fixTableHead">
                                <asp:GridView ID="grdField" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                    OnRowCommand="grdField_RowCommand"
                                    OnRowDataBound="grdField_RowDataBound" OnPreRender="GridView1_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                    CommandName="EditField" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Add Options" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnlAddOption" runat="server" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                                    ToolTip="Add Option" OnClientClick="return AddDrpDownData(this)">  <i class="fa fa-cog"  ></i></asp:LinkButton>
                                                <asp:LinkButton ID="lnlAddOption_LINKED" runat="server" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                                    ToolTip="Add Options" OnClientClick="return AddDrpDownData_LINKED(this)">  <i class="fa fa-cog"  ></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module Name">
                                            <ItemTemplate>
                                                <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module Seq No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="MODULE_SEQNO" runat="server" Text='<%# Bind("MODULE_SEQNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Domain Name">
                                            <ItemTemplate>
                                                <asp:Label ID="DOMAIN" runat="server" Text='<%# Bind("DOMAIN") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:Label ID="FIELDNAME" runat="server" Style="text-align: left" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Field Seq No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="SEQNO" runat="server" Text='<%# Bind("FIELD_SEQNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Variable Name" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Control Type">
                                            <ItemTemplate>
                                                <asp:Label ID="CONTROL" runat="server" Text='<%# Bind("CONTROLTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DB_MASTER', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left disp-none" ItemStyle-Width="20%"
                                            ItemStyle-CssClass="disp-none">
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
                                        <asp:TemplateField HeaderStyle-Width="20%" HeaderStyle-CssClass="align-left disp-none" ItemStyle-Width="20%"
                                            ItemStyle-CssClass="disp-none">
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
                                                <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                    CommandName="DeleteField" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this field : ", Eval("FIELDNAME")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
    <asp:Label ID="Open_Request_temp" Style="display: none;" runat="server" Text="">.</asp:Label>
    <cc2:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel4" TargetControlID="Open_Request_temp"
        BackgroundCssClass="Background">
    </cc2:ModalPopupExtender>
    <asp:Panel ID="Panel4" runat="server" Style="display: none;" CssClass="Popup1">
        <asp:Button runat="server" ID="lnkDELETE1" Style="display: none" />
        <div class="modal-body" runat="server">
            <div id="ModelPopup2">
                <div class="align-center">
                    <asp:Label ID="Label12" runat="server" Style="color: black; font-weight: 600; font-size: 17px;">Do you want to update the module details in the existing fields?</asp:Label>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3">
                        &nbsp;
                    </div>
                    <div class="col-md-9">
                        <asp:Button ID="btnYes" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" CssClass="btn btn-DarkGreen"
                            Text="Yes" OnClick="btnYes_Click" />
                        &nbsp;
                            &nbsp;
                            <asp:Button ID="btnNo" runat="server" Text="No"
                                CssClass="btn btn-danger" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnNo_Click" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
