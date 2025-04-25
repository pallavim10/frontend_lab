<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SPONSOR_LISTING_DASHBOARD.aspx.cs" Inherits="CTMS.SPONSOR_LISTING_DASHBOARD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/jscript">
        function ShowListStatus(element, STATUS) {

            var INVID = $(element).closest('tr').find('td:eq(0)').text().trim();
            var LISTID = $("#<%= ddlList.ClientID %>").val();
            var LISTNAME = $("#<%= ddlList.ClientID %> option:selected").text();
            var test = "SPONSOR_LISTING_DATA.aspx?LISTID=" + LISTID + "&LISTNAME=" + LISTNAME + "&INVID=" + INVID + "&STATUS=" + STATUS + ""
            var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
            window.open(test);
            return false;
        }

        function pageLoad() {

            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: false
            });
        }
    </script>
    <script>
        function ShowList(ID, Name) {

            window.location.href = 'SPONSOR_LISTING_DATA.aspx?LISTID=' + ID + '&LISTNAME=' + Name + '';

        }

        function SetStatusTotals() {

            var fields = '';
            var sum = 0;

            fields = document.getElementsByClassName('statusTOTAL');
            for (var i = 0; i < fields.length; ++i) {
                var item = fields[i];
                sum += parseInt(item.innerHTML);
            }
            $('.StatusHeaderTotal').text(sum);

        }
    </script>
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
    <script src="js/amcharts/amcharts.js"></script>
    <script src="js/amcharts/serial.js"></script>
    <script src="js/amcharts/pie.js"></script>
    <script src="js/amcharts/themes/light.js"></script>
    <link href="js/amcharts/plugins/export/export.css" rel="stylesheet" />
    <script src="js/amcharts/plugins/export/export.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row">
        <asp:ListView ID="lstTile" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstTile_ItemDataBound">
            <GroupTemplate>
                <div class="txt_center col-lg-3">
                    <asp:LinkButton ID="itemPlaceholder" runat="server" />
                </div>
            </GroupTemplate>
            <ItemTemplate>
                <div id="divBox" runat="server">
                    <div class="inner">
                        <asp:Label ID="lblCount" onclick='<%# Eval("OnClick") %>' runat="server" Font-Bold="true"
                            Font-Size="XX-Large"></asp:Label><br />
                        <asp:Label ID="lblNAME" onclick='<%# Eval("OnClick") %>' runat="server" Text='<%# Bind("NAME") %>'
                            Font-Size="Small">   
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
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">
                Status Dashboard</h3>
        </div>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <div class="box-body">
            <div class="form-group">
                <div class="form-group has-warning">
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                <table>
                    <tr>
                        <td class="label">
                            Select
                        </td>
                        <td class="requiredSign">
                            <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                        </td>
                        <td class="control">
                            <asp:DropDownList ID="ddlList" Width="100%" runat="server" CssClass="form-control"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <asp:GridView ID="grdAdverseEvent" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                CssClass="table table-bordered Datatable table-striped notranslate" AlternatingRowStyle-CssClass="alt"
                OnPreRender="grd_data_PreRender" PagerStyle-CssClass="pgr">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Site ID" ItemStyle-CssClass="text-center">
                        <HeaderTemplate>
                            <asp:Label ID="lblSITEIDHeader" runat="server" Text="Site Id"></asp:Label><br />
                            <asp:Label ID="lblSITEIDTotal" CssClass="StatusHeaderSiteID" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSITEID" runat="server" Text='<%#Eval("INVID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" ItemStyle-CssClass="text-center">
                        <HeaderTemplate>
                            <asp:Label ID="TOTALHeader" runat="server" Text="Total"></asp:Label><br />
                            <asp:Label ID="TOTALTotal" CssClass="StatusHeaderTotal" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="TOTAL" CssClass="statusTOTAL" Style="color: Black;" runat="server"
                                Text='<%# Eval("TOTAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Open" ItemStyle-CssClass="text-center fontReviews"
                        HeaderStyle-ForeColor="blue">
                        <HeaderTemplate>
                            <asp:Label ID="lblOPENHeader" runat="server" Text="Open"></asp:Label><br />
                            <asp:Label ID="lblOPENTotal" CssClass="StatusHeaderOpen" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblOPEN" CssClass="StatusOpen" Style="color: blue;"
                                Text='<%# Eval("OPEN") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="For Peer-Review" ItemStyle-CssClass="text-center fontReviews"
                        HeaderStyle-ForeColor="green">
                        <HeaderTemplate>
                            <asp:Label ID="lblForPeerReviewHeader" runat="server" Text="For Peer-Review"></asp:Label><br />
                            <asp:Label ID="lblForPeerReviewTotal" CssClass="StatusHeaderForPeerReview" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblForPeerReview" CssClass="StatusForPeerReview"
                                Style="color: green;" Text='<%# Eval("PR") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reviewed" ItemStyle-CssClass="text-center fontReviews"
                        HeaderStyle-ForeColor="green">
                        <HeaderTemplate>
                            <asp:Label ID="lblReviewedHeader" runat="server" Text="Reviewed"></asp:Label><br />
                            <asp:Label ID="lblReviewedTotal" CssClass="StatusHeaderReviewed" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblReviewed" CssClass="StatusReviewed" Style="color: green;"
                                Text='<%# Eval("REVIEWED") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Query And Reviewed" ItemStyle-CssClass="text-center fontReviews"
                        HeaderStyle-ForeColor="green">
                        <HeaderTemplate>
                            <asp:Label ID="lblQueryAndReviewedHeader" runat="server" Text="Query And Reviewed"></asp:Label><br />
                            <asp:Label ID="lblQueryAndReviewedTotal" CssClass="StatusHeaderQueryAndReviewed"
                                runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblQueryAndReviewed" CssClass="StatusQueryAndReviewed"
                                Style="color: green;" Text='<%# Eval("QNR") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reviewed with Peer View" ItemStyle-CssClass="text-center fontReviews"
                        HeaderStyle-ForeColor="green">
                        <HeaderTemplate>
                            <asp:Label ID="lblReviewedwithPeerViewHeader" runat="server" Text="Reviewed with Peer View"></asp:Label><br />
                            <asp:Label ID="lblReviewedwithPeerViewTotal" CssClass="StatusHeaderReviewedwithPeerView"
                                runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblReviewedwithPeerView" CssClass="StatusReviewedwithPeerView"
                                Style="color: green;" Text='<%# Eval("PNR") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reviewed From Another Listings" HeaderStyle-CssClass="disp-none"
                        ItemStyle-CssClass="text-center disp-none">
                        <HeaderTemplate>
                            <asp:Label ID="lblReviewedFromAnotherListingsHeader" runat="server" Text="Reviewed From Another Listings"></asp:Label><br />
                            <asp:Label ID="lblReviewedFromAnotherListingsTotal" CssClass="StatusHeaderReviewedFromAnotherListings"
                                runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblReviewedFromAnotherListings" CssClass="StatusReviewedFromAnotherListings"
                                Style="color: blueviolet;" Text='<%# Eval("RAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Queries" ItemStyle-CssClass="text-center">
                        <HeaderTemplate>
                            <asp:Label ID="lblTotalQueryHeader" runat="server" Text="Total Queries"></asp:Label><br />
                            <asp:Label ID="lblTotalQueryTotal" CssClass="StatusHeaderTotalQuery" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTotalQuery" CssClass="StatusTotalQuery" Style="color: Black;"
                                Text='<%# Eval("TOTAL_QUERY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Open Queries" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor="blue">
                        <HeaderTemplate>
                            <asp:Label ID="lblOpenQueryHeader" runat="server" Text="Open Queries"></asp:Label><br />
                            <asp:Label ID="lblOpenQueryTotal" CssClass="StatusHeaderOpenQuery" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblOpenQuery" CssClass="StatusOpenQuery" Style="color: blue;"
                                Text='<%# Eval("OPEN_QUERY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Closed Queries" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor="cornflowerblue">
                        <HeaderTemplate>
                            <asp:Label ID="lblCloseQueryHeader" runat="server" Text="Closed Queries"></asp:Label><br />
                            <asp:Label ID="lblCloseQueryTotal" CssClass="StatusHeaderCloseQuery" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCloseQuery" CssClass="StatusCloseQuery" Style="color: cornflowerblue;"
                                Text='<%# Eval("CLOSE_QUERY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issues" ItemStyle-CssClass="text-center" HeaderStyle-ForeColor="red">
                        <HeaderTemplate>
                            <asp:Label ID="lblIssueHeader" runat="server" Text="Issues"></asp:Label><br />
                            <asp:Label ID="lblIssueTotal" CssClass="StatusHeaderIssues" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblIssue" CssClass="StatusIssues" Style="color: red;"
                                Text='<%# Eval("ISSUE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unexpected Events" ItemStyle-CssClass="text-center"
                        HeaderStyle-ForeColor="maroon">
                        <HeaderTemplate>
                            <asp:Label ID="lblUNEXPHeader" runat="server" Text="Unexpected Events"></asp:Label><br />
                            <asp:Label ID="lblUNEXPTotal" CssClass="StatusHeaderUNEXP" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblUNEXP" CssClass="StatusUNEXP" Style="color: maroon;"
                                Text='<%# Eval("UNEXP") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <br />
    <div class="row">
        <asp:ListView ID="lstGraph" runat="server" AutoGenerateColumns="false" OnItemDataBound="lstGraph_ItemDataBound">
            <GroupTemplate>
                <div class="col-md-12">
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
    <br />
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
