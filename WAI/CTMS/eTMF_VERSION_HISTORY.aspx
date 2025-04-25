<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eTMF_VERSION_HISTORY.aspx.cs"
    Inherits="CTMS.eTMF_VERSION_HISTORY" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<title>Diagonsearch</title>
<script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
<link href="Styles/Site.css" rel="stylesheet" type="text/css" />
<link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
<link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
<link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
<link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
<link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
<link rel="icon" href="img/favicon.ico" type="image/x-icon">
<link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
<script src="Scripts/bootstrap.js" type="text/javascript"></script>
<script src="Scripts/ClientValidation.js" type="text/javascript"></script>
<!-- for pikaday datepicker//-->
<link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
<script src="Scripts/moment.js" type="text/javascript"></script>
<script src="Scripts/pikaday.js" type="text/javascript"></script>
<script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
<!-- for pikaday datepicker//-->
<link href="Styles/jquery.alerts.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery.alerts.js" type="text/javascript"></script>
<link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
<script src="Scripts/Select2.js" type="text/javascript"></script>
<link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
<link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
<link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
<script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
<script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
<link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
<script src="js/plugins/moment/moment.min.js" type="text/javascript"></script>
<script src="js/plugins/moment/datetime-moment.js" type="text/javascript"></script>
<link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_OpenDoc.js"></script>
<script type="text/javascript" src="CommonFunctionsJs/eTMF/eTMF_History.js"></script>
<head>
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
</head>
<body>
    <form id="form1" method="post" class="content" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">Version History</h3>
            </div>
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
            </div>
            <asp:GridView ID="gvFiles" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped layerFiles Datatable" OnPreRender="grd_data_PreRender"
                OnRowCommand="gvFiles_RowCommand" OnRowDataBound="gvFiles_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                        ItemStyle-CssClass="disp-none" HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="disp-none" ControlStyle-CssClass="disp-none"
                        ItemStyle-CssClass="disp-none" HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="System Version" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:Label ID="SysVERSION" Width="100%" ToolTip='<%# Bind("SysVERSION") %>' CssClass="label"
                                runat="server" Text='<%# Bind("SysVERSION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="txt_center">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnurl" runat="server" />
                            <asp:Label ID="lblStatus" Width="100%" ToolTip='<%# Bind("Status") %>' CssClass="label"
                                runat="server" Text='<%# Bind("Status") %>'></asp:Label>
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
                            <asp:LinkButton ID="lbtnDocumentHistory" runat="server" ToolTip="Document History"
                                OnClientClick="return DOCUMENT_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                                <i id="iconDochistory" runat="server" class="fa fa-history" style="color: #333333;"
                                    aria-hidden="true"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none" ItemStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnversionHistory" runat="server" ToolTip="Version History"
                                OnClientClick="return VERSION_HISTORY(this);" CommandArgument='<%# Eval("ID") %>'>
                                <i id="iconhistory" runat="server" class="fa fa-files-o" style="color: #333333;"
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
                <EmptyDataTemplate>
                    <div align="center">
                        No records found.
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
