<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_UploadData.aspx.cs" Inherits="CTMS.DM_UploadData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Upload Patient Data
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="height: 300px;">
                <div class="box-body">
                    <div align="left" style="margin-left: 5px">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    <label>
                                        Select File :</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:FileUpload runat="server" ID="filePatData" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="filePatData"
                                        ErrorMessage="Only Excel files are allowed"
                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="Button1" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnSubjectData_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button3" Text="Cancel" runat="server" CssClass="btn btn-danger btn-sm"
                                            OnClick="btnancelModuleField_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
