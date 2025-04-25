<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_PROC_MyUploads.aspx.cs" Inherits="CTMS.eTMF_PROC_MyUploads" %>

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

            var DocID = $(element).closest('tr').find('td:eq(1)').find('span').text();

            var test = "eTMF_Change_Status.aspx?DocID=" + DocID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=500,width=400";
            window.location.href = test;
            return false;
        }

        function ShowDocs(element) {

            var MainRefNo = $(element).closest('tr').find('td:eq(2)').text().trim();
            var DocTypeId = $(element).closest('tr').find('td:eq(3)').text().trim();

            var test = "eTMF_ShowDocs.aspx?MainRefNo=" + MainRefNo + "&DocTypeId=" + DocTypeId;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=520,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function DownloadDoc(element) {

            var ID = $(element).closest('tr').find('td:eq(1)').find('input').val();

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

            var FolderID = $(element).closest('tr').find('td:eq(2)').find('span:eq(0)').text();
            var SubFolderID = $(element).closest('tr').find('td:eq(2)').find('span:eq(1)').text()

            var test = "CTMS_UploadDoc.aspx?FolderID=" + FolderID + "&SubFolderID=" + SubFolderID + "&INVID=9999";

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=275,width=900";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function DOC_TRACKING(element) {

            var ID = $(element).closest('tr').find('td:eq(1)').find('span').text();

            var test = "eTMF_DOCUMENT_TRACKING.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=600";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

        function VERSION_HISTORY(element) {

            var ID = $(element).closest('tr').find('td:eq(1)').find('span').text();

            var test = "eTMF_VERSION_HISTORY.aspx?ID=" + ID;

            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=300,width=1200";
            window.open(test, '_blank', strWinProperty);
            return false;
        }

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
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label runat="server" ID="lblHeader" Text="My Uploads"></asp:Label>
            </h3>
        </div>
        <div class="form-group" style="margin-left:15px">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div runat="server" id="Div14" style="display: inline-flex">
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
        </div>
    </div>
    <div class="box box-warning">
        <br />
        <div class="row" id="div6" runat="server">
            <div class="col-md-12">
                <div class="txt_center col-md-3">
                    <div id="div7" runat="server" style="z-index: 0;" class="small-box bg-yellow">
                        <div class="inner">
                            <asp:Label ID="lblVal1" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName1" runat="server" Text="Collaborate" Font-Size="Small">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
                <div class="txt_center col-md-3">
                    <div id="div8" runat="server" style="z-index: 0;" class="small-box bg-aqua">
                        <div class="inner">
                            <asp:Label ID="lblVal2" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName2" runat="server" Text="Review" Font-Size="Small">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
                <div class="txt_center col-md-3">
                    <div id="div9" runat="server" style="z-index: 0;" class="small-box bg-blue">
                        <div class="inner">
                            <asp:Label ID="lblVal3" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName3" runat="server" Text="QC" Font-Size="Small">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
                <div class="txt_center col-md-3">
                    <div id="div10" runat="server" style="z-index: 0;" class="small-box bg-light-blue">
                        <div class="inner">
                            <asp:Label ID="lblVal4" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName4" runat="server" Text="QA Review" Font-Size="Small">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="txt_center col-md-3">
                    <div id="div11" runat="server" style="z-index: 0;" class="small-box bg-green">
                        <div class="inner">
                            <asp:Label ID="lblVal5" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName5" runat="server" Text="Internal Approval" Font-Size="Larger"
                                Font-Bold="true">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
                <div class="txt_center col-md-3">
                    <div id="div12" runat="server" style="z-index: 0;" class="small-box bg-maroon">
                        <div class="inner">
                            <asp:Label ID="lblVal6" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName6" runat="server" Text="Sponsor Approval" Font-Size="Larger"
                                Font-Bold="true">
                            </asp:Label>
                        </div>
                        <div class="icon">
                            <i class="ion ion-stats-bars"></i>
                        </div>
                        <a href="#" class="small-box-footer"></a>
                    </div>
                </div>
                <div class="txt_center col-md-3">
                    <div id="div13" runat="server" style="z-index: 0;" class="small-box bg-teal">
                        <div class="inner">
                            <asp:Label ID="lblVal7" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                            <asp:Label ID="lblName7" runat="server" Text="Final" Font-Size="Small">
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
        <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="table table-bordered table-striped layerFiles Datatable" OnPreRender="grd_data_PreRender"
            OnRowCommand="gvFiles_RowCommand" OnRowDataBound="gvFiles_RowDataBound">
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
                        <asp:LinkButton runat="server" ID="lbtnStatus" Text='<%# Eval("Status") %>' OnClientClick="return ChangeStatus(this);"></asp:LinkButton>
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
                            OnClientClick="return VERSION_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'><i class="fa fa-history" style="color:#333333;" aria-hidden="true"></i>
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
                <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                            <td colspan="100%" style="padding: 2px;">
                                <div style="float: right; font-size: 13px;">
                                </div>
                                <div>
                                    <div class="rows">
                                        <div class="col-md-12">
                                            <div id="_Docs<%# Eval("ID") %>" style="display: none; position: relative;">
                                                <asp:GridView ID="gvDocs" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped layer4 Datatable" OnPreRender="grd_data_PreRender"
                                                    OnRowCommand="gvFiles_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                                                            ItemStyle-CssClass="disp-none" HeaderText="ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="For">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" Width="100%" ToolTip='<%# Bind("Status") %>' CssClass="label"
                                                                    runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Users">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUsers" Width="100%" ToolTip='<%# Bind("User") %>' CssClass="label"
                                                                    runat="server" Text='<%# Bind("User") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action Taken" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ActionTakenYN" Width="100%" ToolTip='<%# Bind("ActionTakenYN") %>'
                                                                    CssClass="label" runat="server" Text='<%# Bind("ActionTakenYN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action Taken On" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ActionTakenDT" Width="100%" ToolTip='<%# Bind("ActionTakenDT") %>'
                                                                    CssClass="label" runat="server" Text='<%# Bind("ActionTakenDT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Note" ItemStyle-CssClass="txt_center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NOTE" Width="100%" ToolTip='<%# Bind("NOTE") %>' CssClass="label"
                                                                    runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Name (if any)">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn_UploadFileName" Width="100%" ToolTip='<%# Bind("UploadFileName") %>'
                                                                    CommandArgument='<%# Bind("ID") %>' CommandName="Download" runat="server" Font-Bold="true"
                                                                    Text='<%# Bind("UploadFileName") %>'></asp:LinkButton>
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
