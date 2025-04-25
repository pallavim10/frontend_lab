<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="eTMF_Rejected_Docs.aspx.cs" Inherits="CTMS.eTMF_Rejected_Docs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script src="js/plugins/moment/moment.min.js" type="text/javascript"></script>
    <script src="js/plugins/moment/datetime-moment.js" type="text/javascript"></script>
    <link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
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

            $.fn.dataTable.moment('DD-MMM-YYYY');
            $.fn.dataTable.moment('DD-MMM-YYYY HH:mm');

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": true,
                stateSave: false,
                fixedHeader: true
            });

        }

        $(document).ready(function () {

            $.fn.dataTable.moment('DD-MMM-YYYY');
            $.fn.dataTable.moment('DD-MMM-YYYY HH:mm');

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upluploaddoc" runat="server">
        <ContentTemplate>
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">Rejected Documents</h3>
                </div>
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <br />
                <div>
                    <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-striped layerFiles Datatable" OnPreRender="grd_data_PreRender"
                        OnRowDataBound="gvFiles_RowDataBound">
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
                            <asp:TemplateField HeaderText="Spec.">
                                <ItemTemplate>
                                    <asp:Label ID="Spec" Width="100%" ToolTip='<%# Bind("SPEC_CONCAT") %>' CssClass="label" runat="server" Text='<%# Bind("SPEC_CONCAT") %>'></asp:Label>
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
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
