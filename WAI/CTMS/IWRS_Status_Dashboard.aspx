<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IWRS_Status_Dashboard.aspx.cs" Inherits="CTMS.IWRS_Status_Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="Scripts/amcharts.js"></script>
    <script src="https://www.amcharts.com/lib/4/core.js"></script>
    <script src="https://www.amcharts.com/lib/4/charts.js"></script>
    <script src="https://www.amcharts.com/lib/4/themes/animated.js"></script>
    <script src="https://www.amcharts.com/lib/3/serial.js"></script>
    <script src="https://www.amcharts.com/lib/3/pie.js"></script>
    <link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css"
        type="text/css" media="all" />
    <script src="https://www.amcharts.com/lib/3/themes/light.js"></script>
    <link href="Styles/Select2.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Select2.js" type="text/javascript"></script>
    <style type="text/css">
        .select2-container .select2-selection--multiple {
            min-height: 30px;
            width: 300px;
        }
    </style>
    <style>
        #mainchartdiv {
            width: 100%;
            height: 500px;
        }
    </style>
    <script>
        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();

        }

        $(function bindGraph() {

            var a;
            var b;
            var c;

            GenerateGraph_OVERALL();

            var hfData = $("input[id*='hfData']").toArray();
            for (a = 0; a < hfData.length; ++a) {
                GenerateGraph(hfData[a]);
            }

            var hfBarData = $("input[id*='hfBarData']").toArray();
            for (b = 0; b < hfBarData.length; ++b) {
                GenerateGraph_Bar(hfBarData[b]);
            }

            var hfPieData = $("input[id*='hfPieData']").toArray();
            for (c = 0; c < hfPieData.length; ++c) {
                GenerateGraph_Pie(hfPieData[c]);
            }

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="anish">
        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:ListView ID="lstStatusDashboard" runat="server" AutoGenerateColumns="false"
                    OnItemDataBound="lstStatusDashboard_ItemDataBound">
                    <GroupTemplate>
                        <div class="txt_center col-md-3">
                            <asp:LinkButton ID="itemPlaceholder" runat="server" />
                        </div>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div id="divBox" runat="server" style="z-index: 0">
                            <div class="inner">
                                <asp:Label ID="lblCount" runat="server" Font-Bold="true" Text="0" Font-Size="XX-Large"></asp:Label><br />
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Header") %>' Font-Size="Small">   
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <asp:ListView ID="lstTile" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                    <GroupTemplate>
                        <div class="txt_center col-md-3">
                            <asp:LinkButton ID="itemPlaceholder" runat="server" />
                        </div>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div id="divBox" runat="server">
                            <div class="inner">
                                <asp:Label ID="lblCount" runat="server" Font-Bold="true" Text='<%# Bind("Count") %>'
                                    Font-Size="XX-Large"></asp:Label><br />
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUSNAME") %>' Font-Size="Small">   
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <asp:ListView ID="lstTileCols" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTileCols_ItemDataBound">
                    <GroupTemplate>
                        <div class="txt_center col-md-3">
                            <asp:LinkButton ID="itemPlaceholder" runat="server" />
                        </div>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div id="divBox" runat="server">
                            <div class="inner">
                                <asp:Label ID="lblCount" runat="server" Font-Bold="true" Text='<%# Bind("Count") %>'
                                    Font-Size="XX-Large"></asp:Label><br />
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("FIELDNAME") %>' Font-Size="Small">   
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12">
                <div class="box box-danger" id="divs">
                    <div class="box-header">
                        <h4 class="box-title">
                            <asp:Label runat="server" ID="lblMainChart" Text="Select Site : "></asp:Label>
                            <asp:ListBox ID="lstSites" AutoPostBack="true" runat="server" CssClass="width300px select"
                                SelectionMode="Multiple" OnSelectedIndexChanged="lstSites_SelectedIndexChanged"></asp:ListBox>
                        </h4>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:HiddenField ID="hfMainCHart" runat="server" />
                                <asp:HiddenField ID="hfSeries" runat="server" />
                                <div id="mainchartdiv">
                                </div>
                            </div>
                        </div>
                        <!-- /.row - inside box -->
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
            <div class="col-md-12 disp-none">
                <div class="box box-danger" id="Div6">
                    <div class="box-header">
                        <h4 class="box-title">
                            <asp:Label runat="server" ID="lblGraphOverAllStatus" Text="OverAll Status"></asp:Label>
                        </h4>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body no-padding">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:HiddenField ID="hfDataOverAllStatus" runat="server" />
                                <div id="divBarOverAllStatus" runat="server" class="chartdiv">
                                </div>
                            </div>
                        </div>
                        <!-- /.row - inside box -->
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
            <asp:ListView ID="lstStatusGraph" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstStatusGraph_ItemDataBound">
                <GroupTemplate>
                    <div class="col-md-6">
                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                    </div>
                </GroupTemplate>
                <ItemTemplate>
                    <div class="box box-danger" id="Div6">
                        <div class="box-header">
                            <h4 class="box-title">
                                <asp:Label runat="server" ID="lblGraphHeader" Text='<%# Bind("Header") %>'></asp:Label>
                            </h4>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body no-padding">
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
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lstGraph" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstGraph_ItemDataBound">
                <GroupTemplate>
                    <div class="col-md-6">
                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                    </div>
                </GroupTemplate>
                <ItemTemplate>
                    <div class="box box-danger" id="Div6">
                        <div class="box-header">
                            <h4 class="box-title">
                                <asp:Label runat="server" ID="lblGraphHeader" Text='<%# Bind("STATUSNAME") %>'></asp:Label>
                            </h4>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body no-padding">
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
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lstGraphCols" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstGraphCols_ItemDataBound">
                <GroupTemplate>
                    <div class="col-md-6">
                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                    </div>
                </GroupTemplate>
                <ItemTemplate>
                    <div class="box box-danger" id="Div6">
                        <div class="box-header">
                            <h4 class="box-title">
                                <asp:Label runat="server" ID="lblGraphHeader" Text='<%# Bind("FIELDNAME") %>'></asp:Label>
                            </h4>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body no-padding">
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
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lstBars" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstBars_ItemDataBound">
                <GroupTemplate>
                    <div class="col-md-6">
                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                    </div>
                </GroupTemplate>
                <ItemTemplate>
                    <div class="box box-danger" id="Div6">
                        <div class="box-header">
                            <h4 class="box-title">
                                <asp:Label runat="server" ID="lblGraphHeader" Text='<%# Bind("HEADER") %>'></asp:Label>
                            </h4>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body no-padding">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:HiddenField ID="hfBarData" runat="server" />
                                    <div id="divBarChart" runat="server" class="chartdiv">
                                    </div>
                                </div>
                            </div>
                            <!-- /.row - inside box -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                </ItemTemplate>
            </asp:ListView>
            <asp:ListView ID="lstPies" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstPies_ItemDataBound">
                <GroupTemplate>
                    <div class="col-md-6">
                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                    </div>
                </GroupTemplate>
                <ItemTemplate>
                    <div class="box box-danger" id="Div6">
                        <div class="box-header">
                            <h4 class="box-title">
                                <asp:Label runat="server" ID="lblGraphHeader" Text='<%# Bind("HEADER") %>'></asp:Label>
                            </h4>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body no-padding">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:HiddenField ID="hfPieData" runat="server" />
                                    <div id="divPieChart" runat="server" class="chartdiv">
                                    </div>
                                </div>
                            </div>
                            <!-- /.row - inside box -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <br />
        <div class="container-fluid">
            <div class="grid-stack" runat="server" id="divDashboard">
                <asp:Repeater runat="server" ID="repeatDashboard" OnItemDataBound="repeatDashboard_ItemDataBound">
                    <ItemTemplate>
                        <div class="grid-stack-item" data-gs-x="0" data-gs-y="0" data-gs-width="4" data-gs-height="2"
                            runat="server" id="divMain">
                            <asp:HiddenField ID="hf_ID" runat="server" Value='<%# Bind("ID") %>' />
                            <div class="grid-stack-item-content" runat="server" id="divContent">
                                <asp:PlaceHolder ID="placeHolder" runat="server" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <br />
    </div>
    <script type="text/javascript">

        function GenerateGraph_OVERALL() {

            var hfData = eval('(' + $("#MainContent_hfDataOverAllStatus").val() + ')');
            var divBar = AmCharts.makeChart("#MainContent_divBarOverAllStatus", {
                "theme": "light",
                "type": "serial",
                "startDuration": 2,
                "dataProvider": hfData,
                "valueAxes": [{
                    "position": "left",
                    "title": "",
                    "integersOnly": true
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
                "depth3D": 20,
                "angle": 30,
                "chartCursor": {
                    "categoryBalloonEnabled": false,
                    "cursorAlpha": 0
                },
                "categoryField": "INVID",
                "categoryAxis": {
                    "gridPosition": "start",
                    "labelRotation": 90,
                    "integersOnly": true
                },
                "export": {
                    "enabled": true
                }

            });

        }

        function GenerateGraph(hfElement) {

            var hfID = $(hfElement).attr('id');
            var divID = $($('#' + hfID + '').next()[0]).attr('id');
            var hfData = eval('(' + $(hfElement).val() + ')');
            var divBar = AmCharts.makeChart(divID, {
                "theme": "light",
                "type": "serial",
                "startDuration": 2,
                "dataProvider": hfData,
                "valueAxes": [{
                    "position": "left",
                    "title": "",
                    "integersOnly": true
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
                    "labelRotation": 90
                },
                "export": {
                    "enabled": true
                }

            });

        }

        function GenerateGraph_Bar(hfElement) {

            var hfID = $(hfElement).attr('id');
            var divID = $($('#' + hfID + '').next()[0]).attr('id');
            var hfData = eval('(' + $(hfElement).val() + ')');
            var divBar = AmCharts.makeChart(divID, {
                "theme": "light",
                "type": "serial",
                "startDuration": 2,
                "dataProvider": hfData,
                "valueAxes": [{
                    "position": "left",
                    "title": "",
                    "integersOnly": true
                }],
                "graphs": [{
                    "balloonText": "[[VALUE]]: [[COLUMN]]",
                    "fillColorsField": "color",
                    "fillAlphas": 1,
                    "lineAlpha": 0.1,
                    "labelText": "[[VALUE]]",
                    "type": "column",
                    "valueField": "VALUE"
                }],
                "depth3D": 20,
                "angle": 30,
                "chartCursor": {
                    "categoryBalloonEnabled": false,
                    "cursorAlpha": 0,
                    "zoomable": false
                },
                "categoryField": "VALUE",
                "categoryAxis": {
                    "gridPosition": "start",
                    "labelRotation": 90
                },
                "export": {
                    "enabled": true
                }
            });

        }


        function GenerateGraph_Pie(hfElement) {

            var hfID = $(hfElement).attr('id');
            var divID = $($('#' + hfID + '').next()[0]).attr('id');
            var hfData = eval('(' + $(hfElement).val() + ')');
            var divBar = AmCharts.makeChart(divID, {
                "type": "pie",
                "startDuration": 0,
                "theme": "light",
                "addClassNames": true,
                "legend": {
                    "position": "bottom",
                    "marginRight": 100,
                    "autoMargins": false,
                    "labelText": ""
                },
                "defs": {
                    "filter": [{
                        "id": "shadow",
                        "width": "200%",
                        "height": "200%",
                        "feOffset": {
                            "result": "offOut",
                            "in": "SourceAlpha",
                            "dx": 0,
                            "dy": 0
                        },
                        "feGaussianBlur": {
                            "result": "blurOut",
                            "in": "offOut",
                            "stdDeviation": 5
                        },
                        "feBlend": {
                            "in": "SourceGraphic",
                            "in2": "blurOut",
                            "mode": "normal"
                        }
                    }]
                },
                "dataProvider": hfData,
                "valueField": "VALUE",
                "titleField": "COLUMN",
                "descriptionField": "COLUMN",
                "balloonText": "[[value]]: [[description]]",
                "labelText": "[[value]]",
                "export": {
                    "enabled": true
                }

            });

            divBar.addListener("init", handleInit);

            divBar.addListener("rollOverSlice", function (e) {
                handleRollOver(e);
            });

            function handleInit() {
                divBar.legend.addListener("rollOverItem", handleRollOver);
            }

            function handleRollOver(e) {
                var wedge = e.dataItem.wedge.node;
                wedge.parentNode.appendChild(wedge);
            }

        }

    </script>
    <script type="text/javascript">
        am4core.ready(function () {

            // Themes begin
            am4core.useTheme(am4themes_animated);
            // Themes end

            var ChartDATA = eval('(' + $("#MainContent_hfMainCHart").val() + ')');

            var chart = am4core.create('mainchartdiv', am4charts.XYChart)
            chart.colors.step = 4;

            chart.logo.height = -15;

            chart.legend = new am4charts.Legend()
            chart.legend.position = 'top'
            chart.legend.paddingBottom = 20
            chart.legend.labels.template.maxWidth = 95

            var xAxis = chart.xAxes.push(new am4charts.CategoryAxis())
            xAxis.dataFields.category = 'category'
            xAxis.renderer.cellStartLocation = 0.1
            xAxis.renderer.cellEndLocation = 0.9
            xAxis.renderer.grid.template.location = 0;
            xAxis.renderer.minGridDistance = 20;

            var yAxis = chart.yAxes.push(new am4charts.ValueAxis());
            yAxis.min = 0;

            function createSeries(value, name) {
                var series = chart.series.push(new am4charts.ColumnSeries())
                series.dataFields.valueY = value
                series.dataFields.categoryX = 'category'
                series.name = name

                series.events.on("hidden", arrangeColumns);
                series.events.on("shown", arrangeColumns);

                var bullet = series.bullets.push(new am4charts.LabelBullet())
                bullet.interactionsEnabled = false
                bullet.dy = 30;
                bullet.label.text = '{valueY}'
                bullet.label.fill = am4core.color('#ffffff')

                series.columns.template.tooltipText = "Site: {categoryX}\n {name} : {valueY}";

                return series;
            }

            chart.data = ChartDATA;

            var seriesArr = $("#MainContent_hfSeries").val().split(',');

            var numArr = ['first', 'second', 'third', 'fourth', 'fifth', 'sixth', 'seventh', 'eighth', 'ninenth', 'ten', "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty",
                "Twenty-one", "Twenty-two", "Twenty-three", "Twenty-four", "Twenty-five", "Twenty-six", "Twenty-seven", "Twenty-eight", "Twenty-nine", "Thirty"];

            for (var i = 0; i < seriesArr.length; i++) {
                createSeries(numArr[i], seriesArr[i]);
            }

            function arrangeColumns() {

                var series = chart.series.getIndex(0);

                var w = 1 - xAxis.renderer.cellStartLocation - (1 - xAxis.renderer.cellEndLocation);
                if (series.dataItems.length > 1) {
                    var x0 = xAxis.getX(series.dataItems.getIndex(0), "categoryX");
                    var x1 = xAxis.getX(series.dataItems.getIndex(1), "categoryX");
                    var delta = ((x1 - x0) / chart.series.length) * w;
                    if (am4core.isNumber(delta)) {
                        var middle = chart.series.length / 2;

                        var newIndex = 0;
                        chart.series.each(function (series) {
                            if (!series.isHidden && !series.isHiding) {
                                series.dummyData = newIndex;
                                newIndex++;
                            }
                            else {
                                series.dummyData = chart.series.indexOf(series);
                            }
                        })
                        var visibleCount = newIndex;
                        var newMiddle = visibleCount / 2;

                        chart.series.each(function (series) {
                            var trueIndex = chart.series.indexOf(series);
                            var newIndex = series.dummyData;

                            var dx = (newIndex - trueIndex + middle - newMiddle) * delta

                            series.animate({ property: "dx", to: dx }, series.interpolationDuration, series.interpolationEasing);
                            series.bulletsContainer.animate({ property: "dx", to: dx }, series.interpolationDuration, series.interpolationEasing);
                        })
                    }
                }
            }

        });     // end am4core.ready()
    </script>
</asp:Content>
