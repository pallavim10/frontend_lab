<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MM_Advance_Metrics.aspx.cs" Inherits="CTMS.MM_Advance_Metrics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/amcharts/amcharts.js"></script>
    <script src="js/amcharts/serial.js"></script>
    <script src="js/amcharts/pie.js"></script>
    <script src="js/amcharts/themes/light.js"></script>
    <link href="js/amcharts/plugins/export/export.css" rel="stylesheet" />
    <script src="js/amcharts/plugins/export/export.min.js"></script>
    <!-- Resources -->
    <script src="https://www.amcharts.com/lib/4/core.js"></script>
    <script src="https://www.amcharts.com/lib/4/charts.js"></script>
    <script src="https://www.amcharts.com/lib/4/themes/animated.js"></script>
    <script>
        $(function bindGraph() {

            var a;
            var b;
            var c;

            var hfData = $("input[id*='hfData']").toArray();
            for (a = 0; a < hfData.length; ++a) {
                GenerateGraph(hfData[a]);
            }
        });
    </script>
    <style>
        #timelineChartdiv
        {
            height: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Advance Metrics</h3>
        </div>
        <div class="col-md-12">
            <div class="pull-left" style="display: inline-flex;">
                <asp:DropDownList Style="width: 250px;" ID="drpCountry" runat="server" class="form-control drpControl"
                    AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList Style="width: 250px;" ID="drpSites" runat="server" class="form-control drpControl"
                    OnSelectedIndexChanged="drpSites_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <br />
    </div>
    <div class="box-body">
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="row">
                <asp:ListView ID="lstTile" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                    <GroupTemplate>
                        <div class="txt_center col-lg-3">
                            <asp:LinkButton ID="itemPlaceholder" runat="server" />
                        </div>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div id="divBox" runat="server">
                            <div class="inner">
                                <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label><br />
                                <asp:Label ID="lblNAME" runat="server" Text='<%# Bind("MetricsName") %>' Font-Size="Small">   
                                </asp:Label>
                            </div>
                            <asp:HiddenField ID="hdnNumListId" runat="server" />
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="col-md-12 disp-none">
                <div class="row">
                    <div class="box box-danger" id="Div6">
                        <div class="box-header">
                            <h4 class="box-title">
                                Timeline Chart
                            </h4>
                        </div>
                        <div class="col-md-12">
                            <div class="pull-left" style="display: inline-flex;">
                                <asp:DropDownList ID="ddlSUBJID" runat="server" class="form-control drpControl">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control txtDate required" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control txtDate required" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btngetdata" runat="server" Text="Get Data" CssClass="btn btn-primary btn-sm cls-btnSave" OnClick="btngetdata_Click"
                                     />
                            </div>
                        </div>
                        <br />
                        <br />
                        <!-- /.box-header -->
                        <div class="box-body no-padding">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="timelineChartdiv">
                                    </div>
                                    <asp:HiddenField runat="server" ID="hdntimelineChart" />
                                </div>
                            </div>
                            <!-- /.row - inside box -->
                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:ListView ID="lstGraph" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstGraph_ItemDataBound">
                    <GroupTemplate>
                        <div class="col-md-12">
                            <asp:LinkButton ID="itemPlaceholder" runat="server" />
                        </div>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div class="box box-danger" id="Div6">
                            <div class="box-header">
                                <h4 class="box-title">
                                    <asp:Label runat="server" ID="lblGraphHeader" Text='<%# Bind("MetricsName") %>'></asp:Label>
                                </h4>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body no-padding">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:HiddenField ID="hdnNumListId" runat="server" />
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
            </div>
            <br />
        </div>
    </div>
    <script type="text/javascript">

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
                    "title": ""
                }],
                "graphs": [{
                    "balloonText": "[[category]]: <b>[[value]]%</b> <b>[[Counts]]",
                    "fillColorsField": "color",
                    "fillAlphas": 1,
                    "lineAlpha": 0.1,
                    "labelText": "[[Count]]" + "%",
                    "type": "column",
                    "valueField": "Count",
                    "fontSize": 10
                }, {
                    "balloonText": "Mean:<b> [[Mean]]",
                    "fillAlphas": 0,
                    "lineThickness": 2,
                    "lineAlpha": 1,
                    "color": "#000000",
                    "title": "Mean",
                    "valueField": "Mean"
                }],
                "bullets": [{
                    "type": "LabelBullet",
                    "label": {
                        "text": "{value}",
                        "fontSize": 20
                    }
                }],
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
                },
                "listeners": [{

                    "event": "clickGraphItem",
                    "method": function (event) {
                        var INVID = event.item.category;
                        var LISTID = $($('#' + hfID + '').prev()[0]).val();
                        var test = "MM_LISTING_DATA.aspx?LISTID=" + LISTID + "&INVID=" + INVID;
                        window.location.href = test;
                        return false;
                    }
                }]

            });

        }
    </script>
    <script>
        am4core.ready(function () {

            // Themes begin
            am4core.useTheme(am4themes_animated);
            // Themes end

            var chart = am4core.create("timelineChartdiv", am4charts.XYChart);
            chart.hiddenState.properties.opacity = 0; // this creates initial fade-in

            chart.paddingRight = 30;
            chart.dateFormatter.inputDateFormat = "dd-MMM-yyyy";

            var colorSet = new am4core.ColorSet();
            colorSet.saturation = 0.4;

            chart.data = eval('[' + $("#<%=hdntimelineChart.ClientID%>").val() + ']');

            chart.dateFormatter.dateFormat = "dd-MMM-yyyy";
            chart.dateFormatter.inputDateFormat = "dd-MMM-yyyy";

            var categoryAxis = chart.yAxes.push(new am4charts.CategoryAxis());
            categoryAxis.dataFields.category = "category";
            categoryAxis.renderer.grid.template.location = 0;
            categoryAxis.renderer.inversed = true;

            var dateAxis = chart.xAxes.push(new am4charts.DateAxis());
            dateAxis.renderer.minGridDistance = 70;
            dateAxis.baseInterval = { count: 1, timeUnit: "day" };
            dateAxis.renderer.tooltipLocation = 0;

            var series1 = chart.series.push(new am4charts.ColumnSeries());
            series1.columns.template.height = am4core.percent(70);
            series1.columns.template.tooltipText = "{task}: [bold]{openDateX}[/] - [bold]{dateX}[/]";

            series1.dataFields.openDateX = "start";
            series1.dataFields.dateX = "end";
            series1.dataFields.categoryY = "category";
            series1.columns.template.propertyFields.fill = "color"; // get color from data
            series1.columns.template.propertyFields.stroke = "color";
            series1.columns.template.strokeOpacity = 1;

            chart.scrollbarX = new am4core.Scrollbar();

        }); // end am4core.ready()
    </script>
</asp:Content>
