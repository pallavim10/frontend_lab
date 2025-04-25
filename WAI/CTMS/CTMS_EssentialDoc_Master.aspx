<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTMS_EssentialDoc_Master.aspx.cs" Inherits="CTMS.CTMS_EssentialDoc_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/eTMF/eTMF_ConfirmMsg.js"></script>
    <script src="CommonFunctionsJs/eTMF/eTMF_Show_AuditTrail.js"></script>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <style type="text/css">
        .fixTableHead {
            overflow-y: auto;
            max-height: 300px;
            min-height: 300px;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#btnSubmitDocType").click(function () {
                var txtName = $("#txtDocType");
                var result = $.trim(txtName.val());
                txtName.val(result);
            });
        });
    </script>
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Manage Filing Structure</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">Add TMF Model
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label>
                                                    Enter TMF Model:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDocType" ID="RequiredFieldValidator3"
                                                    ValidationGroup="DocType" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox Style="width: 200px;" ID="txtDocType" ValidationGroup="DocType" runat="server"
                                                    CssClass="form-control" onkeypress="return AvoidSpace(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:LinkButton ID="lbtnAudittrail" runat="server" Font-Size="12px" CssClass="btn btn-info" OnClientClick="return showAuditTrail('CTMS_Doc_DocType', this);" ForeColor="White">Audit Trail&nbsp;<span class="glyphicon glyphicon-time 2x"></span></asp:LinkButton>
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
                                                <asp:Button ID="btnSubmitDocType" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                    ValidationGroup="DocType" OnClick="btnSubmitDocType_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnupdateDocType" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                    ValidationGroup="DocType" OnClick="btnupdateDocType_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelDocType" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btncancelDocType_Click" />
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
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">Add Zones
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Select TMF Model:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlDocType"
                                                    ValidationGroup="folder" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:DropDownList Style="width: 200px;" ID="ddlDocType" runat="server" AutoPostBack="true"
                                                    class="form-control drpControl" ValidationGroup="folder" OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Zone Name:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFolder" ID="reqDept"
                                                    ValidationGroup="folder" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Style="width: 200px;" ID="txtFolder" ValidationGroup="folder" runat="server"
                                                    CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Sequence Number:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSeq"
                                                    ValidationGroup="folder" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Width="200px" ID="txtSeq" ValidationGroup="folder" runat="server" CssClass="form-control"
                                                    MaxLength="2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="col-md-4">
                                                <label>
                                                    Enter Reference Number:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFolderRef"
                                                    ValidationGroup="folder" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox Width="200px" ID="txtFolderRef" ValidationGroup="folder" runat="server"
                                                    CssClass="form-control" MaxLength="2"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button ID="btnSubmitFolder" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                    ValidationGroup="folder" OnClick="btnSubmitFolder_Click" />&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnupdateFolder" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                    ValidationGroup="folder" OnClick="btnupdateFolder_Click" />&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btncancelFolder" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btncancelFolder_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h4 class="box-title" align="left">Records
                            </h4>
                            <div class="pull-right">
                                <asp:LinkButton ID="lbnZoneExport" runat="server" OnClick="lbnZoneExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Zone&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                &nbsp;&nbsp;
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div>
                                            <asp:GridView ID="gvFolder" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered Datatable table-striped"
                                                Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvFolder_RowCommand"
                                                OnRowDataBound="gvFolder_RowDataBound" OnPreRender="gvFolder_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnupdateFolder" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="EditZone" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Seq. No." ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reference No." ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Zone Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDept" runat="server" Text='<%# Bind("Folder") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('CTMS_Doc_Folder', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtndeleteFolder" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="DeleteZone" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this zone : ", Eval("Folder")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">Add Sections
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Select Zone:</label>
                                        </div>
                                        <div class="col-md-1 requiredSign">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFolder"
                                                ValidationGroup="SubFolder" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:DropDownList Style="width: 200px;" ID="ddlFolder" runat="server" AutoPostBack="true"
                                                class="form-control drpControl" ValidationGroup="SubFolder" OnSelectedIndexChanged="ddlFolder_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Section Name:</label>
                                        </div>
                                        <div class="col-md-1 requiredSign">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubFolder"
                                                ValidationGroup="SubFolder" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtSubFolder" Style="width: 200px;" ValidationGroup="SubFolder"
                                                runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Sequence Number:</label>
                                        </div>
                                        <div class="col-md-1 requiredSign">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSubSeq"
                                                ValidationGroup="SubFolder" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Width="200px" ID="txtSubSeq" ValidationGroup="SubFolder" runat="server"
                                                CssClass="form-control" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Reference Number:</label>
                                        </div>
                                        <div class="col-md-1 requiredSign">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtSubRef"
                                                ValidationGroup="SubFolder" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Width="200px" ID="txtSubRef" ValidationGroup="SubFolder" runat="server"
                                                CssClass="form-control" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <br />
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Button ID="btnsubmitSubFolder" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                ValidationGroup="SubFolder" OnClick="btnsubmitSubFolder_Click" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnupdateSubFolder" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                ValidationGroup="SubFolder" OnClick="btnupdateSubFolder_Click" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btncancelSubFolder" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btncancelSubFolder_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h4 class="box-title" align="left">Records
                            </h4>
                            <div class="pull-right">
                                <asp:LinkButton ID="lbtnSectionExport" runat="server" OnClick="lbtnSectionExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Section&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                &nbsp;&nbsp;
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div>
                                            <asp:GridView ID="gvSubFolder" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered Datatable table-striped"
                                                Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvSubFolder_RowCommand"
                                                OnRowDataBound="gvSubFolder_RowDataBound" OnPreRender="gvFolder_PreRender">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelID1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnupdateSubFolder" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Seq. No." ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reference No." ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Section Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTask" runat="server" Text='<%# Bind("Sub_Folder_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('CTMS_Doc_SubFolder', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtndeleteSubFolder" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="Delete1" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this section : ", Eval("Sub_Folder_Name")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">Add Artifacts
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <label>
                                                Select Section:</label>
                                        </div>
                                        <div class="col-md-1 requiredSign">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSection"
                                                ValidationGroup="Artifact" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:DropDownList Style="width: 200px;" ID="ddlSection" runat="server" AutoPostBack="true"
                                                class="form-control drpControl" ValidationGroup="Artifact" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Artifact Name:</label>
                                        </div>
                                        <div class="col-md-1 requiredSign">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtArtifact"
                                                ValidationGroup="Artifact" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox ID="txtArtifact" Style="width: 200px;" ValidationGroup="Artifact" runat="server"
                                                Text="" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Sequence Number:</label>
                                        </div>
                                        <div class="col-md-1 requiredSign">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtArtifactSeq"
                                                ValidationGroup="Artifact" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Width="200px" ID="txtArtifactSeq" ValidationGroup="Artifact" runat="server"
                                                CssClass="form-control" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <label>
                                                Enter Reference Number:</label>
                                        </div>
                                        <div class="col-md-1 requiredSign">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtArtifactRef"
                                                ValidationGroup="Artifact" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Width="200px" ID="txtArtifactRef" ValidationGroup="Artifact" runat="server"
                                                CssClass="form-control" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="col-md-4">
                                            <label>
                                                Definition :</label>
                                        </div>
                                        <div class="col-md-1 requiredSign">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtDefinition"
                                                ValidationGroup="Artifact" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:TextBox Width="200px" Height="50px" ID="txtDefinition" ValidationGroup="Artifact" runat="server"
                                                CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Button ID="btnsubmitArtifact" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                ValidationGroup="Artifact" OnClick="btnsubmitArtifact_Click" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnupdateArtifact" Text="Update" runat="server" CssClass="btn btn-primary btn-sm"
                                                ValidationGroup="Artifact" Visible="false" OnClick="btnupdateArtifact_Click" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btncancelArtifact" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="btncancelArtifact_Click" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h4 class="box-title" align="left">Records
                            </h4>
                            <div class="pull-right">
                                <asp:LinkButton ID="lbtnArtifactExport" runat="server" OnClick="lbtnArtifactExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Artifact&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                                &nbsp;&nbsp;
                            </div>
                        </div>
                        <br />
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div style="width: 100%; height: 341px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvArtifact" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered Datatable table-striped"
                                                    Style="width: 96%; border-collapse: collapse; margin-left: 20px;" OnRowCommand="gvArtifact_RowCommand" OnRowDataBound="gvArtifact_RowDataBound" OnPreRender="gvFolder_PreRender">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LabelID2" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnupdateSubFolder" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                    CommandName="Edit1" ToolTip="Edit"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Seq. No." ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSEQNO" runat="server" Text='<%# Bind("SEQNO") %>'></asp:Label>
                                                                <asp:HiddenField ID="ArtifactID" runat="server" Value='<%# Eval("ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reference No." ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Artifact Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblArtfact" runat="server" Text='<%# Bind("Artifact_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Definition">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDefinition" runat="server" Text='<%# Bind("DEFINITION") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Audit Trail" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnAudttrail" runat="server" OnClientClick="return showAuditTrail('CTMS_Doc_Artifact', this);" ToolTip="Audit Trail"><i class="fa fa-clock-o" style="color:blue; font-size:15px"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtndeleteSubFolder" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                    CommandName="Delete1" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to delete this Artifact : ", Eval("Artifact_Name")) %>' ToolTip="Delete"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbnZoneExport" />
            <asp:PostBackTrigger ControlID="lbtnSectionExport" />
            <asp:PostBackTrigger ControlID="lbtnArtifactExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
