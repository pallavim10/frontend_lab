<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DesignBench_HomePage.aspx.cs" Inherits="CTMS.DesignBench_HomePage" %>

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
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript">
        function Check_Version() {
            if ($('#MainContent_txtVesion').val().trim() == '') {
                alert('Please enter DB  Version');
                return false;
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="anish">
        <%--  <script type="text/javascript" src="Scripts/amcharts.js"></script>
        <script src="https://www.amcharts.com/lib/3/serial.js"></script>
        <script src="https://www.amcharts.com/lib/3/pie.js"></script>
        <link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css"
            type="text/css" media="all" />
        <script src="https://www.amcharts.com/lib/3/themes/light.js"></script>--%>
        <script src="js/amcharts/amcharts.js"></script>
        <script src="js/amcharts/serial.js"></script>
        <script src="js/amcharts/pie.js"></script>
        <script src="js/amcharts/themes/light.js"></script>
        <link href="js/amcharts/plugins/export/export.css" rel="stylesheet" />
        <script src="js/amcharts/plugins/export/export.min.js"></script>
        <div class="lblError">
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </div>
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">System-wise module count
                </h3>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="lstTile_MODULES_COUNT" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_MODULES_COUNT_ItemDataBound">
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
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Modules Details count
                </h3>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="lst_BU_TILES_COUNT" runat="server" AutoGenerateColumns="false" OnItemDataBound="lst_BU_TILES_COUNT_ItemDataBound">
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
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Field Details count
                </h3>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="lstFieldDetails" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstFieldDetails_COUNT_ItemDataBound">
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
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Module Wise Listing count
                </h3>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="lstModuleListing" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstModuleListing_ItemDataBound">
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
        <div class="box box-primary">
            <div class="box-header">
                <h3 class="box-title">Rule Details
                </h3>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="lst_Rules" runat="server" AutoGenerateColumns="false" OnItemDataBound="lst_Rules_ItemDataBound">
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

            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel5" TargetControlID="Button_STATUS"
                BackgroundCssClass="Background">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel5" runat="server" Style="display: none;" CssClass="Popup1">
                <asp:Button runat="server" ID="Button_STATUS" Style="display: none" />
                <h5 class="heading">Define Version</h5>
                <div class="modal-body" runat="server">
                    <div id="ModelPopupReturn">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <asp:Label ID="Label3" runat="server" Text="Entered DB Version Number :" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtVesion" CssClass="form-control-model align-center" ValidationGroup="VISION" runat="server" Style="width: 130px;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnSubmit" runat="server" Style="height: 34px; width: 71px; font-size: 14px;" ValidationGroup="VISION" CssClass="btn btn-DarkGreen"
                                        OnClientClick="return Check_Version();" Text="Submit" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
</asp:Content>
