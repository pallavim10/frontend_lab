<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_KITS_SITE_REVERSE_ORDER.aspx.cs" Inherits="CTMS.NIWRS_KITS_SITE_REVERSE_ORDER" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/DivExpandCollapse.js" type="text/javascript"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js" type="text/javascript"></script>
    <%--<link href="CommonStyles/ModalPopup.css" rel="stylesheet" />--%>
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
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

        function ShowPrint(element) {

            var ORDERID = $(element).closest('tr').find('td:eq(3)').find('span').text();
            if (ORDERID != "") {
                var test = "NIWRS_ORDER_REPORT.aspx?ORDERID=" + ORDERID;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
            }
        }

        function CheckShipmentID(element) {

            var str1 = $(element).attr('id');

            if (str1.indexOf('gvKits') != -1) {

                if ($(element).closest('table').closest('tr').prev().find('input').val() == '') {
                    alert('Please Enter Shipment ID for this Order ID');
                    return false;
                }

            }
            else {

                if ($(element).closest('tr').find('input').val() == '') {
                    alert('Please Enter Shipment ID for this Order ID');
                    return false;
                }

            }

        }
    </script>

    <script type="text/javascript">
        function pageLoad() {
            $(".Datatable").parent().parent().addClass('fixTableHead');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Reverse Order</h3>
            <div class="pull-right">
                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Reverse Order&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                &nbsp;&nbsp;
            </div>
        </div>
        <div class="form-group">
            <div class="has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                <asp:Label runat="server" ID="lblHeader" Text="Order Details" Visible="false"></asp:Label>
            </div>
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
            <div style="display: inline-flex">
                <div style="display: inline-flex">
                    <label class="label width60px">
                        Order ID:
                    </label>
                    <div class="Control">
                        <asp:DropDownList runat="server" ID="ddlOrder" CssClass="form-control required width200px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <br />
            <br />

            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvOrders" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped txt_center" OnRowDataBound="gvOrders_RowDataBound" OnRowCommand="gvOrders_RowCommand">
                            <Columns>
                                <asp:TemplateField ItemStyle-CssClass="txtCenter" ControlStyle-CssClass="txt_center"
                                    HeaderStyle-CssClass="txt_center">
                                    <HeaderTemplate>
                                        <a href="JavaScript:ManipulateAll('_ORDER_');" id="_Folder" style="color: #333333"><i
                                            id="img_ORDER_" class="icon-pluss-sign-alt"></i></a>
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
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label>Order Generation Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Generated By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <asp:Label ID="GENERATEBYNAME" runat="server" Text='<%# Bind("GENERATEBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="GENERATED_CAL_DAT" runat="server" Text='<%# Bind("GENERATED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="GENERATED_CAL_TZDAT" runat="server" Text='<%# Eval("GENERATED_CAL_TZDAT")+" "+Eval("GENERATED_TZVAL")  %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label>Order Shipment Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Shipped By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <asp:Label ID="SHIPPEDBYNAME" runat="server" Text='<%# Bind("SHIPPEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="SHIPPED_CAL_DAT" runat="server" Text='<%# Bind("SHIPPED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="SHIPPED_CAL_TZDAT" runat="server" Text='<%# Eval("SHIPPED_CAL_TZDAT")+" "+ Eval("SHIPPED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label>Cancel Order</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Cancelled By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnCancelOrder" runat="server" OnClientClick='<%# string.Format("return ConfirmMsg(\"{0}{1}\");", "Are you sure you want to Cancel this order id : ", Eval("ORDERID")) %>' Visible="false" CommandName="CancelOrder" CssClass="btn btn-default" CommandArgument='<%# Bind("ORDERID") %>'>
                                                 <i class="fa fa-times" style="font-size:13px;color:red"></i>&nbsp;&nbsp;Cancel
                                        </asp:LinkButton>
                                        <div>
                                            <asp:Label ID="CANCELEDBYNAME" Visible="false" runat="server" Text='<%# Bind("CANCELEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="CANCELED_CAL_DAT" runat="server" Visible="false" Text='<%# Bind("CANCELED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="CANCELED_CAL_TZDAT" Visible="false" runat="server" Text='<%# Eval("CANCELED_CAL_TZDAT")+" "+ Eval("CANCELED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Print" ItemStyle-CssClass="width150px">
                                    <ItemTemplate>
                                        <asp:LinkButton OnClientClick="return ShowPrint(this);" CssClass="btn btn-default" runat="server" CommandName="Print"
                                            CommandArgument='<%# Bind("ORDERID") %>' ID="PRINT">
                                                <i class="fa fa-print"></i>&nbsp;&nbsp;Print
                                        </asp:LinkButton>
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
                                                        <asp:GridView ID="gvKits" runat="server" CellPadding="3" AutoGenerateColumns="False" OnPreRender="gvKits_PreRender"
                                                            CssClass="table table-bordered table-striped table-striped1 Datatable">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Kit Number">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="KITNO" runat="server" Text='<%# Bind("KITNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lot Number">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LOTNO" runat="server" Text='<%# Bind("LOTNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Expiry Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="EXPIRY_DATE" runat="server" Text='<%# Bind("EXPIRY_DATE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false" HeaderText="Treatment Arm" ItemStyle-CssClass="width100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="TREAT_GRP" runat="server" Text='<%# Bind("TREAT_GRP") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false" HeaderText="Treatment" ItemStyle-CssClass="width100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="TREAT_GRP_NAME" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
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
        </div>
    </div>
</asp:Content>
