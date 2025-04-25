<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Sponsor_FileStructure.aspx.cs" Inherits="CTMS.Sponsor_FileStructure" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function UploadDoc(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var DocID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_UploadDocument_Doc.aspx?DocID=" + DocID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=1100";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function ADD_NEW_ACTIVITY(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var ArtifactsID = $(element).closest('tr').find('td:eq(1)').find('span').text();
            var DOCTYPEID = $('#MainContent_drpDocType').val();

            var test = "eTMF_ADD_NEW_ACTIVITY.aspx?ArtifactsID=" + ArtifactsID + "&DOCTYPEID=" + DOCTYPEID;

            window.location.href = test;
            return false;

        }

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

        }

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%">
                <asp:Label runat="server" ID="lblHeader" Text="Filing Structure" />
            </h3>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
    </div>
    <div class="box box-success">
        <div class="col-md-12 disp-none">
            <div class="col-md-6">
                <div class="col-md-3">
                    <label>
                        Select Structure :</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="drpDocType" runat="server" CssClass="form-control width300px"
                        OnSelectedIndexChanged="drpDocType_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <asp:GridView ID="gvFolder" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped layer1" OnRowDataBound="gvFolder_RowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="txtCenter" HeaderStyle-CssClass="txt_center"
                    ControlStyle-CssClass="txt_center">
                    <HeaderTemplate>
                        <a href="JavaScript:ManipulateAll('_Folder');" id="_Folder" style="color: #333333"><i
                            id="img_Folder" class="icon-plus-sign-alt"></i></a>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div runat="server" id="anchor">
                            <a href="JavaScript:divexpandcollapse('_Folder<%# Eval("ID") %>');" style="color: #333333">
                                <i id="img_Folder<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false" HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TaskId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ref." ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                            runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Zones" ItemStyle-Width="80%">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Folder" Width="100%" ToolTip='<%# Bind("Folder") %>' CssClass="label"
                            runat="server" Text='<%# Bind("Folder") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                            <td colspan="100%" style="padding: 2px;">
                                <div style="float: right; font-size: 13px;">
                                </div>
                                <div>
                                    <div class="rows">
                                        <div class="col-md-12">
                                            <div id="_Folder<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                <asp:GridView ID="gvSubFolder" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped layer2" OnRowDataBound="gvSubFolder_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                            HeaderStyle-CssClass="txt_center">
                                                            <HeaderTemplate>
                                                                <a href="JavaScript:ManipulateAll('_SubFolder');" id="_Folder" style="color: #333333">
                                                                    <i id="img_SubFolder" class="icon-plus-sign-alt"></i></a>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div runat="server" id="anchor">
                                                                    <a href="JavaScript:divexpandcollapse('_SubFolder<%# Eval("SubFolder_ID") %>');"
                                                                        style="color: #333333"><i id="img_SubFolder<%# Eval("SubFolder_ID") %>" class="icon-plus-sign-alt">
                                                                        </i></a>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                            ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_FolderID" runat="server" Text='<%# Bind("Folder_ID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_SubFolderID" runat="server" Text='<%# Bind("SubFolder_ID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref." ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                                                                    runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sections" ItemStyle-Width="80%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SubFolder" Width="100%" ToolTip='<%# Bind("Sub_Folder_Name") %>'
                                                                    CssClass="label" runat="server" Text='<%# Bind("Sub_Folder_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td colspan="100%" style="padding: 2px;">
                                                                        <div style="float: right; font-size: 13px;">
                                                                        </div>
                                                                        <div>
                                                                            <div class="rows">
                                                                                <div class="col-md-12">
                                                                                    <div id="_SubFolder<%# Eval("SubFolder_ID") %>" style="display: none; position: relative;
                                                                                        overflow: auto;">
                                                                                        <asp:GridView ID="gvArtifact" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                            CssClass="table table-bordered table-striped layer3" OnRowDataBound="gvArtifact_RowDataBound">
                                                                                            <Columns>
                                                                                                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                                                    HeaderStyle-CssClass="txt_center">
                                                                                                    <HeaderTemplate>
                                                                                                        <a href="JavaScript:ManipulateAll('_Docs');" id="_Folder" style="color: #333333"><i
                                                                                                            id="img_Docs" class="icon-plus-sign-alt"></i></a>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <div runat="server" id="anchor">
                                                                                                            <a href="JavaScript:divexpandcollapse('_Docs<%# Eval("ID") %>');" style="color: #333333">
                                                                                                                <i id="img_Docs<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                                                                        </div>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                                                                    ItemStyle-CssClass="disp-none" HeaderText="ArtifactesID">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblArtifacts" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                                                                    ItemStyle-CssClass="disp-none" HeaderText="MainRefNo">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="MainRefNo" runat="server" Text='<%# Bind("MainRefNo") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                                                                    ItemStyle-CssClass="disp-none" HeaderText="DocTypeId">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="DocTypeId" runat="server" Text='<%# Bind("DocTypeId") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Ref." ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                                                                                                            runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Artifacts" ItemStyle-Width="80%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Artifact" Width="100%" ToolTip='<%# Bind("Artifact_Name") %>'
                                                                                                            CssClass="label" runat="server" Text='<%# Bind("Artifact_Name") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                                                                    ItemStyle-CssClass="disp-none">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="btnAddNewActivity" runat="server" Text="Add New Activity" OnClientClick="return ADD_NEW_ACTIVITY(this);"
                                                                                                            ToolTip="Add New Activity"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <tr>
                                                                                                            <td colspan="100%" style="padding: 2px;">
                                                                                                                <div style="float: right; font-size: 13px;">
                                                                                                                </div>
                                                                                                                <div>
                                                                                                                    <div class="rows">
                                                                                                                        <div class="col-md-12">
                                                                                                                            <div id="_Docs<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                                                                                <asp:GridView ID="gvDocs" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                                                    OnRowDataBound="gvDocs_RowDataBound" CssClass="table table-bordered table-striped layer1">
                                                                                                                                    <Columns>
                                                                                                                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                                                                                                            ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:TemplateField HeaderText="Ref." ItemStyle-Width="15%" ItemStyle-CssClass="txt_center">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                                                                                                                                                    runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:TemplateField HeaderText="Unique Ref." ItemStyle-Width="15%" ItemStyle-CssClass="txt_center">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="lbl_UniqueRefNo" Width="100%" ToolTip='<%# Bind("UniqueRefNo") %>'
                                                                                                                                                    CssClass="label" runat="server" Text='<%# Bind("UniqueRefNo") %>'></asp:Label>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:TemplateField HeaderText="Document" ItemStyle-Width="65%">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="DocName" Width="100%" ToolTip='<%# Bind("DocName") %>' CssClass="label"
                                                                                                                                                    runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                                                                                                                                                <div id="divcomment" runat="server" visible="false">
                                                                                                                                                    <asp:Label ID="lblComment" Width="100%" CssClass="label" runat="server" ForeColor="Red"></asp:Label>
                                                                                                                                                </div>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:LinkButton ID="lbtnUploadDoc" runat="server" ToolTip="Upload Document" OnClientClick="return UploadDoc(this);"><i class="fa fa-upload" aria-hidden="true"></i>
                                                                                                                                                </asp:LinkButton>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
                                                                                                                                    </Columns>
                                                                                                                                </asp:GridView>
                                                                                                                            </div>
                                                                                                                        </div>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
