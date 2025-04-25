<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NIWRS_RETENTION_CENTRAL.aspx.cs" Inherits="CTMS.NIWRS_RETENTION_CENTRAL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<script type="text/javascript" src="js/MaxLength.min.js"></script>
    <script type="text/javascript" src="js/MaxLength_Limit.js"></script>--%>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <link href="CommonStyles/FixHeaderStyle.css" rel="stylesheet" />
     <script type="text/javascript">
         function pageLoad() {
             $(".Datatable").dataTable({
                 "bSort": true,
                 "ordering": true,
                 "bDestroy": false,
                 stateSave: true
             });

             
             $(".Datatable").parent().parent().addClass('fixTableHead');
         }
     </script>
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

        function Check_All_UNRetention(element) {

            $('input[type=checkbox][id*=Chek_UNRetention]').each(function () {
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
        function Check_RetentionReasonEntered() {

            if ($('#MainContent_txtRetentionComments').val().trim() == '') {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="box box-warning">
        <div class="box-header">
            <h3 class="box-title">Retention Kits</h3>
            <asp:HiddenField ID="hfKITS" runat="server" />
            <asp:Label runat="server" ID="lblHeader" Text="Retention Kits" Visible="false"></asp:Label>
            <div class="pull-right">
                <asp:LinkButton runat="server" ID="btnExport" OnClick="btnExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Retention Kits&nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
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
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvKits" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnPreRender="gvKits_PreRender"
                            CssClass="table table-bordered Datatable txt_center table-striped notranslate">
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
                                        <asp:Label ID="dtEXPIRY" runat="server" Text='<%# Bind("dtEXPIRY") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="TABLENAME" HeaderStyle-CssClass="disp-none" ItemStyle-CssClass="disp-none">
                                    <ItemTemplate>
                                        <asp:Label ID="TABLENAME" runat="server" Text='<%# Bind("TABLENAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Retention At">
                                    <ItemTemplate>
                                        <asp:Label ID="Location" runat="server" Text='<%# Bind("Location") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Retention Comment">
                                    <ItemTemplate>
                                        <asp:Label ID="RetentionComment" runat="server" Text='<%# Bind("RETENTIONCOMM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label>Retention Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Retention By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <div>
                                                <asp:Label ID="RETENTIONBYNAME" runat="server" Text='<%# Bind("RETENTIONBYNAME") %>' ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="RETENTION_CAL_DAT" runat="server" Text='<%# Bind("RETENTION_CAL_DAT") %>' ForeColor="Green"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="RETENTION_CAL_TZDAT" runat="server" Text='<%# Eval("RETENTION_CAL_TZDAT") + "  " + Eval("RETENTION_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-CssClass="width50px">
                                    <HeaderTemplate>
                                        <asp:Button ID="Retenion" runat="server" CssClass="btn btn-primary btn-sm"
                                            Text="Revert to Use" OnClientClick="UpdateSelectedKits(this);" OnClick="Retenion_Click" />
                                        <br />
                                        <asp:CheckBox ID="ChekAll_Retention" runat="server" onclick="Check_All_UNRetention(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_UNRetention" runat="server" />
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
                <cc2:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2" TargetControlID="Button_Retention"
                    BackgroundCssClass="Background">
                </cc2:ModalPopupExtender>
                <asp:Panel ID="Panel2" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_Retention" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopupReject">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label4" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
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
                                    <asp:Button ID="btnSubmitRetention" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="RejectCommnet" CssClass="btn btn-DarkGreen"
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
