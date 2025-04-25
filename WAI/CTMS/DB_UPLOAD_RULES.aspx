<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_UPLOAD_RULES.aspx.cs" Inherits="CTMS.DB_UPLOAD_RULES" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript">

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'MainContent_Uploadfile') {
                    document.getElementById("<%=btnMappedRulesColumns.ClientID %>").click();
                }
            }
        }

        function UploadFile1(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'MainContent_FileUploadRuleVariable') {
                    document.getElementById("<%=btnMappedRuleVariables.ClientID %>").click();
                }
            }
        }

        function ConfirmMsg(element) {
            alert(element);

            $(location).attr('href', window.location.href);
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
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary" id="divFileUpload" runat="server">
                        <div class="box-header with-border">
                            <h4 class="box-title" align="left" id="lblHeader" runat="server">Upload Rule</h4>
                            <div class="pull-right" style="margin-right: 10px;">
                                <asp:LinkButton ID="lbtnExportRuleSampleFile" runat="server" Text="Export Rule Sample File" CssClass="btn btn-info btn-sm" ForeColor="White" Style="margin-top: 3px" OnClick="lbtnExportRuleSampleFile_Click"> Export Rule Sample File&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Select File :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:FileUpload ID="Uploadfile" runat="server" TabIndex="1" />
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ControlToValidate="Uploadfile"
                                                ErrorMessage="Only Excel files are allowed"
                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                                            </asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="Label1" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnMappedRulesColumns" runat="server" CssClass="disp-none" OnClick="btnMappedRulesColumns_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Visit :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVisit" CssClass="form-control required" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Module Domain :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleDomain" CssClass="form-control required" TabIndex="3">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Field Variable :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVariable" CssClass="form-control required" TabIndex="4">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Rule ID :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleRuleid" CssClass="form-control required" TabIndex="5">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Sequence No :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleSeqno" CssClass="form-control required" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Nature :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleNature" CssClass="form-control  required" TabIndex="7">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Action :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleAction" CssClass="form-control required" TabIndex="8">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Rule Description:</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleDescripation" CssClass="form-control required" TabIndex="9">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Query Text :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleQuerytext" CssClass="form-control required" TabIndex="10">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Condition :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleCondition" CssClass="form-control required" TabIndex="11">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Formula For Set Value :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleFormulaSetValue" CssClass="form-control required" TabIndex="12">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-5">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-5">
                                            <asp:Button ID="btnUpload" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpload_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <div class="box box-primary" id="divRuleVariable" runat="server">
                        <div class="box-header with-border">
                            <h4 class="box-title" align="left" id="lblHeaderVariable" runat="server">Upload Rule Variable</h4>
                            <div class="pull-right" style="margin-right: 10px;">
                                <asp:LinkButton ID="lbtnExportRuleVariableSample" runat="server" Text="Export Rule Variable Name Sample File" CssClass="btn btn-info btn-sm" ForeColor="White" Style="margin-top: 3px" OnClick="lbtnExportRuleVariableSample_Click"> Export Rule Variable Name Sample File&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Rule Variable File:</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:FileUpload ID="FileUploadRuleVariable" runat="server" TabIndex="13" />
                                            <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                                                ControlToValidate="FileUploadRuleVariable"
                                                ErrorMessage="Only Excel files are allowed"
                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                                            </asp:RegularExpressionValidator>
                                            <br />
                                            <asp:Label ID="Label2" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnMappedRuleVariables" runat="server" CssClass="disp-none" OnClick="btnMappedRuleVariables_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Rule ID :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVarRuleId" CssClass="form-control required1" TabIndex="14">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Column Name :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVarColumnName" CssClass="form-control required1" TabIndex="15">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Sequence No :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVarSeqNo" CssClass="form-control required1" TabIndex="16">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Visit :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVarVisit" CssClass="form-control required1" TabIndex="17">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Module Domain :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVarDomain" CssClass="form-control required1" TabIndex="18" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Field Variable :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVarFieldVariable" CssClass="form-control required1" TabIndex="19">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Condition :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVarCondition" CssClass="form-control required1" TabIndex="20">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Is Derived :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVarIsDerived" CssClass="form-control required1" TabIndex="21">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-3">
                                            <label>
                                                Is Derived Formula :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpRuleVarIsderivedformula" CssClass="form-control required1" TabIndex="22">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-5">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-5">
                                            <asp:Button ID="btnuploadruleVariable" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnuploadruleVariable_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btncancelRule" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btncancelRule_Click" />
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
    </div>
</asp:Content>
