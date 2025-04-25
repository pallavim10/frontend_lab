<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NSAE_PROJECTFIELD.aspx.cs" Inherits="CTMS.NSAE_PROJECTFIELD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome-ie7.min.css" rel="stylesheet" />
    <script src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/SAE/SAE_showAuditTrail.js"></script>
    <script type="text/jscript">
        function DisableDiv() {

            $('#MainContent_drpSCControl').attr("disabled", 'true');

            var nodes = document.getElementById("MainContent_divDataLinkages").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            var nodes = document.getElementById("MainContent_DivDataEntry").getElementsByTagName('*');
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].disabled = true;
            }

            $('#MainContent_chkInList').removeAttr("disabled");
        }

        function AddDrpDownData(element) {
            var ID = $(element).closest('tr').find('td:eq(0)').find('span').html();
            var VARIABLENAME = $(element).closest('tr').find('td:eq(3)').find('span').html();
            var FIELDNAME = $(element).closest('tr').find('td:eq(6)').find('span').html();
            var test = "NSAE_AddDrpDownData.aspx?ID=" + ID + "&VARIABLENAME=" + VARIABLENAME + "&FIELDNAME=" + FIELDNAME;
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=330,width=1000";
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
                stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Manage Project Fields
                    </h3>
                </div>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">Update Field
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Select Module:</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList Style="width: 250px;" ID="drpModule" runat="server" class="form-control drpControl required2 required3"
                                                    AutoPostBack="True" OnSelectedIndexChanged="drpModule_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label>
                                                    Enter Seq No :</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox Style="width: 120px;" ID="txtSeqno" MaxLength="3" ValidationGroup="section"
                                                    runat="server" CssClass="form-control numeric required2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 7px;">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Enter Filed Name:</label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox Style="width: 250px;" ID="txtFieldName" ValidationGroup="section" runat="server"
                                                    CssClass="form-control required2"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>
                                                    Enter Variable Name:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label Style="width: 296px;" ID="lblVariableName" ValidationGroup="section"
                                                    runat="server" CssClass="form-control required2"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 7px;">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Select Control Type :</label>
                                            </div>
                                            <div class="col-md-4" style="width: 353px;">
                                                <asp:DropDownList ID="drpSCControl" runat="server" Width="250px" class="form-control drpControl requiredSC required2"
                                                    ValidationGroup="SubCheklist" AutoPostBack="true" OnSelectedIndexChanged="drpSCControl_SelectedIndexChanged">
                                                    <asp:ListItem Text="-Select-" Value="Select">
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
                                            <div class="col-md-6" runat="server" id="DIVmaxLenght" visible="false">
                                                <div class="col-md-5" style="width: 184px;">
                                                    <label>
                                                        Enter Max Length :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox Style="width: 120px;" ID="txtMaxLength" ValidationGroup="section" runat="server"
                                                        CssClass="form-control numeric"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6" runat="server" id="divFormat" visible="false">
                                                <div class="col-md-5" style="width: 184px;">
                                                    <label>
                                                        Decimal Format :</label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtFormat" ValidationGroup="section" runat="server" CssClass="form-control  numeric" Style="width: 120px;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 7px;">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Enter Description:</label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Style="width: 850px;" TextMode="MultiLine" ID="txtDescrip" ValidationGroup="section"
                                                    runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" id="divfeature" runat="server" visible="false">
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
                                                        <div class="col-md-3" id="divBOLD" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkBold" />&nbsp;&nbsp;
                                                                        <label>Bold</label>
                                                        </div>
                                                        <div class="col-md-4" id="divMaskField" runat="server">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInvisible" />&nbsp;&nbsp;
                                                                    <label>Mask Field</label>
                                                        </div>
                                                        <div class="col-md-4" id="divUpperCase" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUppercase" />&nbsp;&nbsp;
                                                                         <label>UpperCase Only</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-3" id="DivReadonly" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRead" />&nbsp;&nbsp;
                                                                        <label>Read only</label>
                                                        </div>
                                                        <div class="col-md-4" id="DivUnderline" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkUnderline" />&nbsp;&nbsp;
                                                                        <label>Underline</label>
                                                        </div>
                                                        <div class="col-md-4" id="DivFreetext" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMultiline" />&nbsp;&nbsp;
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
                                                        <div class="col-md-1" style="width: 172px" id="DivRequiredInfo" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkRequired" />&nbsp;&nbsp;
                                                                    <label>Required Information</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 181px" id="DivMandatoryInfo" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMandatory" />&nbsp;&nbsp;
                                                                    <label>Mandatory Information</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 166px" id="DivCriticalDP" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Critical Data-Point" ID="chkCriticDP" />&nbsp;&nbsp;
                                                                    <label>Critical Data Point</label>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-1" style="width: 172px" id="DivMRChk" runat="server">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMedicalReview" />&nbsp;&nbsp;
                                                                <label>For Medical Review</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 206px" id="DivMedicalAuthRes" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkMEDOP" />&nbsp;&nbsp;
                                                                    <label>Medical Authority Response</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 216px" id="DivDuplicatesChkInfo" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkduplicate" />&nbsp;&nbsp;
                                                                    <label>Duplicates Check Information</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" id="divDataLinkages" runat="server" visible="false">
                                            <div class="box box-warning">
                                                <div class="box-header with-border">
                                                    <h4 class="box-title" align="left" style="color: darkviolet; font-weight: bold; font-size: large; text-decoration: underline;">Data Linkages:</h4>
                                                    <asp:Label ID="lblDataLinkages" runat="server" Text="Not Applicable" Visible="false" Style="font-size: 16px; color: darkorange; font-weight: bold; margin-left: 10px;"></asp:Label>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-3" style="width: 169px" id="DivLinkedFieldParent" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkParentLinked" AutoPostBack="true" OnCheckedChanged="chkParentLinked_CheckedChanged" />&nbsp;&nbsp;
                                                                    <label>Linked Field (Parent)</label>
                                                        </div>
                                                        <div class="col-md-3" style="width: 169px" id="DivLinkedFieldChild" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkChildLinked" AutoPostBack="true" OnCheckedChanged="chkChildLinked_CheckedChanged" />&nbsp;&nbsp;
                                                                    <label>Linked Field (Child)</label>
                                                        </div>
                                                        <div class="col-md-3" style="width: 185px" id="DivLabReferance" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkLab" />&nbsp;&nbsp;
                                                                    <label>Lab Referance Range</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-1" id="divParent" style="width: 169px" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAutoCode" OnCheckedChanged="chkAutoCode_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                                                    <label>AutoCode</label>&nbsp;&nbsp;&nbsp;
                                                                     <asp:DropDownList runat="server" ID="drpAutoCode" Width="75%" CssClass="form-control required" Visible="false">
                                                                         <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                         <asp:ListItem Text="MedDRA" Value="MedDRA"></asp:ListItem>
                                                                         <asp:ListItem Text="WHODD" Value="WHODD"></asp:ListItem>
                                                                     </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-1" style="width: 169px" id="DivLinkedDataflowField" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkREF" />&nbsp;
                                            <label>Linked Dataflow Field</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 184px" id="DivProtocalPredefineData" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" OnCheckedChanged="chkDefault_CheckedChanged" AutoPostBack="true"
                                                                ToolTip="Select if 'YES'" ID="chkDefault" />&nbsp;&nbsp;
                                                                    <label>Protocol Predefine Data</label>
                                                            <asp:TextBox Style="width: 150px;" ID="txtDefaultData" Visible="false" ValidationGroup="section" MaxLength="50"
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
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkAutoNum" />&nbsp;&nbsp;
                                                                    <label>Sequential Auto-Numbering</label>
                                                        </div>
                                                        <div class="col-md-1" style="width: 138px" id="DivNonRepetative" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chknonRepetative" />&nbsp;&nbsp;
                                                                    <label>Non-Repetitive</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-2" style="display: inline-flex; width: 225px" id="DivInListData" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInList" OnCheckedChanged="chkInList_CheckedChanged"
                                                                AutoPostBack="true" />&nbsp;&nbsp;
                                                                    <label>
                                                                        In List Data</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <div runat="server" id="divInlistEditable" visible="false">
                                                                        <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkInlistEditable" />&nbsp;&nbsp;
                                                                        <label>
                                                                            Editable</label>
                                                                    </div>
                                                        </div>
                                                        <div class="col-md-2" style="display: inline-flex; width: 257px" id="DivPrefix" runat="server" visible="false">
                                                            <asp:CheckBox runat="server" ToolTip="Select if 'YES'" ID="chkPrefix" AutoPostBack="true"
                                                                OnCheckedChanged="chkPrefix_CheckedChanged" />&nbsp;&nbsp;
                                                                        <label>Prefix</label>&nbsp;&nbsp;&nbsp;
                                                                        <asp:TextBox Style="width: 150px;" ID="txtPrefix" Visible="false" ValidationGroup="section"
                                                                            runat="server" CssClass="form-control required"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 7px;">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-7">
                                    <asp:Button ID="btnSubmitField" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm disp-none cls-btnSave2" />
                                    <asp:Button ID="btnUpdateField" Text="Update" Visible="false" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                        OnClick="btnUpdateField_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancelField" Text="Cancel" Visible="false" runat="server" CssClass="btn btn-danger btn-sm"
                                        OnClick="btnCancelField_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">Records
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div style="width: 100%; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="grdField" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped Datatable"
                                                    Style="width: 98%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="grdField_RowCommand"
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
                                                                <asp:LinkButton ID="lbtnupdate" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                    CommandName="EditField" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Add Option" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnlAddOption" runat="server" Visible="false" CommandArgument='<%# Bind("ID") %>'
                                                                    ToolTip="Add Option" OnClientClick="return AddDrpDownData(this)"><i class="glyphicon glyphicon-cog"></i></asp:LinkButton>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Variable Name" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="VARIABLENAME" runat="server" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CONTROL TYPE" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                            ItemStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CONTROL" runat="server" Text='<%# Bind("CONTROLTYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MODULENAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MODULENAME" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Field Name" ItemStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="FIELDNAME" runat="server" Style="text-align: left" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MODULEID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="disp-none"
                                                            ItemStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMODULEID" runat="server" Text='<%# Bind("MODULEID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('SAE_PROJECT_MASTER', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtndeleteSection" runat="server" CommandArgument='<%# Bind("ID") %>' OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Field : ", Eval("FIELDNAME")) %>'
                                                                    CommandName="DeleteField" ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
