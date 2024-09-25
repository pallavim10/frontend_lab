<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagementDashboard.aspx.cs" Inherits="SpecimenTracking.UserManagementDashboard" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="dist/js/amcharts/amcharts.js" type="text/javascript"></script>
    <script src="dist/js/amcharts/serial.js" type="text/javascript"></script>
    <script src="dist/js/amcharts/pie.js" type="text/javascript"></script>
    <script src="dist/js/amcharts/themes/light.js" type="text/javascript"></script>
    <script type="text/javascript" src="plugins/chart.js/Chart.min.js"></script>
    <style type="text/css">
        .chartdiv {
            width: 100%;
            height: 100%;
        }
    </style>
    <script type="text/javascript">
        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();

        }

        $(function bindGraph() {

           // var a;

            var hfBarData = $("input[id*='hfBarData']").toArray();
            console.log("hfBarData: " + hfBarData.length);
            for (var a = 0; a < hfBarData.length; a++)
            {
                GenerateGraph_Bar(hfBarData[a], a);
                console.log("hfBarData a: " + a);
            }

        });

    </script>
    <script src="plugins/jquery/jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">User Dashboard</h1>
                    </div>
                    <!-- /.col -->
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="UserManagementDashboard.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Dashboard v3</li>
                        </ol>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </div>
        <!-- /.content-header -->
        <section class="content">
            <div class="container-fluid">
                <div class="row">
                    <asp:ListView ID="lstTile" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                        <LayoutTemplate>
                            <div id="itemPlaceholder" runat="server"></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <!-- Each Card should be in a column -->
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <div class="card">
                                    <div class="card-header border-0">
                                        <div class="d-flex justify-content-between">
                                            <h3 class="card-title"></h3>
                                            <div class="pull-right">
                                                <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <p class="d-flex flex-column">
                                                <%-- <span class="text-bold text-lg"></span>--%>

                                                <div id="divBox" class="info-box w-100">
                                                     <asp:HiddenField ID="hfIconData" runat="server" />
                                                    <span class="info-box-icon elevation-1 bg-secondary">
                                                        <i class="nav-icon fa fa-user" runat="server" id="iconElement"></i></span>

                                                    <div class="info-box-content">
                                                        <span class="info-box-text">
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text='<%# Bind("COUNTS") %>' Font-Size="XX-Large"></asp:Label></span>
                                                        <span class="info-box-number">
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>' Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <!-- /.info-box-content -->
                                                </div>
                                            </p>
                                        </div>
                                        <div class="position-relative mb-4">
                                            <asp:HiddenField ID="hfBarData" runat="server" />
                                            <div id="divPieChart" runat="server" class="chartdiv"></div>
                                        </div>

                                        <%-- <div class="d-flex flex-row justify-content-end">
                                    <span class="mr-2">
                                        <i class="fas fa-square text-primary"></i> This Week
                                    </span>
                                    <span>
                                        <i class="fas fa-square text-gray"></i> Last Week
                                    </span>
                                </div>--%>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </section>

    </div>
    <script type="text/javascript">
        //function GenerateGraph_Bar(hfElement) {
        //    // Get the JSON data from the hidden field
        //    var jsonData = $(hfElement).val();

        //    // Ensure the data is valid before proceeding
        //    if (jsonData) {
        //        // Parse the JSON data from the hidden field
        //        var chartData = JSON.parse(jsonData);

        //        // Extract labels and data points for the bar chart
        //        var labels = chartData.labels;  // Example: ["Red", "Blue", "Yellow", ...]
        //        var dataPoints = chartData.data;  // Example: [12, 19, 3, 5, 2, 3]
        //        console.log("Labels  " + labels + "datapoints  " +dataPoints);

        //        // Find the corresponding chart div to render the chart inside it
        //        var chartDiv = $(hfElement).closest('.box').find('.chartdiv');

        //        // Generate a unique ID for the canvas to avoid conflicts
        //        var canvasId = 'visitors-chart' + $(hfElement).index();
        //        chartDiv.html('<canvas id="' + canvasId + '" width="400" height="300"></canvas>');

        //        // Get the canvas context
        //        var ctx = document.getElementById(canvasId).getContext('2d');

        //        // Create a new bar chart using Chart.js
        //        new Chart(ctx, {
        //            type: 'bar',
        //            data: {
        //                labels: labels, // Use the labels from the hidden field data
        //                datasets: [{
        //                    label: "[[VALUE]]", // Customize this label
        //                    data: dataPoints, // Use the data points from the hidden field
        //                    backgroundColor: 'rgba(75, 192, 192, 0.2)', // Custom colors
        //                    borderColor: 'rgba(75, 192, 192, 1)',
        //                    borderWidth: 1
        //                }]
        //            },
        //            options: {
        //                scales: {
        //                    y: {
        //                        beginAtZero: true
        //                    }
        //                }
        //            }
        //        });
        //    }
        //}

        function GenerateGraph_Bar(hfElement, index) {

            if ($(hfElement).val() != '') {

                var hfID = $(hfElement).attr('id');
                var divID = $($('#' + hfID + '').next()[0]).attr('id');
                console.log("divID: ", divID);
                var hfData = eval('(' + $(hfElement).val() + ')');
                // Find the chart container and set it up
                // Log hfData for inspection
                console.log("hfData: ", hfData);
                console.log("hfData as JSON: " + JSON.stringify(hfData, null, 2));

                // Check specific properties
                console.log("First Item:", hfData[0]);
                console.log("Labels:", hfData.map(item => item.COLUMN));
                console.log("Values:", hfData.map(item => item.VALUE));
                // Find the chart container and set it up
                var chartDiv = $('#' + divID);
                var uniqueId = 'chart-' + index;
                console.log("uniqueId: ", uniqueId);
                chartDiv.html('<div id="' + uniqueId + '" style="width: 100%; height: 300px;"></div>');

                // Log hfData for inspection
                console.log("hfData: ", hfData);

                var chart = AmCharts.makeChart(uniqueId, {
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
                    "categoryField": "COLUMN",
                    "categoryAxis": {
                        "gridPosition": "start",
                        "labelRotation": 90
                    },
                    "export": {
                        "enabled": true
                    }
                });

            }

        }
    </script>
    <script src="plugins/sparklines/sparkline.js" type="text/javascript"></script>
</asp:Content>

