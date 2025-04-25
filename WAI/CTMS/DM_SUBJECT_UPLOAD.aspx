<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_SUBJECT_UPLOAD.aspx.cs" Inherits="CTMS.DM_SUBJECT_UPLOAD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'MainContent_fileScreen') {
                    document.getElementById("<%=btnScrCols.ClientID %>").click();
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
    <div class="box box-primary" style="min-height: 300px;">
        <div class="box-header with-border">
            <h4 class="box-title" align="left">Upload Screening IDs
            </h4>
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <div class="pull-right" style="margin-right: 10px;">
                <asp:LinkButton ID="lbtnExportUploadScreening" ForeColor="White" runat="server" Text="Export Screening IDs" CssClass="btn btn-primary btn-sm" OnClick="lbtnExportUploadScreening_Click">Export Existing&nbsp;&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
            </div>
            <div class="pull-right" style="margin-right: 10px;">
                <asp:LinkButton ID="lbtnExportSampleFile" runat="server" Text="Export Sample File" CssClass="btn btn-info btn-sm" ForeColor="White" OnClick="lbtnExportSampleFile_Click"> Export Sample File&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
        <br />
        <div class="box-body">
            <div align="left" style="margin-left: 5px">
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select File :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:FileUpload runat="server" ID="fileScreen" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="fileScreen"
                                ErrorMessage="Only Excel files are allowed"
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                            </asp:RegularExpressionValidator>
                            <br />
                            <asp:Label ID="Label1" CssClass="warning" runat="server" ForeColor="Red" Text="[ Note : Excel Sheet Columns should not contain Space. ]"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnScrCols" runat="server" CssClass="disp-none" OnClick="btnScrCols_Click" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Country Column :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpScrCountryID" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Site ID Column :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpScrSiteID" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Sub-Site ID Column :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpScrSubSiteID" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Screening ID Column :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="drpScrID" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select Parent ID Column :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="ddlScrIDENT" CssClass="form-control width200px required">
                            </asp:DropDownList>
                        </div>
                    </div>
                   <div class="col-md-6">
                        <div class="col-md-6">
                            <label>
                                Select System :</label>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="ddlScrSystem" CssClass="form-control width200px required">
                                <asp:ListItem Text="--Select--" Value="0" />
                                 <asp:ListItem Text="DDC" Value="DDC" />
                                <asp:ListItem Text="DDM" Value="DDM" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            &nbsp;
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnScrUpload" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                OnClick="btnScrUpload_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnScrCancel" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                        OnClick="btnScrCancel_Click" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
