<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_Snapshot_SITE.aspx.cs" Inherits="CTMS.eTMF_Snapshot_SITE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function OpenDoc(element) {

            var SysFileName = $(element).closest('tr').find('td:eq(1)').text().trim();
            var DOCID = $(element).closest('tr').find('td:eq(0)').text().trim();

            $.ajax({
                type: "POST",
                url: "AjaxFunction.aspx/INSERT_DOC_OPEN_LOGS",
                data: '{DOCID: "' + DOCID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                },
                failure: function (response) {
                    if (response.d == 'Object reference not set to an instance of an object.') {
                        alert("Session Expired");
                        var url = "SessionExpired.aspx";
                        $(location).attr('href', url);
                    }
                }
            });

            var test = "eTMF_Docs/" + SysFileName + "#toolbar=0";

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=700,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function SearchAndHighlights(element) {

            $("#MainContent_gvFolder").find(".highlight").removeClass("highlight");

            var searchword = $(element).val();

            var custfilter = new RegExp(searchword, "ig");
            var repstr = "" + searchword + "";

            if (searchword != "") {
                $('#MainContent_gvFolder').each(function () {
                    $(this).html($(this).html().replace(custfilter, repstr));
                })
            }

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

        function ChangeStatus(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var DocID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_Change_Status.aspx?DocID=" + DocID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=400";
            window.location.href = test;
            return false;
        }

        function ShowDocs(element) {

            var MainRefNo = $(element).closest('tr').find('td:eq(1)').text().trim();
            var DocTypeId = $(element).closest('tr').find('td:eq(2)').text().trim();

            var test = "eTMF_ShowDocs.aspx?MainRefNo=" + MainRefNo + "&DocTypeId=" + DocTypeId;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=520,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function DownloadDoc(element) {

            var ID = $(element).closest('tr').find('td:eq(0)').find('input').val();

            var test = "CTMS_DownloadDoc.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=520,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
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

        function UploadDoc(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var FolderID = $(element).closest('tr').find('td:eq(1)').find('span:eq(0)').text();
            var SubFolderID = $(element).closest('tr').find('td:eq(1)').find('span:eq(1)').text()

            var test = "CTMS_UploadDoc.aspx?FolderID=" + FolderID + "&SubFolderID=" + SubFolderID + "&INVID=9999";

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=275,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

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
        .layer1 {
            color: #0000ff;
        }

        .layer2 {
            color: #800000;
        }

        .layer3 {
            color: #008000;
        }

        .layer4 {
            color: Black;
        }

        .layerFiles {
            color: #800000;
            font-style: italic;
        }
    </style>
    <style>
        .label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: -2px;
            font-weight: bold;
            font-size: 13px;
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header" style="display: inline-flex; width: 100%;">
            <h3 class="box-title" style="width: 100%">
                <asp:Label runat="server" ID="lblHeader" Text="View Documents" />
            </h3>
            <div class="pull-right " style="display: inline-flex;">
                <asp:ImageButton ID="btnRefresh" CssClass="disp-none" runat="server" Style="height: 27px;"
                    ImageUrl="img/Sync.png" OnClick="btnRefresh_Click" ToolTip="Refresh"></asp:ImageButton>
                Show as : &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList runat="server" ID="drpShowAs" CssClass="form-control" AutoPostBack="true"
                    OnSelectedIndexChanged="drpShowAs_SelectedIndexChanged">
                    <asp:ListItem Text="Tree View" Value="Tree View"></asp:ListItem>
                    <asp:ListItem Text="List View" Value="List View"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group has-warning">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
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
                <asp:TemplateField HeaderText="Ref." ItemStyle-Width="5%" ItemStyle-CssClass="txt_center">
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
                <asp:TemplateField HeaderText="Total Documents" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lblCount" CssClass="label" runat="server"></asp:Label>
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
                                                                        style="color: #333333"><i id="img_SubFolder<%# Eval("SubFolder_ID") %>" class="icon-plus-sign-alt"></i></a>
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
                                                        <asp:TemplateField HeaderText="Ref." ItemStyle-Width="7%" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                                                                    runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sections" ItemStyle-Width="78%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SubFolder" Width="100%" ToolTip='<%# Bind("Sub_Folder_Name") %>'
                                                                    CssClass="label" runat="server" Text='<%# Bind("Sub_Folder_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Documents" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCount" Text="0" CssClass="label" runat="server"></asp:Label>
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
                                                                                    <div id="_SubFolder<%# Eval("SubFolder_ID") %>" style="display: none; position: relative; overflow: auto;">
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
                                                                                                <asp:TemplateField HeaderText="Ref." ItemStyle-Width="8%" ItemStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_RefNo" Width="100%" ToolTip='<%# Bind("RefNo") %>' CssClass="label"
                                                                                                            runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Artifacts" ItemStyle-Width="82%">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbl_Artifact" Width="100%" ToolTip='<%# Bind("Artifact_Name") %>'
                                                                                                            CssClass="label" runat="server" Text='<%# Bind("Artifact_Name") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Total Documents" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblCount" Text="0" CssClass="label" runat="server"></asp:Label>
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
                                                                                                                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                                                                                                                            HeaderStyle-CssClass="txt_center">
                                                                                                                                            <HeaderTemplate>
                                                                                                                                                <a href="JavaScript:ManipulateAll('_Files');" id="_Folder" style="color: #333333"><i
                                                                                                                                                    id="img_Files" class="icon-plus-sign-alt"></i></a>
                                                                                                                                            </HeaderTemplate>
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <div runat="server" id="anchor">
                                                                                                                                                    <a href="JavaScript:divexpandcollapse('_Files<%# Eval("ID") %>');" style="color: #333333">
                                                                                                                                                        <i id="img_Files<%# Eval("ID") %>" class="icon-plus-sign-alt"></i></a>
                                                                                                                                                </div>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
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
                                                                                                                                                <div id="divcomment" runat="server">
                                                                                                                                                    <asp:Label ID="lblComment" Text='<%# Bind("Comment") %>' Width="100%" CssClass="label"
                                                                                                                                                        runat="server" ForeColor="Red"></asp:Label>
                                                                                                                                                </div>
                                                                                                                                            </ItemTemplate>
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:TemplateField HeaderText="Total Documents" ItemStyle-Width="10%" ItemStyle-CssClass="txt_center">
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="lblCount" Text="0" CssClass="label" runat="server"></asp:Label>
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
                                                                                                                                                                    <div id="_Files<%# Eval("ID") %>" style="display: none; position: relative; overflow: auto;">
                                                                                                                                                                        <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                                                                                            Width="100%" Width="100%" OnRowCommand="gvFiles_RowCommand" CssClass="table table-bordered table-striped layerFiles Datatable"
                                                                                                                                                                            OnPreRender="grd_data_PreRender" OnRowDataBound="gvFiles_RowDataBound">
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
                                                                                                                                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                                                                                                                                                    ItemStyle-CssClass="disp-none">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="lbl_Name" Width="100%" ToolTip='<%# Bind("AutoNomenclature") %>' CssClass="label"
                                                                                                                                                                                            runat="server" Text='<%# Bind("AutoNomenclature") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="File Name">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="lbl_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                                                                                                                                                                            CssClass="label" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                                                                                                                                                                                        <asp:LinkButton ID="lbtn_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                                                                                                                                                                            runat="server" CssClass="label" OnClientClick="return OpenDoc(this);" Text='<%# Bind("UploadFileName") %>'></asp:LinkButton>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="File Type" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="lbtnFileType" runat="server" Font-Size="Larger"><i id="ICONCLASS"
                                                                                                                                                                                            runat="server" class="fas fa-file-text"></i></asp:Label>
                                                                                                                                                                                        <asp:Label ID="lbl_FileSize" Width="100%" CssClass="label" Font-Size="X-Small" runat="server" Text='<%# Bind("CAL_Size") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="System Version">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="lbl_VersionID" Width="100%" ToolTip='<%# Bind("VersionID") %>' CssClass="label"
                                                                                                                                                                                            runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label runat="server" ID="lbtnStatus" CssClass="label" Text='<%# Eval("Status") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="DOC_VERSIONNO" Width="100%" ToolTip='<%# Bind("DOC_VERSIONNO") %>'
                                                                                                                                                                                            CssClass="label" runat="server" Text='<%# Bind("DOC_VERSIONNO") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Document Date" ItemStyle-CssClass="txt_center">
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
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvFiles_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            Width="100%" CssClass="table table-bordered table-striped layerFiles Datatable"
            OnPreRender="grd_data_PreRender" OnRowCommand="gvFiles_RowCommand" OnRowDataBound="gvFiles_RowDataBound">
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
                <asp:TemplateField HeaderText="Ref. No.">
                    <ItemTemplate>
                        <asp:Label ID="MainRefNo" Width="100%" ToolTip='<%# Bind("MainRefNo") %>' CssClass="label"
                            runat="server" Text='<%# Bind("MainRefNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Country">
                    <ItemTemplate>
                        <asp:Label ID="COUNTRYNAME" Width="100%" ToolTip='<%# Bind("COUNTRYNAME") %>' CssClass="label"
                            runat="server" Text='<%# Bind("COUNTRYNAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Site Id">
                    <ItemTemplate>
                        <asp:Label ID="SiteID" Width="100%" ToolTip='<%# Bind("SiteID") %>' CssClass="label"
                            runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                    ItemStyle-CssClass="disp-none">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Name" Width="100%" ToolTip='<%# Bind("AutoNomenclature") %>' CssClass="label"
                            runat="server" Text='<%# Bind("AutoNomenclature") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                            CssClass="label" runat="server" Text='<%# Bind("UploadFileName") %>'></asp:Label>
                        <asp:LinkButton ID="lbtn_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                            runat="server" CssClass="label" OnClientClick="return OpenDoc(this);" Text='<%# Bind("UploadFileName") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="File Type" HeaderStyle-CssClass="txt_center" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="lbtnFileType" runat="server" Font-Size="Larger"><i id="ICONCLASS"
                            runat="server" class="fas fa-file-text"></i></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="System Version">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VersionID" Width="100%" ToolTip='<%# Bind("VersionID") %>' CssClass="label"
                            runat="server" Text='<%# Bind("VersionID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lbtnStatus" CssClass="label" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="DOC_VERSIONNO" Width="100%" ToolTip='<%# Bind("DOC_VERSIONNO") %>'
                            CssClass="label" runat="server" Text='<%# Bind("DOC_VERSIONNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Date" ItemStyle-CssClass="txt_center">
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
