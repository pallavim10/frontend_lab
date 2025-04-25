<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_UPLOAD_MODULE_FIELDS.aspx.cs" Inherits="CTMS.DM_UPLOAD_MODULE_FIELDS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script type="text/javascript">

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'MainContent_fileModuleField') {
                    document.getElementById("<%=btnMODULE_FIELD.ClientID %>").click();
                }
            }
        }

        function ConfirmMsg(element) {
            alert(element);

            location.reload();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-primary">
        <div class="box-header">
            <h4 class="box-title" id="lblHeader" runat="server">Upload Modules/Fields</h4>
            <div class="pull-right" style="margin-right: 10px;">
                <asp:LinkButton ID="lbtnExportSampleFile" runat="server" Text="Export Sample File" CssClass="btn btn-info btn-sm" ForeColor="White" OnClick="lbtnExportSampleFile_Click" Style="margin-top: 3px"> Export Sample File&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="box-body">
            <div align="left" style="margin-left: 20px">
                <div class="row">
                    <div class="col-md-2">
                        <label>Select File :</label>
                    </div>
                    <div class="col-md-6">
                        <asp:FileUpload runat="server" ID="fileModuleField" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="fileModuleField"
                            ErrorMessage="Only Excel files are allowed"
                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                        </asp:RegularExpressionValidator>
                        <br />
                        <asp:Label ID="Label1" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnMODULE_FIELD" runat="server" CssClass="disp-none" OnClick="btnMODULE_FIELD_Click" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="Col-md-4">
                        <div class="col-md-2" style="width: 166px;">
                            <label>Module Name:</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="drpModulename" CssClass="form-control width150px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="Col-md-4">
                        <div class="col-md-2" style="width: 166px;">
                            <label>Domain Name:</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="drpDomainName" CssClass="form-control width150px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="Col-md-4">
                        <div class="col-md-2" style="width: 166px;">
                            <label>Module SEQNO:</label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="drpModuleSeqNo" CssClass="form-control width150px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Field Name:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpFieldname" CssClass="form-control width150px required"></asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Variable Name:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpVarialbleName" CssClass="form-control width150px required"></asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Description:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpDescription" CssClass="form-control width150px required"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Control Type:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpControlType" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Decimal Format:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpFormat" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Field SEQNO:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpFieldSEQNO" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Length:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpLenght" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Upper Case Only:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpUppercase" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Bold:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpBold" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Underline:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpUnderline" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Read Only:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpReadonly" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Freetext:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpMultiline" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Required Information:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpRequired" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Mask Field:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpInvisible" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Auto Code:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpAutocode" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Auto Code Library:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpAutoCodeLibrary" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>In List:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpInlist" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>In List Editable:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpInlisteditable" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Lab Reference Range :</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpLabdefualt" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Seq. Auto-Numbering:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpAutonumber" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Linked Field :</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpReference" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Critical Data Point:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpCriticalDP" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Prefix YN:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpPrefixYN" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Prefix Data:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpPrefixdata" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Duplicates Check Info:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpDuplicatescheck" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Non-Repetitive:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpNonRepetative" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Mandatory Information:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpMandatory" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Protocol Predefined Data:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpDefaultData" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Linked Field (Parent):</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpLinkedfieldprent" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Linked Field (Child):</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpLinkedfieldchild" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>Medical Auth. Response:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpMedOpinion" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>Answer:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpAnswer" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="width: 166px;">
                        <label>PGL Type:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpPGLTYPE" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2" style="width: 166px;">
                        <label>SDV:</label>
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="drpSDV" CssClass="form-control width150px required">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-5">
                            &nbsp;
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnUploadModuleField" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnUploadModuleField_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnancelModuleField" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                        OnClick="btnancelModuleField_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
