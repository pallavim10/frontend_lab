<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eTMF_DOCUMENT_TRACKING.aspx.cs"
    Inherits="CTMS.eTMF_DOCUMENT_TRACKING" %>

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
<head>
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
</head>
<body>
    <form id="form1" method="post" class="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Document Tracking</h3>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered table-striped layerFiles Datatable"
            OnPreRender="grd_data_PreRender">
            <EmptyDataTemplate>
                <div align="center">
                    No records found.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
