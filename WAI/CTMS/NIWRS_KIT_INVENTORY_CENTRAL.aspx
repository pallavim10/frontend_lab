<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_KIT_INVENTORY_CENTRAL.aspx.cs" Inherits="CTMS.NIWRS_KIT_INVENTORY_CENTRAL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <%-- <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
     <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript">

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

        function ToggleCheckBox(element) {

            if ($(element).attr('previousValue') == 'true') {

                $(element).attr('checked', false)

            } else {

                $(element).attr('checked', true)

            }
            $(element).attr('previousValue', $(element).attr('checked'));
        }

        function Check_All_Block(element) {

            $('input[type=checkbox][id*=Chek_Block]').each(function () {

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }

        function Check_All_Destroy(element) {

            $('input[type=checkbox][id*=Chek_Destroy]').each(function () {

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }

        function Check_All_Return(element) {

            $('input[type=checkbox][id*=Chek_Return]').each(function () {

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }


        function Check_All_Expire(element) {

            $('input[type=checkbox][id*=Chek_Expire]').each(function () {

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }


        function Check_All_Retention(element) {

            $('input[type=checkbox][id*=Chek_Retention]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }
    </script>
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

        function Check_BlockReasonEntered() {

            if ($('#MainContent_txtBlockComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

        function Check_ExpireReasonEntered() {

            if ($('#MainContent_txtExpireComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

        function Check_DestroyReasonEntered() {

            if ($('#MainContent_txtDestroyComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

        function Check_ReturnReasonEntered() {

            if ($('#MainContent_txtReturnComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

        function Check_RetentionReasonEntered() {

            if ($('#MainContent_txtRetentionComments').val().trim() == '') {
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
            <h3 class="box-title">Kit Inventory</h3>
            <asp:HiddenField ID="hfKITS" runat="server" />
            <asp:Label runat="server" ID="lblHeader" Text="Kit Inventory" Visible="false"></asp:Label>
            <div class="pull-right">
                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Kit Inventory&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                &nbsp;&nbsp;
            </div>
        </div>
        <div class="form-group">
            <div class="has-warning">
                <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
            </div>
            <div class="rows">
                <div class="col-md-12">
                    <label>
                        Export Kit Inventory
                    </label>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvKits" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="table table-bordered Datatable txt_center table-striped notranslate"
                            OnPreRender="grd_data_PreRender">
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
                                <asp:TemplateField Visible="false" HeaderText="Treatment Arm">
                                    <ItemTemplate>
                                        <asp:Label ID="TREAT_GRP_NAME" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="TABLENAME" runat="server" Text='<%# Bind("TABLENAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                    <HeaderTemplate>
                                        <asp:Button ID="Destroy" runat="server" CssClass="btn btn-primary btn-sm"
                                            Text="Destroy" OnClientClick="UpdateSelectedKits(this); return CheckShipmentID(this);" OnClick="btn_Destroy" />
                                        <br />
                                        <asp:CheckBox ID="ChekAll_Destroy" GroupName="All" runat="server" onclick="Check_All_Destroy(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_Destroy"  runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                    <HeaderTemplate>
                                        <asp:Button ID="Return" runat="server" CssClass="btn btn-primary btn-sm"
                                            Text="Return" OnClientClick="UpdateSelectedKits(this); return CheckShipmentID(this);" OnClick="btn_Return" />
                                        <br />
                                        <asp:CheckBox ID="ChekAll_Return" GroupName="All" runat="server" onclick="Check_All_Return(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_Return"  runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                    <HeaderTemplate>
                                        <asp:Button ID="Retention" runat="server" CssClass="btn btn-primary btn-sm"
                                            Text="Retention" OnClientClick="UpdateSelectedKits(this);" OnClick="btn_Retention" />
                                        <br />
                                        <asp:CheckBox ID="ChekAll_Retention" runat="server" onclick="Check_All_Retention(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_Retention" runat="server" onclick="ToggleCheckBox(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                    <HeaderTemplate>
                                        <asp:Button ID="Block" runat="server" CssClass="btn btn-primary btn-sm"
                                            Text="Block" OnClientClick="UpdateSelectedKits(this); return CheckShipmentID(this);" OnClick="btn_Block" />
                                        <br />
                                        <asp:CheckBox ID="ChekAll_Block" runat="server" onclick="Check_All_Block(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_Block" runat="server" onclick="ToggleCheckBox(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                    <HeaderTemplate>
                                        <asp:Button ID="Expire" runat="server" CssClass="btn btn-primary btn-sm"
                                            Text="Expire" OnClientClick="UpdateSelectedKits(this);" OnClick="Expire_Click" />
                                        <br />
                                        <asp:CheckBox ID="ChekAll_Expire" runat="server" onclick="Check_All_Expire(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_Expire" runat="server" onclick="ToggleCheckBox(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
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
                                    <asp:TextBox ID="txtBlockComments" CssClass="form-control-model" MaxLength="200" ValidationGroup="BlockCommnet" TextMode="MultiLine"
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

                <br />
                <asp:ModalPopupExtender ID="ModalPopupExtender7" runat="server" PopupControlID="Panel7" TargetControlID="Button_Expire"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel7" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_Expire" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopupExpire">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label1" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtExpireComments" CssClass="form-control-model" MaxLength="200" ValidationGroup="ExpireCommnet" TextMode="MultiLine"
                                        runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitExpire" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="ExpireCommnet" CssClass="btn btn-DarkGreen"
                                        OnClientClick="return Check_ExpireReasonEntered();" Text="Submit" OnClick="btnSubmitExpire_Click" />
                                    &nbsp; &nbsp;
                            <asp:Button ID="btnCancelExpire" runat="server" Text="Cancel" ValidationGroup="ExpireCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelExpire_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />

                <asp:ModalPopupExtender ID="ModalPopupExtender6" runat="server" PopupControlID="Panel6" TargetControlID="Button_Retention"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel6" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_Retention" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopupRetention">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label8" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtRetentionComments" CssClass="form-control-model" MaxLength="200" ValidationGroup="RetentionCommnet" TextMode="MultiLine"
                                        runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitRetention" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="ReturnCommnet" CssClass="btn btn-DarkGreen"
                                        OnClientClick="return Check_RetentionReasonEntered();" Text="Submit" OnClick="btnSubmitRetention_Click" />
                                    &nbsp;
                                            &nbsp;
                                            <asp:Button ID="btnCancelRetention" runat="server" Text="Cancel" ValidationGroup="RetentionCommnet"
                                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelRetention_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />

                <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="Panel4" TargetControlID="Button_Destroy"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel4" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_Destroy" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopupDestroy">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label2" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDestroyComments" CssClass="form-control-model" MaxLength="200" ValidationGroup="DestroyCommnet" TextMode="MultiLine"
                                        runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitDestroy" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="DestroyCommnet" CssClass="btn btn-DarkGreen"
                                        OnClientClick="return Check_DestroyReasonEntered();" Text="Submit" OnClick="btnSubmitDestroy_Click" />
                                    &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelDestroy" runat="server" Text="Cancel" ValidationGroup="DestroyCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelDestroy_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />

                <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" PopupControlID="Panel5" TargetControlID="Button_Return"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel5" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_Return" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopupReturn">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label3" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtReturnComments" CssClass="form-control-model" MaxLength="200" ValidationGroup="ReturnCommnet" TextMode="MultiLine"
                                        runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitReturn" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="ReturnCommnet" CssClass="btn btn-DarkGreen"
                                        OnClientClick="return Check_ReturnReasonEntered();" Text="Submit" OnClick="btnSubmitReturn_Click" />
                                    &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelReturn" runat="server" Text="Cancel" ValidationGroup="ReturnCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelReturn_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
