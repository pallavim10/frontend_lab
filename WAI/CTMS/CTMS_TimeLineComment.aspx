<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CTMS_TimeLineComment.aspx.cs"
    Inherits="CTMS.CTMS_TimeLineComment" %>

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
</head>
<body>
    <form id="form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Comment</h3>
        </div>
        <div class="row">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="box-body">
                <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                    <label class="col-lg-3 width100px label">
                        Task Name</label>
                    <div class="col-lg-3">
                        <asp:Label runat="server" ID="lbl_Task_Name" Width="400px"  />
                        <asp:HiddenField runat="server" ID="hfdID" />
                    </div>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="form-group" style="display: inline-flex; margin-bottom: 6px;">
                <label class="col-lg-3 width100px label">
                    Comment</label>
                <div class="col-lg-8">
                    <asp:TextBox runat="server" ID="txtComment" Width="400px" Height="100px" TextMode="MultiLine"
                        CssClass="form-control required"> 
                    </asp:TextBox>
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
                        <asp:Button ID="btnSubmit" runat="server" Text="Save" 
                            CssClass="btn btn-primary btn-sm" onclick="btnSubmit_Click"
                             />
                    </div>
                </div>
            </div>
        </div>
        <br />
    </div>
    </form>
</body>
</html>
