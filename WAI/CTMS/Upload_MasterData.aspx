<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Upload_MasterData.aspx.cs" Inherits="CTMS.Upload_MasterData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Upload Data
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
            <div class="col-md-6">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left;">
                        <h4 class="box-title" align="left">
                            Upload Sponsor Master
                        </h4>
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select File :</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:FileUpload runat="server" ID="fileSponserMaster" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnUploadSponserMaster" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="btnUploadSponserMaster_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnancelSponserMaster" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnancelSponserMaster_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left;">
                        <h4 class="box-title" align="left">
                            Upload Employee Master
                        </h4>
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select File :</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:FileUpload runat="server" ID="fileEmployeeMaster" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnUploadEmpoyeeMaster" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnUploadEmpoyeeMaster_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnancelSponserMaster_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-6">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px; width: 100%;">
                        <h4 class="box-title" align="left">
                            Upload Site Master
                        </h4>
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select File :</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:FileUpload runat="server" ID="fileSiteMaster" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnUploadSiteMaster" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnUploadSiteMaster_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button3" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnancelSponserMaster_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px; width: 100%;">
                        <h4 class="box-title" align="left">
                            Upload Investigator Master
                        </h4>
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select File :</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:FileUpload runat="server" ID="fileINVMaster" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnUploadInvMaster" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnUploadInvMaster_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button5" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnancelSponserMaster_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-6">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left;">
                        <h4 class="box-title" align="left">
                            Upload Investigator Team Members
                        </h4>
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select File :</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:FileUpload runat="server" ID="fileINVMEMBER" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnUploadINVTEAMMEM" Text="Upload" runat="server" 
                                            CssClass="btn btn-primary btn-sm cls-btnSave" onclick="btnUploadINVTEAMMEM_Click"
                                            />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button4" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnancelSponserMaster_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left;">
                        <h4 class="box-title" align="left">
                            Upload Ethics Commitee
                        </h4>
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select File :</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:FileUpload runat="server" ID="fileEthicsCommity" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnuploadEthicsCommity" Text="Upload" runat="server" 
                                            CssClass="btn btn-primary btn-sm" onclick="btnuploadEthicsCommity_Click"
                                            />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button7" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnancelSponserMaster_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div class="row">
        <div class="col-md-12">
            <div class="col-md-6">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left;">
                        <h4 class="box-title" align="left">
                            Upload Ethics Commitee Team Member
                        </h4>
                    </div>
                    <div class="box-body">
                        <div align="left" style="margin-left: 5px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>
                                            Select File :</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:FileUpload runat="server" ID="fileEthicsCommityMem" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnUploadEthicsCommityMem" Text="Upload" runat="server" 
                                            CssClass="btn btn-primary btn-sm cls-btnSave" onclick="btnUploadEthicsCommityMem_Click" 
                                            />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button6" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnancelSponserMaster_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
