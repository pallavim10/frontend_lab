<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="eTMF_ImportExportSpecs.aspx.cs" Inherits="CTMS.eTMF_ImportExportSpecs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 60px;
            width: 300px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false,
                fixedHeader: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }

        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {

                if ($(fileUpload).attr('id') == 'MainContent_fileSPECS') {
                    document.getElementById("<%=btnSPECS.ClientID %>").click();
                }
            }
        }

        $(document).ready(function () {
            $(".nav-tabs a").click(function (event) {
                event.preventDefault();
                $(this).parent().addClass("active");
                $(this).parent().siblings().removeClass("active");
                var tab = $(this).attr("href");
                $(".tab-content").not(tab).css("display", "none");
                $(tab).fadeIn();
            });
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Import/Export Specs
            </h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnID" runat="server" />
                    <asp:Label runat="server" ID="lblHeader" Text="Export Sample Specs" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div id="tabscontainer" class="nav-tabs-custom" runat="server">
        <ul class="nav nav-tabs">
            <li id="li1" runat="server" class="active"><a href="#tab-1" data-toggle="tab">Import</a></li>
            <li id="li2" runat="server"><a href="#tab-2" data-toggle="tab">Export </a></li>
            <li id="li3" runat="server"><a href="#tab-3" data-toggle="tab">History</a></li>
        </ul>
        <div class="tab">
            <div id="tab-1" class="tab-content current">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left"></h4>
                            </div>
                            <br />
                            <div class="box-body">
                                <div align="left" style="margin-left: 5px">
                                    <div class="row">
                                        <div class="col-md-1" style="width: 99px;">
                                            <label>
                                                Select File :</label>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:FileUpload runat="server" ID="fileSPECS" />
                                            <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                                                ControlToValidate="fileSPECS"
                                                ErrorMessage="Only Excel files are allowed"
                                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.xls|.XLS|.xlsx|.XLSX)$">
                                            </asp:RegularExpressionValidator>
                                            <asp:Button ID="btnSPECS" runat="server" CssClass="disp-none" OnClick="btnSPECS_Click" />
                                            <br />
                                            <asp:Label runat="server" ID="lblFileName"></asp:Label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnexportsample" runat="server" Text="Export Sample File" CssClass="btn btn-info" OnClick="btnexportsmaple_Click" />
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divImport" visible="false">
                            <div class="box box-primary">
                                <div class="box-header with-border" style="float: left;">
                                    <h4 class="box-title" align="left">Document Masters from Sheet :
                                        <asp:Label runat="server" ID="lblDocMasterSheetName"></asp:Label>
                                    </h4>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Zone Ref. No. Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpZoneRef" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Zone Name Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpZone" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Section Ref. No. Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpSectionRef" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Section Name Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpSection" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Artifact Ref. No. Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpArtifactRef" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Artifact Name Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpArtifact" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Artifact Definition Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpArtifactDefin" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-5">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                            <div class="box box-primary">
                                <div class="box-header with-border" style="float: left;">
                                    <h4 class="box-title" align="left">Expected Document from Sheet :
                        <asp:Label runat="server" ID="lblExpDocSheetName"></asp:Label>
                                    </h4>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Document Ref. No. Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpDocRef" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Document Name Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpDocName" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Replace Superseded Version Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpAutoReplace" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Document Level Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpVerType" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Ver. Date Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpVerDate" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Ver. SPEC Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpVerSPEC" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Access Control Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpUnblind" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                            <div class="box box-primary">
                                <div class="box-header with-border" style="float: left;">
                                    <h4 class="box-title" align="left">Document Additional settings from Sheet :
                        <asp:Label runat="server" ID="lblSettingSheetName"></asp:Label>
                                    </h4>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Date Convention Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpDateTitle" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Spec. Convention Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpSPECtitle" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Instructions Column :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpInstruction" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Select Comments :</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:DropDownList runat="server" ID="drpcomments" CssClass="form-control width200px required">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>

                        </div>
                        <div class="box box-primary" runat="server" id="divOptions" visible="false">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Spec. Options from Sheet :
                        <asp:Label runat="server" ID="lblOptionSheetName"></asp:Label>
                                </h4>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Zone Ref. No. Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpZoneRef_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Zone Name Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpZone_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Section Ref. No. Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpSectionRef_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Section Name Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpSection_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Artifact Ref. No. Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpArtifactRef_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Artifact Name Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpArtifact_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Document Ref. No. Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpDocRef_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Document Name Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpDocName_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Spec. Sequence No. Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpSEQNO_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Spec. Text Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpTEXT_SPEC" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <div class="box box-primary" runat="server" id="divGroup" visible="false">
                            <div class="box-header with-border" style="float: left;">
                                <h4 class="box-title" align="left">Groups from Sheet :
                                    <asp:Label runat="server" ID="lblGroupSheetName"></asp:Label>
                                </h4>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Zone Ref. No. Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpZoneRef_GRP" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Zone Name Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpZone_GRP" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Section Ref. No. Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpSectionRef_GRP" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Section Name Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpSection_GRP" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Artifact Ref. No. Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpArtifactRef_GRP" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Artifact Name Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpArtifact_GRP" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Group Name Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpGroupName_GRP" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Event Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpEvent_GRP" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <label>
                                            Select Milestone Column :</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="drpMilestone_GRP" CssClass="form-control width200px required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <div class="box box-primary">
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnRandUpload" Text="Import" runat="server" CssClass="btn btn-primary btn-sm cls-btnSave"
                                            OnClick="btnRandUpload_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnRandCancel" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btnRandCancel_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="tab-2" class="tab-content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left"></h4>
                        </div>
                        <br />
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-6">
                                            <label>
                                                Click this button to export the uploaded specifications</label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:LinkButton ID="LbtnExportSpecs" runat="server" ForeColor="White" CssClass="btn btn-info" OnClick="LbtnExportSpecs_Click"><i class="fa fa-download"></i> &nbsp; Export Specs &nbsp;</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        &nbsp;
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="tab-3" class="tab-content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h4 class="box-title" align="left"></h4>
                            <div class="pull-right">
                                <asp:LinkButton ID="lbnsExport" runat="server" CssClass="btn btn-info" OnClick="lbnsExport_Click" ForeColor="White" Style="margin-top: 3px; margin-right: 3px;">Export history&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <div class="row">
                                <div style="width: 96%; overflow: auto; margin-left: 10px;">
                                    <asp:GridView ID="Grd_spec_Log" runat="server" AutoGenerateColumns="True" Width="98%"
                                        CssClass="table table-bordered table-striped Datatable" OnPreRender="Grd_spec_Log_PreRender">
                                    </asp:GridView>
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

