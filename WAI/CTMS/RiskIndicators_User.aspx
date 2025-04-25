<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RiskIndicators_User.aspx.cs" Inherits="CTMS.RiskIndicators_User" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <link href="fonts/NEW%20FONT%20AWESOME/css/all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                document.getElementById('img' + divname).className = 'icon-minus-sign-alt';

            } else {
                div.style.display = "none";
                document.getElementById('img' + divname).className = 'icon-plus-sign-alt';
            }
        }

        function ManipulateAll(ID) {
            var img = document.getElementById('img' + ID);

            if (img.className == 'icon-plus-sign-alt') {
                img.className = 'icon-minus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "inline");
                $("i[id*='" + ID + "']").removeClass('icon-plus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-minus-sign-alt');
            } else {
                img.className = 'icon-plus-sign-alt'
                $("div[id*='" + ID + "']").css("display", "none");
                $("i[id*='" + ID + "']").removeClass('icon-minus-sign-alt');
                $("i[id*='" + ID + "']").addClass('icon-plus-sign-alt');
            }
        }

    </script>
    <script type="text/javascript">

        $(function () {
            redblinkFont();
            yellowblinkFont();
        });

        function redblinkFont() {
            var blinks = document.getElementsByClassName("redblink");

            for (var a = 0; a < blinks.length; ++a) {
                blinks[a].style.color = "red";
            }

            setTimeout("setredblinkFont()", 500);
        }

        function setredblinkFont() {
            var blinks = document.getElementsByClassName("redblink");

            for (var a = 0; a < blinks.length; ++a) {
                blinks[a].style.color = "";
            }
            setTimeout("redblinkFont()", 500);

        }

        function yellowblinkFont() {
            var blinks = document.getElementsByClassName("yellowblink");

            for (var a = 0; a < blinks.length; ++a) {
                blinks[a].style.color = "Amber";
            }

            setTimeout("setyellowblinkFont()", 750);
        }

        function setyellowblinkFont() {
            var blinks = document.getElementsByClassName("yellowblink");

            for (var a = 0; a < blinks.length; ++a) {
                blinks[a].style.color = "";
            }
            setTimeout("yellowblinkFont()", 750);

        }

        function greenblinkFont() {
            var blinks = document.getElementsByClassName("greenblink");

            for (var a = 0; a < blinks.length; ++a) {
                blinks[a].style.color = "green";
            }
            setTimeout("setgreenblinkFont()", 1000);
        }

        function setgreenblinkFont() {
            var blinks = document.getElementsByClassName("greenblink");

            for (var a = 0; a < blinks.length; ++a) {
                blinks[a].style.color = "";
            }
            setTimeout("greenblinkFont()", 1000);
        }


        var timer = 0;
        var delay = 200;
        var prevent = false;

        $(document).ready(function () {
            $(".SetTrigger").mousedown(function (e) {
                if (e.button == 2) {
                    var test = "RM_Mng_RiskIndicators.aspx?User=User";
                    var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no";
                    window.open(test, '_self');
                    return false;
                }
            });
        });

        $(document).ready(function () {
            $(".SetTrigger").click(function (e) {

                var TileID = $(this).find('input').val();
                timer = setTimeout(function (e) {
                    if (!prevent) {

                        var test = "RM_Indic_Charts.aspx?TILEID=" + TileID;
                        var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no,height=575px,width=1900px";
                        window.open(test, '_blank', strWinProperty);
                        return false;

                    }
                    prevent = false;
                }, delay);
            });
        });


    </script>
    <style>
        .chartdiv
        {
            width: 100%;
            height: 200px;
            margin-bottom: -10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="anish">
        <script src="js/amcharts/amcharts.js"></script>
        <script src="js/amcharts/serial.js"></script>
        <script src="js/amcharts/pie.js"></script>
        <script src="js/amcharts/gauge.js" type="text/javascript"></script>
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
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <br />
        <asp:HiddenField runat="server" ID="hfGood" />
        <asp:HiddenField runat="server" ID="hfBad" />
        <asp:HiddenField runat="server" ID="hfWorst" />
        <div class="box box-primary MngTrigger">
            <div class="box-header">
                <h3 class="box-title">
                    Project Level Alerts
                </h3>
            </div>
            <asp:GridView ID="gvRiskIndic" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                OnRowDataBound="gvRiskIndic_RowDataBound" CssClass="table table-bordered table-striped">
                <Columns>
                    <asp:TemplateField HeaderText="Risk Indicators" ItemStyle-CssClass="txt_center" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label Style="width: 150px; text-align: center;" ID="lblRiskIndic" Text='<%# Bind("TileName") %>'
                                runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Result" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Style="width: 150px; text-align: center;" ID="lblResult" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Score" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label Style="width: 150px; text-align: center;" ID="lblScore" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="txt_center disp-none" HeaderStyle-CssClass="disp-none"  HeaderText="Actionable" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label Style="width: 150px; text-align: center;" ID="lblAction" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Flag" ItemStyle-CssClass="txt_center" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <i class="fa fa-flag" visible="false" runat="server" id="Igreen" style="font-size: 25px;
                                color: Green"></i><i class="fa fa-flag" visible="false" runat="server" id="Iyellow"
                                    style="font-size: 25px; color: #FFBF00"></i><i class="fa fa-flag" visible="false"
                                        runat="server" id="Ired" style="font-size: 25px; color: Red"></i>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div class="box box-primary MngTrigger">
            <div class="box-header">
                <h3 class="box-title">
                    Country Level Alerts
                </h3>
            </div>
            <asp:GridView ID="gvCountryAlerts" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped" OnRowDataBound="gvCountryAlerts_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="txtCenter" ItemStyle-Width="5%" ControlStyle-CssClass="txt_center"
                        HeaderStyle-CssClass="txt_center">
                        <HeaderTemplate>
                            <a href="JavaScript:ManipulateAll('_COUNTRY');" id="_COUNTRY" style="color: #333333">
                                <i id="img_COUNTRY" class="icon-plus-sign-alt"></i></a>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div runat="server" id="anchor">
                                <a href="JavaScript:divexpandcollapse('_COUNTRY<%# Eval("COUNTRYID") %>');" style="color: #333333">
                                    <i id="img_COUNTRY<%# Eval("COUNTRYID") %>" class="icon-plus-sign-alt"></i></a>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Country" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txtCenter">
                        <ItemTemplate>
                            <asp:Label ID="lbl_COUNTRY" ToolTip='<%# Bind("COUNTRYNAME") %>' CssClass="label"
                                runat="server" Text='<%# Bind("COUNTRYNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Score" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txtCenter">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalScore" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actionable" ItemStyle-CssClass="txt_center disp-none"
                        HeaderStyle-CssClass="txtCenter disp-none">
                        <ItemTemplate>
                            <asp:Label ID="lblActionableGood" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actionable" ItemStyle-CssClass="txt_center disp-none"
                        HeaderStyle-CssClass="txtCenter disp-none">
                        <ItemTemplate>
                            <asp:Label ID="lblActionableBad" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actionable" ItemStyle-CssClass="txt_center disp-none"
                        HeaderStyle-CssClass="txtCenter disp-none">
                        <ItemTemplate>
                            <asp:Label ID="lblActionableWorst" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <tr>
                                <td colspan="100%" style="padding: 2px;">
                                    <div style="float: right; font-size: 13px;">
                                    </div>
                                    <div>
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <div id="_COUNTRY<%# Eval("COUNTRYID") %>" style="display: none; position: relative;
                                                    overflow: auto;">
                                                    <asp:GridView ID="gvCountryRiskIndic" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        OnRowDataBound="gvCountryRiskIndic_RowDataBound" CssClass="table table-bordered table-striped">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Risk Indicators" ItemStyle-CssClass="txt_center" ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label Style="width: 150px; text-align: center;" ID="lblRiskIndic" Text='<%# Bind("TileName") %>'
                                                                        runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Result" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label Style="width: 150px; text-align: center;" ID="lblResult" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Score" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label Style="width: 150px; text-align: center;" ID="lblScore" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-CssClass="txt_center" HeaderText="Actionable" ItemStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <asp:Label Style="width: 150px; text-align: center;" ID="lblAction" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Flag" ItemStyle-CssClass="txt_center" ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <i class="fa fa-flag" visible="false" runat="server" id="Igreen" style="font-size: 25px;
                                                                        color: Green"></i><i class="fa fa-flag" visible="false" runat="server" id="Iyellow"
                                                                            style="font-size: 25px; color: #FFBF00"></i><i class="fa fa-flag" visible="false"
                                                                                runat="server" id="Ired" style="font-size: 25px; color: Red"></i>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="Blue" />
                                                        <HeaderStyle ForeColor="Blue" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div class="box box-primary MngTrigger">
            <div class="box-header">
                <h3 class="box-title">
                    Investigator Level Alerts
                </h3>
            </div>
            <asp:GridView ID="gvInvAlerts" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="table table-bordered table-striped" OnRowDataBound="gvInvAlerts_RowDataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="txtCenter" ItemStyle-Width="5%" ControlStyle-CssClass="txt_center"
                        HeaderStyle-CssClass="txt_center">
                        <HeaderTemplate>
                            <a href="JavaScript:ManipulateAll('_INVID');" id="_INVID" style="color: #333333"><i
                                id="img_INVID" class="icon-plus-sign-alt"></i></a>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div runat="server" id="anchor">
                                <a href="JavaScript:divexpandcollapse('_INVID<%# Eval("INVNAME") %>');" style="color: #333333">
                                    <i id="img_INVID<%# Eval("INVID") %>" class="icon-plus-sign-alt"></i></a>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inv. ID" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txtCenter"
                        ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lbl_INVID" ToolTip='<%# Bind("INVNAME") %>' CssClass="label" runat="server"
                                Text='<%# Bind("INVNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Score" ItemStyle-Width="15%" ItemStyle-CssClass="txt_center"
                        HeaderStyle-CssClass="txtCenter">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalScore" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actionable" ItemStyle-CssClass="txt_center" HeaderStyle-CssClass="txtCenter">
                        <ItemTemplate>
                            <asp:Label ID="lblActionable" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <tr>
                                <td colspan="100%" style="padding: 2px;">
                                    <div style="float: right; font-size: 13px;">
                                    </div>
                                    <div>
                                        <div class="rows">
                                            <div class="col-md-12">
                                                <div id="_INVID<%# Eval("INVNAME") %>" style="display: none; position: relative;
                                                    overflow: auto;">
                                                    <asp:GridView ID="gvInvRiskCat" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                        OnRowDataBound="gvInvRiskCat_RowDataBound" CssClass="table table-bordered table-striped">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-CssClass="txtCenter" ItemStyle-Width="5%" ControlStyle-CssClass="txt_center"
                                                                HeaderStyle-CssClass="txt_center">
                                                                <HeaderTemplate>
                                                                    <a href="JavaScript:ManipulateAll('_CATID');" id="_INVID" style="color: #333333"><i
                                                                        id="img_CATID" class="icon-plus-sign-alt"></i></a>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <div runat="server" id="anchor">
                                                                        <a href="JavaScript:divexpandcollapse('_CATID<%# String.Format("{0},{1}", DataBinder.Eval(Container.DataItem, "Category"), DataBinder.Eval(Container.DataItem, "INVID"))%>');"
                                                                            style="color: #333333"><i id="img_CATID<%# String.Format("{0},{1}", DataBinder.Eval(Container.DataItem, "Category"), DataBinder.Eval(Container.DataItem, "INVID"))%>"
                                                                                class="icon-plus-sign-alt"></i></a>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category" ItemStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:Label Style="width: 150px; text-align: center;" ID="lblRiskIndic" Text='<%# Bind("Category") %>'
                                                                        runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td colspan="100%" style="padding: 2px;">
                                                                            <div style="float: right; font-size: 13px;">
                                                                            </div>
                                                                            <div>
                                                                                <div class="rows">
                                                                                    <div class="col-md-12">
                                                                                        <div id="_CATID<%# String.Format("{0},{1}", DataBinder.Eval(Container.DataItem, "Category"), DataBinder.Eval(Container.DataItem, "INVID"))%>"
                                                                                            style="display: none; position: relative; overflow: auto;">
                                                                                            <asp:GridView ID="gvInvRiskIndic" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                OnRowDataBound="gvInvRiskIndic_RowDataBound" CssClass="table table-bordered table-striped">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Risk Indicators" ItemStyle-CssClass="txt_center" ItemStyle-Width="30%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label Style="width: 150px; text-align: center;" ID="lblRiskIndic" Text='<%# Bind("TileName") %>'
                                                                                                                runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Result" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label Style="width: 150px; text-align: center;" ID="lblResult" runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Score" ItemStyle-CssClass="txt_center" ItemStyle-Width="10%">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label Style="width: 150px; text-align: center;" ID="lblScore" runat="server"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Flag" ItemStyle-CssClass="txt_center" ItemStyle-Width="30%">
                                                                                                        <ItemTemplate>
                                                                                                            <i class="fa fa-flag" visible="false" runat="server" id="Igreen" style="font-size: 25px;
                                                                                                                color: Green"></i><i class="fa fa-flag" visible="false" runat="server" id="Iyellow"
                                                                                                                    style="font-size: 25px; color: #FFBF00"></i><i class="fa fa-flag" visible="false"
                                                                                                                        runat="server" id="Ired" style="font-size: 25px; color: Red"></i>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <RowStyle ForeColor="Blue" />
                                                                                                <HeaderStyle ForeColor="Blue" />
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle ForeColor="Maroon" />
                                                        <HeaderStyle ForeColor="Maroon" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
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

        $(function () {

            var $contextMenu = $("#contextMenu");

            $("body").on("contextmenu", "table tr", function (e) {
                $contextMenu.css({
                    display: "block",
                    left: e.pageX,
                    top: e.pageY
                });
                return false;
            });

            $('html').click(function () {
                $contextMenu.hide();
            });

        });
    </script>
    <script type="text/javascript">
        function Manage_Dashboard() {
            var test = "Manage_Dashboard.aspx?Section=Risk Indicators&Type=Chart";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test, '_blank');
            return false;
        }

        function Manage_Tile() {
            var test = "Manage_Dashboard.aspx?Section=Risk Indicators&Type=Tile";
            var strWinProperty = "toolbar=no,menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test, '_blank');
            return false;
        }

    </script>
    <script type="text/javascript">
        $(function bindSpeedoMeter() {

            var a;
            var hfResult = $("input[id*='hfResult']").toArray();
            for (a = 0; a < hfResult.length; ++a) {
                GenerateSpeedoMeter(hfResult[a]);
            }

        });
    </script>
    <script src="js/amcharts/amcharts.js"></script>
    <script src="js/amcharts/serial.js"></script>
    <script src="js/amcharts/pie.js"></script>
    <script src="js/amcharts/themes/light.js"></script>
    <link href="js/amcharts/plugins/export/export.css" rel="stylesheet" />
    <script src="js/amcharts/plugins/export/export.min.js"></script>
    <script src="js/amcharts/gauge.js"></script>
    <script type="text/javascript">


        function GenerateSpeedoMeter() {

            var hfResultID = $(hfResultElement).attr('id');
            var hfLevel1ID = $('#' + hfResultID + '').next().attr('id');
            var hfLevel2ID = $('#' + hfLevel1ID + '').next().attr('id');
            var DivID = $('#' + hfLevel2ID + '').next().attr('id');
            var Good = $('#MainContent_hfGood').attr('value');
            var Bad = $('#MainContent_hfBad').attr('value');
            var Worst = $('#MainContent_hfWorst').attr('value');

            var Level1 = $('#' + hfLevel1ID + '').val();
            var Level2 = $('#' + hfLevel2ID + '').val();
            var Result = $('#' + hfResultID + '').val();

            if (Level1 == '') {
                Level1 = 0;
            }
            if (Level2 == '') {
                Level2 = 0;
            }
            if (Result == '') {
                Result = 0;
            }

            if (parseInt(Level1) > parseInt(Level2)) {

                if (parseInt(Level1) > parseInt(Result)) {
                    var Max = parseInt(Level1) + 20;
                }
                else {
                    var Max = parseInt(Result) + 20;
                }

                var gaugeChart = AmCharts.makeChart(DivID, {
                    "type": "gauge",
                    "axes": [{
                        "startAngle": -90,
                        "endAngle": 90,
                        "radius": "55%",
                        "endValue": Max,
                        "bands": [{
                            "color": "#cc4748",
                            "endValue": Level2,
                            "startValue": 0,
                            "innerRadius": "125%",
                            "radius": "170%",
                            "balloonText": '[0-' + Level2 + '] : ' + Worst + ''
                        }, {
                            "color": "#fdd400",
                            "endValue": Level1,
                            "startValue": Level2,
                            "innerRadius": "125%",
                            "radius": "170%",
                            "balloonText": '[' + Level2 + '-' + Level1 + '] : ' + Bad + ''
                        }, {
                            "color": "#84b761",
                            "endValue": Max,
                            "startValue": Level1,
                            "innerRadius": "125%",
                            "radius": "170%",
                            "balloonText": '[' + Level1 + '-' + Max + '] : ' + Good + ''
                        }]
                    }],
                    "arrows": [{
                        "radius": "150%",
                        "value": Result
                    }],
                    "export": {
                        "enabled": false
                    }
                });

            }
            else {

                if (parseInt(Level2) > parseInt(Result)) {
                    var Max = parseInt(Level2) + 20;
                }
                else {
                    var Max = parseInt(Result) + 20;
                }

                var gaugeChart = AmCharts.makeChart(DivID, {
                    "type": "gauge",
                    "axes": [{
                        "startAngle": -90,
                        "endAngle": 90,
                        "radius": "55%",
                        "endValue": Max,
                        "bands": [{
                            "color": "#84b761",
                            "endValue": Level1,
                            "startValue": 0,
                            "innerRadius": "125%",
                            "radius": "170%",
                            "balloonText": '[0-' + Level1 + '] : ' + Good + ''
                        }, {
                            "color": "#fdd400",
                            "endValue": Level2,
                            "startValue": Level1,
                            "innerRadius": "125%",
                            "radius": "170%",
                            "balloonText": '[' + Level1 + '-' + Level2 + '] : ' + Bad + ''
                        }, {
                            "color": "#cc4748",
                            "endValue": Max,
                            "startValue": Level2,
                            "innerRadius": "125%",
                            "radius": "170%",
                            "balloonText": '[' + Level2 + '-' + Max + '] : ' + Worst + ''
                        }]
                    }],
                    "arrows": [{
                        "radius": "150%",
                        "value": Result,
                        "balloonText": Result
                    }],
                    "export": {
                        "enabled": false
                    }
                });

            }



        }

    </script>
</asp:Content>
