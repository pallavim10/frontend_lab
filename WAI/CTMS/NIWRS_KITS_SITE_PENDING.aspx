<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_KITS_SITE_PENDING.aspx.cs" Inherits="CTMS.NIWRS_KITS_SITE_PENDING" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <%-- <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/DivExpandCollapse.js" type="text/javascript"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
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

        function CheckShipmentDetails(element) {

            if ($(element).closest('tr').find('td:eq(5)').find('input').val() == '') {
                alert('Please Enter Shipment ID');
                return false;
            }
            else if ($(element).closest('tr').find('td:eq(6)').find('input').val() == '') {
                alert('Please Enter Date for this Order ID');
                return false;
            }
        }

        function UpdateSelectedKits(element) {

            var btnIndex = $(element).closest('th').index();
            var kitIndex = $(element).closest('tr').find('th:contains(Kit)').index();

            var selectedKitNos = "";

            var table = $(element).closest('table').DataTable();

            table.rows().every(function () {
                var $row = $(this.node());
                var checkbox = $($row.find('td')[btnIndex]).find('input');
                var KitNumber = $($row.find('td')[kitIndex]).find('span').text();

                if (checkbox.length > 0 && KitNumber.length > 0 && checkbox.prop('checked')) {
                    selectedKitNos += "," + KitNumber;
                }
            });

            $('#<%= hfKITS.ClientID %>').val(selectedKitNos);

        }

        function Check_All_Quarantine(element) {

            var gvID = $(element).closest('table').attr('id');

            $('#' + gvID + ' input[type=checkbox][id*=Chek_Quarantine]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }

        function Check_All_Block(element) {

            var gvID = $(element).closest('table').attr('id');

            $('#' + gvID + ' input[type=checkbox][id*=Chek_Block]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }

        function Check_All_Damage(element) {

            var gvID = $(element).closest('table').attr('id');

            $('#' + gvID + ' input[type=checkbox][id*=Chek_Damage]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }

        function Check_QuarantineReasonEntered() {

            if ($('#MainContent_txtQuarantineComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

        function Check_BlockReasonEntered() {

            if ($('#MainContent_txtBlockComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

        function Check_DamageReasonEntered() {

            if ($('#MainContent_txtDamageComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Pending Shipments</h3>
            <asp:HiddenField ID="hfKITS" runat="server" />
            <asp:Label runat="server" ID="lblHeader" Text="Pending Shipments" Visible="false"></asp:Label>
            <div class="pull-right">
                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Pending Shipments&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
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
                            CssClass="table table-bordered table-striped txt_center" OnRowDataBound="gvOrders_RowDataBound"
                            OnRowCommand="gvOrders_RowCommand">
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
                                <asp:TemplateField HeaderText="Country/Site ID">
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
                                <asp:TemplateField HeaderText="Shipment ID" ItemStyle-CssClass="width250pximp">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SHIPMENTID" runat="server" Width="100%" CssClass="txtCenter form-control"
                                            Text='<%# Bind("SHIPMENTID") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shipment Date" ItemStyle-CssClass="width250pximp">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SHIPMENTDATE" runat="server" Width="100%" CssClass="txtCenter txtDate form-control"
                                            Text='<%# Bind("SHIPMENTDATE") %>'></asp:TextBox>
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
                                                <asp:Label ID="GENERATED_CAL_TZDAT" runat="server" Text='<%# Eval("GENERATED_CAL_TZDAT") + " "+ Eval("GENERATED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ship Order" ItemStyle-CssClass="width150px">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CommandName="Accept" CssClass="btn btn-default" CommandArgument='<%# Bind("ORDERID") %>'
                                            OnClientClick="return CheckShipmentDetails(this);" ID="ACCEPT">
                                                <i class="fa fa-truck"></i>&nbsp;&nbsp;Ship
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="TABLENAME" runat="server" Text='<%# Bind("TABLENAME") %>'></asp:Label>
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
                                                                <asp:TemplateField Visible="false" HeaderText="Treatment" ItemStyle-CssClass="width100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="TREAT_GRP_NAME" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
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
                                                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                                                    <HeaderTemplate>
                                                                        <asp:Button ID="Quarantine" runat="server" CssClass="btn btn-primary btn-sm"
                                                                            Text="Quarantine" OnClientClick="UpdateSelectedKits(this);" OnClick="btn_Quarantine" /><br />
                                                                        <asp:CheckBox ID="ChekAll_Quarantine" runat="server" onclick="Check_All_Quarantine(this)" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="Chek_Quarantine" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                                                    <HeaderTemplate>
                                                                        <asp:Button ID="Block" runat="server" CssClass="btn btn-primary btn-sm"
                                                                            Text="Block" OnClientClick="UpdateSelectedKits(this);" OnClick="btn_Block" /><br />
                                                                        <asp:CheckBox ID="ChekAll_Block" runat="server" onclick="Check_All_Block(this)" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="Chek_Block" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                                                    <HeaderTemplate>
                                                                        <asp:Button ID="Damage" runat="server" CssClass="btn btn-primary btn-sm"
                                                                            Text="Damage" OnClientClick="UpdateSelectedKits(this);" OnClick="btn_Damage" /><br />
                                                                        <asp:CheckBox ID="ChekAll_Damage" runat="server" onclick="Check_All_Damage(this)" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="Chek_Damage" runat="server" />
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
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="Button_Popup"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel1" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_Popup" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="disp-none">
                        <asp:Label ID="Label7" runat="server" Text="Table Name"></asp:Label>
                        <asp:TextBox ID="txt_TableName" runat="server"></asp:TextBox>
                        <asp:Label ID="Label6" runat="server" Text="Cont Id"></asp:Label>
                        <asp:TextBox ID="txt_ContId" runat="server"></asp:TextBox>
                        <asp:Label ID="Label5" runat="server" Text="Variable Name"></asp:Label>
                        <asp:TextBox ID="txt_VariableName" runat="server"></asp:TextBox>
                    </div>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopup">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label11" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtQuarantineComments" MaxLength="200" CssClass="form-control-model" TextMode="MultiLine"
                                        runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitQuarantine" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="Commnet" CssClass="btn btn-DarkGreen"
                                        OnClientClick="return Check_QuarantineReasonEntered();" Text="Submit" OnClick="btnSubmitQuarantine_Click" />
                                    &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelQuarantine" runat="server" Text="Cancel" ValidationGroup="Commnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelQuarantine_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2" TargetControlID="Button_Block"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel2" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_Block" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopupBlock">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label4" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtBlockComments" MaxLength="200" CssClass="form-control-model" ValidationGroup="BlockCommnet" TextMode="MultiLine"
                                        runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitBlock" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="BlockCommnet" CssClass="btn btn-DarkGreen"
                                        OnClientClick="return Check_BlockReasonEntered();" Text="Submit" OnClick="btnSubmitBlock_Click" />
                                    &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelBlock" runat="server" Text="Cancel" ValidationGroup="BlockCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelBlock_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="Panel3" TargetControlID="Button_Damage"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel3" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_Damage" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopupDamage">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label1" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDamageComments" MaxLength="200" CssClass="form-control-model" ValidationGroup="DamageCommnet" TextMode="MultiLine"
                                        runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitDamage" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="DamageCommnet" CssClass="btn btn-DarkGreen"
                                        OnClientClick="return Check_DamageReasonEntered();" Text="Submit" OnClick="btnSubmitDamage_Click" />
                                    &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelDamage" runat="server" Text="Cancel" ValidationGroup="DamageCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelDamage_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

