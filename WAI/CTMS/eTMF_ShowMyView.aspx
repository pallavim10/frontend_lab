<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_ShowMyView.aspx.cs" Inherits="CTMS.eTMF_ShowMyView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
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

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

        }

        $(document).ready(function () {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });

        });

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
    <script>
        function ChangeStatus(element) {

            var row = element.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;

            var DocID = $(element).closest('tr').find('td:eq(0)').find('span').text();

            var test = "eTMF_Change_Status.aspx?DocID=" + DocID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=400";
            window.location.href = test;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="form-group">
            <div class="form-group">
                <div class="row">
                    <br />
                    <div class="col-md-12">
                        <div class="label col-md-2">
                            Select Your View:
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="drpView" runat="server" CssClass="form-control width300px required">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-5">
                            <asp:Button ID="btnShowFiles" runat="server" Text="Show Files" OnClick="btnShowFiles_Click"
                                CssClass="btn btn-primary btn-sm cls-btnSave" />
                        </div>
                    </div>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader"></asp:Label>
            </h3>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row" id="div4" runat="server">
            <div class="col-md-12">
                <div class="txt_center col-md-3">
                    <div id="divBox" runat="server" style="z-index: 0;" class="small-box bg-red">
                        <div class="inner">
                            <asp:Label ID="lblVal" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName" runat="server" Text="Estimated" Font-Size="Small">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
                <div class="txt_center col-md-3">
                    <div id="div1" runat="server" style="z-index: 0;" class="small-box bg-yellow">
                        <div class="inner">
                            <asp:Label ID="lblVal1" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName1" runat="server" Text="Old / Replaced" Font-Size="Small">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
                <div class="txt_center col-md-3">
                    <div id="div2" runat="server" style="z-index: 0;" class="small-box bg-aqua">
                        <div class="inner">
                            <asp:Label ID="lblVal2" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName2" runat="server" Text="Draft" Font-Size="Small">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
                <div class="txt_center col-md-3">
                    <div id="div3" runat="server" style="z-index: 0;" class="small-box bg-blue">
                        <div class="inner">
                            <asp:Label ID="lblVal3" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName3" runat="server" Text="Final" Font-Size="Small">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="box box-primary">
        <div class="form-group">
            <div class="row" style="padding-top: 15px;">
                <div class="col-md-12">
                    <div id="divCountry" runat="server">
                        <div class="label col-md-1 width140px">
                            Select Country : &nbsp;
                            <asp:Label ID="Label13" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="drpCountry" AutoPostBack="true" CssClass="form-control"
                                OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="divINVID" runat="server">
                        <div class="label col-md-1">
                            Site ID : &nbsp;
                            <asp:Label ID="Label14" runat="server" Font-Size="Small" ForeColor="#FF3300" Text="*"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="drpSites_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="div5" runat="server">
                        <div class="label col-md-1">
                            Show as :
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="drpShowAs" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="drpShowAs_SelectedIndexChanged">
                                <asp:ListItem Text="List View" Value="List View"></asp:ListItem>
                                <asp:ListItem Text="Tree View" Value="Tree View"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <asp:GridView ID="gvTreeZone" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped layer1" OnRowDataBound="gvTreeZone_RowDataBound">
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
                                                <asp:GridView ID="gvTreeSection" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped layer2" OnRowDataBound="gvTreeSection_RowDataBound">
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
                                                                                        <asp:GridView ID="gvTreeArtifact" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                            CssClass="table table-bordered table-striped layer3" OnRowDataBound="gvTreeArtifact_RowDataBound">
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
                                                                                                                                <asp:GridView ID="gvTreeDocs" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                                                    OnRowDataBound="gvTreeDocs_RowDataBound" CssClass="table table-bordered table-striped layer1">
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
                                                                                                                                                                        <asp:GridView ID="gvTreeFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                                                                                            Width="100%" OnRowCommand="gvTreeFiles_RowCommand" CssClass="table table-bordered table-striped layerFiles Datatable"
                                                                                                                                                                            OnPreRender="grd_data_PreRender" OnRowDataBound="gvTreeFiles_RowDataBound">
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
                                                                                                                                                                                        <asp:Label ID="Country" Width="100%" ToolTip='<%# Bind("Country") %>' CssClass="label"
                                                                                                                                                                                            runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="SIte Id">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="SiteID" Width="100%" ToolTip='<%# Bind("SiteID") %>' CssClass="label"
                                                                                                                                                                                            runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Spec.">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="Spec" Width="100%" ToolTip='<%# Bind("SPEC_CONCAT") %>' CssClass="label"
                                                                                                                                                                                            runat="server" Text='<%# Bind("SPEC_CONCAT") %>'></asp:Label>
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
                                                                                                                                                                                        <asp:Label ID="SysVERSION" Width="100%" ToolTip='<%# Bind("SysVERSION") %>' CssClass="label"
                                                                                                                                                                                            runat="server" Text='<%# Bind("SysVERSION") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label runat="server" ID="lbtnStatus" CssClass="label" Text='<%# Eval("Status") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="DocVERSION" Width="100%" ToolTip='<%# Bind("DocVERSION") %>'
                                                                                                                                                                                            CssClass="label" runat="server" Text='<%# Bind("DocVERSION") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Document Date" ItemStyle-CssClass="txt_center">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="DocDATE" Width="100%" ToolTip='<%# Bind("DocDATE") %>' CssClass="label"
                                                                                                                                                                                            runat="server" Text='<%# Bind("DocDATE") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField HeaderText="Note" ItemStyle-CssClass="txt_center">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="NOTE" Width="100%" ToolTip='<%# Bind("NOTE") %>' CssClass="label"
                                                                                                                                                                                            runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField>
                                                                                                                                                                                    <HeaderTemplate>
                                                                                                                                                                                        <label>Uploading Details</label><br />
                                                                                                                                                                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Uploaded By]</label><br />
                                                                                                                                                                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                                                                                                                                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                                                                                                                                                                    </HeaderTemplate>
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <div>
                                                                                                                                                                                            <div>
                                                                                                                                                                                                <asp:Label ID="UPLOADBYNAME" runat="server" Text='<%# Bind("UPLOADBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                                                                                                                                                            </div>
                                                                                                                                                                                            <div>
                                                                                                                                                                                                <asp:Label ID="UPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                                                                                                                                                            </div>
                                                                                                                                                                                            <div>
                                                                                                                                                                                                <asp:Label ID="UPLOAD_CAL_TZDAT" runat="server" Text='<%# Bind("UPLOAD_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                                                                                                                                                                                            </div>
                                                                                                                                                                                        </div>
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
                                                                                                                                                                                        <asp:LinkButton ID="lbtnDownloadDoc" runat="server" ToolTip="Download" CommandName="Download" CssClass="btn"
                                                                                                                                                                                            CommandArgument='<%# Eval("ID") %>'><i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                                                                                                                                                                                        </asp:LinkButton>
                                                                                                                                                                                    </ItemTemplate>
                                                                                                                                                                                </asp:TemplateField>
                                                                                                                                                                                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                                        <asp:Label ID="lblQC" runat="server" ToolTip="QC Document" CommandArgument='<%# Eval("ID") %>'
                                                                                                                                                                                            Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconQC" runat="server"
                                                                                                                                                                                                class="fa fa-check"></i></asp:Label>
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
        <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
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
                <asp:TemplateField HeaderText="Country">
                    <ItemTemplate>
                        <asp:Label ID="Country" Width="100%" ToolTip='<%# Bind("Country") %>' CssClass="label"
                            runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SIte Id">
                    <ItemTemplate>
                        <asp:Label ID="SiteID" Width="100%" ToolTip='<%# Bind("SiteID") %>' CssClass="label"
                            runat="server" Text='<%# Bind("SiteID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Spec.">
                    <ItemTemplate>
                        <asp:Label ID="Spec" Width="100%" ToolTip='<%# Bind("SPEC_CONCAT") %>' CssClass="label"
                            runat="server" Text='<%# Bind("SPEC_CONCAT") %>'></asp:Label>
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
                        <asp:Label ID="SysVERSION" Width="100%" ToolTip='<%# Bind("SysVERSION") %>' CssClass="label"
                            runat="server" Text='<%# Bind("SysVERSION") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lbtnStatus" CssClass="label" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Version" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="DocVERSION" Width="100%" ToolTip='<%# Bind("DocVERSION") %>'
                            CssClass="label" runat="server" Text='<%# Bind("DocVERSION") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Document Date" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="DocDATE" Width="100%" ToolTip='<%# Bind("DocDATE") %>' CssClass="label"
                            runat="server" Text='<%# Bind("DocDATE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Note" ItemStyle-CssClass="txt_center">
                    <ItemTemplate>
                        <asp:Label ID="NOTE" Width="100%" ToolTip='<%# Bind("NOTE") %>' CssClass="label"
                            runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <label>Uploading Details</label><br />
                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Uploaded By]</label><br />
                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <div>
                                <asp:Label ID="UPLOADBYNAME" runat="server" Text='<%# Bind("UPLOADBYNAME") %>' ForeColor="Blue"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="UPLOAD_CAL_DAT" runat="server" Text='<%# Bind("UPLOAD_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="UPLOAD_CAL_TZDAT" runat="server" Text='<%# Bind("UPLOAD_CAL_TZDAT") %>' ForeColor="Red"></asp:Label>
                            </div>
                        </div>
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
                        <asp:LinkButton ID="lbtnDownloadDoc" runat="server" ToolTip="Download" CommandName="Download" CssClass="btn"
                            CommandArgument='<%# Eval("ID") %>'><i class="fa fa-download" style="color:#333333;" aria-hidden="true"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                    <ItemTemplate>
                        <asp:Label ID="lblQC" runat="server" ToolTip="QC Document" CommandArgument='<%# Eval("ID") %>'
                            Style="color: #333333; font-size: initial; font-weight: bold;"><i id="iconQC" runat="server"
                                class="fa fa-check"></i></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </div>
</asp:Content>
