<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_ADD_NEW_RULE.aspx.cs" Inherits="CTMS.DM_ADD_NEW_RULE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/DM/DM_ConfirmMsg.js"></script>
    <script src="js/Input%20Mask/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script type="text/javascript" src="CommonFunctionsJs/ControlJS.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/DB/DB_showAuditTrail.js"></script>
<%--    <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <script type="text/javascript">
        function validateRadioButton() {
            var isSelected = $("input[name$='rbtActions']:checked").length > 0;

            if (!isSelected) {
                $("#<%= rbtActions.ClientID %>").css("border", "1px solid red");
                return false;
            }
            else {
                $("#<%= rbtActions.ClientID %>").css("border", "");
                return true; // Allow form submission
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="SM" runat="server">
    </asp:ScriptManager>
    <%--    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
    </script>--%>
    <asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box box-info">
                <div class="box-header" runat="server" id="expandHeader">
                    <h3 class="box-title">Added Rules</h3>
                </div>
                <asp:HiddenField ID="hdnVISITID" runat="server" />
                <asp:HiddenField ID="hdnMODULEID" runat="server" />
                <asp:HiddenField ID="hdnFIELDID" runat="server" />
                <asp:HiddenField ID="hdnID" runat="server" />
                <asp:HiddenField ID="hdnVariableID" runat="server"></asp:HiddenField>
                <asp:HiddenField ID="hdnTested" runat="server" />
            </div>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Add New Rule</h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 150px;">
                                    <asp:Label ID="Label3" class="label" runat="server" Text="Select Visit:"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="control">
                                        <asp:DropDownList ID="ddlVisit" class="form-control width300px required" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlVisit_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" style="width: 150px;">
                                    <asp:Label ID="Label1" class="label" runat="server" Text="Select Module:"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="control">
                                        <asp:DropDownList ID="ddlModule" Width="351px" class="form-control required" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 150px;">
                                    <asp:Label ID="Label2" class="label" runat="server" Text="Select Field:"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="control">
                                        <asp:DropDownList ID="ddlField" class="form-control width300px required" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" style="width: 150px;">
                                    <asp:Label ID="Label34" class="label" runat="server" Text="Enter Rule ID:"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ID="txtRuleID" Width="100px" CssClass="form-control required required2" MaxLength="10"> 
                                    </asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="Label43" class="label" runat="server" Text="SEQ NO :"></asp:Label>
                                </div>
                                <div class="col-md-1">
                                    <asp:TextBox runat="server" ID="txtSEQNO" Width="70px" CssClass="form-control required required2 numeric txt_center" MaxLength="5"> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 150px;">
                                    <asp:Label ID="Label42" class="label" runat="server" Text="Select Nature :"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="control">
                                        <asp:DropDownList ID="ddlNature" class="form-control required required2" runat="server">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Missing Data" Value="Missing Data"></asp:ListItem>
                                            <asp:ListItem Text="Incomplete Data" Value="Incomplete Data"></asp:ListItem>
                                            <asp:ListItem Text="Inconsistent Data" Value="Inconsistent Data"></asp:ListItem>
                                            <asp:ListItem Text="Protocol Deviation" Value="Protocol Deviation"></asp:ListItem>
                                            <asp:ListItem Text="Not Applicable" Value="Not Applicable"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" style="width: 150px;">
                                    <asp:Label ID="Label4" class="label" runat="server" Text="Actions :"></asp:Label>
                                </div>
                                <div class="col-md-2" style="width: 198px;">
                                    <asp:RadioButtonList ID="rbtActions" class="required required2" runat="server"
                                        OnTextChanged="rbtActions_TextChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Generate Query" Value="Generate Query"></asp:ListItem>
                                        <asp:ListItem Text="Set Field Value" Value="Set Field Value"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 13%;">
                                    <asp:Label ID="Label29" class="label" runat="server" Text="Rule Description :"></asp:Label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="txtDescription" Width="97%" Height="60px" TextMode="MultiLine" MaxLength="1000"
                                        CssClass="form-control required required2"> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2" style="width: 13%;">
                                    <asp:Label ID="Label6" class="label" runat="server" Text="Query Text :"></asp:Label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox runat="server" ID="txtQuery" Width="97%" Height="60px" TextMode="MultiLine" MaxLength="1000"
                                        CssClass="form-control required required2"> 
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="pull-left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnBack" Text="Back to Rule List" runat="server" Style="color: white;"
                                        CssClass="btn btn-primary btn-sm" OnClick="btnBack_Click" />
                                </div>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnsubmit" Text="Submit & Define Variables" runat="server" Style="color: white;"
                                        CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnsubmit_Click" OnClientClick="return validateRadioButton();" />
                                    <asp:LinkButton ID="lbtnUpdate" Text="Update & Define Variables" runat="server" Style="color: white;"
                                        Visible="false" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
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
                                                        <asp:DropDownList ID="ddlVisit1" class="form-control width300px required1" runat="server"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlVisit1_SelectedIndexChanged">
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
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
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
                                        <div style="width: 100%; height: 403px; overflow: auto;">
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
                                                            <asp:Label ID="lblVariableName" runat="server" Text='<%# Bind("VARIABLENAME_DEF") %>'></asp:Label>
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
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('DM_Rule_Variables', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10" ItemStyle-Width="10" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" CommandArgument='<%# Bind("ID") %>' CommandName="DeleteRule"
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
            <div class="box box-group" id="divDefineCondition" visible="false" runat="server"
                style="background-color: lightcyan;">
                <div class="box-header">
                    <h3 class="box-title">Define Conditions</h3>
                </div>
                <div class="row" runat="server" id="divSetCondition" visible="false">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label11" class="label" runat="server" Text="Enter Condition :"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtcondtion" Width="96%" Height="60px" TextMode="MultiLine"
                                CssClass="form-control required2"> 
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" id="divforsetvalue" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label12" class="label" runat="server" Text="Enter Formula for Set Value :"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtformulaforsetvalue" Width="96%" Height="60px"
                                TextMode="MultiLine" CssClass="form-control required2"> 
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="pull-right" style="margin-right: 15px;">
                            <asp:LinkButton ID="lbtnSubmitConditions" Text="Submit & Evaluate Condition" runat="server" MaxLength="500"
                                Style="color: white;" CssClass="btn btn-primary btn-sm cls-btnSave2" OnClick="lbtnSubmitConditions_Click" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
            <div class="box box-warning" id="divEvaluateConditions" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 264px;">
                            <div class="box-header">
                                <h3 class="box-title">Evaluate Conditions</h3>
                            </div>
                            <div class="box-body">
                                <asp:Repeater runat="server" ID="repeatVariables" OnItemDataBound="repeatVariables_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <asp:Label runat="server" class="label" ID="lblConditionVariable" Text='<%# Bind("VARIABLENAME_DEF") %>'></asp:Label>
                                                    <asp:Label runat="server" class="label" ID="lblConditionControlType" Text='<%# Bind("CONTROLTYPE") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label runat="server" class="label" ID="Derived" Text='<%# Bind("Derived") %>'
                                                        Visible="false"></asp:Label>
                                                </div>
                                                <div class="col-md-9">
                                                    <asp:DropDownList ID="DRP_FIELD" runat="server" Visible="false" Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="TXT_FIELD" runat="server" autocomplete="off" Width="200px" Visible="false"></asp:TextBox>
                                                    <asp:Repeater runat="server" ID="repeat_CHK">
                                                        <ItemTemplate>
                                                            <div class="col-md-12">
                                                                <asp:CheckBox ID="CHK_FIELD" runat="server" CssClass="checkbox" Text='<%# Bind("TEXT") %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:Repeater runat="server" ID="repeat_RAD">
                                                        <ItemTemplate>
                                                            <div class="col-md-12">
                                                                <asp:RadioButton ID="RAD_FIELD" runat="server" onclick="return RadioCheck(this);"
                                                                    CssClass="radio" Text='<%# Bind("TEXT") %>' />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-5">
                                            <asp:LinkButton ID="lbtnCheckEvaluateCondition" Text="TEST CONDITION" runat="server"
                                                Style="color: white; width: 100%;" CssClass="btn btn-primary btn-sm cls-btnSave2"
                                                OnClick="lbtnCheckEvaluateCondition_Click" />
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="box box-primary" style="min-height: 264px;">
                            <div class="box-header">
                                <h3 class="box-title">Results</h3>
                            </div>
                            <div class="box-body">
                                <div class="row" id="divSyntexError" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <asp:Label ID="Label14" Style="width: 100%; color: Red;" Font-Bold="true" Text="Syntext Error: "
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:Label ID="lblRed" Style="width: 100%; color: Red;" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <asp:Repeater runat="server" ID="rptEvaluateResults">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label runat="server" Style="color: Blue" ID="Label13" Text='<%# Bind("VARIABLENAME") %>'></asp:Label>
                                                </div>
                                                <div class="col-md-9">
                                                    <asp:Label runat="server" Style="min-height: 20px; color: Blue; min-width: 200px; width: auto; height: auto;"
                                                        ID="lblConditions" Text='<%# Bind("DATA") %>'></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="row" id="divResult" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <asp:Label ID="Label15" Style="width: 100%; color: Green;" Font-Bold="true" Text="Results: "
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="col-md-10">
                                            <asp:Label ID="lblBlack" Style="width: 100%; color: Green" Font-Bold="true" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="divFinalSubmit" runat="server" visible="false">
                <div class="col-md-12">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-8">
                        <asp:LinkButton ID="lbtnFinalSubmit" Text="Final Submit" runat="server" Style="color: white; width: 100%;"
                            CssClass="btn btn-primary btn-sm cls-btnSave2" OnClick="lbtnFinalSubmit_Click" />
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
