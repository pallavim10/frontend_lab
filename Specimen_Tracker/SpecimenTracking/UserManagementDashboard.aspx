<%@ Page  Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagementDashboard.aspx.cs" Inherits="SpecimenTracking.UserManagementDashboard" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
  <%-- <script src="dist/js/amcharts/amcharts.js" type="text/javascript"></script>
    <script src="dist/js/amcharts/serial.js" type="text/javascript"></script>
    <script src="dist/js/amcharts/pie.js" type="text/javascript"></script>
    <script src="dist/js/amcharts/themes/light.js" type="text/javascript"></script>--%>
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

            var a;

            var hfBarData = $("input[id*='hfBarData']").toArray();
            for (a = 0; a < hfBarData.length; ++a) {
                GenerateGraph_Bar(hfBarData[a]);
            }

        });
        

    </script>
    <script src="plugins/jquery/jquery.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0 text-dark">UMT Dashboard</h1>
                    </div>
                    <!-- /.col -->
                   <%-- <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="HomePage.aspx">UMT</a></li>
                            <li class="breadcrumb-item active">UMT Dashboard</li>
                        </ol>
                    </div>--%>
                </div>
            </div>
        </div>
    <section class="content">
    <div class="container-fluid">
    <div class="row">
    <div class="col-md-12">       
    <div class="box-body">
        <div class="form-group">
            <div class="form-group has-warning">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                        <div class="data-original-title">
                            </div>
            <div class="row">
                 <asp:ListView ID="lstTile" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                                <GroupTemplate>
                                    <div class="col-lg-6 col-xs-6">
                                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                    </div>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <div class="box box-danger ">
                                       <%-- <div class="col-md-12 p-0">
                                            <div id="divBox" runat="server" style="margin-top: 20px;">--%>
                                                 
                                               <%-- <div class="">
                                                    <asp:Label ID="lblCount" runat="server" Font-Bold="true" Text='<%# Bind("COUNTS") %>' Font-Size="XX-Large"></asp:Label><br />
                                                    <asp:Label ID="lblNAME" runat="server" Text='<%# Bind("NAME") %>' Font-Size="Small"></asp:Label>
                                                </div>  --%>                                             
                                               <%-- <a href="#" class="small-box-footer"></a>
                                            </div>
                                        </div>--%>
                                        <div class="box-body no-padding">
                                            <div class="row">
                                                <div class="col-md-12">
                                                   
                                                    <div class="card card-info card-outline fade-in-out">
                                                         <div class="card-header">
                                                    <div class="pull-right">
                                            <button type="button" class="btn btn-tool pull-right" data-card-widget="collapse"><i class="fas fa-minus"></i></button>
                                                   </div>
                                                       
                                                     <%-- cssClass='<%# Eval("Color")  class='<%# Eval("Icon") %>'%>' --%>
                                                    <div class="info-box-content">
                                                         <div class="info-box inner">
                                                    <span class="info-box-icon" style="width: 100px"><i id="ICONCLASS" runat="server"></i></span> 
                                                        <span class="info-box-number text-lg textcoutcolor">
                                                             <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>' Font-Size="Small" CssClass="font-weight-bold"></asp:Label><br />
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text='<%# Bind("COUNTS") %>' Font-Size="XX-Large"></asp:Label><br />
                                                   </span>
                                                    </div>
                                                </div>
                                                   </div>
                                                      
                                                            <div class="card-body">
                                                    <asp:HiddenField ID="hfBarData" runat="server" />
                                                    <div id="divPieChart" runat="server" class="chartdiv"></div>
                                                </div>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
            </div>
        </div>
    </div>
        </div>
        </div>
    </div>
        </section>
        </div>
<script type="text/javascript">
    function GenerateGraph_Bar(hfElement) {
        // Get the JSON data from the hidden field
        var jsonData = $(hfElement).val();

        // Ensure the data is valid before proceeding
        if (jsonData) {
            // Parse the JSON data from the hidden field
            var chartData = JSON.parse(jsonData);

            // Extract labels and data points for the bar chart
            var labels = chartData.labels;  // Example: ["Red", "Blue", "Yellow", ...]
            var dataPoints = chartData.data;  // Example: [12, 19, 3, 5, 2, 3]

            // Find the corresponding chart div to render the chart inside it
            var chartDiv = $(hfElement).closest('.box').find('.chartdiv');

            // Generate a unique ID for the canvas to avoid conflicts
            var canvasId = 'barChart' + $(hfElement).index();
            chartDiv.html('<canvas id="' + canvasId + '" width="400" height="300"></canvas>');

            // Get the canvas context
            var ctx = document.getElementById(canvasId).getContext('2d');

            // Create a new bar chart using Chart.js
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels, // Use the labels from the hidden field data
                    datasets: [{
                        label: "[[VALUE]]", // Customize this label
                        data: dataPoints, // Use the data points from the hidden field
                        backgroundColor: 'rgba(75, 192, 192, 0.2)', // Custom colors
                        borderColor: 'rgba(75, 192, 192, 1)',
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        }
    }
</script>
    
    <script src="plugins/sparklines/sparkline.js" type="text/javascript"></script>
</asp:Content>

