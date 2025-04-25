<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NCTMS_VISIT_TRACKES.aspx.cs"
    Inherits="CTMS.NCTMS_VISIT_TRACKES" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <title>WAI</title>
    <script src="Scripts/Jquery1.12.4.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Common-Bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="img/favicon.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="img/favicon.ico" type="image/x-icon" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/CommonFunction.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <!-- for pikaday datepicker//-->
    <link href="Styles/pikaday.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/moment.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.js" type="text/javascript"></script>
    <script src="Scripts/pikaday.jquery.js" type="text/javascript"></script>
    <!-- for pikaday datepicker//-->
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Styles/datatables/dataTables.bootstrap.css" />
    <script src="js/plugins/moment/moment.min.js" type="text/javascript"></script>
    <script src="js/plugins/moment/datetime-moment.js" type="text/javascript"></script>
    <link href="js/plugins/datatables/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <%--  <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>--%>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- jQuery Knob Chart -->
    <script src="js/plugins/jqueryKnob/jquery.knob.js" type="text/javascript"></script>
    <!-- daterangepicker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="js/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="js/AdminLTE/app.js" type="text/javascript"></script>
    <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/graph.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .fontred
        {
            color: Red;
        }
        .fontBlue
        {
            color: Blue;
            cursor: pointer;
        }
        .circleQueryCountRed
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Red;
        }
        .circleQueryCountGreen
        {
            width: 15px;
            height: 15px;
            border-radius: 50%;
            font-size: 10px;
            color: Yellow;
            text-align: center;
            background: Green;
        }
        .YellowIcon
        {
            color: Yellow;
        }
        .GreenIcon
        {
            color: Green;
        }
        .strikeThrough
        {
            text-decoration: line-through;
        }
    </style>
    <style type="text/css">
        .form-control
        {
            width: 100%;
            min-width: 100px;
        }
        .brd-1px-redimp
        {
            border: 2px solid !important;
            border-color: Red !important;
        }
        .brd-1px-maroonimp
        {
            border: 2px solid !important;
            border-color: Maroon !important;
        }
        .style1
        {
            width: 100%;
            margin-right: 142px;
        }
    </style>
    <script>
        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                stateSave: false,
                fixedHeader: true
            });
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
    </script>
</head>
<body>
    <form id="form1" method="post" class="content" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div class="box box-danger">
        <div class="box-header">
            <h3 class="box-title" style="width: 100%;">
                Tracker Data &nbsp&nbsp&nbsp <a href="JavaScript:ManipulateAll('_Pat_');" id="_Folder"
                    style="color: #333333; font-size: larger; margin-top: 5px;"><i id="img_Pat_" class="icon-plus-sign-alt">
                    </i></a>
            </h3>
        </div>
        <div class="row">
            <div class="lblError">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
        </div>
        <asp:Repeater runat="server" OnItemDataBound="repeatData_ItemDataBound" ID="repeatData">
            <ItemTemplate>
                <div class="box box-primary">
                    <div class="box-header">
                        <div runat="server" style="display: inline-flex; padding: 0px; margin: 4px 0px 0px 10px;"
                            id="anchor">
                            <a href="JavaScript:divexpandcollapse('_Pat_<%# Eval("MODULEID") %>');" style="color: #333333">
                                <i id="img_Pat_<%# Eval("MODULEID") %>" class="icon-plus-sign-alt"></i></a>
                            <h3 class="box-title" style="padding-top: 0px;">
                                <asp:Label ID="lblHeader" runat="server" Text='<%# Bind("MODULENAME") %>'></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <div id="_Pat_<%# Eval("MODULEID") %>" style="display: none; position: relative;
                        overflow: auto;">
                        <div class="box-body">
                            <div class="rows">
                                <div style="width: 100%; overflow: auto;">
                                    <div>
                                        <asp:GridView ID="grd_Records" runat="server" CellPadding="3" AutoGenerateColumns="True"
                                            CssClass="table table-bordered table-striped Datatable" ShowHeader="True" AllowSorting="true"
                                            EmptyDataText="No Records Available" CellSpacing="2" OnPreRender="gvEmp_PreRender"
                                            OnRowDataBound="grd_Records_RowDataBound">
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
