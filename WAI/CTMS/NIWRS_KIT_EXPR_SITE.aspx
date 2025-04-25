<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_KIT_EXPR_SITE.aspx.cs" Inherits="CTMS.NIWRS_KIT_EXPR_SITE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>
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

        function Check_All_UnQuarantine(element) {

            var gvID = $(element).closest('table').attr('id');

            $('#' + gvID + ' input[type=checkbox][id*=Chek_UnQuarantine]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });
        }

        function Check_All_Destroy(element) {

            var gvID = $(element).closest('table').attr('id');

            $('#' + gvID + ' input[type=checkbox][id*=Chek_Destroy]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });
        }

        function Check_All_Return(element) {

            var gvID = $(element).closest('table').attr('id');

            $('#' + gvID + ' input[type=checkbox][id*=Chek_Return]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });
        }

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

        function Check_UnQuarantineReasonEntered() {

            if ($('#MainContent_txtUnQuarantineComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box box-warning">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label runat="server" ID="lblHeader" Text="Expired Kits"></asp:Label></h3>
                    <asp:HiddenField ID="hfKITS" runat="server" />
                    &nbsp&nbsp&nbsp
                    <div class="pull-right">
                        <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Expired Kits&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
                        &nbsp;&nbsp;
                    </div>
                </div>
                <br />
                <div class="form-group">
                    <div class="has-warning">
                        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                    </div>
                    <div class="rows">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvKits" HeaderStyle-CssClass="txt_center" runat="server" AutoGenerateColumns="false"
                                    OnPreRender="grd_data_PreRender" CssClass="table table-bordered Datatable txt_center table-striped notranslate">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Country Name">
                                            <ItemTemplate>
                                                <asp:Label ID="COUNTRY" runat="server" Text='<%# Bind("COUNTRY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site  ID">
                                            <ItemTemplate>
                                                <asp:Label ID="SITEID" runat="server" Text='<%# Bind("SITEID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order ID">
                                            <ItemTemplate>
                                                <asp:Label ID="ORDERID" runat="server" Text='<%# Bind("ORDERID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shipment ID">
                                            <ItemTemplate>
                                                <asp:Label ID="SHIPMENTID" runat="server" Text='<%# Bind("SHIPMENTID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                        <asp:TemplateField HeaderText="Treatment Code">
                                            <ItemTemplate>
                                                <asp:Label ID="TREAT_GRP" runat="server" Text='<%# Bind("TREAT_GRP") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Treatment Arms">
                                            <ItemTemplate>
                                                <asp:Label ID="TREAT_GRP_NAME" runat="server" Text='<%# Bind("TREAT_GRP_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expiry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="EXPIRY_DATE" runat="server" Text='<%# Bind("EXPIRY_DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expired Comment">
                                            <ItemTemplate>
                                                <asp:Label ID="EXPIRECOMM" runat="server" Text='<%# Bind("EXPIRECOMM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expired At">
                                            <ItemTemplate>
                                                <asp:Label ID="Location" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <label>Expired Details</label><br />
                                                <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Expired By]</label><br />
                                                <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                                <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div>
                                                    <div>
                                                        <asp:Label ID="EXPIREBYNAME" runat="server" Text='<%# Bind("EXPIREBYNAME") %>' ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="EXPIRE_CAL_DAT" runat="server" Text='<%# Bind("EXPIRE_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="EXPIRE_CAL_TZDAT" runat="server" Text='<%# Eval("EXPIRE_CAL_TZDAT") + " "+ Eval("EXPIRE_TZVAL") %>' ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                            <ItemTemplate>
                                                <asp:Label ID="TABLENAME" runat="server" Text='<%# Bind("TABLENAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                            <HeaderTemplate>
                                                <asp:Button ID="UnQuarantine" runat="server" CssClass="btn btn-primary btn-sm"
                                                    Text="Revert to Use" OnClientClick="UpdateSelectedKits(this);" OnClick="btn_UnQuarantine" />
                                                <br />
                                                <asp:CheckBox ID="ChekAll_Quarantine" runat="server" onclick="Check_All_UnQuarantine(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Chek_UnQuarantine" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                            <HeaderTemplate>
                                                <asp:Button ID="Destroy" runat="server" CssClass="btn btn-primary btn-sm"
                                                    Text="Destroy" OnClientClick="UpdateSelectedKits(this);" OnClick="btn_Destroy" />
                                                <br />
                                                <asp:CheckBox ID="ChekAll_Destroy" GroupName="All" runat="server" onclick="Check_All_Destroy(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Chek_Destroy" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                            <HeaderTemplate>
                                                <asp:Button ID="Return" runat="server" CssClass="btn btn-primary btn-sm"
                                                    Text="Return" OnClientClick="UpdateSelectedKits(this);" OnClick="btn_Return" />
                                                <br />
                                                <asp:CheckBox ID="ChekAll_Return" GroupName="All" runat="server" onclick="Check_All_Return(this)" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Chek_Return" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
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
                                            &nbsp;&nbsp;
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
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnCancelReturn" runat="server" Text="Cancel" ValidationGroup="ReturnCommnet"
                                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelReturn_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
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
                                            <asp:TextBox ID="txtUnQuarantineComments" CssClass="form-control-model" MaxLength="200" TextMode="MultiLine"
                                                runat="server" Style="width: 250px;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                            &nbsp;
                                        </div>
                                        <div class="col-md-9">
                                            <asp:Button ID="btnSubmitUnQuarantine" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="Commnet" CssClass="btn btn-DarkGreen"
                                                OnClientClick="return Check_UnQuarantineReasonEntered();" Text="Submit" OnClick="btnSubmitUnQuarantine_Click" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnCancelUnQuarantine" runat="server" Text="Cancel" ValidationGroup="Commnet"
                                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelUnQuarantine_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
