<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IWRS_MNG_RANDNO.aspx.cs" Inherits="CTMS.IWRS_MNG_RANDNO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <script src="CommonFunctionsJs/Datatable1.js"></script>
    <link href="CommonStyles/ModalPopup.css" rel="stylesheet" />
    <script type="text/javascript">
        function pageload() {

            $('input[type="checkbox"]').click(function () {
                if ($(this).is(':checked')) {
                    $(this).prop("checked", false);
                } else {
                    $(this).prop("checked", true);
                }
            });

        }

        function Check_All_Block(element) {

            $('input[type=checkbox][id*=Chek_Block]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }

            });

        }

        function Check_BlockReasonEntered() {

            if ($('#MainContent_txtBlockComments').val().trim() == '') {
                alert('Please enter Comments');
                return false;
            }
        };

        function Check_All_UnBlock(element) {

            $('input[type=checkbox][id*=Chek_UnBlock]').each(function () {
                // >>this<< refers to specific checkbox

                if ($(element).prop('checked') == true) {
                    $(this).prop('checked', true);
                }
                else {
                    $(this).prop('checked', false);
                }
            });
        }

        function Check_UnBlockReasonEntered() {

            if ($('#MainContent_txtUnBlockComments').val().trim() == '') {
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
            <h3 class="box-title">Manage Randomization Numbers</h3>
            <asp:Label runat="server" ID="lblHeader" Text="Manage Randomization Numbers" Visible="false"></asp:Label>
            <div class="pull-right">
                <asp:LinkButton ID="lbnMNGRANDMExport" runat="server" OnClick="lbnMNGRANDMExport_Click" Font-Size="12px" Style="margin-top: 3px;" CssClass="btn btn-info" ForeColor="White">Export Randomization &nbsp;<span class="glyphicon glyphicon-download 2x"></span></asp:LinkButton>
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
                        <label class="label ">
                            From Block:
                        </label>
                        <div class="Control">
                            <asp:DropDownList runat="server" ID="ddlFromBlock" CssClass="form-control required width200px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="display: inline-flex">
                    <div style="display: inline-flex">
                        <label class="label ">
                            To Block:
                        </label>
                        <div class="Control">
                            <asp:DropDownList runat="server" ID="ddlToBlock" CssClass="form-control required width200px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div style="display: inline-flex">
                    <div style="display: inline-flex">
                        <asp:Button ID="btnGetData" runat="server" Text="Get Data"
                            CssClass="btn btn-primary" OnClick="btnGetData_Click" />
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grdRandNos" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CssClass="table table-bordered Datatable txt_center table-striped notranslate"
                            OnPreRender="grd_data_PreRender" OnRowDataBound="grdRandNos_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Randomization Number">
                                    <ItemTemplate>
                                        <asp:Label ID="RANDNO" runat="server" Text='<%# Bind("RANDNO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Block Number">
                                    <ItemTemplate>
                                        <asp:Label ID="BLOCK" runat="server" Text='<%# Bind("BLOCK") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="STATUSNAME" runat="server" Text='<%# Bind("STATUSNAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <label>Blocked Details</label><br />
                                        <label style="color: blue; font-weight: lighter; margin-bottom: 0px;">[Blocked By]</label><br />
                                        <label style="color: green; font-weight: lighter; margin-bottom: 0px;">[Datetime(Server)]</label><br />
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">[Datetime(User), (Timezone)]</label>
                                        <label style="color: red; font-weight: lighter; margin-bottom: 0px;">Comment</label>
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
                                                <asp:Label ID="BLOCKED_CAL_TZDAT" runat="server" Text='<%# Eval("BLOCKED_CAL_TZDAT")+" "+Eval("BLOCKED_TZVAL") %>' ForeColor="Red"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Label ID="BLOCKED_COMM" runat="server" Text='<%# Bind("BLOCKED_COMM") %>'></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <HeaderTemplate>
                                        <asp:Button ID="Block" runat="server" CssClass="btn btn-primary"
                                            Text="Block" OnClick="btn_Block" />
                                        <br />
                                        <asp:CheckBox ID="ChekAll_Block" runat="server" onclick="Check_All_Block(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_Block" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false">
                                    <HeaderTemplate>
                                        <asp:Button ID="Unblock" runat="server" CssClass="btn btn-primary"
                                            Text="Unblock" OnClick="btn_UnBlock" />
                                        <br />
                                        <asp:CheckBox ID="ChekAll_UnBlock" runat="server" onclick="Check_All_UnBlock(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chek_UnBlock" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <asp:ModalPopupExtender ID="modalpop_Block" runat="server" PopupControlID="pnl_Block" TargetControlID="Button_Block"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnl_Block" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_Block" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopupBlock">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label4" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtBlockComments" CssClass="form-control-model" ValidationGroup="BlockCommnet" TextMode="MultiLine"
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
                <asp:ModalPopupExtender ID="modalpop_UnBlock" runat="server" PopupControlID="pnl_UnBlock" TargetControlID="Button_UnBlock"
                    BackgroundCssClass="Background">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnl_UnBlock" runat="server" Style="display: none;" CssClass="Popup1">
                    <asp:Button runat="server" ID="Button_UnBlock" Style="display: none" />
                    <h5 class="heading">Please Enter Comment</h5>
                    <div class="modal-body" runat="server">
                        <div id="ModelPopupUnBlock">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="Label1" runat="server" CssClass="wrapperLable" Text="Comments" Style="color: black; font-weight: 600; font-size: 14px;"></asp:Label>
                                </div>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtUnBlockComments" CssClass="form-control-model" ValidationGroup="UnBlockCommnet" TextMode="MultiLine"
                                        runat="server" Style="width: 250px;"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    &nbsp;
                                </div>
                                <div class="col-md-9">
                                    <asp:Button ID="btnSubmitUnBlock" runat="server" Style="margin-left: 37px; height: 34px; width: 71px; font-size: 14px;" ValidationGroup="UnBlockCommnet" CssClass="btn btn-DarkGreen"
                                        OnClientClick="return Check_UnBlockReasonEntered();" Text="Submit" OnClick="btnSubmitUnBlock_Click" />
                                    &nbsp;
                            &nbsp;
                            <asp:Button ID="btnCancelUnBlock" runat="server" Text="Cancel" ValidationGroup="UnBlockCommnet"
                                CssClass="btn btn-DarkGreen" Style="height: 34px; width: 71px; font-size: 14px;" OnClick="btnCancelUnBlock_Click" />
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
