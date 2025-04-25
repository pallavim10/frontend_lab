<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Manage_Dashboard.aspx.cs"
    Inherits="CTMS.Manage_Dashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
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
    <script src="Scripts/ClientValidation.js" type="text/javascript"></script>
    <!-- for Jquery Popup//-->
    <%--<script src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>--%>
    <%--<link href="Styles/Jquery.ui.css" rel="stylesheet" type="text/css" />--%>
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
    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script src="js/plugins/morris/morris.min.js" type="text/javascript"></script>
    <!-- Sparkline -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- fullCalendar -->
    <script src="js/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
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
    <link href="Styles/graph.css" rel="stylesheet" type="text/css" />
    <!-- Dynamic Dashboard (This is for GridStack.js) -->
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">--%>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.11.0/jquery-ui.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lodash.js/4.17.0/lodash.min.js"></script>
    <link rel="stylesheet" href="js/GridStack/gridstack.css" />
    <link rel="stylesheet" href="js/GridStack/gridstack-extra.css" />
    <script src="js/GridStack/gridstack.js"></script>
    <script src="js/GridStack/gridstack.jQueryUI.js"></script>
    <script type="text/javascript">

        function SET_Dashboard() {

            var divArray = $('#divDashboard > div').toArray();
            for (a = 0; a < divArray.length; ++a) {

                var divMain = $(divArray[a]).attr('id');
                var ID = $('#' + divMain).find('input[type=hidden]').attr('value');
                var X = $(divArray[a]).attr('data-gs-x');
                var Y = $(divArray[a]).attr('data-gs-y');
                var Width = $(divArray[a]).attr('data-gs-width');
                var Height = $(divArray[a]).attr('data-gs-height');

                $.ajax({
                    type: "POST",
                    url: "AjaxFunction.aspx/SET_Dashboard",
                    data: '{X:"' + X + '",  Y: "' + Y + '", Width: "' + Width + '" ,Height: "' + Height + '",ID: "' + ID + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    failure: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }
                    }
                });
            }
            alert("Dashboard Set Successfully.");
            window.close();
        }      
    </script>
</head>
<body style="cursor: auto;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="Scripts/amcharts.js"></script>
    <script src="https://www.amcharts.com/lib/3/serial.js"></script>
    <script src="https://www.amcharts.com/lib/3/pie.js"></script>
    <link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css"
        type="text/css" media="all" />
    <script src="https://www.amcharts.com/lib/3/themes/light.js"></script>
    <div>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    Manage Dashboard
                </h3>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">
                                Available Charts
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div>
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <asp:GridView ID="gvAvailableCharts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chart Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChartId" runat="server" Visible="false" Text='<%# Bind("Chart_ID") %>'></asp:Label>
                                                            <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                            <asp:Label ID="lblChart" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
                <div class="col-md-1">
                    <div class="box-body">
                        <div style="min-height: 300px;">
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnAdd" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="lbtnAdd_Click"></asp:LinkButton>
                            </div>
                            <div class="row txtCenter">
                                &nbsp;
                            </div>
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnRemove" ForeColor="White" Text="Remove" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="lbtnRemove_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">
                                Used Charts
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                        <div>
                                            <asp:GridView ID="gvAddedCharts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Chart Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChartId" runat="server" Visible="false" Text='<%# Bind("Chart_ID") %>'></asp:Label>
                                                            <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                            <asp:Label ID="lblChartName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
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
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    Adjust Dashboard
                </h3>
                <div class="pull-right" style="margin-right: 5%;">
                    <asp:LinkButton ID="lbtnSet" ForeColor="White" Text="Set" runat="server" CssClass="btn btn-primary btn-sm"
                        OnClientClick="SET_Dashboard();"></asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="grid-stack" runat="server" id="divDashboard">
                <asp:Repeater runat="server" ID="repeatDashboard" OnItemDataBound="repeatDashboard_ItemDataBound">
                    <ItemTemplate>
                        <div class="grid-stack-item" data-gs-x="0" data-gs-y="0" data-gs-width="4" data-gs-height="1"
                            runat="server" id="divMain">
                            <asp:HiddenField ID="hf_ID" runat="server" Value='<%# Bind("ID") %>' />
                            <div class="grid-stack-item-content" runat="server" id="divContent">
                                <div id="divBox" runat="server">
                                    <div class="inner SetTrigger txt_center">
                                        <div class="col-md-12" style="display: inline-flex;">
                                            <div class="col-md-4">
                                                <asp:Label runat="server" ID="lblScore"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblVal" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>&nbsp;
                                            </div>
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <br />
                                        <asp:Label ID="lblName" runat="server" Text="Tile" Font-Size="Small">
                                        </asp:Label>
                                        <asp:HiddenField runat="server" ID="hfIndicID" Value="1" />
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-stats-bars"></i>
                                    </div>
                                    <a href="#" class="small-box-footer"></a>
                                </div>
                                <asp:PlaceHolder ID="placeHolder" runat="server" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    </form>
    <script type="text/javascript">
        $(function () {
            var options = {
                cellHeight: 120,
                verticalMargin: 1
            };
            $('.grid-stack').gridstack(options);
        });
    </script>
</body>
</html>
