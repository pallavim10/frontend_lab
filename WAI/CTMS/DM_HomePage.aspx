<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DM_HomePage.aspx.cs" Inherits="CTMS.DM_HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
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
    <script type="text/javascript" src="js/amcharts/amcharts.js"></script>
    <script type="text/javascript" src="js/amcharts/serial.js"></script>
    <script type="text/javascript" src="js/amcharts/pie.js"></script>
    <script type="text/javascript" src="js/amcharts/themes/light.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                    AutoPostBack="true" OnSelectedIndexChanged="drpSites_SelectedIndexChanged">
                </asp:DropDownList>
        </div>
    </div>

    <asp:Repeater runat="server" OnItemDataBound="repeatData_ItemDataBound" ID="repeatData">
        <ItemTemplate>
            <div class="box box-primary" runat="server" id="ListingTileDiv">
                <div class="box-header">
                    <h3 class="box-title" style="padding-top: 0px;">
                        <asp:Label ID="lblHeaderTile" runat="server" Text='<%# Bind("TYPE") %>'></asp:Label>
                    </h3>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:ListView ID="lstTile" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                            <GroupTemplate>
                                <div class="txt_center col-md-3">
                                    <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                </div>
                            </GroupTemplate>
                            <ItemTemplate>
                                <div id="divBox" runat="server">
                                    <div class="inner">
                                        <asp:Label ID="lblCount" runat="server"
                                            Text='<%# Eval("Count") != null ? Eval("Count") : "0" %>'
                                            Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblTileName" runat="server"
                                            Text='<%# Eval("TILE") != null && Eval("TILE").ToString() != "" 
                                                ? Eval("TILE") : Eval("NAME") != null && Eval("NAME").ToString() != "" 
                                                ? Eval("NAME") : "" %>' Font-Size="Small"></asp:Label>
                                        <div class="icon">
                                            <i class="ion ion-stats-bars"></i>
                                        </div>
                                        <a href="#" class="small-box-footer"></a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
            <div class="row no-padding">
                <div class="col-md-12">
                    <asp:ListView ID="lstListingGraph" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstListingGraph_ItemDataBound">
                        <GroupTemplate>
                            <div class="txt_center">
                                <asp:LinkButton ID="itemPlaceholder" runat="server" />
                            </div>
                        </GroupTemplate>
                        <ItemTemplate>
                            <div class="box box-danger" id="Div6">
                                <div class="box-header">
                                    <h4 class="box-title">
                                        <asp:Label runat="server" ID="lblGraphHeader" Text='<%# Bind("NAME") %>'></asp:Label>
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
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

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
                    "labelRotation": 90
                },
                "export": {
                    "enabled": true
                }

            });

        }
    </script>
</asp:Content>
