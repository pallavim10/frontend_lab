<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_KITS_SITE_SHIPED.aspx.cs" Inherits="CTMS.NIWRS_KITS_SITE_SHIPED" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").dataTable({
                "bSort": true,
                "ordering": true,
                "bDestroy": false,
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                stateSave: true
            });

            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
    <script type="text/javascript">

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">Shipped Orders</h3>
                    <asp:Label runat="server" ID="lblHeader" Text="Shipped Orders" Visible="false"></asp:Label>
                    <div class="pull-right">
                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Shipped Details&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        &nbsp;&nbsp;
                    </div>
                </div>
                <div class="form-group">
                    <div class="has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="rows">
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width60px">
                                    Country:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control required width200px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div style="display: inline-flex">
                            <div style="display: inline-flex">
                                <label class="label width60px">
                                    Site ID:
                                </label>
                                <div class="Control">
                                    <asp:DropDownList runat="server" ID="ddlSite" CssClass="form-control required width200px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvOrders" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-striped txt_center" OnRowDataBound="gvOrders_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                            HeaderStyle-CssClass="txt_center">
                                            <HeaderTemplate>
                                                <a href="JavaScript:ManipulateAll('_ORDER_');" id="_Folder" style="color: #333333"><i
                                                    id="img_ORDER_" class="icon-plus-sign-alt"></i></a>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div runat="server" id="anchor">
                                                    <a href="JavaScript:divexpandcollapse('_ORDER_<%# Eval("ORDERID") %>');" style="color: #333333">
                                                        <i id="img_ORDER_<%# Eval("ORDERID") %>" class="icon-plus-sign-alt"></i></a>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="COUNTRYNAME" runat="server" Text='<%# Bind("COUNTRYNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site ID">
                                            <ItemTemplate>
                                                <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order ID">
                                            <ItemTemplate>
                                                <asp:Label ID="ORDERID" runat="server" Text='<%# Bind("ORDERID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Kits">
                                            <ItemTemplate>
                                                <asp:Label ID="TOTALKITS" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Generated By">
                                            <ItemTemplate>
                                                <asp:Label ID="GENERATEBY" runat="server" Text='<%# Bind("GENERATEBY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Generated On">
                                            <ItemTemplate>
                                                <asp:Label ID="GENERATEDT" runat="server" Text='<%# Bind("GENERATEDT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shipment Id">
                                            <ItemTemplate>
                                                <asp:Label ID="SHIPMENTID" runat="server" Text='<%# Bind("SHIPMENTID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shipment On">
                                            <ItemTemplate>
                                                <asp:Label ID="SHIPMENTDATE" runat="server" Text='<%# Bind("SHIPMENTDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" style="padding: 2px; padding-left: 4%;">
                                                        <div style="float: right; font-size: 13px;">
                                                        </div>
                                                        <div>
                                                            <div id="_ORDER_<%# Eval("ORDERID") %>" style="display: none; position: relative; overflow: auto;">
                                                                <asp:GridView ID="gvKits" runat="server" CellPadding="3" Width="100%" AutoGenerateColumns="False" OnPreRender="gvKits_PreRender"
                                                                    CssClass="table table-bordered table-striped table-striped1 Datatable">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Kit Number" ItemStyle-CssClass="width100px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="KITNO" runat="server" Text='<%# Bind("KITNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Lot Number" ItemStyle-CssClass="width100px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="LOTNO" runat="server" Text='<%# Bind("LOTNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Expiry Date" ItemStyle-CssClass="width100px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="dtExpire" runat="server" Text='<%# Bind("dtExpire") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
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
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
