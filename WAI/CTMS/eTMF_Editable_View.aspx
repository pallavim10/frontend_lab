<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_Editable_View.aspx.cs" Inherits="CTMS.eTMF_Editable_View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <style>
        .label
        {
            display: inline-block;
            max-width: 100%;
            margin-bottom: -2px;
            font-weight: bold;
            font-size: 13px;
            margin-left: 0px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                stateSave: false,
                fixedHeader: true
            });

        }

        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                stateSave: false,
                fixedHeader: true
            });

        });

        function DOC_TRACKING(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_DOCUMENT_TRACKING.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=600";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function VERSION_HISTORY(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_VERSION_HISTORY.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

    </script>
    <style>
        .layer1
        {
            color: #0000ff;
        }
        .layer2
        {
            color: #800000;
        }
        .layer3
        {
            color: #008000;
        }
        .layer4
        {
            color: Black;
        }
        .layerFiles
        {
            color: #800000;
            font-style: italic;
        }
    </style>
    <script>
        function ChangeStatus(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var URL = $(element).closest('tr').find('td:eq(8)').find('input').val();

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=400";
            window.location.href = URL;
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="Editable Docs-Listing"></asp:Label>
            </h3>
        </div>
        <div class="form-group" style="margin-left:15px">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div runat="server" id="Div8" style="display: inline-flex">
                 <div style="display: inline-flex">
                            <label class="label width70px">
                                Using  :
                            </label>
                            <div class="Control">
                                <asp:DropDownList ID="ddlAction" runat="server" class="form-control drpControl"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                                    <asp:ListItem   Value="0" Text="--Select--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="eTMF"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
            </div>
            <div runat="server" id="Div2" style="display: inline-flex">
                <div style="display: inline-flex">
                    <label class="label width70px">
                        Structure :
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpDocType" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="drpDocType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div runat="server" id="Div1" style="display: inline-flex">
                <div style="display: inline-flex">
                    <label class="label width70px">
                        Zones :
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="ddlZone" runat="server" class="form-control drpControl" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlZone_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div runat="server" id="Div3" style="display: inline-flex">
                <div style="display: inline-flex">
                    <label class="label width70px">
                        Sections :
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="ddlSections" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSections_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div runat="server" id="Div4" style="display: inline-flex">
                <div style="display: inline-flex">
                    <label class="label width70px">
                        Artifacts :
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="ddlArtifacts" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlArtifacts_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div runat="server" id="Div5" style="display: inline-flex">
                <div style="display: inline-flex">
                    <label class="label width70px">
                        Documents :
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpDocument" runat="server" class="form-control drpControl"
                            AutoPostBack="true" OnSelectedIndexChanged="drpDocument_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div runat="server" id="Div6" style="display: inline-flex">
                <div style="display: inline-flex">
                    <label class="label width70px">
                        Status :
                    </label>
                    <div class="Control">
                        <asp:DropDownList ID="drpStatus" runat="server" class="form-control drpControl" AutoPostBack="true"
                            OnSelectedIndexChanged="drpStatus_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="All" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Draft" Value="Draft"></asp:ListItem>
                            <asp:ListItem Text="Old / Replaced" Value="Old_Replaced"></asp:ListItem>
                            <asp:ListItem Text="Final" Value="Final"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-primary">
        <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped layerFiles Datatable" OnPreRender="grd_data_PreRender"
            OnRowDataBound="gvFiles_RowDataBound" OnRowCommand="gvFiles_RowCommand">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                    ItemStyle-CssClass="disp-none" HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                    ItemStyle-CssClass="disp-none" HeaderText="SysFileName">
                    <ItemTemplate>
                        <asp:Label ID="SysFileName" runat="server" Text='<%# Bind("SysFileName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Country">
                    <ItemTemplate>
                        <asp:Label ID="COUNTRYNAME" Width="100%" ToolTip='<%# Bind("COUNTRYNAME") %>' CssClass="label"
                            runat="server" Text='<%# Bind("COUNTRYNAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SIte Id">
                    <ItemTemplate>
                        <asp:Label ID="SiteID" Width="100%" ToolTip='<%# Bind("SiteID") %>' CssClass="label"
                            runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Name" Width="100%" ToolTip='<%# Bind("AutoNomenclature") %>' CssClass="label"
                            runat="server" Text='<%# Bind("AutoNomenclature") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                            CssClass="label" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File Type" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbtnFileType" runat="server" Font-Size="Larger"><i id="ICONCLASS"
                            runat="server" class="fas fa-file-text"></i></asp:Label>
                        <asp:Label ID="lbl_FileSize" Width="100%" CssClass="label" Font-Size="X-Small" runat="server" Text='<%# Bind("CAL_Size") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="System Version" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                    ItemStyle-CssClass="disp-none">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VersionID" Width="100%" ToolTip='<%# Bind("VersionID") %>' CssClass="label"
                            runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" Width="100%" ToolTip='<%# Bind("Status") %>' CssClass="label"
                            runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="DOC_VERSIONNO" Width="100%" ToolTip='<%# Bind("DOC_VERSIONNO") %>'
                            CssClass="label" runat="server" Text='<%# Bind("DOC_VERSIONNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document DateTime" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="DOC_DATETIME" Width="100%" ToolTip='<%# Bind("DOC_DATETIME") %>' CssClass="label"
                            runat="server" Text='<%# Bind("DOC_DATETIME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Note" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="NOTE" Width="100%" ToolTip='<%# Bind("NOTE") %>' CssClass="label"
                            runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Uploaded By" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_User" Width="100%" ToolTip='<%# Bind("User") %>' CssClass="label"
                            runat="server" Text='<%# Bind("User") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Uploaded DateTime" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_UploadDateTime" Width="100%" ToolTip='<%# Bind("UploadDateTime") %>'
                            CssClass="label" runat="server" Text='<%# Bind("UploadDateTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnversionHistory" runat="server" ToolTip="Version History"
                            OnClientClick="return VERSION_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                            <i id="iconhistory" runat="server" class="fa fa-history" style="color: #333333;"
                                aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDocumentTrack" runat="server" ToolTip="Document Track" CommandArgument='<%# Eval("ID") %>'
                            OnClientClick="return DOC_TRACKING(this);"><i class="fa fa-map-marked-alt" style="color:#333333;" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDownloadDoc" runat="server" ToolTip="Download" CommandName="Download" CssClass="btn"
                            CommandArgument='<%# Eval("ID") %>'><i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDownloadDocEditable" runat="server" ToolTip="Download Other Type File" CssClass="btn"
                            CommandName="Download_OtherType_File" CommandArgument='<%# Eval("ID") %>'><i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
