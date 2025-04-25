<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DB_UPLOAD_HELP_INSTRUCTION.aspx.cs" Inherits="CTMS.DB_UPLOAD_HELP_INSTRUCTION" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="CommonFunctionsJs/btnSave_Required.js"></script>
    <script type="text/javascript" src="CommonFunctionsJs/TabIndex.js"></script>
    <script type="text/javascript">

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'MainContent_Uploadfile') {
                    document.getElementById("<%=btnMappedInstructionColumns.ClientID %>").click();
                }
            }
        }
        function ConfirmMsg(element) {
            alert(element);

            $(location).attr('href', window.location.href);
        }
    </script>


    <style type="text/css">
        .card {
            background-color: #ffffff;            
            margin: 20px;
            transition: 0.40s;
            border-radius: 0.50rem;
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
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
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary" id="divFileUpload" runat="server">
                        <div class="box-header with-border">
                            <h4 class="box-title" align="left" id="lblHeader" runat="server">Upload Help (Instructions)</h4>
                            <div class="pull-right" style="margin-right: 10px;">
                                <asp:LinkButton ID="lbtnExportInstructionSampleFile" runat="server" Text="Export Help (Instructions) Sample File" CssClass="btn btn-info btn-sm" ForeColor="White" Style="margin-top: 3px" OnClick="lbtnExportInstructionSampleFile_Click" TabIndex="1"> Export Instruction Sample File&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </div>
                        <div class="box-body align-center" >
                            <div align="left" style="margin-left: 5px; padding: 1px ">
                                <div class="card" style="padding: 30px;">
                                  
                                    <div class="row">
                                            <div class="col-md-3">
                                                <label> Select File :</label>
                                            </div>
                                            <div class="col-md-9">
                                                <asp:FileUpload ID="Uploadfile" runat="server" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ControlToValidate="Uploadfile"
                                                    ErrorMessage="Only Excel files are allowed"
                                                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                                                </asp:RegularExpressionValidator>
                                                <br />
                                                <asp:Label ID="Label1" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                                            </div>
                                        <div class="col-md-6">
                                            <asp:Button ID="btnMappedInstructionColumns" runat="server" CssClass="disp-none" OnClick="btnMappedInstructionColumns_Click" />
                                        </div>
                                        </div>
                                        
                                   
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>
                                             Select Module :</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpModule" CssClass="form-control required">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                     <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>
                                                CRF Help Instruction :</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpCRF_Help_Instruction" CssClass="form-control required">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                     <br />
                                    <div class="row">

                                        <div class="col-md-3">
                                            <label>
                                                Pharmacovigilance Help Instruction :</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" Width="330px" ID="drpPharma_Help_Instruction" CssClass="form-control required">
                                            </asp:DropDownList>
                                        </div>


                                    </div>
                                     <br />

                                    <div class="row">
                                       
                                            <div class="col-md-3">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button ID="btnUpload" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btnUpload_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click" />
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
    </div>
</asp:Content>
