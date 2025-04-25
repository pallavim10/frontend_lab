<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RiskDashboard.aspx.cs" Inherits="CTMS.RiskDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <!-- Dynamic Dashboard (This is for GridStack.js) -->
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.11.0/jquery-ui.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lodash.js/4.17.0/lodash.min.js"></script>
    <link rel="stylesheet" href="js/GridStack/gridstack.css" />
    <link rel="stylesheet" href="js/GridStack/gridstack-extra.css" />
    <script src="js/GridStack/gridstack.js"></script>
    <script src="js/GridStack/gridstack.jQueryUI.js"></script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="anish">
        <script src="js/amcharts/amcharts.js"></script>
        <script src="js/amcharts/serial.js"></script>
        <script src="js/amcharts/pie.js"></script>
        <script src="js/amcharts/themes/light.js"></script>
        <link href="js/amcharts/plugins/export/export.css" rel="stylesheet" />
        <script src="js/amcharts/plugins/export/export.min.js"></script>
        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
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
            <div class="pull-right">
                <input type="button" value="Export Charts" onclick="exportReport();" class="btn btn-primary btn-sm cls-btnSave margin-right10"
                    style="float: right" />
            </div>
        </div>
        <br />
        <br />
        <br />
        <div class="container-fluid">
            <div class="grid-stack" runat="server" id="div1">
                <asp:Repeater runat="server" ID="repeatTiles" OnItemDataBound="repeatTiles_ItemDataBound">
                    <ItemTemplate>
                        <div class="grid-stack-item" data-gs-x="0" data-gs-y="0" data-gs-width="4" data-gs-height="2"
                            runat="server" id="divMain">
                            <div class="grid-stack-item-content" style="overflow-x: hidden; overflow-y: hidden;"
                                runat="server" id="divContent">
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
    <div class="dropdown-menu dropdown-menu-sm" id="context-menu">
        <ul style="margin-left: -23px;">
            <li class="fa fa-cog"><a class="dropdown-item" href="#" onclick="Manage_Tile();"
                style="color: #333333">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Manage Tiles</a></li>
            <hr style="margin: 5px;" />
            <li class="icon-bar-chart"><a class="dropdown-item" href="#" onclick="Manage_Dashboard();"
                style="color: #333333">&nbsp;&nbsp;&nbsp;&nbsp; Manage Charts</a> </li>
        </ul>
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
    </script>
    <script type="text/javascript">
        function exportReport() {
            console.log("Starting export...");
            // Define IDs of the charts we want to include in the report
            //var ids = ["colorBar", "catchart", "ExposureNSCLCBar", "randomised", "EventImpact", "EventType", "pieex1", "pieex", "riskcat", "divRiskDonut1", "divRiskDonut2", "divRiskDonut3", "divRiskDonut4", "divRiskPie", "ExposuremCRCBar", "divTrancRiskPie", "chartdiv", "chartdiv1", "StatusNSCLCBar", "countrywise", "gradewise", "bymonth", "byquarter", "SubjectPerSiteBar", "eventinterest", "doublebar", "pieinnsclc", "SiteWisemCRCBar", "labmcrc", "labnsclc"];
            var ids = []
            for (var x = 0; x < AmCharts.charts.length; x++) {
                ids[x] = AmCharts.charts[x].div.id;
            }
            // Collect actual chart objects out of the AmCharts.charts array
            var charts = {}
            var charts_remaining = ids.length;

            for (var i = 0; i < ids.length; i++) {
                for (var x = 0; x < AmCharts.charts.length; x++) {
                    if (AmCharts.charts[x].div.id == ids[i])

                        charts[ids[i]] = AmCharts.charts[x];

                }
            }


            // Trigger export of each chart
            for (var x in charts) {
                if (charts.hasOwnProperty(x)) {
                    var chart = charts[x];
                    chart["export"].capture({}, function () {
                        this.toPNG({}, function (data) {

                            // Save chart data into chart object itself
                            this.setup.chart.exportedImage = data;

                            // Reduce the remaining counter
                            charts_remaining--;

                            // Check if we got all of the charts
                            if (charts_remaining == 0) {
                                // Yup, we got all of them
                                // Let's proceed to putting PDF together                              
                                generatePDF();
                            }

                        });
                    });
                }
            }

            function generatePDF() {

                // Log
                console.log("Generating PDF...");
                var layout = {
                    "content": [],
                    "styles": {
                        "header": {
                            "fontSize": 18,
                            "bold": true,
                            "background": 'pink',
                            "width": '70%'
                        }
                    }
                };

                for (var i = 0; i < ids.length; i++) {
                    layout.content.push({
                        "columns": [{
                            "width": "50%",
                            "image": charts[ids[i]].exportedImage,
                            "fit": [500, 600]
                        }],
                        "columnGap": 10
                    });
                }

                // Trigger the generation and download of the PDF
                // We will use the first chart as a base to execute Export on
                chart["export"].toPDF(layout, function (data) {
                    this.download(data, "application/pdf", "reporting.pdf");
                });

            }
        }


        $(function () {
            var options = {
                cellHeight: 120,
                verticalMargin: 1
            };


            var a;
            var abc = $('.grid-stack').toArray();
            for (a = 0; a < abc.length; ++a) {
                $(abc[a]).gridstack(options);
                grid = $(abc[a]).data('gridstack');
                grid.disable();
            }
        });
    </script>
    <script type="text/javascript">
        $('.anish').on('contextmenu', function (e) {
            var top = e.pageY - 10;
            var left = e.pageX - 90;
            $("#context-menu").css({
                display: "block",
                top: top,
                left: left
            }).addClass("show");
            return false; //blocks default Webbrowser right click menu
        }).on("click", function () {
            $("#context-menu").removeClass("show").hide();
        });

        $("#context-menu ul").on("click", function () {
            $(this).parent().removeClass("show").hide();
        });
    </script>
    <script type="text/javascript">
        function Manage_Dashboard() {
            var test = "Manage_Dashboard.aspx?Section=Risk Management&Type=Chart";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test, '_blank');
            return false;
        }

        function Manage_Tile() {
            var test = "Manage_Dashboard.aspx?Section=Risk Management&Type=Tile";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test, '_blank');
            return false;
        }

    </script>
</asp:Content>
