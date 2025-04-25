<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RM_Mng_RiskIndicators.aspx.cs" Inherits="CTMS.RM_Mng_RiskIndicators" %>

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
    <script type="text/javascript">

        function SET_RiskIndicator() {

            var URLs = "";
            var NextURLs = "";

            if (window.location.href.includes('User=User')) {
                URLs = 'AjaxFunction.aspx/SET_RiskIndicator_User'
                NextURLs = 'RiskIndicators_User.aspx';
            }
            else {
                URLs = 'AjaxFunction.aspx/SET_RiskIndicator'
                NextURLs = 'RiskIndicators.aspx';
            }

            var divArray = $('#MainContent_divDashboard > div').toArray();
            for (a = 0; a < divArray.length; ++a) {

                var divMain = $(divArray[a]).attr('id');
                var ID = $('#' + divMain).find('input[type=hidden]').attr('value');
                var X = $(divArray[a]).attr('data-gs-x');
                var Y = $(divArray[a]).attr('data-gs-y');
                var Width = $(divArray[a]).attr('data-gs-width');
                var Height = $(divArray[a]).attr('data-gs-height');

                $.ajax({
                    type: "POST",
                    url: URLs,
                    data: '{X:"' + X + '",  Y: "' + Y + '", Width: "' + Width + '" ,Height: "' + Height + '",ID: "' + ID + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    failure: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }
                    },
                    Success: function (response) {
                        if (response.d == 'Object reference not set to an instance of an object.') {
                            alert("Session Expired");
                            var url = "SessionExpired.aspx";
                            $(location).attr('href', url);
                        }
                        else {
                            alert("Contact administrator not suceesfully updated")
                        }
                    }
                });
            }

            alert("Risk Indicators Set Successfully.");
            window.location.href = NextURLs;
        }      
    </script>
    <script type="text/javascript">
        $(function () {
            var options = {
                cellHeight: 120,
                verticalMargin: 1
            };
            $('.grid-stack').gridstack(options);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    Manage Risk Indicators
                </h3>
            </div>
            <div class="box-body">
                <div class="form-group">
                    <div class="form-group has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">
                                Available Risk Indicators
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div>
                                        <div style="width: 100%; height: 264px; overflow: auto;">
                                            <asp:GridView ID="gvAvailableCharts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Risk Indicators Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                            <asp:Label ID="lblChart" runat="server" Text='<%# Bind("TileName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="box-body">
                        <div style="min-height: 300px;">
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnAdd" ForeColor="White" Text="Add" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="lbtnAdd_Click"></asp:LinkButton>
                            </div>
                            <div class="row txtCenter">
                                &nbsp;
                            </div>
                            <div class="row txtCenter">
                                <asp:LinkButton ID="lbtnRemove" ForeColor="White" Text="Remove" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="lbtnRemove_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-primary" style="min-height: 300px;">
                        <div class="box-header with-border" style="float: left;">
                            <h4 class="box-title" align="left">
                                Used Risk Indicators
                            </h4>
                        </div>
                        <div class="box-body">
                            <div align="left" style="margin-left: 5px">
                                <div class="row">
                                    <div style="width: 100%; height: 264px; overflow: auto;">
                                        <div>
                                            <asp:GridView ID="gvAddedCharts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                                                Style="width: 91%; border-collapse: collapse; margin-left: 20px;">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-CssClass="width30px txtCenter">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Chk_Sel_Fun" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Risk Indicators Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                            <asp:Label ID="lblChartName" runat="server" Text='<%# Bind("TileName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-warning">
            <div class="box-header">
                <h3 class="box-title">
                    Adjust Risk Indicators
                </h3>
                <div class="pull-right" style="margin-right: 5%;">
                    <asp:LinkButton ID="lbtnSet" ForeColor="White" Text="Set" runat="server" CssClass="btn btn-primary btn-sm"
                        OnClientClick="SET_RiskIndicator();"></asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="grid-stack" runat="server" id="divDashboard">
                <asp:Repeater runat="server" ID="repeatDashboard" OnItemDataBound="repeatDashboard_ItemDataBound">
                    <ItemTemplate>
                        <div class="grid-stack-item" data-gs-x="0" data-gs-y="0" data-gs-width="4" data-gs-height="1"
                            runat="server" id="divMain">
                            <asp:HiddenField ID="hf_ID" runat="server" Value='<%# Bind("ID") %>' />
                            <div class="grid-stack-item-content" runat="server" id="divContent">
                                <div id="divBox" runat="server">
                                    <div class="inner txt_center">
                                        <div class="col-md-12" style="display: inline-flex;">
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblVal" Text="0" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:Label>&nbsp;
                                            </div>
                                            <div class="col-md-4">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <br />
                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("TileName") %>' Font-Size="Small">
                                        </asp:Label>
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
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
