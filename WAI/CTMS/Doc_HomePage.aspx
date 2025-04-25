<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Doc_HomePage.aspx.cs" Inherits="CTMS.Doc_HomePage" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="anish">
        <script type="text/javascript" src="Scripts/amcharts.js"></script>
        <script src="https://www.amcharts.com/lib/3/serial.js"></script>
        <script src="https://www.amcharts.com/lib/3/pie.js"></script>
        <link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css"
            type="text/css" media="all" />
        <script src="https://www.amcharts.com/lib/3/themes/light.js"></script>
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
            var test = "Manage_Dashboard.aspx?Section=Documents&Type=Chart";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test, '_blank');
            return false;
        }

        function Manage_Tile() {
            var test = "Manage_Dashboard.aspx?Section=Documents&Type=Tile";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test, '_blank');
            return false;
        }

    </script>
</asp:Content>
