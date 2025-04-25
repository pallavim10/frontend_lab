<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CTMS_CreateAgendaMomUpload.aspx.cs"
    Inherits="CTMS.CTMS_CreateAgendaMomUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <%-- <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />--%>
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
    <style type="text/css">
        #outerContainer #mainContainer div.toolbar
        {
            display: none !important; /* hide PDF viewer toolbar */
        }
        #outerContainer #mainContainer #viewerContainer
        {
            top: 0 !important; /* move doc up into empty bar space */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Upload Meeting MoM</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-6">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left; top: 0px; left: 0px;">
                            <h4 class="box-title" align="left">
                                Add Document
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Agenda ID:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMeetingID" ID="reqSec"
                                                    ValidationGroup="Sec" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox  Width="55px" Enabled="false" ID="txtMeetingID" ValidationGroup="Sec" runat="server"
                                                    CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                <label>
                                                    Attach Documents:</label>
                                            </div>
                                            <div class="col-md-1 requiredSign">
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="FileUpload1" ID="RequiredFieldValidator3"
                                                    ValidationGroup="Sec" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="X-Small" />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                             <div class="col-md-1 requiredSign">
                                               
                                            </div>
                                            <div class="col-md-7">
                                                <asp:Button ID="btnsubmitSec" Text="Submit" runat="server" CssClass="btn btn-primary btn-sm"
                                                    ValidationGroup="Sec" OnClick="btnsubmitSec_Click" />
                                                <asp:Button ID="btncancelSec" Text="Cancel" runat="server" CssClass="btn btn-primary btn-sm"
                                                    OnClick="btncancelSec_Click" />
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
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">
                                Records
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div>
                                    <div class="rows">
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <div>
                                                <asp:GridView ID="gvMaterial" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-striped Datatable1" OnRowCommand="gvMaterial_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Agenda ID" ItemStyle-CssClass="disp-none" HeaderStyle-CssClass="disp-none">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ID" Width="100%" ToolTip='<%# Bind("AgendaID") %>' CssClass="label"
                                                                    runat="server" Text='<%# Bind("AgendaID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Name" ItemStyle-Width="100%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Section" Width="100%" ToolTip='<%# Bind("FileName") %>' CssClass="label"
                                                                    runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="txt_center" ItemStyle-Width="1%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDownloadDoc" runat="server" ToolTip="View" CommandArgument='<%# Bind("ID") %>'><i class="fa fa-search" style="color:#333333;" aria-hidden="true"></i>
                                                                </asp:LinkButton>
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
        </div>
    </div>
    </form>
</body>
</html>
