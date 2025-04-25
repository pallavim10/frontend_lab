<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CodingDashboard.aspx.cs" Inherits="CTMS.CodingDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/bootstrap.js" type="text/javascript"></script>
    <link rel="stylesheet" href="js/GridStack/gridstack.css" />
    <link rel="stylesheet" href="js/GridStack/gridstack-extra.css" />
    <script src="js/GridStack/gridstack.js"></script>
    <script src="js/GridStack/gridstack.jQueryUI.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="anish">
        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">No. of Terms Available for Coding by Modules
                </h3>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="lstUncoded" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                        <GroupTemplate>
                            <div class="txt_center col-md-3">
                                <asp:LinkButton ID="itemPlaceholder" runat="server" />
                            </div>
                        </GroupTemplate>
                        <ItemTemplate>
                            <div id="divBox" runat="server">
                                <div class="inner">
                                    <asp:Label ID="lblCount" Text='<%# Bind("COUNTS") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblLIB" runat="server" Text='<%# Bind("MODULENAME") %>' Font-Size="Small">   
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
        <div class="row">
            <div class="col-md-3">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">No. of Codes Available for Re-Coding by Libraries
                        </h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12 txt_center">
                            <asp:ListView ID="lstRecoding" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                                <GroupTemplate>
                                    <div class="col-md-12 txtCenter">
                                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                    </div>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <div id="divBox" runat="server">
                                        <div class="inner">
                                            <asp:Label ID="lblCount" Text='<%# Bind("COUNTS") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblLIB" runat="server" Text='<%# Bind("LIBRARIES") %>' Font-Size="Small">   
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
            </div>
            <div class="col-md-3">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">No. of Terms Available for Approval by Libraries
                        </h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12 txtCenter">
                            <asp:ListView ID="lstForApproval" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                                <GroupTemplate>
                                    <div class="col-md-12 txtCenter">
                                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                    </div>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <div id="divBox" runat="server">
                                        <div class="inner">
                                            <asp:Label ID="lblCount" Text='<%# Bind("COUNTS") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblLIB" runat="server" Text='<%# Bind("LIBRARIES") %>' Font-Size="Small">   
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
            </div>
            <div class="col-md-3">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">No. of Approved Codes by Libraries
                        </h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12 txtCenter">
                            <asp:ListView ID="lstApproved" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                                <GroupTemplate>
                                    <div class="col-md-12 txtCenter">
                                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                    </div>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <div id="divBox" runat="server">
                                        <div class="inner">
                                            <asp:Label ID="lblCount" Text='<%# Bind("COUNTS") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblLIB" runat="server" Text='<%# Bind("LIBRARIES") %>' Font-Size="Small">   
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
            </div>
            <div class="col-md-3">
                <div class="box box-primary">
                    <div class="box-header">
                        <h3 class="box-title">No. of Disapproved Codes by Libraries
                        </h3>
                    </div>
                    <div class="row">
                        <div class="col-md-12 txtCenter">
                            <asp:ListView ID="lstDisapproved" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
                                <GroupTemplate>
                                    <div class="col-md-12 txtCenter">
                                        <asp:LinkButton ID="itemPlaceholder" runat="server" />
                                    </div>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <div id="divBox" runat="server">
                                        <div class="inner">
                                            <asp:Label ID="lblCount" Text='<%# Bind("COUNTS") %>' runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblLIB" runat="server" Text='<%# Bind("LIBRARIES") %>' Font-Size="Small">   
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
            </div>
        </div>
</asp:Content>
