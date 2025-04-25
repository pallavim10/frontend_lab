<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ETMF_HomePage.aspx.cs" Inherits="CTMS.ETMF_HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    AutoPostBack="true" OnSelectedIndexChanged="drpSites_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <br />
        <div class="box box-primary" runat="server" id="divGroupMetrics" visible="false">
            <div class="box-header">
                <h3 class="box-title">Event / Milesstone Metrics
                </h3>
            </div>
            <div class="row">
                <div class="col-md-12" style="display: inline-flex;">
                    <div class="col-md-4 label" style="display: inline-flex;">
                        View By : &nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlViewby" Width="250px" runat="server" class="form-control drpControl required" AutoPostBack="true" OnSelectedIndexChanged="ddlViewby_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 label" style="display: inline-flex;">
                        <asp:Label ID="lblViewType" runat="server" Text="Select :"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="drpDocType" Width="250px" runat="server" class="form-control drpControl required">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <asp:Button runat="server" ID="btnShow" Text="Show Data" CssClass="btn btn-info cls-btnSave" OnClick="btnShow_Click" />
                    </div>
                </div>
                <br />
                <br />
                <div class="col-md-12">
                    <asp:ListView ID="lstGroups" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                        <GroupTemplate>
                            <div class="txt_center col-md-3">
                                <asp:LinkButton ID="itemPlaceholder" runat="server" />
                            </div>
                        </GroupTemplate>
                        <ItemTemplate>
                            <div id="divBox" runat="server">
                                <div class="inner">
                                    <asp:Label ID="lblTotal" Text='<%# Bind("Total") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Text="/" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                    <asp:Label ID="lblExpected" Text='<%# Bind("Expected") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblZone" runat="server" Text='<%# Bind("ZONE") %>' Font-Size="Small">   
                                    </asp:Label>
                                    <asp:HiddenField ID="hfZoneID" Value='<%# Bind("ZoneId") %>' runat="server" />
                                </div>
                                <div class="icon">
                                    <i class="ion ion-stats-bars"></i>
                                </div>
                                <a href="#" class="small-box-footer"></a>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <br />
            </div>
        </div>
        <br />
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Overall Metrics
                </h3>
            </div>
            <div class="row" id="divtiles" runat="server">
                <div class="col-md-12">
                    <div class="txt_center col-md-3">
                        <div id="div3" runat="server" style="z-index: 0;" class="small-box bg-aqua">
                            <div class="inner">
                                <asp:Label ID="lblTotalUploadCount" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblTotalUpload" runat="server" Text="Total Uploaded" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                    <div class="txt_center col-md-3">
                        <div id="div4" runat="server" style="z-index: 0;" class="small-box bg-blue">
                            <div class="inner">
                                <asp:Label ID="lblCurrentCount" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblCurrent" runat="server" Text="Total Current" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                    <div class="txt_center col-md-3">
                        <div id="div5" runat="server" style="z-index: 0;" class="small-box bg-light-blue">
                            <div class="inner">
                                <asp:Label ID="lblOldCount" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblOld" runat="server" Text="Total Superseded / Replaced" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                    <div class="txt_center col-md-3">
                        <div id="div2" runat="server" style="z-index: 0;" class="small-box bg-light-blue">
                            <div class="inner">
                                <asp:Label ID="lblPendingQCCount" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblPendingQC" runat="server" Text="Total Pending for QC" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Upload Metrics
                </h3>
            </div>
            <div class="row" id="div6" runat="server">
                <div class="col-md-12">
                    <div class="txt_center col-md-3">
                        <div id="div10" runat="server" style="z-index: 0;" class="small-box bg-teal">
                            <div class="inner">
                                <asp:Label ID="lblUpload1MonthCount" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblUpload1Month" runat="server" Text="Uploaded in Last 1 Month" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                    <div class="txt_center col-md-3">
                        <div id="div7" runat="server" style="z-index: 0;"
                            class="small-box bg-CORNFLOWERBLUE">
                            <div class="inner">
                                <asp:Label ID="lblUpload15Count" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblUpload15" runat="server" Text="Uploaded in Last 15 Days" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                    <div class="txt_center col-md-3">
                        <div id="div8" runat="server" style="z-index: 0;"
                            class="small-box bg-DARKCYAN">
                            <div class="inner">
                                <asp:Label ID="lblUpload7Count" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblUpload7" runat="server" Text="Uploaded in Last 7 Days" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                    <div class="txt_center col-md-3">
                        <div id="div9" runat="server" style="z-index: 0;"
                            class="small-box bg-CORNFLOWERBLUE">
                            <div class="inner">
                                <asp:Label ID="lblUpload24Count" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblUpload24" runat="server" Text="Uploaded in Last 24 Hours" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Expiry Metrics
                </h3>
            </div>
            <div class="row" id="div11" runat="server">
                <div class="col-md-12">
                    <div class="txt_center col-md-3">
                        <div id="div12" runat="server" style="z-index: 0;" class="small-box bg-teal">
                            <div class="inner">
                                <asp:Label ID="lblExp3MonthCount" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblExp3Month" runat="server" Text="Expiring in 3 Months" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                    <div class="txt_center col-md-3">
                        <div id="div13" runat="server" style="z-index: 0;"
                            class="small-box bg-CORNFLOWERBLUE">
                            <div class="inner">
                                <asp:Label ID="lblExp1MonthCount" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblExp1Month" runat="server" Text="Expiring in 1 Month" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                    <div class="txt_center col-md-3">
                        <div id="div14" runat="server" style="z-index: 0;"
                            class="small-box bg-DARKCYAN">
                            <div class="inner">
                                <asp:Label ID="lblExp15DaysCount" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblExp15Days" runat="server" Text="Expiring in 15 Days" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                    <div class="txt_center col-md-3">
                        <div id="div15" runat="server" style="z-index: 0;"
                            class="small-box bg-CORNFLOWERBLUE">
                            <div class="inner">
                                <asp:Label ID="lblExp7DaysCount" runat="server" Text="0" Style="font-size: XX-Large; font-weight: bold;"></asp:Label>&nbsp;
                            <br />
                                <asp:Label ID="lblExp7Days" runat="server" Text="Expiring in 7 Days" Font-Size="Small">
                                </asp:Label>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="#" class="small-box-footer"></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Zone wise QC Metrics (Pending/Total)
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
                                    <asp:Label ID="lblCountQCPending" Text='<%# Bind("QCPending") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                    <asp:Label ID="Label3" runat="server" Text="/" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                    <asp:Label ID="lblCountCurrent" Text='<%# Bind("Current") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblZone" runat="server" Text='<%# Bind("ZONE") %>' Font-Size="Small">   
                                    </asp:Label>
                                    <asp:HiddenField ID="hfZoneID" Value='<%# Bind("ZoneId") %>' runat="server" />
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
    </div>
</asp:Content>
