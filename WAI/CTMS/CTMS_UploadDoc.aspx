<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CTMS_UploadDoc.aspx.cs"
    Inherits="CTMS.CTMS_UploadDoc" %>

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
<body>
    <form id="form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Upload Document</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width120px label">
                        <asp:Label runat="server" ID="lbl1" Text="Task"></asp:Label>
                    </label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:Label ID="lbl_Task" runat="server"></asp:Label>
                        <asp:Label ID="lbl_Task_ID" Visible="false" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width120px label">
                        <asp:Label runat="server" ID="lbl2" Text="Sub Task Name"></asp:Label>
                    </label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:Label ID="lbl_Sub_Task" runat="server"></asp:Label>
                        <asp:Label ID="lbl_Sub_Task_ID" Visible="false" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal" id="divINVID" runat="server">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width120px label">
                        Site ID</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:DropDownList runat="server" ID="drpSites" CssClass="form-control">
                        </asp:DropDownList>
                        <%--<asp:Label ID="lbl_INVID" runat="server"></asp:Label>--%>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width120px label">
                        Select File</label>
                    <div class="col-lg-3">
                        :</div>
                    <div class="col-lg-3">
                        <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="X-Small" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width120px label">
                        &nbsp;</label>
                    <div class="col-lg-3">
                        &nbsp;</div>
                    <div class="col-lg-3">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary btn-sm"
                            OnClick="btnUpload_Click" />
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
    </form>
</body>
</html>
