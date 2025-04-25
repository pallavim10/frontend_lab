<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagementDashboard.aspx.cs" Inherits="CTMS.UserManagementDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/amcharts/amcharts.js"></script>
    <script src="js/amcharts/serial.js"></script>
    <script src="js/amcharts/pie.js"></script>
    <script src="js/amcharts/themes/light.js"></script>
    <style>
        .chartdiv {
            width: 100%;
            height: 300px;
        }
    </style>
    <script>
        function pageLoad() {
            //CalculateRPN();
            $('.select').select2();

        }

        $(function bindGraph() {

            var a;

            var hfBarData = $("input[id*='hfBarData']").toArray();
            for (a = 0; a < hfBarData.length; ++a) {
                GenerateGraph_Bar(hfBarData[a]);
            }

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box-body">
        <div class="form-group">
            <div class="form-group has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="row">
                <asp:ListView ID="lstTile" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                    <GroupTemplate>
                        <div class="txt_center col-lg-6">
                            <asp:LinkButton ID="itemPlaceholder" runat="server" />
                        </div>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div class="box box-danger" id="Div6">
                            <div class="col-md-12">
                                <div id="divBox" runat="server" style="margin-top: 20px;">
                                    <div class="inner">
                                        <asp:Label ID="lblCount" runat="server" Font-Bold="true" Text='<%# Bind("COUNTS") %>'
                                            Font-Size="XX-Large"></asp:Label><br />
                                        <asp:Label ID="lblNAME" runat="server" Text='<%# Bind("NAME") %>'
                                            Font-Size="Small">   
                                        </asp:Label>
                                    </div>
                                    <div class="icon">
                                        <i class="ion ion-stats-bars"></i>
                                    </div>
                                    <a href="#" class="small-box-footer"></a>
                                </div>
                            </div>
                            <div class="box-body no-padding">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:HiddenField ID="hfBarData" runat="server" />
                                        <div id="divPieChart" runat="server" class="chartdiv">
                                        </div>
                                    </div>
                                </div>
                                <!-- /.row - inside box -->
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        function GenerateGraph_Bar(hfElement) {

            if ($(hfElement).val() != '') {

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
</asp:Content>
