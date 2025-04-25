<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_UploadExcel.aspx.cs" Inherits="CTMS.DM_UploadExcel" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function Set_Map() {

            var test = "DM_Mapping_Tool.aspx";
            var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test, '_blank');
            return false;
        }

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'MainContent_fileModuleField') {
                    document.getElementById("<%=btnMODULE_FIELD.ClientID %>").click();
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Data Upload
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
            <div class="box box-primary" style="min-height: 300px;">
                <div class="box-header with-border" style="float: left;">
                    <h4 class="box-title" align="left">
                        Upload Modules/Fields
                    </h4>
                </div>
                <div class="box-body">
                    <div align="left" style="margin-left: 5px">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label>
                                        Select File :</label>
                                </div>
                                <div class="col-md-6">
                                    <asp:FileUpload runat="server" ID="fileModuleField" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnMODULE_FIELD" runat="server" CssClass="disp-none" OnClick="btnMODULE_FIELD_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Select ModuleName Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpModulename" CssClass="form-control width200px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Select FieldName Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpFieldname" CssClass="form-control width200px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Select VariableName Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpVarialbleName" CssClass="form-control width200px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Select ControlType Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpControlType" CssClass="form-control width200px required5">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Select Answer Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpAnswer" CssClass="form-control width200px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Select SEQNO Column :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpSEQNO" CssClass="form-control width200px required5">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Select Length Column:</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpLenght" CssClass="form-control width200px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="col-md-6">
                                    <label>
                                        Select Description Column:</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="drpDescription" CssClass="form-control width200px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnUploadModuleField" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                        OnClick="btnUploadModuleField_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnancelModuleField" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                        OnClick="btnancelModuleField_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
   <%-- <div class="row">
        <div class="col-md-12">
            <div class="col-md-6">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left;">
                        <h4 class="box-title" align="left">
                            Upload Subject Master
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
                                        <asp:FileUpload runat="server" ID="fileSubject" />
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
                                        <asp:Button ID="btnSubject" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnSubject_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button2" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnancelModuleField_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-6">
                <div class="box box-primary" style="min-height: 300px;">
                    <div class="box-header with-border" style="float: left; top: 0px; left: 0px; width: 100%;">
                        <h4 class="box-title" align="left">
                            Upload Patient Data
                        </h4>
                        <div class="pull-right" style="margin-right: 5%; margin-top: 1%;">
                            <asp:LinkButton runat="server" ID="lbtnViewMap" Text="Set Mappings" OnClientClick="Set_Map();"
                                CssClass="btn btn-primary btn-sm cls-btnSave3" Style="color: white;"></asp:LinkButton>
                        </div>
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
                                        <asp:FileUpload runat="server" ID="filePatData" />
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
                                        <asp:Button ID="Button1" Text="Upload" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnSubjectData_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button3" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                            OnClick="btnancelModuleField_Click" />
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
