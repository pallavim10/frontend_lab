<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SafetyDashboard.aspx.cs" Inherits="CTMS.SafetyDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="js/amcharts/amcharts.js"></script>
    <script src="js/amcharts/serial.js"></script>
    <script src="js/amcharts/pie.js"></script>
    <script src="js/amcharts/themes/light.js"></script>
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
    <br />
    <br />
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">SAE Details
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
                                <asp:Label ID="Count" Text='<%# Bind("Count") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                <br />
                                <asp:Label ID="TILE" runat="server" Text='<%# Bind("TILE") %>' Font-Size="Small">   
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
    </div>
    <br />
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">SAEs Pending Reporting
            </h3>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:ListView ID="lstListingTile_Pending" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstListingTile_Pending_ItemDataBound">
                    <GroupTemplate>
                        <div class="txt_center col-lg-3">
                            <asp:LinkButton ID="itemPlaceholder" runat="server" />
                        </div>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div id="divBox" runat="server" style="margin-top: 20px;">
                            <div class="inner">
                                <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label><br />
                                <asp:Label ID="lblNAME" runat="server" Text='<%# Bind("NAME") %>' Font-Size="Small">   
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
    </div>
    <br />
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">SAEs serious
            </h3>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:ListView ID="lstListingTile_Total" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstListingTile_Total_ItemDataBound">
                    <GroupTemplate>
                        <div class="txt_center col-lg-3">
                            <asp:LinkButton ID="itemPlaceholder" runat="server" />
                        </div>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div id="divBox" runat="server" style="margin-top: 20px;">
                            <div class="inner">
                                <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label><br />
                                <asp:Label ID="lblNAME" runat="server" Text='<%# Bind("NAME") %>' Font-Size="Small">   
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
    </div>
    <br />
</asp:Content>
