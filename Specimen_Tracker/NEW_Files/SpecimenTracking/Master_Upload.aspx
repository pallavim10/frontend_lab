<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Master_Upload.aspx.cs" Inherits="SpecimenTracking.Master_Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script src="Scripts/btnSave_Required.js"></script>
    <style type="text/css">
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #0000ff;
        }

        .select2-container--default .select2-selection--single {
            margin-top: 2px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function pageLoad() {

            $('.select').select2();

        }
    </script>

    <script>
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'ContentPlaceHolder1_fileSpecimen') {
                    document.getElementById("<%=btnSIDCols.ClientID %>").click();
                }
                else if ($(fileUpload).attr('id') == 'ContentPlaceHolder1_fileSubject') {
                    document.getElementById("<%=btnSubcols.ClientID %>").click();
                }
                else if ($(fileUpload).attr('id') == 'ContentPlaceHolder1_FileBoxList') {
                    document.getElementById("<%=btnBoxList.ClientID %>").click();
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">Master Upload</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Master Upload</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Upload Specimen IDs</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExportSpecimen" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbtnExportSpecimen_Click" ForeColor="Black">Export Specimen IDs &nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                    <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Select File : &nbsp;</label>
                                                    <asp:Label ID="Label3" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:FileUpload ID="fileSpecimen" runat="server" class="form-control  w-50" />
                                                    <asp:RegularExpressionValidator ID="RegexValidator" runat="server"
                                                        ControlToValidate="fileSpecimen"
                                                        ValidationExpression="^.*\.(xlsx|xls|csv)$"
                                                        ErrorMessage="Please upload a valid Excel file (.xlsx|.xls|.csv)"
                                                        Display="Dynamic"
                                                        ForeColor="Red" />
                                                </div>
                                                <div>
                                                    <asp:Button ID="btnSIDCols" runat="server" CssClass="d-none" OnClick="btnSIDCols_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Select Column For SID : &nbsp;</label>
                                                    <asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlcolumnSID" runat="server" CssClass="form-control required w-100" SelectionMode="Single"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Select Column For Site : &nbsp;</label>
                                                    <asp:Label ID="Label2" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlcolumnSite" runat="server" CssClass="form-control required w-100" SelectionMode="Single"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <center>
                                    <asp:LinkButton runat="server" ID="lbtnSubmit" Text="Upload" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton runat="server" ID="lbtnCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="lbtnCancel_Click"></asp:LinkButton>

                                </center>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
        </section>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Upload Subject IDs</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbnexportSubjectIds" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" OnClick="lbnexportSubjectIds_Click" ForeColor="Black">Export Subject IDs &nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                   <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Select File : &nbsp;</label>
                                                    <asp:Label ID="Label4" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:FileUpload ID="fileSubject" runat="server" class="form-control w-50" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        ControlToValidate="fileSubject"
                                                        ValidationExpression="^.*\.(xlsx|xls|csv)$"
                                                        ErrorMessage="Please upload a valid Excel file (.xlsx|.xls|.csv)"
                                                        Display="Dynamic"
                                                        ForeColor="Red" />
                                                </div>
                                                <div>
                                                    <asp:Button ID="btnSubcols" runat="server" CssClass="d-none" OnClick="btnSubcols_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Select Column For Site ID : &nbsp;</label>
                                                    <asp:Label ID="Label6" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlSiteID" runat="server" CssClass="form-control required1 w-100" SelectionMode="Single"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Select Column For Subject ID : &nbsp;</label>
                                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="ddlSubjectID" runat="server" CssClass="form-control required1 w-100" SelectionMode="Single"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <center>
                                    <asp:LinkButton runat="server" ID="btnSubSubject" Text="Upload" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave1" OnClick="btnSubSubject_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton runat="server" ID="btnCalSubject" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="btnCalSubject_Click"></asp:LinkButton>
                                </center>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
        </section>
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Upload Box List</h3>
                                <div class="pull-right">
                                    <asp:LinkButton ID="lbtnExportBoxlist" runat="server" Font-Size="14px" Style="margin-top: 3px;" CssClass="btn btn-default" ForeColor="Black" OnClick="lbtnExportBoxlist_Click">Export Box List &nbsp;<span class="fas fa-download btn-xs"></span></asp:LinkButton>
                                    &nbsp;&nbsp; 
                                   <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Select File : &nbsp;</label>
                                                    <asp:Label ID="Label7" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:FileUpload ID="FileBoxList" runat="server" class="form-control w-50" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                        ControlToValidate="FileBoxList"
                                                        ValidationExpression="^.*\.(xlsx|xls|csv)$"
                                                        ErrorMessage="Please upload a valid Excel file (.xlsx|.xls|.csv)"
                                                        Display="Dynamic"
                                                        ForeColor="Red" />
                                                </div>
                                                <div>
                                                    <asp:Button ID="btnBoxList" runat="server" CssClass="d-none" OnClick="btnBoxList_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Select Column For Site Id: &nbsp;</label>
                                                    <asp:Label ID="Label11" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpsiteId" runat="server" CssClass="form-control required2 w-100" SelectionMode="Single"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Select Column For Sequence No: &nbsp;</label>
                                                    <asp:Label ID="Label8" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpSequenceNo" runat="server" CssClass="form-control required2 w-100" SelectionMode="Single"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="rows">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Select Column For Slot No : &nbsp;</label>
                                                    <asp:Label ID="Label10" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpslotno" runat="server" CssClass="form-control required2 w-100" SelectionMode="Single"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Select Column For Box No: &nbsp;</label>
                                                    <asp:Label ID="Label9" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                                                    <asp:DropDownList ID="drpBoxno" runat="server" CssClass="form-control required2 w-100" SelectionMode="Single"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <center>
                                    <asp:LinkButton runat="server" ID="btnBoxUpload" Text="Upload" ForeColor="White" CssClass="btn btn-primary btn-sm cls-btnSave2" OnClick="btnBoxUpload_Click"></asp:LinkButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton runat="server" ID="btnBoxCancel" Text="Cancel" ForeColor="White" CssClass="btn btn-primary btn-sm" OnClick="btnBoxCancel_Click"></asp:LinkButton>
                                </center>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
        </section>
    </div>
</asp:Content>
