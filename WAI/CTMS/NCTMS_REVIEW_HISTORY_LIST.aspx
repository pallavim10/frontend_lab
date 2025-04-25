<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NCTMS_REVIEW_HISTORY_LIST.aspx.cs"
    Inherits="CTMS.NCTMS_REVIEW_HISTORY_LIST" %>

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
    <script language="javascript" type="text/javascript">

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
</head>
<body>
    <form id="form1" method="post" class="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Review History List</h3>
        </div>
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <div class="box">
            <asp:GridView runat="server" ID="grdVisits" AutoGenerateColumns="True" CssClass="table table-bordered table-striped Datatable"
                OnPreRender="gvEmp_PreRender">
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
