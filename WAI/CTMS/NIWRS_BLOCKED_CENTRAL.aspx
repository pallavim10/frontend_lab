<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="NIWRS_BLOCKED_CENTRAL.aspx.cs" Inherits="CTMS.NIWRS_BLOCKED_CENTRAL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <%--   <script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
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

        function Check_All_UnQuarantine(element) {

            $('input[type=checkbox][id*=Chek_UnQuarantine]').each(function () {
                // >>this<< refers to specific checkbox

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
    </script>
    <script type="text/javascript">

        function Check_UnQuarantineReasonEntered() {

            if ($('#MainContent_txtUnQuarantineComments').val().trim() == '') {
                $('#MainContent_txtUnQuarantineComments').val('');
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Blocked Kits</h3>
             <asp:HiddenField ID="hfKITS" runat="server" />
            <asp:Label runat="server" ID="lblHeader" Text="Blocked Kits" Visible="false"></asp:Label>
            <div class="pull-right">
                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Blocked Kits&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
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
                        <asp:GridView ID="gvKits" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            OnPreRender="grd_data_PreRender" CssClass="table table-bordered  txt_center table-striped notranslate Datatable">
                            <Columns>
                                <asp:TemplateField HeaderText="Country">
                                    <ItemTemplate>
                                        <asp:Label ID="COUNTRYNAME" runat="server" Text='<%# Bind("COUNTRY") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Blocked At">
                                    <ItemTemplate>
                                        <asp:Label ID="Location" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="AcceptComments" runat="server" Text='<%# Bind("ACCEPTCOMM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Blocked Comment">
                                    <ItemTemplate>
                                        <asp:Label ID="BlockedComment" runat="server" Text='<%# Bind("BLOCKEDCOMM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label>Blocked Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Blocked By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <asp:Label ID="BLOCKEDBYNAME" runat="server" Text='<%# Bind("BLOCKEDBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="BLOCKED_CAL_DAT" runat="server" Text='<%# Bind("BLOCKED_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="BLOCKED_CAL_TZDAT" runat="server" Text='<%# Eval("BLOCKED_CAL_TZDAT") +"  "+ Eval("BLOCKED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
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
                                        <asp:Button ID="Expire" runat="server" CssClass="btn btn-primary btn-sm"
                                            Text="Expire" OnClientClick="UpdateSelectedKits(this);" OnClick="Expire_Click" />
                                        <br />
                                        <asp:CheckBox ID="ChekAll_Expire" runat="server" onclick="Check_All_Expire(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_Expire" runat="server" onclick="ToggleCheckBox(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="TABLENAME" runat="server" Text='<%# Bind("TABLENAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="Button_Popup"
                    BackgroundCssClass="Background">
                </cc1:ModalPopupExtender>
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
                                    &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelUnQuarantine" runat="server" Text="Cancel" ValidationGroup="Commnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelUnQuarantine_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
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
                                        OnClientClick="return Check_ExpireReasonEntered();" Text="Submit" OnClick="btnSubmitExpire_Click"/>
                                    &nbsp; &nbsp;
                            <asp:Button ID="btnCancelExpire" runat="server" Text="Cancel" ValidationGroup="ExpireCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelExpire_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
