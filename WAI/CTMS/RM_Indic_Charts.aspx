<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RM_Indic_Charts.aspx.cs"
    Inherits="CTMS.RM_Indic_Charts" %>

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
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <link href="Styles/graph.css" rel="stylesheet" type="text/css" />
    <script src="js/AdminLTE/app.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/amcharts.js"></script>
    <script src="Scripts/serial.js"></script>
    <script src="js/amcharts/themes/light.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="box box-danger" id="Div6">
            <div class="box-header">
                <h4 class="box-title">
                    <asp:Label runat="server" ID="lblGraphHeader"></asp:Label>
                </h4>
            </div>
            <!-- /.box-header -->
            <div class="box-body ">
                <div class="row">
                    <div class="col-md-12">
                        <asp:HiddenField ID="hfData" runat="server" />
                        <div id="divBar" runat="server" class="chartdiv">
                        </div>
                    </div>
                </div>
                <!-- /.row - inside box -->
            </div>
            <!-- /.box-body -->
        </div>
    </div>
    <br />
    <script type="text/javascript">

        var hfData = window.eval('(' + $("#<%=hfData.ClientID%>").val() + ')');

        var divBar = AmCharts.makeChart("divBar", {
            "theme": "light",
            "type": "serial",
            "startDuration": 2,
            "dataProvider": hfData,
            "valueAxes": [{
                "position": "left",
                "title": ""
            }],
            "graphs": [{
                "balloonText": "[[category]]: <b>[[value]]</b>",
                "fillColorsField": "color",
                "fillAlphas": 1,
                "lineAlpha": 0.1,
                "labelText": "[[Count]]",
                "type": "column",
                "valueField": "Count"
            }],
            "bullets": [{
                "type": "LabelBullet",
                "label": {
                    "text": "{value}",
                    "fontSize": 20
                }
            }],
            "depth3D": 20,
            "angle": 30,
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "INVID",
            "categoryAxis": {
                "gridPosition": "start",
                "labelRotation": 90,
                "categoryAxis.dashLength": 100,
                "categoryAxis.gridPosition": "start",
                "autoGridCount": "true",
                "gridPosition": "start",
                "autoGridCount": "true",
                "minHorizontalGap": 0
            },
            "export": {
                "enabled": true
            }

        });

    </script>
    </form>
</body>
</html>
