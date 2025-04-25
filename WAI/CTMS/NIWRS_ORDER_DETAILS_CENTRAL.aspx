<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_ORDER_DETAILS_CENTRAL.aspx.cs" Inherits="CTMS.NIWRS_ORDER_DETAILS_CENTRAL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/DivExpandCollapse.js"></script>
    <script src="CommonFunctionsJs/IWRS/IWRS_ConfirmMsg.js"></script>
    <%-- <script src="CommonFunctionsJs/Datatable1.js"></script>--%>
    <script type="text/javascript">
        function ShowPrint(element) {

            var ORDERID = $(element).closest('tr').find('td:eq(2)').find('span').text();
            if (ORDERID != "") {
                var test = "NIWRS_ORDER_REPORT.aspx?ORDERID=" + ORDERID;
                var strWinProperty = "menubar=no,location=no,scrollbars=yes,titlebar=no";
                window.open(test, '_blank');
                return false;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Order Detail Form</h3>
            <asp:Label runat="server" ID="lblHeader" Text="Order Detail Form" Visible="false"></asp:Label>
            <div class="pull-right">
                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" style="margin-top:3px;" CssClass="btn btn-info" ForeColor="White">Export Kit Order Detail&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton> &nbsp;&nbsp;
            </div>
        </div>
        <div class="form-group">
            <div class="has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="rows">
                <div style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label width90px">
                            Select Country:
                        </label>
                        <div class="Control">
                            <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control required width200px"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvOrders" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="table table-bordered Datatable txt_center table-striped notranslate" OnPreRender="gvOrders_PreRender" OnRowDataBound="gvOrders_RowDataBound" OnRowCommand="gvOrders_RowCommand">
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
                                        <asp:Label ID="COUNTRYNAME" runat="server" Text='<%# Bind("COUNTRY") %>'></asp:Label>
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
                                                <asp:Label ID="GENERATED_CAL_TZDAT" runat="server" Text='<%# Eval("GENERATED_CAL_TZDAT")+ "  "+Eval("GENERATED_TZVAL") %>' ForeColor="Red"></asp:Label>
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
                                                <asp:Label ID="SHIPPED_CAL_TZDAT" runat="server" Text='<%# Eval("SHIPPED_CAL_TZDAT") + "  " +Eval("SHIPPED_TZVAL")%>' ForeColor="Red"></asp:Label>
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
                                            <asp:Label ID="CANCELED_CAL_TZDAT" Visible="false" runat="server" Text='<%# Eval("CANCELED_CAL_TZDAT")+ "  " +Eval("CANCELED_TZVAL")%>' ForeColor="Red"></asp:Label>
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
                                                        <asp:GridView ID="gvKits" runat="server" CellPadding="3" AutoGenerateColumns="False"
                                                            CssClass="table table-bordered table-striped Datatable table-striped1" OnPreRender="gvOrders_PreRender">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Kit Number">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="KITNO" runat="server" Text='<%# Bind("KITNO") %>' Style="text-align: center"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Lot Number">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LOTNO" runat="server" Text='<%# Bind("LOTNO") %>' Style="text-align: center"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Expiry Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="EXPIRY_DATE" runat="server" Text='<%# Bind("EXPIRY_DATE") %>' Style="text-align: center"></asp:Label>
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
</asp:Content>
